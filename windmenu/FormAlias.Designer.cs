namespace windmenu
{
    partial class FormAlias
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelAlias = new System.Windows.Forms.Label();
            this.textBoxAlias = new System.Windows.Forms.TextBox();
            this.labelTarget = new System.Windows.Forms.Label();
            this.textBoxAliasTarget = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonValidate = new System.Windows.Forms.Button();
            this.buttonFindTarget = new System.Windows.Forms.Button();
            this.openFileDialogTarget = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // labelAlias
            // 
            this.labelAlias.AutoSize = true;
            this.labelAlias.Location = new System.Drawing.Point(12, 9);
            this.labelAlias.Name = "labelAlias";
            this.labelAlias.Size = new System.Drawing.Size(32, 13);
            this.labelAlias.TabIndex = 0;
            this.labelAlias.Text = "Alias:";
            // 
            // textBoxAlias
            // 
            this.textBoxAlias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAlias.Location = new System.Drawing.Point(12, 25);
            this.textBoxAlias.Name = "textBoxAlias";
            this.textBoxAlias.Size = new System.Drawing.Size(274, 20);
            this.textBoxAlias.TabIndex = 1;
            // 
            // labelTarget
            // 
            this.labelTarget.AutoSize = true;
            this.labelTarget.Location = new System.Drawing.Point(12, 48);
            this.labelTarget.Name = "labelTarget";
            this.labelTarget.Size = new System.Drawing.Size(41, 13);
            this.labelTarget.TabIndex = 2;
            this.labelTarget.Text = "Target:";
            // 
            // textBoxAliasTarget
            // 
            this.textBoxAliasTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAliasTarget.Location = new System.Drawing.Point(12, 64);
            this.textBoxAliasTarget.Name = "textBoxAliasTarget";
            this.textBoxAliasTarget.Size = new System.Drawing.Size(193, 20);
            this.textBoxAliasTarget.TabIndex = 3;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(130, 91);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonValidate
            // 
            this.buttonValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonValidate.Location = new System.Drawing.Point(211, 91);
            this.buttonValidate.Name = "buttonValidate";
            this.buttonValidate.Size = new System.Drawing.Size(75, 23);
            this.buttonValidate.TabIndex = 5;
            this.buttonValidate.Text = "Add alias";
            this.buttonValidate.UseVisualStyleBackColor = true;
            this.buttonValidate.Click += new System.EventHandler(this.buttonValidate_Click);
            // 
            // buttonFindTarget
            // 
            this.buttonFindTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFindTarget.Location = new System.Drawing.Point(211, 62);
            this.buttonFindTarget.Name = "buttonFindTarget";
            this.buttonFindTarget.Size = new System.Drawing.Size(75, 23);
            this.buttonFindTarget.TabIndex = 6;
            this.buttonFindTarget.Text = "Find...";
            this.buttonFindTarget.UseVisualStyleBackColor = true;
            this.buttonFindTarget.Click += new System.EventHandler(this.buttonFindTarget_Click);
            // 
            // openFileDialogTarget
            // 
            this.openFileDialogTarget.DefaultExt = "*.exe";
            this.openFileDialogTarget.FileName = "*.exe";
            this.openFileDialogTarget.Title = "Target of the alias...";
            // 
            // FormAlias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 120);
            this.Controls.Add(this.buttonFindTarget);
            this.Controls.Add(this.buttonValidate);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxAliasTarget);
            this.Controls.Add(this.labelTarget);
            this.Controls.Add(this.textBoxAlias);
            this.Controls.Add(this.labelAlias);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAlias";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "New alias...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAlias;
        private System.Windows.Forms.TextBox textBoxAlias;
        private System.Windows.Forms.Label labelTarget;
        private System.Windows.Forms.TextBox textBoxAliasTarget;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonValidate;
        private System.Windows.Forms.Button buttonFindTarget;
        private System.Windows.Forms.OpenFileDialog openFileDialogTarget;
    }
}