using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    static class DAHelper
    {
        /// <summary>Maximal Length of unmanaged Windows-Path-strings</summary>
        private const int MAX_PATH = 260;
        /// <summary>Maximal Length of unmanaged Typename</summary>
        private const int MAX_TYPE = 80;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SHFILEINFO
        {
            public SHFILEINFO(bool b)
            {
                hIcon = IntPtr.Zero;
                iIcon = 0;
                dwAttributes = 0;
                szDisplayName = "";
                szTypeName = "";
            }
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_TYPE)]
            public string szTypeName;
        };

        [Flags]
        enum SHGFI : int
        {
            ExeType = 0x000002000
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

        public static bool IsCommandLine(string path)
        {
            SHFILEINFO data = new SHFILEINFO();
            IntPtr dword = SHGetFileInfo(path, 0, ref data, 0, 0x000002000);
            int loword = unchecked((short)(long)dword);
            int hiword = unchecked((short)((long)dword >> 16));

            if (loword == 0) // not an executable file, probably a shortcut
                return false;
            return hiword == 0; // if hiword !=0, it is probably a gui application
        }
    }
}
