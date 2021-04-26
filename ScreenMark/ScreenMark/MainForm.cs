// <copyright file="MainForm.cs" company="PublicDomain.com">
//     CC0 1.0 Universal (CC0 1.0) - Public Domain Dedication
//     https://creativecommons.org/publicdomain/zero/1.0/legalcode
// </copyright>

namespace ScreenMark
{
    // Directives
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Xml.Serialization;
    using Microsoft.VisualBasic;
    using PublicDomain;

    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Gets or sets the associated icon.
        /// </summary>
        /// <value>The associated icon.</value>
        private Icon associatedIcon = null;

        /// <summary>
        /// The settings data.
        /// </summary>
        private SettingsData settingsData = null;

        /// <summary>
        /// The settings data path.
        /// </summary>
        private string settingsDataPath = $"{Application.ProductName}-SettingsData.txt";

        /// <summary>
        /// The draw target. [0 = Screen, 1= Working area, 2 = Active window]
        /// </summary>
        private int drawTarget = 0;

        /// <summary>
        /// The mod shift.
        /// </summary>
        public const int MOD_SHIFT = 0x4;

        /// <summary>
        /// The mod control.
        /// </summary>
        public const int MOD_CONTROL = 0x2;

        /// <summary>
        /// The mod alternate.
        /// </summary>
        public const int MOD_ALT = 0x1;

        /// <summary>
        /// The wm hotkey.
        /// </summary>
        private const int WM_HOTKEY = 0x312;

        /// <summary>
        /// Registers the hot key.
        /// </summary>
        /// <returns><c>true</c>, if hot key was registered, <c>false</c> otherwise.</returns>
        /// <param name="hWnd">H window.</param>
        /// <param name="id">Identifier.</param>
        /// <param name="fsModifiers">Fs modifiers.</param>
        /// <param name="vk">Vk.</param>
        [DllImport("User32")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        /// <summary>
        /// Unregisters the hot key.
        /// </summary>
        /// <returns><c>true</c>, if hot key was unregistered, <c>false</c> otherwise.</returns>
        /// <param name="hWnd">H window.</param>
        /// <param name="id">Identifier.</param>
        [DllImport("User32")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// Gets the dc.
        /// </summary>
        /// <returns>The dc.</returns>
        /// <param name="hwnd">Hwnd.</param>
        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        /// <summary>
        /// Releases the dc.
        /// </summary>
        /// <param name="hwnd">Hwnd.</param>
        /// <param name="dc">Dc.</param>
        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ScreenMark.MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            // The InitializeComponent() call is required for Windows Forms designer support.
            this.InitializeComponent();

            /* Settings data */

            // Check for settings file
            if (!File.Exists(this.settingsDataPath))
            {
                // Create new settings file
                this.SaveSettingsFile(this.settingsDataPath, new SettingsData());
            }

            // Load settings from disk
            this.settingsData = this.LoadSettingsFile(this.settingsDataPath);

            // Set mark color
            this.SetMarkColor(false);

            // Set width menu item text
            this.width5ToolStripMenuItem.Text = $"&Width ({this.settingsData.PenWidth})";

            // Set rounding checked state
            this.floorToolStripMenuItem.Checked = this.settingsData.FloorRounding;
            this.ceilingToolStripMenuItem.Checked = !this.settingsData.FloorRounding;

            // Set mark size
            if (this.settingsData.MarkSizePixels > -1)
            {
                // Pixel
                this.SetMarkSize("pixel", this.settingsData.MarkSizePixels);
            }
            else
            {
                // Percentage
                this.SetMarkSize("percentage", this.settingsData.MarkSizePiercentage);
            }

            // TODO Set checked radio button [Can be improved]
            foreach (RadioButton radioButton in this.mainTableLayoutPanel.Controls.OfType<RadioButton>())
            {
                // Check for matching name
                if (radioButton.Name == this.settingsData.SelectedRadioButton)
                {
                    // Set checked state
                    radioButton.Checked = true;
                }
            }

            // Set draw target variable
            this.SetDrawTarget();

            // Set interval text 
            this.setToolStripMenuItem.Text = $"&Set ({this.settingsData.DrawInterval})";

            // Topmost
            this.alwaysOnTopToolStripMenuItem.Checked = this.settingsData.TopMost;
            this.TopMost = this.settingsData.TopMost;

            // Move cursor
            this.moveCursorToMarkToolStripMenuItem.Checked = this.settingsData.MoveCursorToMark;

            // TODO Enable Hotkeys
            this.enablehotkeysToolStripMenuItem.Checked = this.settingsData.EnableHotkeys;
        }

        /// <summary>
        /// Window procedure.
        /// </summary>
        /// <param name="m">M.</param>
        protected override void WndProc(ref Message m)
        {
            try
            {
                // Check for hotkey message and there are hotkeys registered
                if (m.Msg == WM_HOTKEY)
                {
                    // Hit mark button
                    this.markButton.PerformClick();
                }
            }
            catch
            {
                // TODO Advise user
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// Registers the hotkeys.
        /// </summary>
        public void RegisterHotkeys()
        {
            // Register ALT + SHIFT + M
            RegisterHotKey(this.Handle, 0, MOD_ALT + MOD_SHIFT, Convert.ToInt16(Keys.M));
        }

        /// <summary>
        /// Unregisters the hotkeys.
        /// </summary>
        public void UnregisterHotkeys()
        {
            // Unregister active hotkey
            UnregisterHotKey(this.Handle, 0);
        }

        /// <summary>
        /// Sets the draw target.
        /// </summary>
        private void SetDrawTarget()
        {
            // Set draw target 
            switch (this.settingsData.SelectedRadioButton)
            {
                // 0 = Center screen
                case "screenCenterRadioButton":
                    this.drawTarget = 0;
                    break;

                // 1 = Center working area
                case "workingAreaCenterRadioButton":
                    this.drawTarget = 1;
                    break;

                // 2 = Center active window
                case "activeWindowCenterRadioButton":
                    this.drawTarget = 2;
                    break;
            }
        }

        /// <summary>
        /// Handles the mark button click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnMarkButtonClick(object sender, EventArgs e)
        {
            // Check current text
            if (this.markButton.Text.StartsWith("&M", StringComparison.InvariantCulture))
            {
                // Set text 
                this.markButton.Text = "&Stop";

                // Enable timer
                this.drawIntervalTimer.Start();
            }
            else
            {
                // Set text 
                this.markButton.Text = "&Mark";

                // Enable timer
                this.drawIntervalTimer.Stop();
            }
        }

        /// <summary>
        /// Handles the exit tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Close program
            this.Close();
        }

        /// <summary>
        /// Handles the more releases public domain giftcom tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnMoreReleasesPublicDomainGiftcomToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Open current website
            Process.Start("https://publicdomaingift.com");
        }

        /// <summary>
        /// Handles the original thread donation codercom tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnOriginalThreadDonationCodercomToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Open orignal thread
            Process.Start("https://www.donationcoder.com/forum/index.php?topic=18632.msg444797#msg444797");
        }

        /// <summary>
        /// Handles the source code githubcom tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnSourceCodeGithubcomToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Open GitHub repository
            Process.Start("https://github.com/publicdomain/screenmark");
        }

        /// <summary>
        /// Handles the options tool strip menu item drop down item clicked event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnOptionsToolStripMenuItemDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Set clicked item
            var clickedItem = (ToolStripMenuItem)e.ClickedItem;

            // Exclude sub-menus
            if (clickedItem.DropDownItems.Count > 0)
            {
                // Halt flow
                return;
            }

            // Toggle checked
            clickedItem.Checked = !clickedItem.Checked;

            /* Process actions and save settings*/

            // Act based on name
            switch (clickedItem.Name)
            {
                // Topmost
                case "alwaysOnTopToolStripMenuItem":
                    this.TopMost = this.alwaysOnTopToolStripMenuItem.Checked;
                    this.settingsData.TopMost = this.alwaysOnTopToolStripMenuItem.Checked;
                    break;

                // Hotkeys
                case "enablehotkeysToolStripMenuItem":
                    // Enable/Disable hotkeys
                    if (this.enablehotkeysToolStripMenuItem.Checked)
                    {
                        this.RegisterHotkeys();
                    }
                    else
                    {
                        this.UnregisterHotkeys();
                    }

                    this.settingsData.EnableHotkeys = this.enablehotkeysToolStripMenuItem.Checked;
                    break;

                // Move cursor
                case "moveCursorToMarkToolStripMenuItem":
                    this.settingsData.MoveCursorToMark = this.moveCursorToMarkToolStripMenuItem.Checked;
                    break;
            }

        }

        /// <summary>
        /// Marks the size tool strip menu item drop down item clicked event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnMarkSizeToolStripMenuItemDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Set clicked item
            var clickedItem = (ToolStripMenuItem)e.ClickedItem;

            // Parsed integer
            int parsedInt;

            // Check for pixels
            if (clickedItem.Name == this.setPixelsToolStripMenuItem.Name)
            {
                // Try to parse integer from user input
                if (int.TryParse(Interaction.InputBox("Please enter pixels (integer):", "Pixels"), out parsedInt) && parsedInt > 0)
                {
                    // Set pixels
                    this.SetMarkSize("pixel", parsedInt);
                }
            }
            else // Set percentage
            {
                // Try to parse integer from user input
                if (int.TryParse(Interaction.InputBox("Please enter percentage (integer):", "Pixels"), out parsedInt) && parsedInt > 0)
                {
                    // Set percentage
                    this.SetMarkSize("percentage+", parsedInt);
                }
            }
        }

        /// <summary>
        /// Sets the size of the mark.
        /// </summary>
        /// <param name="target">Target.</param>
        /// <param name="value">Value.</param>
        private void SetMarkSize(string target, int value)
        {
            // Check target
            if (target == "pixel")
            {
                // Set on settings data
                this.settingsData.MarkSizePixels = value;
                this.settingsData.MarkSizePiercentage = -1;

                // Set menu item text
                this.setPixelsToolStripMenuItem.Text = $"&Set pixels ({value})";
                this.setPercentageToolStripMenuItem.Text = "&Set percentage";
            }
            else
            {
                // Set on settings data
                this.settingsData.MarkSizePixels = -1;
                this.settingsData.MarkSizePiercentage = value;

                // Set menu item text
                this.setPixelsToolStripMenuItem.Text = "&Set pixels";
                this.setPercentageToolStripMenuItem.Text = $"&Set percentage ({value})";
            }
        }

        /// <summary>
        /// Handles the about tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnAboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            // TODO Add code
        }

        /// <summary>
        /// Handles the screen center radio button checked changed event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            // Set name
            string radioButtonName = ((RadioButton)sender).Name;

            // Iterate radio buttons
            foreach (RadioButton radioButton in this.mainTableLayoutPanel.Controls.OfType<RadioButton>())
            {
                // Check for clicked
                if (radioButton.Name != radioButtonName)
                {
                    // Reset 
                    radioButton.Font = new Font(radioButton.Font, radioButton.Font.Style & ~FontStyle.Bold);
                }
                else
                {
                    // Set 
                    radioButton.Font = new Font(radioButton.Font, radioButton.Font.Style | FontStyle.Bold);
                }
            }

            // Set on settings data
            this.settingsData.SelectedRadioButton = radioButtonName;

            // Set draw target variable
            this.SetDrawTarget();
        }

        /// <summary>
        /// Handles the pixel rounding tool strip menu item drop down item clicked event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnPixelRoundingToolStripMenuItemDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Set clicked item
            var clickedItem = (ToolStripMenuItem)e.ClickedItem;

            // Uncheck all
            foreach (ToolStripMenuItem item in this.pixelRoundingToolStripMenuItem.DropDownItems)
            {
                // Uncheck
                item.Checked = false;
            }

            // Toggle checked
            clickedItem.Checked = !clickedItem.Checked;

            // Float rounding
            this.settingsData.FloorRounding = this.floorToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Ons the mark pen tool strip menu item drop down item clicked.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnMarkPenToolStripMenuItemDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Set clicked item
            var clickedItem = (ToolStripMenuItem)e.ClickedItem;

            // Check for color
            if (clickedItem.Name == this.colorToolStripMenuItem.Name)
            {
                // Show color dialog
                DialogResult dialogResult = this.markColorDialog.ShowDialog();

                // Check the user clicked OK
                if (dialogResult == DialogResult.OK)
                {
                    // Set mark color
                    this.SetMarkColor(true);
                }
            }
            else // Pen width
            {
                // Pen width
                int penWidth;

                // Try to get width
                if (int.TryParse(Interaction.InputBox("Please enter pen width", "Width"), out penWidth) && penWidth > 0)
                {
                    // Set on settings data
                    this.settingsData.PenWidth = penWidth;

                    // Set width text
                    this.width5ToolStripMenuItem.Text = $"&Width ({penWidth})";
                }
            }
        }

        /// <summary>
        /// Sets the color of the mark.
        /// </summary>
        /// <param name="setSettingsData">If set to <c>true</c> set settings data.</param>
        private void SetMarkColor(bool setSettingsData)
        {
            // Set menu item text
            this.colorToolStripMenuItem.Text = $"&Color ({this.markColorDialog.Color.Name})";

            // Test for no settings data
            if (!setSettingsData)
            {
                return;
            }

            // Set mark color on settings data
            this.settingsData.MarkColor = this.markColorDialog.Color;
        }

        /// <summary>
        /// Handles the main form form closing event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnMainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            // Save settings data to disk
            this.SaveSettingsFile(this.settingsDataPath, this.settingsData);
        }

        /// <summary>
        /// Loads the settings file.
        /// </summary>
        /// <returns>The settings file.</returns>
        /// <param name="settingsFilePath">Settings file path.</param>
        private SettingsData LoadSettingsFile(string settingsFilePath)
        {
            // Use file stream
            using (FileStream fileStream = File.OpenRead(settingsFilePath))
            {
                // Set xml serialzer
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(SettingsData));

                // Return populated settings data
                return xmlSerializer.Deserialize(fileStream) as SettingsData;
            }
        }

        /// <summary>
        /// Saves the settings file.
        /// </summary>
        /// <param name="settingsFilePath">Settings file path.</param>
        /// <param name="settingsDataParam">Settings data parameter.</param>
        private void SaveSettingsFile(string settingsFilePath, SettingsData settingsDataParam)
        {
            try
            {
                // Use stream writer
                using (StreamWriter streamWriter = new StreamWriter(settingsFilePath, false))
                {
                    // Set xml serialzer
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(SettingsData));

                    // Serialize settings data
                    xmlSerializer.Serialize(streamWriter, settingsDataParam);
                }
            }
            catch (Exception exception)
            {
                // Advise user
                MessageBox.Show($"Error saving settings file.{Environment.NewLine}{Environment.NewLine}Message:{Environment.NewLine}{exception.Message}", "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the draw interval tool strip menu item drop down item clicked event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnDrawIntervalToolStripMenuItemDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Set clicked item
            var clickedItem = (ToolStripMenuItem)e.ClickedItem;

            // Draw interval [set to please compiler / unassigned variable]
            int drawInterval = 50;

            // Check for set
            if (clickedItem.Text.StartsWith("&S", StringComparison.InvariantCulture))
            {
                // Try to get interval
                if (int.TryParse(Interaction.InputBox("Please enter draw interval (milliseconds)", "Interval", this.settingsData.DrawInterval.ToString()), out drawInterval) && drawInterval > 0)
                {
                    // Halt flow
                    return;
                }
            }
            else
            {
                // Set interval
                drawInterval = int.Parse(clickedItem.Text.Substring(0, clickedItem.Text.IndexOf(" ", StringComparison.InvariantCulture) + 1));
            }

            // Set interval text
            this.setToolStripMenuItem.Text = $"&Set ({drawInterval})";

            // Set on settings data
            this.settingsData.DrawInterval = drawInterval;
        }

        /// <summary>
        /// Handles the draw interval timer tick event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnDrawIntervalTimerTick(object sender, EventArgs e)
        {
            // TODO Add code
        }
    }
}
