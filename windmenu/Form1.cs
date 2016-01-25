using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace windmenu
{
    public partial class Form1 : Form
    {
        private void runProgram(string name)
        {
            bool ran = false;
            Process status;
            // check if name is in alias list
            foreach (string s in Program.aliases)
            {
                if (s.Split('=')[0] == name && !ran)
                {
                    try
                    {
                        status = Process.Start(s.Split('=')[1]);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error: " + e, "Could not run program", MessageBoxButtons.OK);
                    }
                    ran = true;
                }
            }
            // if not an alias, try running it as a command
            if (!ran)
            {
                try
                {
                    status = Process.Start(name);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e, "Could not run program", MessageBoxButtons.OK);
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void cancel()
        {
            Application.Exit();
        }

        private void validate()
        {
            runProgram(textBoxMain.Text);
            Application.Exit();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            FormSettings settingsForm = new FormSettings();
            settingsForm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Program.colors.Length == 5)
            {
                // main
                BackColor = ColorTranslator.FromHtml(Program.colors[0]);
                // textbox
                textBoxMain.BackColor = ColorTranslator.FromHtml(Program.colors[1]);
                textBoxMain.ForeColor = ColorTranslator.FromHtml(Program.colors[2]);
                // button
                buttonSettings.BackColor = ColorTranslator.FromHtml(Program.colors[3]);
                buttonSettings.ForeColor = ColorTranslator.FromHtml(Program.colors[4]);
            }
        }

        private void textBoxMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                validate();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                cancel();
            }
        }
    }
}
