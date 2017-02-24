using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.Environment;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Server
{
    static class Binaries
    {
        private static bool stringContainsAnyOf(string str,
            IEnumerable<string> list)
        {
            foreach (string word in list)
                if (str.ToLower().Contains(word.ToLower()))
                    return true;
            return false;
        }

        private static List<string> recursivelyListLinks(string path)
        {
            List<string> output = new List<string>();
            // Without this we will get exceptions in non-english installations
            // of windows
            try
            {
                output.AddRange(Directory.EnumerateFiles(path, "*.lnk"));
                foreach (string dir in Directory.EnumerateDirectories(path))
                    output.AddRange(recursivelyListLinks(dir));
            }
            catch (Exception)
            { }

            return output;
        }

        public static List<KeyValuePair<string,string>> GetBinaries(
            bool ParseStartmenu, Regex[] BlackList)
        {
            string[] paths = GetEnvironmentVariable("PATH").Split(';');
            string[] exts = GetEnvironmentVariable("PATHEXT").Split(';');

            List<KeyValuePair<string, string>> output =
                new List<KeyValuePair<string, string>>();
            Console.WriteLine("Parsing paths");
            foreach (string path in paths)
            {
                if (Directory.Exists(path))
                {
                    foreach (string file in Directory.EnumerateFiles(path))
                    {
                        bool matchesExt = false;
                        foreach (string ext in exts)
                            if (file.ToUpper().EndsWith(ext))
                                matchesExt = true;
                        if (!matchesExt)
                            continue;

                        string name = Path.GetFileNameWithoutExtension(file);
                        bool onBlacklist = false;

                        foreach (Regex rgx in BlackList)
                            if (rgx.IsMatch(name))
                                onBlacklist = true;

                        if (!onBlacklist)
                            output.Add(new KeyValuePair<string, string>(
                                Path.GetFileNameWithoutExtension(file),
                                file));
                    }
                }
                else
                {
                    //Console.WriteLine("Path is invalid {" + path + "}");
                }
            }

            if (ParseStartmenu)
            {
                Console.WriteLine("Parsing start menus");
                string userSm = GetFolderPath(SpecialFolder.ApplicationData) +
                    @"\Microsoft\Windows\Start Menu";
                string globlSm = @"C:\ProgramData\Microsoft\Windows\Start Menu";
                List<string> links = new List<string>();
                links.AddRange(recursivelyListLinks(userSm));
                links.AddRange(recursivelyListLinks(globlSm));
                foreach (string link in links)
                {
                    string name = Path.GetFileNameWithoutExtension(link);
                    bool onBlacklist = false;

                    foreach (Regex rgx in BlackList)
                        if (rgx.IsMatch(name))
                            onBlacklist = true;

                    if (!onBlacklist)
                        output.Add(new KeyValuePair<string, string>(
                            Path.GetFileNameWithoutExtension(link), link));
                }
            }

            Console.WriteLine("Done parsing");
            return output;
        }
    }
}
