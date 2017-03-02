using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO.MemoryMappedFiles;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using static Server.Settings;
using System.IO;

namespace Server
{
    class Program
    {
        private class linksComparer :
            EqualityComparer<KeyValuePair<string,string>>
        {
            public override bool Equals(KeyValuePair<string, string> x,
                KeyValuePair<string, string> y)
            {
                return x.Key == y.Key;
            }

            public override int GetHashCode(KeyValuePair<string, string> obj)
            {
                return obj.GetHashCode();
            }
        }

        public static void TreatRequest(string request,
            ref List<KeyValuePair<string,string>> list)
        {
            int index = list.FindIndex(x => x.Key == request.Substring(3));
            if (index != -1)
            {
                if (File.Exists(list[index].Value))
                {
                    if (DAHelper.IsCommandLine(list[index].Value))
                    {
                        Process.Start("cmd", "/c \"" + list[index].Value +"&&pause\"");
                    }
                    else
                        Process.Start(list[index].Value);
                }
                else
                    UAHelper.RunApplication(list[index].Value);
            }
            else
                Console.WriteLine("\tProgram requested was not in the list");
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        static int Main(string[] args)
        {
#if DEBUG
            AllocConsole();
#endif
            LoadSettings();
            Regex[] blacklist = new Regex[Regexes.Length];

            for (int i = 0; i < Regexes.Length; i++)
                blacklist[i] = new Regex(Regexes[i], RegexIgnoreCase ?
                    RegexOptions.IgnoreCase : RegexOptions.None);

            List<KeyValuePair<string,string>> Links = Binaries.GetBinaries(
                ParseStartMenu, blacklist);

            Links = Links.Distinct(new linksComparer()).ToList();
            Links.Sort(Comparer<KeyValuePair<string, string>>.Create(
                (i1, i2) => i1.Key.CompareTo(i2.Key)));

            List<string> LinksNames = new List<string>(Links.Count);

            for (int i = 0; i < Links.Count; i++)
                LinksNames.Add(Links[i].Key);
            
            byte[] toWrite = Encoding.Unicode.GetBytes(string.Join("|",
                LinksNames));

            MemoryMappedFile mmf;
            try
            {
                mmf = MemoryMappedFile.CreateNew("windmenu",
                    8 + toWrite.Length);
                MemoryMappedViewAccessor va = mmf.CreateViewAccessor();
                va.Write(0, toWrite.LongLength);
                va.WriteArray(8, toWrite, 0, toWrite.Length);
                va.Dispose();
            }
            catch(Exception)
            {
                MessageBox.Show("windmenu Server seems to already be running.",
                    "windmenu Server");
                return 1;
            }

            TcpListener serverSocket = new TcpListener(IPAddress.Loopback,
                12321);
            TcpClient clientSocket = default(TcpClient);
            serverSocket.Start();
            Console.WriteLine("Server Started");
            bool shouldRun = true;

            while (shouldRun)
            {
                clientSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine("Accepted client");
                NetworkStream stream = clientSocket.GetStream();

                byte[] clientRequestSize = new byte[2];
                stream.Read(clientRequestSize, 0, 2);
                int inSize = clientRequestSize[0] * 256 + clientRequestSize[1];
                byte[] clientRequest = new byte[inSize];
                stream.Read(clientRequest, 0, inSize);
                string data = Encoding.Unicode.GetString(clientRequest);

                Console.WriteLine("\tClient request: " + data);
                if (data.StartsWith("run"))
                    TreatRequest(Encoding.Unicode.GetString(clientRequest),
                        ref Links);
                else
                    Console.WriteLine("\tRequest was invalid");

                Console.WriteLine("Connexion closed");
                clientSocket.Close();
            }
            serverSocket.Stop();
            mmf.Dispose();
            return 0;
        }
    }
}
