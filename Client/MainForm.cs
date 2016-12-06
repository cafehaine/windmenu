using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class MainForm : Form
    {
        private string[] programList;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string data = "hello";
            TcpClient clientSocket = new TcpClient();

            try { clientSocket.Connect("127.0.0.1", 12321); }
            catch
            {
                MessageBox.Show("Server unreachable.");
                Close();
                return;
            }

            NetworkStream serverStream = clientSocket.GetStream();
            byte[] messageSize = new byte[] { (byte)(data.Length / 256), (byte)(data.Length % 256) };
            byte[] outStream = Encoding.UTF8.GetBytes(data);
            serverStream.Write(messageSize, 0, 2);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            byte[] serverMessageSize = new byte[clientSocket.ReceiveBufferSize];
            serverStream.Read(serverMessageSize, 0, 2);

            int inSize = serverMessageSize[0] * 256 + serverMessageSize[1];

            byte[] serverStatus = new byte[1];
            serverStream.Read(serverStatus, 0, 1);
            byte[] serverOutput = new byte[inSize];
            serverStream.Read(serverOutput, 0, inSize - 1);

            programList = Encoding.UTF8.GetString(serverOutput).Split('|');

            foreach (string element in programList)
                suggestions.Text += element + " ";

            clientSocket.Close();
        }

        private void textInput_TextChanged(object sender, EventArgs e)
        {
            suggestions.Text = "";
            int suggestionCount = 0;
            int i = 0;
            while (suggestionCount < 20 && i < programList.Length)
            {
                string element = programList[i];
                if (element.ToLower().Contains(textInput.Text.ToLower()))
                {
                    suggestions.Text += element + " ";
                    suggestionCount++;
                }
                i++;
            }
        }

        private void textInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int index = Array.FindIndex(programList,
                    s => s.ToLower().Contains(textInput.Text.ToLower()));
                if (index != -1)
                {
                    TcpClient clientSocket;
                    string data = "run" + programList[index];
                    clientSocket = new TcpClient();

                    try { clientSocket.Connect("127.0.0.1", 12321); }
                    catch
                    {
                        MessageBox.Show("Server unreachable.");
                        return;
                    }

                    NetworkStream serverStream = clientSocket.GetStream();
                    byte[] messageSize = new byte[] { (byte)(data.Length / 256), (byte)(data.Length % 256) };
                    byte[] outStream = Encoding.UTF8.GetBytes(data);
                    serverStream.Write(messageSize, 0, 2);
                    serverStream.Write(outStream, 0, outStream.Length);
                    serverStream.Flush();

                    byte[] serverMessageSize = new byte[clientSocket.ReceiveBufferSize];
                    serverStream.Read(serverMessageSize, 0, 2);

                    int inSize = serverMessageSize[0] * 256 + serverMessageSize[1];

                    byte[] serverStatus = new byte[1];
                    serverStream.Read(serverStatus, 0, 1);
                    byte[] serverOutput = new byte[inSize];
                    serverStream.Read(serverOutput, 0, inSize - 1);

                    string output = Encoding.UTF8.GetString(serverOutput);

                    if (serverStatus[0] != 6)
                        MessageBox.Show(output);

                    clientSocket.Close();
                }
                Close();
            }
            else if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
