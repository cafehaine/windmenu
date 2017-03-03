using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    static class KeyboardConverter
    {
        // Code by Ivan Petrov ( http://stackoverflow.com/a/38787314 )
        static public string KeyCodeToUnicode(Keys key)
        {
            byte[] keyboardState = new byte[255];
            bool keyboardStateStatus =
                NativeMethods.GetKeyboardState(keyboardState);

            if (!keyboardStateStatus)
            {
                return "";
            }

            uint virtualKeyCode = (uint)key;
            uint scanCode = NativeMethods.MapVirtualKey(virtualKeyCode, 0);
            IntPtr inputLocaleIdentifier = NativeMethods.GetKeyboardLayout(0);

            StringBuilder result = new StringBuilder();
            NativeMethods.ToUnicodeEx(virtualKeyCode, scanCode, keyboardState,
                result, 5, 0, inputLocaleIdentifier);

            return result.ToString();
        }

        private static class NativeMethods
        {
            [DllImport("user32.dll")]
            public static extern bool GetKeyboardState(byte[] lpKeyState);

            [DllImport("user32.dll")]
            public static extern uint MapVirtualKey(uint uCode, uint uMapType);

            [DllImport("user32.dll")]
            public static extern IntPtr GetKeyboardLayout(uint idThread);

            [DllImport("user32.dll")]
            public static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);
        }
    }
}
