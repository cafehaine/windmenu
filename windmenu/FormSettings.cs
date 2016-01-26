using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32; // for registry reading

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

        private string joinSettings(List<string> input)
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
                Program.aliases.Add(aliasesForm.alias + "=\"" + aliasesForm.aliasTarget + "\"");
                listBoxAliases.Items.Add(aliasesForm.alias + "=\"" + aliasesForm.aliasTarget + "\"");
            }
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            // ALIAS TAB
            listBoxAliases.Items.Clear();
            foreach (string s in Program.aliases)
            {
                listBoxAliases.Items.Add(s);
            }
            // COLOR TAB
            if (Program.colors.Count == 5)
            {
                // main
                panelColorsDemo.BackColor = ColorTranslator.FromHtml(Program.colors[0]);
                // textbox
                textBoxColorDemo.BackColor = ColorTranslator.FromHtml(Program.colors[1]);
                textBoxColorDemo.ForeColor = ColorTranslator.FromHtml(Program.colors[2]);
                // button
                buttonColorsDemo.BackColor = ColorTranslator.FromHtml(Program.colors[3]);
                buttonColorsDemo.ForeColor = ColorTranslator.FromHtml(Program.colors[4]);
            }
            // PATH TAB
            string keyName = @"SYSTEM\CurrentControlSet\Control\Session Manager\Environment\";
            string pathVar = (string)Registry.LocalMachine.OpenSubKey(keyName).GetValue("PATH", "", RegistryValueOptions.DoNotExpandEnvironmentNames);
            listBoxPath.Items.Clear();
            listBoxPath.Items.AddRange(pathVar.Split(';'));
        } // setting the values of some lists
        #region Color buttons
        private void buttonAliasesRemove_Click(object sender, EventArgs e)
        {
            if (listBoxAliases.SelectedIndex != -1)
            {
                string toRemove = listBoxAliases.Items[listBoxAliases.SelectedIndex].ToString();
                listBoxAliases.Items.Remove(toRemove);
                Program.aliases.Remove(toRemove);
            }
        }

        private void buttonColorsBackground_Click(object sender, EventArgs e)
        {
            colorDialogColors.ShowDialog();
            panelColorsDemo.BackColor = colorDialogColors.Color;
        }

        private void buttonColorsButtonBackground_Click(object sender, EventArgs e)
        {
            colorDialogColors.ShowDialog();
            buttonColorsDemo.BackColor = colorDialogColors.Color;
        }

        private void buttonColorsButtonForeground_Click(object sender, EventArgs e)
        {
            colorDialogColors.ShowDialog();
            buttonColorsDemo.ForeColor = colorDialogColors.Color;
        }

        private void buttonColorsTextBackground_Click(object sender, EventArgs e)
        {
            colorDialogColors.ShowDialog();
            textBoxColorDemo.BackColor = colorDialogColors.Color;
        }

        private void buttonColorsTextForeground_Click(object sender, EventArgs e)
        {
            colorDialogColors.ShowDialog();
            textBoxColorDemo.ForeColor = colorDialogColors.Color;
        }

        private void buttonColorsSave_Click(object sender, EventArgs e)
        {
            Program.colors.Clear();
            Program.colors.Add(ColorTranslator.ToHtml(panelColorsDemo.BackColor));
            Program.colors.Add(ColorTranslator.ToHtml(textBoxColorDemo.BackColor));
            Program.colors.Add(ColorTranslator.ToHtml(textBoxColorDemo.ForeColor));
            Program.colors.Add(ColorTranslator.ToHtml(buttonColorsDemo.BackColor));
            Program.colors.Add(ColorTranslator.ToHtml(buttonColorsDemo.ForeColor));
        }
        #endregion
    }
}
