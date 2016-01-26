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
    public partial class FormPath : Form
    {
        public bool isSetPath = false;
        public string path = "";
        public FormPath()
        {
            InitializeComponent();
        }

        private void buttonValidate_Click(object sender, EventArgs e)
        {
            isSetPath = true;
            path = textBoxMain.Text;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            isSetPath = false;
            Close();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialogMain.ShowDialog();
            textBoxMain.Text = folderBrowserDialogMain.SelectedPath;
        }
    }
}
