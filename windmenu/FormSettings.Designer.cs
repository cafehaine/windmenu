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
            this.buttonColorsTextForeground = new System.Windows.Forms.Button();
            this.buttonColorsButtonBackground = new System.Windows.Forms.Button();
            this.buttonColorsButtonForeground = new System.Windows.Forms.Button();
            this.buttonColorsTextBackground = new System.Windows.Forms.Button();
            this.buttonColorsBackground = new System.Windows.Forms.Button();
            this.labelColorsPreview = new System.Windows.Forms.Label();
            this.panelColorsDemo = new System.Windows.Forms.Panel();
            this.textBoxColorDemo = new System.Windows.Forms.TextBox();
            this.buttonColorsDemo = new System.Windows.Forms.Button();
            this.linkLabelGithub = new System.Windows.Forms.LinkLabel();
            this.colorDialogColors = new System.Windows.Forms.ColorDialog();
            this.buttonColorsSave = new System.Windows.Forms.Button();
            this.tabControlSettings.SuspendLayout();
            this.tabPageAliases.SuspendLayout();
            this.tabPageColors.SuspendLayout();
            this.panelColorsDemo.SuspendLayout();
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
            this.tabPageColors.Controls.Add(this.buttonColorsSave);
            this.tabPageColors.Controls.Add(this.buttonColorsTextForeground);
            this.tabPageColors.Controls.Add(this.buttonColorsButtonBackground);
            this.tabPageColors.Controls.Add(this.buttonColorsButtonForeground);
            this.tabPageColors.Controls.Add(this.buttonColorsTextBackground);
            this.tabPageColors.Controls.Add(this.buttonColorsBackground);
            this.tabPageColors.Controls.Add(this.labelColorsPreview);
            this.tabPageColors.Controls.Add(this.panelColorsDemo);
            this.tabPageColors.Location = new System.Drawing.Point(4, 22);
            this.tabPageColors.Name = "tabPageColors";
            this.tabPageColors.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageColors.Size = new System.Drawing.Size(252, 182);
            this.tabPageColors.TabIndex = 1;
            this.tabPageColors.Text = "Colors";
            this.tabPageColors.UseVisualStyleBackColor = true;
            // 
            // buttonColorsTextForeground
            // 
            this.buttonColorsTextForeground.Location = new System.Drawing.Point(87, 35);
            this.buttonColorsTextForeground.Name = "buttonColorsTextForeground";
            this.buttonColorsTextForeground.Size = new System.Drawing.Size(78, 23);
            this.buttonColorsTextForeground.TabIndex = 6;
            this.buttonColorsTextForeground.Text = "Text Fore";
            this.buttonColorsTextForeground.UseVisualStyleBackColor = true;
            this.buttonColorsTextForeground.Click += new System.EventHandler(this.buttonColorsTextForeground_Click);
            // 
            // buttonColorsButtonBackground
            // 
            this.buttonColorsButtonBackground.Location = new System.Drawing.Point(87, 6);
            this.buttonColorsButtonBackground.Name = "buttonColorsButtonBackground";
            this.buttonColorsButtonBackground.Size = new System.Drawing.Size(78, 23);
            this.buttonColorsButtonBackground.TabIndex = 5;
            this.buttonColorsButtonBackground.Text = "Button Back";
            this.buttonColorsButtonBackground.UseVisualStyleBackColor = true;
            this.buttonColorsButtonBackground.Click += new System.EventHandler(this.buttonColorsButtonBackground_Click);
            // 
            // buttonColorsButtonForeground
            // 
            this.buttonColorsButtonForeground.Location = new System.Drawing.Point(171, 6);
            this.buttonColorsButtonForeground.Name = "buttonColorsButtonForeground";
            this.buttonColorsButtonForeground.Size = new System.Drawing.Size(75, 23);
            this.buttonColorsButtonForeground.TabIndex = 4;
            this.buttonColorsButtonForeground.Text = "Button Fore";
            this.buttonColorsButtonForeground.UseVisualStyleBackColor = true;
            this.buttonColorsButtonForeground.Click += new System.EventHandler(this.buttonColorsButtonForeground_Click);
            // 
            // buttonColorsTextBackground
            // 
            this.buttonColorsTextBackground.Location = new System.Drawing.Point(6, 35);
            this.buttonColorsTextBackground.Name = "buttonColorsTextBackground";
            this.buttonColorsTextBackground.Size = new System.Drawing.Size(75, 23);
            this.buttonColorsTextBackground.TabIndex = 3;
            this.buttonColorsTextBackground.Text = "Text Back";
            this.buttonColorsTextBackground.UseVisualStyleBackColor = true;
            this.buttonColorsTextBackground.Click += new System.EventHandler(this.buttonColorsTextBackground_Click);
            // 
            // buttonColorsBackground
            // 
            this.buttonColorsBackground.Location = new System.Drawing.Point(6, 6);
            this.buttonColorsBackground.Name = "buttonColorsBackground";
            this.buttonColorsBackground.Size = new System.Drawing.Size(75, 23);
            this.buttonColorsBackground.TabIndex = 2;
            this.buttonColorsBackground.Text = "Background";
            this.buttonColorsBackground.UseVisualStyleBackColor = true;
            this.buttonColorsBackground.Click += new System.EventHandler(this.buttonColorsBackground_Click);
            // 
            // labelColorsPreview
            // 
            this.labelColorsPreview.AutoSize = true;
            this.labelColorsPreview.Location = new System.Drawing.Point(7, 129);
            this.labelColorsPreview.Name = "labelColorsPreview";
            this.labelColorsPreview.Size = new System.Drawing.Size(48, 13);
            this.labelColorsPreview.TabIndex = 1;
            this.labelColorsPreview.Text = "Preview:";
            // 
            // panelColorsDemo
            // 
            this.panelColorsDemo.Controls.Add(this.textBoxColorDemo);
            this.panelColorsDemo.Controls.Add(this.buttonColorsDemo);
            this.panelColorsDemo.Location = new System.Drawing.Point(6, 145);
            this.panelColorsDemo.Name = "panelColorsDemo";
            this.panelColorsDemo.Size = new System.Drawing.Size(240, 31);
            this.panelColorsDemo.TabIndex = 0;
            // 
            // textBoxColorDemo
            // 
            this.textBoxColorDemo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxColorDemo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxColorDemo.Location = new System.Drawing.Point(84, 8);
            this.textBoxColorDemo.Name = "textBoxColorDemo";
            this.textBoxColorDemo.Size = new System.Drawing.Size(153, 13);
            this.textBoxColorDemo.TabIndex = 1;
            this.textBoxColorDemo.Text = "Demo Text";
            // 
            // buttonColorsDemo
            // 
            this.buttonColorsDemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonColorsDemo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonColorsDemo.Location = new System.Drawing.Point(3, 3);
            this.buttonColorsDemo.Name = "buttonColorsDemo";
            this.buttonColorsDemo.Size = new System.Drawing.Size(75, 25);
            this.buttonColorsDemo.TabIndex = 0;
            this.buttonColorsDemo.Text = "Settings";
            this.buttonColorsDemo.UseVisualStyleBackColor = true;
            // 
            // linkLabelGithub
            // 
            this.linkLabelGithub.AutoSize = true;
            this.linkLabelGithub.Location = new System.Drawing.Point(19, 231);
            this.linkLabelGithub.Name = "linkLabelGithub";
            this.linkLabelGithub.Size = new System.Drawing.Size(38, 13);
            this.linkLabelGithub.TabIndex = 4;
            this.linkLabelGithub.TabStop = true;
            this.linkLabelGithub.Text = "Github";
            this.linkLabelGithub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelGithub_LinkClicked);
            // 
            // buttonColorsSave
            // 
            this.buttonColorsSave.Location = new System.Drawing.Point(10, 88);
            this.buttonColorsSave.Name = "buttonColorsSave";
            this.buttonColorsSave.Size = new System.Drawing.Size(233, 23);
            this.buttonColorsSave.TabIndex = 7;
            this.buttonColorsSave.Text = "Save colors";
            this.buttonColorsSave.UseVisualStyleBackColor = true;
            this.buttonColorsSave.Click += new System.EventHandler(this.buttonColorsSave_Click);
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
            this.tabPageColors.ResumeLayout(false);
            this.tabPageColors.PerformLayout();
            this.panelColorsDemo.ResumeLayout(false);
            this.panelColorsDemo.PerformLayout();
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
        private System.Windows.Forms.Button buttonColorsTextForeground;
        private System.Windows.Forms.Button buttonColorsButtonBackground;
        private System.Windows.Forms.Button buttonColorsButtonForeground;
        private System.Windows.Forms.Button buttonColorsTextBackground;
        private System.Windows.Forms.Button buttonColorsBackground;
        private System.Windows.Forms.Label labelColorsPreview;
        private System.Windows.Forms.Panel panelColorsDemo;
        private System.Windows.Forms.TextBox textBoxColorDemo;
        private System.Windows.Forms.Button buttonColorsDemo;
        private System.Windows.Forms.ColorDialog colorDialogColors;
        private System.Windows.Forms.Button buttonColorsSave;
    }
}