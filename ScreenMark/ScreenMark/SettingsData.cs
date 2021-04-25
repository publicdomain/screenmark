﻿// // <copyright file="SettingsData.cs" company="PublicDomain.com">
// //     CC0 1.0 Universal (CC0 1.0) - Public Domain Dedication
// //     https://creativecommons.org/publicdomain/zero/1.0/legalcode
// // </copyright>
// // <auto-generated />

namespace PublicDomain
{
    // Directives
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// Urlister settings.
    /// </summary>
    public class SettingsData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:PublicDomain.SettingsData"/> class.
        /// </summary>
        public SettingsData()
        {
            // Parameterless constructor
        }

        /// <summary>
        /// Gets or sets the color of the mark.
        /// </summary>
        /// <value>The color of the mark.</value>
        public Color MarkColor { get; set; } = Color.Blue;

        /// <summary>
        /// Gets or sets the width of the pen.
        /// </summary>
        /// <value>The width of the pen.</value>
        public int PenWidth { get; set; } = 5;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:PublicDomain.SettingsData"/> floor rounding.
        /// </summary>
        /// <value><c>true</c> if floor rounding; otherwise, <c>false</c>.</value>
        public bool FloorRounding { get; set; } = true;

        /// <summary>
        /// Gets or sets the mark size pixels.
        /// </summary>
        /// <value>The mark size pixels.</value>
        public int MarkSizePixels { get; set; } = 50;

        /// <summary>
        /// Gets or sets the mark size piercentage.
        /// </summary>
        /// <value>The mark size piercentage.</value>
        public int MarkSizePiercentage { get; set; } = -1;

        /// <summary>
        /// Gets or sets the selected radio button.
        /// </summary>
        /// <value>The selected radio button.</value>
        public string SelectedRadioButton { get; set; } = "screenCenterRadioButton";

        /// <summary>
        /// Gets or sets the draw interval.
        /// </summary>
        /// <value>The draw interval.</value>
        public int DrawInterval { get; set; } = 50;
    }
}