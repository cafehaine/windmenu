using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
        private Point oldMousePos;
        private string lastSuggested = string.Empty;

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
            ShowInTaskbar = false;

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

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x80;
                return cp;
            }
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
            oldMousePos = Cursor.Position;
            Cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Position = new Point(ClientRectangle.Width / 10,
                ClientRectangle.Height);
            Cursor.Clip = new Rectangle(Location, Size);
            Cursor.Hide();
            BringToFront();
            Activate();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Cursor.Clip = new Rectangle();
            Cursor.Position = oldMousePos;
            Cursor.Show();
            base.OnClosing(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
            else if (e.KeyCode == Keys.Enter)
            {
                if (suggestions.Count > 0 || text.StartsWith(lastSuggested,
                    StringComparison.OrdinalIgnoreCase))
                {
                    TcpClient clientSocket;
                    string data;
                    if (suggestions.Count > 0)
                        data = "run" + suggestions[suggIndex];
                    else
                        data = "run" + lastSuggested + "|" +
                            text.Substring(lastSuggested.Length + 1);
                    clientSocket = new TcpClient();

                    try { clientSocket.Connect(System.Net.IPAddress.Loopback, 12321); }
                    catch
                    {
                        MessageBox.Show("Server unreachable.");
                        return;
                    }

                    NetworkStream serverStream = clientSocket.GetStream();
                    byte[] outStream = Encoding.Unicode.GetBytes(data);
                    byte[] messageSize = new byte[] {
                        (byte)(outStream.Length / 256),
                        (byte)(outStream.Length % 256) };

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
            else if (e.KeyCode == Keys.Tab)
            {
                text = suggestions[suggIndex];
                updateSuggestions();
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

            SizeF textSize = g.MeasureString(text, Font);
            if (textSize.Width < ClientRectangle.Width / 10)
                g.DrawString(text, Font, barFore, new RectangleF(1, 1, ClientRectangle.Width / 10, ClientRectangle.Height - 2));
            else
                g.DrawString(text, Font, barFore, new RectangleF(ClientRectangle.Width / 10 - textSize.Width, 1, textSize.Width, ClientRectangle.Height - 2));

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

        private class comparer : IComparer
        {
            string input;

            public comparer(string Input)
            { input = Input; }

            int IComparer.Compare(object a, object b)
            {
                string x = (string)a;
                string y = (string)b;
                if (x == y)
                    return 0;
                bool xStarts = x.StartsWith(input, StringComparison.OrdinalIgnoreCase);
                bool yStarts = y.StartsWith(input, StringComparison.OrdinalIgnoreCase);
                if (xStarts ^ yStarts)
                {
                    if (xStarts)
                        return -1;
                    return 1;
                }
                else
                    return string.Compare(x, y, true);
            }
        }

        private void updateSuggestions()
        {
            if (suggestions.Count != 0)
                lastSuggested = suggestions[suggIndex];

            suggIndex = 0;
            suggestions.Clear();
            int i = 0;
            Graphics temp = Graphics.FromHwndInternal(Handle);
            float xPosition = 0;

            Array.Sort(programList, new comparer(text));

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
