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
                Process.Start(list[index].Value);
            else
                Console.WriteLine("\tProgram requested was not in the list");
        }

        static int Main(string[] args)
        {
            Regex[] blacklist = new Regex[6];
            string[] regexes = new string[] {".*uninstall.*", ".*help.*",
                ".*manual.*", ".*read ?me.*", ".*register.*", "\\{.*\\}" };

            for (int i = 0; i < regexes.Length; i++)
                blacklist[i] = new Regex(regexes[i], RegexOptions.IgnoreCase);

            List<KeyValuePair<string,string>> Links = Binaries.GetBinaries(
                true, blacklist);

            // load blacklist from config file

            Links = Links.Distinct(new linksComparer()).ToList();
            Links.Sort(Comparer<KeyValuePair<string, string>>.Create(
                (i1, i2) => i1.Key.CompareTo(i2.Key)));

            List<string> LinksNames = new List<string>(Links.Count);

            for (int i = 0; i < Links.Count; i++)
                LinksNames.Add(Links[i].Key);
            
            byte[] toWrite = Encoding.Unicode.GetBytes(string.Join("|", LinksNames));

            MemoryMappedFile mmf;
            try
            {
                mmf = MemoryMappedFile.CreateNew("windmenu", 8 + toWrite.Length);
                MemoryMappedViewAccessor va = mmf.CreateViewAccessor();
                va.Write(0, toWrite.LongLength);
                va.WriteArray(8, toWrite, 0, toWrite.Length);
                va.Dispose();
            }
            catch(Exception)
            {
                MessageBox.Show("windmenu Server seems to already be running.", "windmenu Server");
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

                byte[] clientRequestSize =
                    new byte[clientSocket.ReceiveBufferSize];

                int inSize = clientRequestSize[0] * 256 + clientRequestSize[1];

                byte[] clientRequest = new byte[inSize];
                stream.Read(clientRequest, 0, inSize);
                string data = Encoding.Unicode.GetString(clientRequest);

                Console.WriteLine("\tClient request: " + data);
                if (data.StartsWith("run"))
                    TreatRequest(Encoding.Unicode.GetString(clientRequest), ref Links);
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
