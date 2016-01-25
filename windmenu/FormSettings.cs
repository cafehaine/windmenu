using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace windmenu
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void buttonValidate_Click(object sender, EventArgs e)
        {
            save();
            this.Close();
        }

        private string joinSettings(string[] input)
        {
            string output = "";
            foreach (string s in input)
            {
                output += s + ";";
            }
            return output.Substring(0,output.Length -1);
        }
        private void save()
        {
            Program.ini.IniWriteValue("Colors", "list", joinSettings(Program.colors));
            Program.ini.IniWriteValue("Aliases", "list", joinSettings(Program.aliases));
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabelGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/psgarsenal/windmenu");
        }

        private void buttonAliasesAdd_Click(object sender, EventArgs e)
        {
            FormAlias aliasesForm = new FormAlias();
            aliasesForm.ShowDialog();
            if (aliasesForm.created)
            {
                Program.aliases[Program.aliases.Length] = (aliasesForm.alias + ";" + aliasesForm.aliasTarget);
            }
        }
    }
}
