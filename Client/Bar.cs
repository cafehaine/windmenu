using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.MemoryMappedFiles;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    class Bar : Form
    {
        public enum Position : byte
        {
            top = 1,
            bottom = 0
        }

        #region Variables

        private Color barBack;
        private SolidBrush barFore;
        private SolidBrush selBack;
        private SolidBrush selFore;
        private Screen output;
        private Position pos;

        private int barHeight;
        private string text = string.Empty;
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

            barBack = NormalBack;
            barFore = new SolidBrush(NormalFore);
            selBack = new SolidBrush(FocusedBack);
            selFore = new SolidBrush(FocusedFore);

            FormBorderStyle = FormBorderStyle.None;

            // Retreive program list:

            try
            {
                MemoryMappedFile mmf = MemoryMappedFile.OpenExisting("windmenu", MemoryMappedFileRights.Read);
                MemoryMappedViewAccessor va = mmf.CreateViewAccessor(0, 8, MemoryMappedFileAccess.Read);
                long sizeData;
                va.Read(0, out sizeData);
                va.Dispose();
                va = mmf.CreateViewAccessor(8, sizeData, MemoryMappedFileAccess.Read);
                byte[] data = new byte[sizeData];
                va.ReadArray(0, data, 0, (int)sizeData);
                va.Dispose();
                mmf.Dispose();
                
                programList = Encoding.Unicode.GetString(data).Split('|');
            }
            catch (Exception)
            {
                throw new Exception("Communication error with the server.\nTry killing the server and starting it again.");
            }

            suggestions = new List<string>();
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
            updateSuggestions();
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

                    try { clientSocket.Connect(System.Net.IPAddress.Loopback, 12321); }
                    catch
                    {
                        MessageBox.Show("Server unreachable.");
                        return;
                    }

                    NetworkStream serverStream = clientSocket.GetStream();
                    byte[] outStream = Encoding.Unicode.GetBytes(data);
                    byte[] messageSize = new byte[] { (byte)(outStream.Length / 256), (byte)(outStream.Length % 256) };
                    serverStream.Write(messageSize, 0, 2);
                    serverStream.Write(outStream, 0, outStream.Length);

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
                if (suggIndex < suggestions.Count - 1)
                {
                    suggIndex++;
                    Invalidate();
                }
            }
            else
            {
                string input = KeyboardConverter.KeyCodeToUnicode(e.KeyCode);
                if (input != "")
                {
                    text += input;
                    updateSuggestions();
                }
            }

        }

        protected override void OnLostFocus(EventArgs e)
        {
            #if !DEBUG
                Close();
            #endif
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(barBack);

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
                    g.DrawString(sugg, Font, barFore, new PointF(leftPos, 1));

                leftPos += size.Width + 2;
            }
        }

        private void updateSuggestions()
        {
            suggIndex = 0;
            suggestions.Clear();
            int i = 0;
            Graphics temp = Graphics.FromHwndInternal(Handle);
            float xPosition = 0;

            while (xPosition < ClientRectangle.Width * 0.9F && i < programList.Length)
            {
                string element = programList[i];
                if (element.ToLower().Contains(text.ToLower()))
                {
                    suggestions.Add(element);
                    xPosition += temp.MeasureString(element, Font).Width + 2;
                }
                i++;
            }

            temp.Dispose();

            Invalidate();
        }
    }
}
