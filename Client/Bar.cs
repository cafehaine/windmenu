using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    class Bar : Form
    {
        public enum Position : int
        {
            top = 1,
            bottom = 0
        }

        #region Variables
        private SolidBrush barBack;
        private SolidBrush barFore;
        private SolidBrush selBack;
        private SolidBrush selFore;
        private Screen output;
        private Position pos;

        private int barHeight;
        private string text = "";
        private string[] programList;
        private List<string> suggestions;
        private int suggIndex = 0;

        #endregion

        public Bar(Position Pos, Color NormalBack, Color NormalFore,
            Color FocusedBack, Color FocusedFore, Font Font)
        {
            Text = "BTWM-EXCLUDED windwenu";
            BackColor = NormalBack;
            ForeColor = NormalFore;
            this.Font = Font;
            output = Screen.PrimaryScreen;
            pos = Pos;

            DoubleBuffered = true; // Reduce flickering on redraw
            TopMost = true; // Draw on top of everything

            barBack = new SolidBrush(NormalBack);
            barFore = new SolidBrush(NormalFore);
            selBack = new SolidBrush(FocusedBack);
            selFore = new SolidBrush(FocusedFore);

            FormBorderStyle = FormBorderStyle.None;

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
            suggestions = new List<string>(programList.Length);

            foreach (string element in programList)
                suggestions.Add(element);

            clientSocket.Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            int x, y, width, height;

            width = output.Bounds.Width;
            height = Font.Height + 2;

            barHeight = height;

            x = output.Bounds.X;

            switch (pos)
            {
                case Position.top:
                    y = 0;
                    break;
                default:
                    y = output.Bounds.Y + output.Bounds.Height - height;
                    break;
            }

            SetBoundsCore(x, y, width, height, BoundsSpecified.All);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
            else if (e.KeyCode == Keys.Enter)
            {
                if (suggestions.Count > 0)
                {
                    TcpClient clientSocket;
                    string data = "run" + suggestions[suggIndex];
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
            else if (e.KeyCode == Keys.Back)
            {
                if (text.Length == 0)
                    return;
                text = text.Substring(0, text.Length - 1);
                updateSuggestions();
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (suggIndex > 0)
                {
                    suggIndex--;
                    Invalidate();
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (suggIndex < suggestions.Count)
                {
                    suggIndex++;
                    Invalidate();
                }
            }
            else
            {
                text += KeyboardConverter.KeyCodeToUnicode(e.KeyCode);
                updateSuggestions();
            }

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.FillRectangle(barBack, ClientRectangle);

            float RightMost = ClientRectangle.Width;

            g.DrawString(text, Font, barFore, new RectangleF(1, 1, ClientRectangle.Width / 10, ClientRectangle.Height - 2));

            float leftPos = ClientRectangle.Width / 10 + 2;

            for (int i = 0; i < suggestions.Count; i++)
            {
                string sugg = suggestions[i];
                SizeF size = g.MeasureString(sugg, Font);
                if (i == suggIndex)
                {
                    g.FillRectangle(selBack, new Rectangle((int)leftPos, 0, (int)size.Width, barHeight));
                    g.DrawString(sugg, Font, selFore, new PointF(leftPos, 1));
                }
                else
                {
                    g.DrawString(sugg, Font, barFore, new PointF(leftPos, 1));
                }
                leftPos += size.Width;
            }
        }

        private void updateSuggestions()
        {
            suggIndex = 0;
            suggestions.Clear();
            int i = 0;
            while (suggestions.Count < 20 && i < programList.Length)
            {
                string element = programList[i];
                if (element.ToLower().Contains(text.ToLower()))
                    suggestions.Add(element);
                i++;
            }
            Invalidate();
        }
    }
}
