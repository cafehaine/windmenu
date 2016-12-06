using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;

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

        public static string TreatRequest(string request,
            ref List<KeyValuePair<string,string>> list)
        {
            string output = string.Empty;
            if (request.StartsWith("run"))
            {
                int index = list.FindIndex(x => x.Key == request.Substring(3));
                if (index != -1)
                {
                    Process.Start(list[index].Value);
                    output = "\x06";
                }
                else
                    output += "\x15Key missing from list.";
            }
            else
            {
                output = "\x06";
                foreach (KeyValuePair<string, string> kvp in list)
                    output += kvp.Key + "|";
                output.Substring(0, output.Length - 1);
            }
            return output;
        }

        static void Main(string[] args)
        {
            List<KeyValuePair<string,string>> Links = Binaries.GetBinaries(true,
                new string[] {"uninstall", "help", "manual", "readme",
                    "read me", "register" });
            // load blacklist from config file
            Links = Links.Distinct(new linksComparer()).ToList();
            Links.Sort(Comparer<KeyValuePair<string, string>>.Create(
                (i1, i2) => i1.Key.CompareTo(i2.Key)));

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
                stream.Read(clientRequestSize, 0, 2);

                int inSize = clientRequestSize[0] * 256 + clientRequestSize[1];

                byte[] clientRequest = new byte[inSize];
                stream.Read(clientRequest, 0, inSize);

                Console.WriteLine("\tClient request: " +
                    Encoding.UTF8.GetString(clientRequest));
                string output = TreatRequest(
                    Encoding.UTF8.GetString(clientRequest), ref Links);
                Console.WriteLine("\tServer answer: " + output.Substring(1));

                byte[] outSize = new byte[] { (byte)(output.Length / 256),
                    (byte)(output.Length % 256) };
                byte[] sendBytes = Encoding.UTF8.GetBytes(output);
                stream.Write(outSize, 0, 2);
                stream.Write(sendBytes, 0, sendBytes.Length);

                Console.WriteLine("Connexion closed");
                clientSocket.Close();
            }
            serverSocket.Stop();
        }
    }
}
