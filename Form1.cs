using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ActiveWindowTitleApp
{
    public partial class Form1 : Form
    {
        // Windows API to get the handle of the active window
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        // Windows API to get the window title of the active window
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        // Windows API to set the keyboard hook
        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hmod, uint dwThreadId);

        // Windows API to remove the hook
        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhook);

        // Windows API to call the next hook in the chain
        [DllImport("user32.dll")]
        private static extern int CallNextHookEx(IntPtr hhook, int nCode, IntPtr wParam, IntPtr lParam);

        // Define hook constants
        private const int WH_KEYBOARD_LL = 13;  // Low-level keyboard hook
        private const int WM_KEYDOWN = 0x0100;

        private HookProc _hookProc;
        private IntPtr _hookID = IntPtr.Zero;

        private const string targetWindow = "Discord"; // The name of the window you want to block keyboard input for

        public Form1()
        {
            InitializeComponent();
            InitializeTimer();
        }

        // Delegate to handle the hook procedure
        private delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        private void InitializeTimer()
        {
            // Set up the timer to check the active window every 500ms
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 500;
            timer.Tick += Timer_Tick;
            timer.Start();

            // Set up the keyboard hook
            _hookProc = new HookProc(KeyboardHookProc);
            _hookID = SetWindowsHookEx(WH_KEYBOARD_LL, _hookProc, IntPtr.Zero, 0);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Get the active window's title
            IntPtr hwnd = GetForegroundWindow();
            StringBuilder windowTitle = new StringBuilder(256);
            GetWindowText(hwnd, windowTitle, 256);

            // Update the label with the active window's title
            activeWindowTitleLabel.Text = "Active Window: " + windowTitle.ToString();
        }

        // Keyboard hook procedure
        private int KeyboardHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // If the nCode is less than 0, the hook should not process the key event
            if (nCode < 0)
            {
                return CallNextHookEx(_hookID, nCode, wParam, lParam);
            }

            // Get the active window's title
            IntPtr hwnd = GetForegroundWindow();
            StringBuilder windowTitle = new StringBuilder(256);
            GetWindowText(hwnd, windowTitle, 256);

            // If the active window title contains the target window name (e.g., Firefox), block the key press
            if (windowTitle.ToString().Contains(targetWindow))
            {
                // Block the key press by returning 1 (this prevents the key from being passed to the application)
                return 1;
            }

            // Otherwise, let the key press pass through
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        // Cleanup: Remove the hook when the application is closed
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_hookID != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_hookID);
            }
            base.OnFormClosing(e);
        }

        // Correctly override Dispose method
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Clean up managed resources here, if needed
                if (_hookID != IntPtr.Zero)
                {
                    UnhookWindowsHookEx(_hookID); // Remove the keyboard hook
                }
            }
            base.Dispose(disposing); // Call the base Dispose method
        }
    }
}
