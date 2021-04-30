﻿// <copyright file="MainForm.Designer.cs" company="PublicDomain.com">
//     CC0 1.0 Universal (CC0 1.0) - Public Domain Dedication
//     https://creativecommons.org/publicdomain/zero/1.0/legalcode
// </copyright>
// <auto-generated />

namespace ScreenMark
{
    partial class MainForm
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.hotkeysTextToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.hotkeysToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawIntervalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enablehotkeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setPixelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setPercentageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markPenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.width5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pixelRoundingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.floorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ceilingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markAllScreensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveCursorToMarkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moreReleasesPublicDomainGiftcomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.originalThreadDonationCodercomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceCodeGithubcomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.markButton = new System.Windows.Forms.Button();
            this.screenCenterRadioButton = new System.Windows.Forms.RadioButton();
            this.workingAreaCenterRadioButton = new System.Windows.Forms.RadioButton();
            this.activeWindowCenterRadioButton = new System.Windows.Forms.RadioButton();
            this.markColorDialog = new System.Windows.Forms.ColorDialog();
            this.drawIntervalTimer = new System.Windows.Forms.Timer(this.components);
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.mainTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    this.hotkeysTextToolStripStatusLabel,
                                    this.hotkeysToolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 188);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(205, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // hotkeysTextToolStripStatusLabel
            // 
            this.hotkeysTextToolStripStatusLabel.Name = "hotkeysTextToolStripStatusLabel";
            this.hotkeysTextToolStripStatusLabel.Size = new System.Drawing.Size(53, 17);
            this.hotkeysTextToolStripStatusLabel.Text = "Hotkeys:";
            // 
            // hotkeysToolStripStatusLabel
            // 
            this.hotkeysToolStripStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hotkeysToolStripStatusLabel.Name = "hotkeysToolStripStatusLabel";
            this.hotkeysToolStripStatusLabel.Size = new System.Drawing.Size(95, 17);
            this.hotkeysToolStripStatusLabel.Text = "CTRL + ALT + M";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    this.fileToolStripMenuItem,
                                    this.optionsToolStripMenuItem,
                                    this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(205, 24);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.OnExitToolStripMenuItemClick);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    this.drawIntervalToolStripMenuItem,
                                    this.alwaysOnTopToolStripMenuItem,
                                    this.enablehotkeysToolStripMenuItem,
                                    this.markSizeToolStripMenuItem,
                                    this.markPenToolStripMenuItem,
                                    this.pixelRoundingToolStripMenuItem,
                                    this.markAllScreensToolStripMenuItem,
                                    this.moveCursorToMarkToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.OnOptionsToolStripMenuItemDropDownItemClicked);
            // 
            // drawIntervalToolStripMenuItem
            // 
            this.drawIntervalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    this.setToolStripMenuItem,
                                    this.msToolStripMenuItem,
                                    this.msToolStripMenuItem1,
                                    this.msToolStripMenuItem2,
                                    this.msToolStripMenuItem3,
                                    this.msToolStripMenuItem4,
                                    this.msToolStripMenuItem5,
                                    this.msToolStripMenuItem6,
                                    this.msToolStripMenuItem7});
            this.drawIntervalToolStripMenuItem.Name = "drawIntervalToolStripMenuItem";
            this.drawIntervalToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.drawIntervalToolStripMenuItem.Text = "&Draw interval";
            this.drawIntervalToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.OnDrawIntervalToolStripMenuItemDropDownItemClicked);
            // 
            // setToolStripMenuItem
            // 
            this.setToolStripMenuItem.Name = "setToolStripMenuItem";
            this.setToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.setToolStripMenuItem.Text = "&Set";
            // 
            // msToolStripMenuItem
            // 
            this.msToolStripMenuItem.Name = "msToolStripMenuItem";
            this.msToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.msToolStripMenuItem.Text = "50 ms";
            // 
            // msToolStripMenuItem1
            // 
            this.msToolStripMenuItem1.Name = "msToolStripMenuItem1";
            this.msToolStripMenuItem1.Size = new System.Drawing.Size(111, 22);
            this.msToolStripMenuItem1.Text = "75 ms";
            // 
            // msToolStripMenuItem2
            // 
            this.msToolStripMenuItem2.Name = "msToolStripMenuItem2";
            this.msToolStripMenuItem2.Size = new System.Drawing.Size(111, 22);
            this.msToolStripMenuItem2.Text = "100 ms";
            // 
            // msToolStripMenuItem3
            // 
            this.msToolStripMenuItem3.Name = "msToolStripMenuItem3";
            this.msToolStripMenuItem3.Size = new System.Drawing.Size(111, 22);
            this.msToolStripMenuItem3.Text = "150 ms";
            // 
            // msToolStripMenuItem4
            // 
            this.msToolStripMenuItem4.Name = "msToolStripMenuItem4";
            this.msToolStripMenuItem4.Size = new System.Drawing.Size(111, 22);
            this.msToolStripMenuItem4.Text = "200 ms";
            // 
            // msToolStripMenuItem5
            // 
            this.msToolStripMenuItem5.Name = "msToolStripMenuItem5";
            this.msToolStripMenuItem5.Size = new System.Drawing.Size(111, 22);
            this.msToolStripMenuItem5.Text = "300 ms";
            // 
            // msToolStripMenuItem6
            // 
            this.msToolStripMenuItem6.Name = "msToolStripMenuItem6";
            this.msToolStripMenuItem6.Size = new System.Drawing.Size(111, 22);
            this.msToolStripMenuItem6.Text = "400 ms";
            // 
            // msToolStripMenuItem7
            // 
            this.msToolStripMenuItem7.Name = "msToolStripMenuItem7";
            this.msToolStripMenuItem7.Size = new System.Drawing.Size(111, 22);
            this.msToolStripMenuItem7.Text = "500 ms";
            // 
            // alwaysOnTopToolStripMenuItem
            // 
            this.alwaysOnTopToolStripMenuItem.Name = "alwaysOnTopToolStripMenuItem";
            this.alwaysOnTopToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.alwaysOnTopToolStripMenuItem.Text = "&Always on top";
            // 
            // enablehotkeysToolStripMenuItem
            // 
            this.enablehotkeysToolStripMenuItem.Name = "enablehotkeysToolStripMenuItem";
            this.enablehotkeysToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.enablehotkeysToolStripMenuItem.Text = "Enable &hotkeys";
            // 
            // markSizeToolStripMenuItem
            // 
            this.markSizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    this.setPixelsToolStripMenuItem,
                                    this.setPercentageToolStripMenuItem});
            this.markSizeToolStripMenuItem.Name = "markSizeToolStripMenuItem";
            this.markSizeToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.markSizeToolStripMenuItem.Text = "&Mark size";
            this.markSizeToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.OnMarkSizeToolStripMenuItemDropDownItemClicked);
            // 
            // setPixelsToolStripMenuItem
            // 
            this.setPixelsToolStripMenuItem.Name = "setPixelsToolStripMenuItem";
            this.setPixelsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.setPixelsToolStripMenuItem.Text = "&Set pixels";
            // 
            // setPercentageToolStripMenuItem
            // 
            this.setPercentageToolStripMenuItem.Name = "setPercentageToolStripMenuItem";
            this.setPercentageToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.setPercentageToolStripMenuItem.Text = "&Set percentage";
            this.setPercentageToolStripMenuItem.Visible = false;
            // 
            // markPenToolStripMenuItem
            // 
            this.markPenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    this.colorToolStripMenuItem,
                                    this.width5ToolStripMenuItem});
            this.markPenToolStripMenuItem.Name = "markPenToolStripMenuItem";
            this.markPenToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.markPenToolStripMenuItem.Text = "Mark &pen";
            this.markPenToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.OnMarkPenToolStripMenuItemDropDownItemClicked);
            // 
            // colorToolStripMenuItem
            // 
            this.colorToolStripMenuItem.Name = "colorToolStripMenuItem";
            this.colorToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.colorToolStripMenuItem.Text = "&Color";
            // 
            // width5ToolStripMenuItem
            // 
            this.width5ToolStripMenuItem.Name = "width5ToolStripMenuItem";
            this.width5ToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.width5ToolStripMenuItem.Text = "&Width";
            // 
            // pixelRoundingToolStripMenuItem
            // 
            this.pixelRoundingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    this.floorToolStripMenuItem,
                                    this.ceilingToolStripMenuItem});
            this.pixelRoundingToolStripMenuItem.Name = "pixelRoundingToolStripMenuItem";
            this.pixelRoundingToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.pixelRoundingToolStripMenuItem.Text = "Pixel &rounding";
            this.pixelRoundingToolStripMenuItem.Visible = false;
            this.pixelRoundingToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.OnPixelRoundingToolStripMenuItemDropDownItemClicked);
            // 
            // floorToolStripMenuItem
            // 
            this.floorToolStripMenuItem.Name = "floorToolStripMenuItem";
            this.floorToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.floorToolStripMenuItem.Text = "&Floor";
            // 
            // ceilingToolStripMenuItem
            // 
            this.ceilingToolStripMenuItem.Name = "ceilingToolStripMenuItem";
            this.ceilingToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.ceilingToolStripMenuItem.Text = "&Ceiling";
            // 
            // markAllScreensToolStripMenuItem
            // 
            this.markAllScreensToolStripMenuItem.Name = "markAllScreensToolStripMenuItem";
            this.markAllScreensToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.markAllScreensToolStripMenuItem.Text = "Mark &all screens";
            this.markAllScreensToolStripMenuItem.Visible = false;
            // 
            // moveCursorToMarkToolStripMenuItem
            // 
            this.moveCursorToMarkToolStripMenuItem.Name = "moveCursorToMarkToolStripMenuItem";
            this.moveCursorToMarkToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.moveCursorToMarkToolStripMenuItem.Text = "Move &cursor to mark";
            this.moveCursorToMarkToolStripMenuItem.Visible = false;
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    this.moreReleasesPublicDomainGiftcomToolStripMenuItem,
                                    this.originalThreadDonationCodercomToolStripMenuItem,
                                    this.sourceCodeGithubcomToolStripMenuItem,
                                    this.toolStripSeparator5,
                                    this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // moreReleasesPublicDomainGiftcomToolStripMenuItem
            // 
            this.moreReleasesPublicDomainGiftcomToolStripMenuItem.Name = "moreReleasesPublicDomainGiftcomToolStripMenuItem";
            this.moreReleasesPublicDomainGiftcomToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.moreReleasesPublicDomainGiftcomToolStripMenuItem.Text = "&More releases @ PublicDomainGift.com";
            this.moreReleasesPublicDomainGiftcomToolStripMenuItem.Click += new System.EventHandler(this.OnMoreReleasesPublicDomainGiftcomToolStripMenuItemClick);
            // 
            // originalThreadDonationCodercomToolStripMenuItem
            // 
            this.originalThreadDonationCodercomToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("originalThreadDonationCodercomToolStripMenuItem.Image")));
            this.originalThreadDonationCodercomToolStripMenuItem.Name = "originalThreadDonationCodercomToolStripMenuItem";
            this.originalThreadDonationCodercomToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.originalThreadDonationCodercomToolStripMenuItem.Text = "&Original thread @ DonationCoder.com";
            this.originalThreadDonationCodercomToolStripMenuItem.Click += new System.EventHandler(this.OnOriginalThreadDonationCodercomToolStripMenuItemClick);
            // 
            // sourceCodeGithubcomToolStripMenuItem
            // 
            this.sourceCodeGithubcomToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sourceCodeGithubcomToolStripMenuItem.Image")));
            this.sourceCodeGithubcomToolStripMenuItem.Name = "sourceCodeGithubcomToolStripMenuItem";
            this.sourceCodeGithubcomToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.sourceCodeGithubcomToolStripMenuItem.Text = "Source code @ Github.com";
            this.sourceCodeGithubcomToolStripMenuItem.Click += new System.EventHandler(this.OnSourceCodeGithubcomToolStripMenuItemClick);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(281, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.OnAboutToolStripMenuItemClick);
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.ColumnCount = 1;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Controls.Add(this.markButton, 0, 3);
            this.mainTableLayoutPanel.Controls.Add(this.screenCenterRadioButton, 0, 0);
            this.mainTableLayoutPanel.Controls.Add(this.workingAreaCenterRadioButton, 0, 1);
            this.mainTableLayoutPanel.Controls.Add(this.activeWindowCenterRadioButton, 0, 2);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 24);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 4;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(205, 164);
            this.mainTableLayoutPanel.TabIndex = 4;
            // 
            // markButton
            // 
            this.markButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.markButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.markButton.Location = new System.Drawing.Point(3, 126);
            this.markButton.Name = "markButton";
            this.markButton.Size = new System.Drawing.Size(199, 35);
            this.markButton.TabIndex = 0;
            this.markButton.Text = "&Mark";
            this.markButton.UseVisualStyleBackColor = true;
            this.markButton.Click += new System.EventHandler(this.OnMarkButtonClick);
            // 
            // screenCenterRadioButton
            // 
            this.screenCenterRadioButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.screenCenterRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.screenCenterRadioButton.Location = new System.Drawing.Point(3, 3);
            this.screenCenterRadioButton.Name = "screenCenterRadioButton";
            this.screenCenterRadioButton.Size = new System.Drawing.Size(199, 35);
            this.screenCenterRadioButton.TabIndex = 1;
            this.screenCenterRadioButton.Text = "&Screen center";
            this.screenCenterRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.screenCenterRadioButton.UseVisualStyleBackColor = true;
            this.screenCenterRadioButton.CheckedChanged += new System.EventHandler(this.OnRadioButtonCheckedChanged);
            // 
            // workingAreaCenterRadioButton
            // 
            this.workingAreaCenterRadioButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workingAreaCenterRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.workingAreaCenterRadioButton.Location = new System.Drawing.Point(3, 44);
            this.workingAreaCenterRadioButton.Name = "workingAreaCenterRadioButton";
            this.workingAreaCenterRadioButton.Size = new System.Drawing.Size(199, 35);
            this.workingAreaCenterRadioButton.TabIndex = 2;
            this.workingAreaCenterRadioButton.Text = "&Working area center";
            this.workingAreaCenterRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.workingAreaCenterRadioButton.UseVisualStyleBackColor = true;
            this.workingAreaCenterRadioButton.CheckedChanged += new System.EventHandler(this.OnRadioButtonCheckedChanged);
            // 
            // activeWindowCenterRadioButton
            // 
            this.activeWindowCenterRadioButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activeWindowCenterRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activeWindowCenterRadioButton.Location = new System.Drawing.Point(3, 85);
            this.activeWindowCenterRadioButton.Name = "activeWindowCenterRadioButton";
            this.activeWindowCenterRadioButton.Size = new System.Drawing.Size(199, 35);
            this.activeWindowCenterRadioButton.TabIndex = 3;
            this.activeWindowCenterRadioButton.Text = "&Active window center";
            this.activeWindowCenterRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.activeWindowCenterRadioButton.UseVisualStyleBackColor = true;
            this.activeWindowCenterRadioButton.CheckedChanged += new System.EventHandler(this.OnRadioButtonCheckedChanged);
            // 
            // markColorDialog
            // 
            this.markColorDialog.Color = System.Drawing.Color.Blue;
            // 
            // drawIntervalTimer
            // 
            this.drawIntervalTimer.Interval = 50;
            this.drawIntervalTimer.Tick += new System.EventHandler(this.OnDrawIntervalTimerTick);
            // 
            // MainForm
            // 
            this.AcceptButton = this.markButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(205, 210);
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ScreenMark";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnMainFormFormClosing);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.Timer drawIntervalTimer;
        private System.Windows.Forms.ToolStripMenuItem setToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem msToolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem msToolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem msToolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem msToolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem msToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem msToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem msToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem msToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawIntervalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem width5ToolStripMenuItem;
        private System.Windows.Forms.ColorDialog markColorDialog;
        private System.Windows.Forms.ToolStripMenuItem colorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markPenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setPixelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setPercentageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markSizeToolStripMenuItem;
        private System.Windows.Forms.RadioButton activeWindowCenterRadioButton;
        private System.Windows.Forms.RadioButton workingAreaCenterRadioButton;
        private System.Windows.Forms.RadioButton screenCenterRadioButton;
        private System.Windows.Forms.Button markButton;
        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.ToolStripMenuItem alwaysOnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveCursorToMarkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markAllScreensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ceilingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem floorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pixelRoundingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enablehotkeysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem sourceCodeGithubcomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem originalThreadDonationCodercomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moreReleasesPublicDomainGiftcomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripStatusLabel hotkeysToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel hotkeysTextToolStripStatusLabel;
        private System.Windows.Forms.StatusStrip statusStrip;
    }
}
