namespace windmenu
{
    partial class FormSettings
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
            this.buttonValidate = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabPageAliases = new System.Windows.Forms.TabPage();
            this.listBoxAliases = new System.Windows.Forms.ListBox();
            this.buttonAliasesAdd = new System.Windows.Forms.Button();
            this.buttonAliasesRemove = new System.Windows.Forms.Button();
            this.tabPageColors = new System.Windows.Forms.TabPage();
            this.linkLabelGithub = new System.Windows.Forms.LinkLabel();
            this.tabControlSettings.SuspendLayout();
            this.tabPageAliases.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonValidate
            // 
            this.buttonValidate.Location = new System.Drawing.Point(197, 226);
            this.buttonValidate.Name = "buttonValidate";
            this.buttonValidate.Size = new System.Drawing.Size(75, 23);
            this.buttonValidate.TabIndex = 1;
            this.buttonValidate.Text = "OK";
            this.buttonValidate.UseVisualStyleBackColor = true;
            this.buttonValidate.Click += new System.EventHandler(this.buttonValidate_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(116, 226);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlSettings.Controls.Add(this.tabPageAliases);
            this.tabControlSettings.Controls.Add(this.tabPageColors);
            this.tabControlSettings.Location = new System.Drawing.Point(12, 12);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(260, 208);
            this.tabControlSettings.TabIndex = 3;
            // 
            // tabPageAliases
            // 
            this.tabPageAliases.Controls.Add(this.listBoxAliases);
            this.tabPageAliases.Controls.Add(this.buttonAliasesAdd);
            this.tabPageAliases.Controls.Add(this.buttonAliasesRemove);
            this.tabPageAliases.Location = new System.Drawing.Point(4, 22);
            this.tabPageAliases.Name = "tabPageAliases";
            this.tabPageAliases.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAliases.Size = new System.Drawing.Size(252, 182);
            this.tabPageAliases.TabIndex = 0;
            this.tabPageAliases.Text = "Aliases";
            this.tabPageAliases.UseVisualStyleBackColor = true;
            // 
            // listBoxAliases
            // 
            this.listBoxAliases.FormattingEnabled = true;
            this.listBoxAliases.Items.AddRange(new object[] {
            " "});
            this.listBoxAliases.Location = new System.Drawing.Point(6, 6);
            this.listBoxAliases.Name = "listBoxAliases";
            this.listBoxAliases.Size = new System.Drawing.Size(240, 134);
            this.listBoxAliases.TabIndex = 2;
            // 
            // buttonAliasesAdd
            // 
            this.buttonAliasesAdd.Location = new System.Drawing.Point(171, 153);
            this.buttonAliasesAdd.Name = "buttonAliasesAdd";
            this.buttonAliasesAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAliasesAdd.TabIndex = 1;
            this.buttonAliasesAdd.Text = "Add alias";
            this.buttonAliasesAdd.UseVisualStyleBackColor = true;
            this.buttonAliasesAdd.Click += new System.EventHandler(this.buttonAliasesAdd_Click);
            // 
            // buttonAliasesRemove
            // 
            this.buttonAliasesRemove.Location = new System.Drawing.Point(59, 153);
            this.buttonAliasesRemove.Name = "buttonAliasesRemove";
            this.buttonAliasesRemove.Size = new System.Drawing.Size(106, 23);
            this.buttonAliasesRemove.TabIndex = 0;
            this.buttonAliasesRemove.Text = "Remove selected";
            this.buttonAliasesRemove.UseVisualStyleBackColor = true;
            this.buttonAliasesRemove.Click += new System.EventHandler(this.buttonAliasesRemove_Click);
            // 
            // tabPageColors
            // 
            this.tabPageColors.Location = new System.Drawing.Point(4, 22);
            this.tabPageColors.Name = "tabPageColors";
            this.tabPageColors.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageColors.Size = new System.Drawing.Size(252, 182);
            this.tabPageColors.TabIndex = 1;
            this.tabPageColors.Text = "Colors";
            this.tabPageColors.UseVisualStyleBackColor = true;
            // 
            // linkLabelGithub
            // 
            this.linkLabelGithub.AutoSize = true;
            this.linkLabelGithub.Location = new System.Drawing.Point(12, 239);
            this.linkLabelGithub.Name = "linkLabelGithub";
            this.linkLabelGithub.Size = new System.Drawing.Size(38, 13);
            this.linkLabelGithub.TabIndex = 4;
            this.linkLabelGithub.TabStop = true;
            this.linkLabelGithub.Text = "Github";
            this.linkLabelGithub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelGithub_LinkClicked);
            // 
            // FormSettings
            // 
            this.AcceptButton = this.buttonValidate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.ControlBox = false;
            this.Controls.Add(this.linkLabelGithub);
            this.Controls.Add(this.tabControlSettings);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonValidate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.tabControlSettings.ResumeLayout(false);
            this.tabPageAliases.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonValidate;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.TabPage tabPageAliases;
        private System.Windows.Forms.TabPage tabPageColors;
        private System.Windows.Forms.ListBox listBoxAliases;
        private System.Windows.Forms.Button buttonAliasesAdd;
        private System.Windows.Forms.Button buttonAliasesRemove;
        private System.Windows.Forms.LinkLabel linkLabelGithub;
    }
}