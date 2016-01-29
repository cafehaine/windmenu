using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32; // for registry io (path)
using System.IO; // to create a reg file
using System.Diagnostics; // run processes

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
            return output.Substring(0, output.Length - 1);
        }

        private void save()
        {
            Program.ini.IniWriteValue("Colors", "list", joinSettings(Program.colors));
            Program.ini.IniWriteValue("Aliases", "list", joinSettings(Program.aliases));
        } // function to save aliases and colors into the ini file

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        } // exit without saving

        private void linkLabelGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/psgarsenal/windmenu");
        } // open da gates

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
            
            listBoxPath.Items.Clear();
            foreach (string s in Program.pathList)
            {
                listBoxPath.Items.Add(s);
            }
        } // setting the values of some lists

        #region Color buttons
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

        #region Alias tab
        private void buttonAliasesAdd_Click(object sender, EventArgs e)
        {
            FormAlias aliasesForm = new FormAlias();
            aliasesForm.ShowDialog();
            if (aliasesForm.created)
            {
                Program.aliases.Add(aliasesForm.alias + "=\"" + aliasesForm.aliasTarget + "\"");
                listBoxAliases.Items.Add(aliasesForm.alias + "=\"" + aliasesForm.aliasTarget + "\"");
            }
        } // add an alias to the list

        private void buttonAliasesRemove_Click(object sender, EventArgs e)
        {
            if (listBoxAliases.SelectedIndex != -1)
            {
                string toRemove = listBoxAliases.Items[listBoxAliases.SelectedIndex].ToString();
                listBoxAliases.Items.Remove(toRemove);
                Program.aliases.Remove(toRemove);
            }
        } // remove an alias from the list
        #endregion

        #region Path tab
        private void buttonPathRemove_Click(object sender, EventArgs e)
        {
            if (listBoxPath.SelectedIndex != -1)
            {
                string toRemove = listBoxPath.Items[listBoxPath.SelectedIndex].ToString();
                listBoxPath.Items.Remove(toRemove);
                Program.pathList.Remove(toRemove);
            }
        }

        private void buttonPathAdd_Click(object sender, EventArgs e)
        {
            FormPath pathForm = new FormPath();
            pathForm.ShowDialog();
            if (pathForm.isSetPath)
            {
                listBoxPath.Items.Add(pathForm.path);
                Program.pathList.Add(pathForm.path);
            }
        }

        private void buttonPathSave_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("This operation is dangerous, are you sure you want to write your changes?", "This operation is dangerous!", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                string pathOld = (string)Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Environment\").GetValue("PATH", "", RegistryValueOptions.DoNotExpandEnvironmentNames);
                string backupPath = Environment.GetEnvironmentVariable("USERPROFILE") + "\\path.reg";
                createPathReg(pathOld, backupPath);
                if (writePath(joinSettings(Program.pathList)))
                {
                    MessageBox.Show("You may need to reboot in order to see the path change take effect. Also a backup of your old path was written in \"" + backupPath + "\".", "Path written");
                }
            }
        }

        private string convertPath(string input)
        {
            string output = "";
            output += BitConverter.ToString(Encoding.ASCII.GetBytes(input)).Replace("-",",00,") + ",00";
            return output;
        }

        private string createPathReg(string value, string regPath)
        {
            StreamWriter stream = new StreamWriter(regPath);
            stream.WriteLine("Windows Registry Editor Version 5.00");
            stream.WriteLine("[HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment]");
            stream.Write("\"Path\"=hex(2):");
            stream.Write(convertPath(value));
            stream.Write("\n");
            stream.Close();
            return regPath;
        } // create a reg file to set path from value, and returns the path

        private bool writePath(string path)
        {
            // Create reg file
            string pathToRegFile = createPathReg(path, Path.GetTempPath() + Guid.NewGuid().ToString() + ".reg");
            // Write change
            var psi = new ProcessStartInfo();
            psi.FileName = "regedit.exe";
            psi.Arguments = "/S "+ pathToRegFile;
            psi.Verb = "runas";
            try
            {
                Process p = Process.Start(psi);
                p.WaitForExit();
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show("Failed to get required priviledges", "Impossible to write path", MessageBoxButtons.OK);
                File.Delete(pathToRegFile);
                return false;
            }
            File.Delete(pathToRegFile);
            return true;
        }
        #endregion
    }
}
