﻿// <copyright file="MainForm.cs" company="PublicDomain.com">
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
            if (this.settingsData.markSizePixels > -1)
            {
                // Pixel
                this.SetMarkSize("pixel", this.settingsData.markSizePixels);
            }
            else
            {
                // Percentage
                this.SetMarkSize("percentage", this.settingsData.markSizePiercentage);
            }

            // TODO Set checked radio button [Can be improved]
            foreach (RadioButton radioButton in this.mainTableLayoutPanel.Controls.OfType<RadioButton>())
            {
                // Check for matching name
                if (radioButton.Name == this.settingsData.selectedRadioButton)
                {
                    // Set checked state
                    radioButton.Checked = true;
                }
            }

            // Set draw target variable
            this.SetDrawTarget();
        }

        /// <summary>
        /// Sets the draw target.
        /// </summary>
        private void SetDrawTarget()
        {
            // Set draw target 
            switch (this.settingsData.selectedRadioButton)
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
            // TODO Add code
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
                this.settingsData.markSizePixels = value;
                this.settingsData.markSizePiercentage = -1;

                // Set menu item text
                this.setPixelsToolStripMenuItem.Text = $"&Set pixels ({value})";
                this.setPercentageToolStripMenuItem.Text = "&Set percentage";
            }
            else
            {
                // Set on settings data
                this.settingsData.markSizePixels = -1;
                this.settingsData.markSizePiercentage = value;

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
            // TODO Add code
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
    }
}
