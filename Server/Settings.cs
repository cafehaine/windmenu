using System;
using System.IO;
using static System.Environment;

namespace Server
{
    public static class Settings
    {
        public static bool ParseStartMenu = true;
        public static string[] Regexes = new string[] {".*uninstall.*", ".*help.*",
                ".*manual.*", ".*read ?me.*", ".*register.*", "\\{.*\\}" };
        public static bool RegexIgnoreCase = true;

        public static void LoadSettings()
        {
            char sep = Path.DirectorySeparatorChar;
            string configPath = GetFolderPath(SpecialFolder.ApplicationData) + sep + "windmenu" + sep + "config.txt";
            try
            {
                if (!File.Exists(configPath))
                    return;
            }
            catch (Exception)
            {
                Console.WriteLine("Could not determine if config files exists or not.");
                return;
            }

            string[] data;
            try
            {
                data = File.ReadAllLines(configPath);
            }
            catch (Exception)
            {
                Console.WriteLine("Could not read the config file, even though it exists.");
                return;
            }
            for (int i = 0; i < data.Length; i++)
            {
                string line = data[i].Trim();
                if (line == string.Empty || line.StartsWith("#"))
                    continue;
                if (!line.Contains("="))
                {
                    Console.WriteLine("Config: Line " + i + ": missing '='");
                    continue;
                }
                string[] split = line.Split(new char[] { '=' }, 2);
                switch (split[0])
                {
                    case "ParseStartMenu":
                        if (split[1] == "true")
                            ParseStartMenu = true;
                        else if (split[1] == "false")
                            ParseStartMenu = false;
                        else
                            Console.WriteLine("Config: Line " + i + ": Unknown value " + split[1]);
                        break;
                    case "RegexIgnoreCase":
                        if (split[1] == "true")
                            RegexIgnoreCase = true;
                        else if (split[1] == "false")
                            RegexIgnoreCase = false;
                        else
                            Console.WriteLine("Config: Line " + i + ": Unknown value " + split[1]);
                        break;
                    case "Regexes":
                        Regexes = split[1].Split(';');
                        break;
                    default:
                        Console.WriteLine("Config: Line " + i + ": Unknown key " + split[0]);
                        break;
                }
            }
        }
    }
}
