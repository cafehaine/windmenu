using System;
using System.Drawing;
using System.Windows.Forms;

namespace Client
{
    static class Program
    {
        private static int hexToDec(char x)
        {
            if (x >= 48 && x <= 57)
                return x - 48;
            if (x >= 65 && x <= 70)
                return x - 55;
            return 0;
        }

        private static Color parseHex(string hex, Color def)
        {
            hex = hex.Substring(1);
            if (hex.Length == 3)
                hex = new string(new char[]{ hex[0], hex[0], hex[1], hex[1],
                    hex[2], hex[2] });
            else if (hex.Length != 6)
                return def;
            int R, G, B;
            R = hexToDec(hex[0]) * 16 + hexToDec(hex[1]);
            G = hexToDec(hex[2]) * 16 + hexToDec(hex[3]);
            B = hexToDec(hex[4]) * 16 + hexToDec(hex[5]);
            return Color.FromArgb(R, G, B);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
{
            #region Argument parsing
            Bar.Position Pos = Bar.Position.top;
            Color NormalBack = Color.Black;
            Color NormalFore = Color.White;
            Color FocusedBack = Color.Red;
            Color FocusedFore = Color.White;
            Font Font = new Font("Courier New", 14);

            int i = 0;
            while (i < args.Length)
            {
                switch (args[i])
                {
                    case "-b":
                        Pos = Bar.Position.bottom;
                        break;
                    case "-nb":
                        if (i + 1 < args.Length && args[i + 1].StartsWith("#"))
                        {
                            NormalBack = parseHex(args[i + 1], NormalBack);
                            i++;
                        }
                        break;
                    case "-nf":
                        if (i + 1 < args.Length && args[i + 1].StartsWith("#"))
                        {
                            NormalFore = parseHex(args[i + 1], NormalFore);
                            i++;
                        }
                        break;
                    case "-sb":
                        if (i + 1 < args.Length && args[i + 1].StartsWith("#"))
                        {
                            FocusedBack = parseHex(args[i + 1], FocusedBack);
                            i++;
                        }
                        break;
                    case "-sf":
                        if (i + 1 < args.Length && args[i + 1].StartsWith("#"))
                        {
                            FocusedFore = parseHex(args[i + 1], FocusedFore);
                            i++;
                        }
                        break;
                    case "-fn":
                        if (i + 1 < args.Length && args[i + 1].Contains(":"))
                        {
                            string[] fnt = args[i + 1].Split(':');
                            float size;
                            if (float.TryParse(args[1], out size))
                                Font = new Font(fnt[0], size);
                            i++;
                        }
                        break;

                }
                i++;
            }
            #endregion

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if !DEBUG
            try
            {
                Application.Run(new Bar(Pos, NormalBack, NormalFore,
                    FocusedBack, FocusedFore, Font));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,"windmenu Client");
            }
#else
            Application.Run(new Bar(Pos, NormalBack, NormalFore,
                FocusedBack, FocusedFore, Font));
#endif
        }
    }
}
