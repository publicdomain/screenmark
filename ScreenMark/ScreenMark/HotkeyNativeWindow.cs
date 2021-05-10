// <copyright file="HotkeyNativeWindow.cs" company="PublicDomain.com">
//     CC0 1.0 Universal (CC0 1.0) - Public Domain Dedication
//     https://creativecommons.org/publicdomain/zero/1.0/legalcode
// </copyright>

namespace PublicDomain
{
    // Directives
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    /// Hotkey native window.
    /// </summary>
    public sealed class HotkeyNativeWindow : NativeWindow, IDisposable
    {
        private static int WM_HOTKEY = 0x0312;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PublicDomain.HotkeyNativeWindow"/> class.
        /// </summary>
        public HotkeyNativeWindow()
        {
            // Create the handle
            this.CreateHandle(new CreateParams());
        }

        /// <summary>
        /// Windows the proc.
        /// </summary>
        /// <param name="m">M.</param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_HOTKEY)
            {
                HorkeyPressed(this, null);
            }
        }

        public event EventHandler HorkeyPressed;

        /// <summary>
        /// Releases all resource used by the <see cref="T:PublicDomain.HotkeyNativeWindow"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:PublicDomain.HotkeyNativeWindow"/>.
        /// The <see cref="Dispose"/> method leaves the <see cref="T:PublicDomain.HotkeyNativeWindow"/> in an unusable
        /// state. After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:PublicDomain.HotkeyNativeWindow"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:PublicDomain.HotkeyNativeWindow"/> was occupying.</remarks>
        public void Dispose()
        {
            this.DestroyHandle();
        }
    }
}
