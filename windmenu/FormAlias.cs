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
    public partial class FormAlias : Form
    {
        public FormAlias()
        {
            InitializeComponent();
        }

        private void buttonFindTarget_Click(object sender, EventArgs e)
        {
            openFileDialogTarget.ShowDialog();
            textBoxAliasTarget.Text = openFileDialogTarget.FileName;
        }
    }
}
