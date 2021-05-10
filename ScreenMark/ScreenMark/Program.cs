// <copyright file="Program.cs" company="PublicDomain.com">
//     CC0 1.0 Universal (CC0 1.0) - Public Domain Dedication
//     https://creativecommons.org/publicdomain/zero/1.0/legalcode
// </copyright>

using System;
using System.Windows.Forms;
using PublicDomain;

namespace ScreenMark
{
    /// <summary>
    /// Class with program entry point.
    /// </summary>
    internal sealed class Program
    {
        /// <summary>
        /// The draw interval timer.
        /// </summary>
        public static System.Timers.Timer DrawIntervalTimer = new System.Timers.Timer();

        /// <summary>
        /// Program entry point.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            // Set timer settings
            DrawIntervalTimer.AutoReset = false;
            DrawIntervalTimer.Interval = 50;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

    }
}
