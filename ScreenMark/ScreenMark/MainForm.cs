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
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Xml.Serialization;
    using Microsoft.VisualBasic;
    using Microsoft.Win32;
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
        /// The rdw invalidate.
        /// </summary>
        const int RDW_INVALIDATE = 0x0001;

        /// <summary>
        /// The rdw allchildren.
        /// </summary>
        const int RDW_ALLCHILDREN = 0x0080;

        /// <summary>
        /// The rdw updatenow.
        /// </summary>
        const int RDW_UPDATENOW = 0x0100;

        /// <summary>
        /// Redraws the window.
        /// </summary>
        /// <returns><c>true</c>, if window was redrawn, <c>false</c> otherwise.</returns>
        /// <param name="hwnd">Hwnd.</param>
        /// <param name="rcUpdate">Rc update.</param>
        /// <param name="regionUpdate">Region update.</param>
        /// <param name="flags">Flags.</param>
        [DllImport("User32.dll")]
        static extern bool RedrawWindow(IntPtr hwnd, IntPtr rcUpdate, IntPtr regionUpdate, int flags);

        /// <summary>
        /// Gets the foreground window.
        /// </summary>
        /// <returns>The foreground window.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// Gets the window rect.
        /// </summary>
        /// <returns><c>true</c>, if window rect was gotten, <c>false</c> otherwise.</returns>
        /// <param name="hWnd">H window.</param>
        /// <param name="lpRect">Lp rect.</param>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        /// <summary>
        /// Rect.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left; //Leftmost coordinate
            public int Top; //Top coordinate
            public int Right; //The rightmost coordinate
            public int Bottom; //The bottom coordinate
        }

        /// <summary>
        /// Invalidates the rect.
        /// </summary>
        /// <returns><c>true</c>, if rect was invalidated, <c>false</c> otherwise.</returns>
        /// <param name="hWnd">H window.</param>
        /// <param name="lpRect">Lp rect.</param>
        /// <param name="bErase">If set to <c>true</c> b erase.</param>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool InvalidateRect(IntPtr hWnd, ref Rectangle lpRect, bool bErase);

        /// <summary>
        /// Gets the desktop window.
        /// </summary>
        /// <returns>The desktop window.</returns>
        [DllImport("user32.dll")]
        public extern static IntPtr GetDesktopWindow();

        /// <summary>
        /// Sets the cursor position.
        /// </summary>
        /// <returns><c>true</c>, if cursor position was set, <c>false</c> otherwise.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        /// <summary>
        /// Mouses the event.
        /// </summary>
        /// <param name="dwFlags">Dw flags.</param>
        /// <param name="dx">Dx.</param>
        /// <param name="dy">Dy.</param>
        /// <param name="cButtons">C buttons.</param>
        /// <param name="dwExtraInfo">Dw extra info.</param>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        /// <summary>
        /// The mouseeventf leftdown.
        /// </summary>
        public const int MOUSEEVENTF_LEFTDOWN = 0x02;

        /// <summary>
        /// The mouseeventf leftup.
        /// </summary>
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        /// <summary>
        /// Registers the hot key.
        /// </summary>
        /// <returns><c>true</c>, if hot key was registered, <c>false</c> otherwise.</returns>
        /// <param name="hWnd">H window.</param>
        /// <param name="id">Identifier.</param>
        /// <param name="fsModifiers">Fs modifiers.</param>
        /// <param name="vk">Vk.</param>
        [DllImport("User32")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        /// <summary>
        /// Unregisters the hot key.
        /// </summary>
        /// <returns><c>true</c>, if hot key was unregistered, <c>false</c> otherwise.</returns>
        /// <param name="hWnd">H window.</param>
        /// <param name="id">Identifier.</param>
        [DllImport("User32")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// The mod shift.
        /// </summary>
        private const int MOD_SHIFT = 0x4;

        /// <summary>
        /// The mod control.
        /// </summary>
        private const int MOD_CONTROL = 0x2;

        /// <summary>
        /// The mod alternate.
        /// </summary>
        private const int MOD_ALT = 0x1;

        /// <summary>
        /// The hotkey native window.
        /// </summary>
        private HotkeyNativeWindow hotkeyNativeWindow = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ScreenMark.MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            // The InitializeComponent() call is required for Windows Forms designer support.
            this.InitializeComponent();

            // Set hotkey native window
            this.hotkeyNativeWindow = new HotkeyNativeWindow();

            // Set the hotkey event hanfler
            this.hotkeyNativeWindow.HorkeyPressed += this.OnHotkeyPressed;

            /* Set icons */

            // Set associated icon from exe file
            this.associatedIcon = Icon.ExtractAssociatedIcon(typeof(MainForm).GetTypeInfo().Assembly.Location);

            // Set public domain weekly tool strip menu item image
            this.moreReleasesPublicDomainGiftcomToolStripMenuItem.Image = this.associatedIcon.ToBitmap();

            // Set taskbar icon
            this.mainNotifyIcon.Icon = this.Icon;

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

            // Move cursor to mark
            this.moveCursorToMarkToolStripMenuItem.Checked = this.settingsData.MoveCursorToMark;

            // Click mark center
            this.clickMarkCenterToolStripMenuItem.Checked = this.settingsData.ClickMarkCenter;

            // Autostart on login
            this.autostartOnloginToolStripMenuItem.Checked = this.settingsData.AutostartOnLogin;
            this.ProcessRunAtStartupRegistry();

            // Start minimized to tray
            this.startMinimizedToTrayToolStripMenuItem.Checked = this.settingsData.StartMinimizedToTray;
            if (this.settingsData.StartMinimizedToTray)
            {
                // Send to tray
                this.SendToSystemTray();
            }

            // Enable Hotkeys
            this.enablehotkeysToolStripMenuItem.Checked = this.settingsData.EnableHotkeys;
            this.ProcessHotkeys();

            /* Timer */

            // Set draw interval timer elapsed
            Program.DrawIntervalTimer.Elapsed += this.OnDrawIntervalTimerTick;
        }

        /// <summary>
        /// Processes the run at startup registry action.
        /// </summary>
        private void ProcessRunAtStartupRegistry()
        {
            // Open registry key
            using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {
                // Check for run at startup in settings data
                if (this.settingsData.AutostartOnLogin)
                {
                    // Add app value
                    registryKey.SetValue(Application.ProductName, $"\"{Application.ExecutablePath}\"");
                }
                else
                {
                    // Check for app value
                    if (registryKey.GetValue(Application.ProductName) != null)
                    {
                        // Erase app value
                        registryKey.DeleteValue(Application.ProductName, false);
                    }
                }
            }
        }

        /// <summary>
        /// Handler the hotkey pressed event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        public void OnHotkeyPressed(object sender, EventArgs e)
        {
            // Hit mark button
            this.markButton.PerformClick();
        }

        /// <summary>
        /// Registers the hotkeys.
        /// </summary>
        public void RegisterHotkeys()
        {
            // Register ALT + SHIFT + M
            RegisterHotKey(this.hotkeyNativeWindow.Handle, 0, MOD_CONTROL + MOD_ALT, Convert.ToInt16(Keys.M));
        }

        /// <summary>
        /// Unregisters the hotkeys.
        /// </summary>
        public void UnregisterHotkeys()
        {
            // Unregister active hotkey
            UnregisterHotKey(this.hotkeyNativeWindow.Handle, 0);
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
                this.markToolStripMenuItem.Text = "&Stop";

                // Set center opint
                Point centerPoint = this.GetCenterPoint();

                // Set cursor position
                if (this.moveCursorToMarkToolStripMenuItem.Checked)
                {
                    // Move cursor to center point
                    SetCursorPos(centerPoint.X, centerPoint.Y);
                }

                // Click mark center
                if (this.clickMarkCenterToolStripMenuItem.Checked)
                {
                    // Perform left click
                    mouse_event(MOUSEEVENTF_LEFTDOWN, centerPoint.X, centerPoint.Y, 0, 0);
                    mouse_event(MOUSEEVENTF_LEFTUP, centerPoint.X, centerPoint.Y, 0, 0);
                }

                // Enable timer
                Program.DrawIntervalTimer.Start();
            }
            else
            {
                // Set text 
                this.markButton.Text = "&Mark";
                this.markToolStripMenuItem.Text = "&Mark";

                // Disable timer
                Program.DrawIntervalTimer.Stop();

                // Remove mark from screen
                this.ClearMark();
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
                    // Set on settnigs data
                    this.settingsData.EnableHotkeys = this.enablehotkeysToolStripMenuItem.Checked;

                    // Process hotkeys
                    this.ProcessHotkeys();
                    break;

                // Move cursor
                case "moveCursorToMarkToolStripMenuItem":
                    this.settingsData.MoveCursorToMark = this.moveCursorToMarkToolStripMenuItem.Checked;
                    break;

                // Click mark center
                case "clickMarkCenterToolStripMenuItem":
                    this.settingsData.ClickMarkCenter = this.clickMarkCenterToolStripMenuItem.Checked;
                    break;

                // Autostart on login
                case "autostartOnloginToolStripMenuItem":
                    this.settingsData.AutostartOnLogin = this.autostartOnloginToolStripMenuItem.Checked;

                    // Process autostart registry action
                    this.ProcessRunAtStartupRegistry();
                    break;

                // Start minimized to tray
                case "startMinimizedToTrayToolStripMenuItem":
                    this.settingsData.StartMinimizedToTray = this.startMinimizedToTrayToolStripMenuItem.Checked;
                    break;
            }
        }

        /// <summary>
        /// Processes the hotkeys.
        /// </summary>
        private void ProcessHotkeys()
        {
            // Enable/Disable hotkeys according to settings data 
            if (this.settingsData.EnableHotkeys)
            {
                this.RegisterHotkeys();
            }
            else
            {
                this.UnregisterHotkeys();
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
            // Set license text
            var licenseText = $"CC0 1.0 Universal (CC0 1.0) - Public Domain Dedication{Environment.NewLine}" +
                $"https://creativecommons.org/publicdomain/zero/1.0/legalcode{Environment.NewLine}{Environment.NewLine}" +
                $"Libraries and icons have separate licenses.{Environment.NewLine}{Environment.NewLine}" +
                $"Computer icon by Clker-Free-Vector-Images - Pixabay License{Environment.NewLine}" +
                $"https://pixabay.com/vectors/computer-screen-apple-happy-304585/{Environment.NewLine}{Environment.NewLine}" +
                $"Patreon icon used according to published brand guidelines{Environment.NewLine}" +
                $"https://www.patreon.com/brand{Environment.NewLine}{Environment.NewLine}" +
                $"GitHub mark icon used according to published logos and usage guidelines{Environment.NewLine}" +
                $"https://github.com/logos{Environment.NewLine}{Environment.NewLine}" +
                $"DonationCoder icon used with permission{Environment.NewLine}" +
                $"https://www.donationcoder.com/forum/index.php?topic=48718{Environment.NewLine}{Environment.NewLine}" +
                $"PublicDomain icon is based on the following source images:{Environment.NewLine}{Environment.NewLine}" +
                $"Bitcoin by GDJ - Pixabay License{Environment.NewLine}" +
                $"https://pixabay.com/vectors/bitcoin-digital-currency-4130319/{Environment.NewLine}{Environment.NewLine}" +
                $"Letter P by ArtsyBee - Pixabay License{Environment.NewLine}" +
                $"https://pixabay.com/illustrations/p-glamour-gold-lights-2790632/{Environment.NewLine}{Environment.NewLine}" +
                $"Letter D by ArtsyBee - Pixabay License{Environment.NewLine}" +
                $"https://pixabay.com/illustrations/d-glamour-gold-lights-2790573/{Environment.NewLine}{Environment.NewLine}";

            // Prepend sponsors
            licenseText = $"RELEASE SPONSORS:{Environment.NewLine}{Environment.NewLine}* Jesse Reichler{Environment.NewLine}{Environment.NewLine}=========={Environment.NewLine}{Environment.NewLine}" + licenseText;

            // Set title
            string programTitle = typeof(MainForm).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title;

            // Set version for generating semantic version 
            Version version = typeof(MainForm).GetTypeInfo().Assembly.GetName().Version;

            // Set about form
            var aboutForm = new AboutForm(
                $"About {programTitle}",
                $"{programTitle} {version.Major}.{version.Minor}.{version.Build}",
                $"Made for: Curt{Environment.NewLine}DonationCoder.com{Environment.NewLine}Day #130, Week #19 @ May 10, 2021",
                licenseText,
                this.Icon.ToBitmap())
            {
                // Set about form icon
                Icon = this.associatedIcon,

                // Set always on top
                TopMost = this.TopMost
            };

            // Show about form
            aboutForm.ShowDialog();
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
                this.markColorDialog.ShowDialog();

                // Set mark color
                this.SetMarkColor(true);
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
                // Set mark color dialog
                this.markColorDialog.Color = Color.FromArgb(this.settingsData.MarkColor);
            }
            else
            {
                // Set mark color on settings data
                this.settingsData.MarkColor = this.markColorDialog.Color.ToArgb();
            }
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
            int drawInterval = this.settingsData.DrawInterval;

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
        /// TODO Draws the mark. [0.x version. CAN BE *QUITE* OPTIMIZED]
        /// </summary>
        private void DrawMark()
        {
            // Invalidate previous mark
            this.InvalidateMark();

            // Set desktop device context
            IntPtr desktopDC = GetDC(IntPtr.Zero);
            Graphics g = Graphics.FromHdc(desktopDC);

            // Create pen.
            Pen pen = new Pen(this.markColorDialog.Color, this.settingsData.PenWidth);

            // Declare line width, height, size
            int lineWidth = 0, lineHeight = 0, lineSize = 0, halfLineSize = 0;

            // Determine width and height
            if (this.settingsData.MarkSizePixels > -1)
            {
                // Pixels
                lineWidth = this.settingsData.MarkSizePixels;
                lineHeight = this.settingsData.MarkSizePixels;
                lineSize = this.settingsData.MarkSizePixels;
                halfLineSize = lineSize / 2;
            }
            else
            {
                // TODO Percentage by height
            }

            // Declare centers
            int widthCenter = 0, heightCenter = 0;

            // Get center point
            Point centerPoint = this.GetCenterPoint();

            // Check if must halt
            if (centerPoint.Equals(new Point(0, 0)))
            {
                // Halt flow
                return;
            }

            // Set centers
            widthCenter = centerPoint.X;
            heightCenter = centerPoint.Y;

            // Draw vertical and horizontal lines
            g.DrawLine(pen, widthCenter - halfLineSize, heightCenter, widthCenter + halfLineSize, heightCenter);
            g.DrawLine(pen, widthCenter, heightCenter - halfLineSize, widthCenter, heightCenter + halfLineSize);
        }

        /// <summary>
        /// Gets the center point.
        /// </summary>
        /// <returns>The center point.</returns>
        private Point GetCenterPoint()
        {
            // Declare centers
            int heightCenter = 0, widthCenter = 0;

            // Switch targets
            switch (this.settingsData.SelectedRadioButton)
            {
                // screenCenterRadioButton
                case "screenCenterRadioButton":
                    heightCenter = Screen.PrimaryScreen.Bounds.Height / 2;
                    widthCenter = Screen.PrimaryScreen.Bounds.Width / 2;

                    break;

                // workingAreaCenterRadioButton
                case "workingAreaCenterRadioButton":
                    heightCenter = Screen.PrimaryScreen.WorkingArea.Height / 2;
                    widthCenter = Screen.PrimaryScreen.WorkingArea.Width / 2;

                    break;

                // activeWindowCenterRadioButton
                case "activeWindowCenterRadioButton":

                    // TODO Set foregronud window handle to compare [Comparison can be done earlier]
                    IntPtr foregroundWindow = GetForegroundWindow();

                    // Check for a match
                    if (this.Handle == foregroundWindow)
                    {
                        // Halt flow returning 0,0 point 
                        return new Point(0, 0);
                    }

                    // Set rectangle
                    RECT rect = new RECT();
                    GetWindowRect(foregroundWindow, ref rect);

                    // Set rectangle-based variables
                    int width = rect.Right - rect.Left;
                    int height = rect.Bottom - rect.Top;
                    int x = rect.Left;
                    int y = rect.Top;

                    // Set centers
                    widthCenter = x + width / 2;
                    heightCenter = y + height / 2;

                    break;
            }

            // Return centers via Point
            return new Point(widthCenter, heightCenter);
        }

        /// <summary>
        /// Sends the program to the system tray.
        /// </summary>
        private void SendToSystemTray()
        {
            // Hide main form
            this.Hide();

            // Remove from task bar
            this.ShowInTaskbar = false;

            // Minimize
            this.WindowState = FormWindowState.Minimized;

            // Show notify icon 
            this.mainNotifyIcon.Visible = true;
        }

        /// <summary>
        /// Restores the window back from system tray to the foreground.
        /// </summary>
        private void RestoreFromSystemTray()
        {
            // Make form visible again
            this.Show();

            // Restore in task bar
            this.ShowInTaskbar = true;

            // Return window back to normal
            this.WindowState = FormWindowState.Normal;

            // Hide system tray icon
            this.mainNotifyIcon.Visible = false;

            // Check if must re-enable hotkeys 
            if (this.enablehotkeysToolStripMenuItem.Checked)
            {
                // Re-register hotkeys
                this.RegisterHotkeys();
            }
        }

        /// <summary>
        /// Handles the main notify icon mouse click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnMainNotifyIconMouseClick(object sender, MouseEventArgs e)
        {
            // Check for left click
            if (e.Button == MouseButtons.Left)
            {
                // Restore window 
                this.RestoreFromSystemTray();
            }
        }

        /// <summary>
        /// Clears the mark.
        /// </summary>
        private void ClearMark()
        {
            // Redraw desktop
            RedrawWindow(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, RDW_INVALIDATE | RDW_ALLCHILDREN | RDW_UPDATENOW);
        }

        /// <summary>
        /// Invalidates the mark.
        /// </summary>
        private void InvalidateMark()
        {
            // TODO Set rectangle to invalidate [Can be optimized]
            Rectangle invalidateRect = new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            // Invalidate desktop
            InvalidateRect(GetDesktopWindow(), ref invalidateRect, true);
        }

        /// <summary>
        /// Handles the draw interval timer tick event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnDrawIntervalTimerTick(object sender, EventArgs e)
        {
            // Draw the mark
            this.DrawMark();

            // Check for an active sign
            if (this.markButton.Text == "&Stop")
            {
                // Re-enable it
                Program.DrawIntervalTimer.Start();
            }
        }

        /// <summary>
        /// Handles the minimize tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnMinimizeToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Send to system tray
            this.SendToSystemTray();
        }

        /// <summary>
        /// Handles the show tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnShowToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Restore from system tra
            this.RestoreFromSystemTray();
        }
    }
}
