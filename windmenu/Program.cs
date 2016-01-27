using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ini; // Load and save ini files
using Microsoft.Win32; // for registry reading (path)

namespace windmenu
{
    static class Program
    {
        static public List<string> colors;
        static public List<string> aliases;
        static public List<string> pathList;
        static public IniFile ini;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // read settings
            string iniPath = Application.UserAppDataPath;
            ini = new IniFile(iniPath + "\\\\windmenu.ini");
            colors = ini.IniReadValue("Colors", "list").Split(';').ToList();
            aliases = ini.IniReadValue("Aliases", "list").Split(';').ToList();
            // read path value
            string tempPath = (string)Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Environment\").GetValue("PATH", "", RegistryValueOptions.DoNotExpandEnvironmentNames);
            pathList = tempPath.Split(';').ToList();
            // start application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}