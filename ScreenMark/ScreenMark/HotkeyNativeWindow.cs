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
        /// The wm hotkey.
        /// </summary>
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
            // Destroy the handle
            this.DestroyHandle();
        }

        /// <summary>
        /// Registers the hotkeys.
        /// </summary>
        public void RegisterHotkeys()
        {
            // Register SHIFT + ALT + S
            RegisterHotKey(this.Handle, 0, MOD_SHIFT + MOD_ALT, Convert.ToInt16(Keys.S));
        }

        /// <summary>
        /// Unregisters the hotkeys.
        /// </summary>
        public void UnregisterHotkeys()
        {
            // Unregister active hotkey
            UnregisterHotKey(this.Handle, 0);
        }
    }
}
