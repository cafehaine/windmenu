using System;
using System.Collections.Generic;
using System.IO;
using static System.Environment;
using System.Text.RegularExpressions;

namespace Server
{
    static class Binaries
    {
        private static List<string> recursivelyListLinks(string path)
        {
            List<string> output = new List<string>();
            // Without this we will get exceptions in non-english installations
            // of windows (yay microsoft.)
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

        /// <summary>
        /// Return a list of all the binaries and links.
        /// </summary>
        /// <param name="ParseStartmenu">Should we crawl the start menu</param>
        /// <param name="BlackList">Array of regexes</param>
        /// <returns></returns>
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
                    Console.WriteLine("Path is invalid {" + path + "}");
                }
            }

            if (ParseStartmenu)
            {
                Console.WriteLine("Parsing start menus");
                string userSm = GetFolderPath(SpecialFolder.StartMenu);
                string globlSm = GetFolderPath(SpecialFolder.CommonStartMenu);


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
                output.AddRange(
                    UAHelper.GetApplicationList());
            }

            Console.WriteLine("Done parsing");
            return output;
        }
    }
}
