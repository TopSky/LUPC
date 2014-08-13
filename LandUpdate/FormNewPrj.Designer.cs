namespace LandUpdate
{
    partial class FormNewPrj
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
            this.名称 = new System.Windows.Forms.Label();
            this.projNameText = new System.Windows.Forms.TextBox();
            this.projPathText = new System.Windows.Forms.TextBox();
            this.pathBrowser = new System.Windows.Forms.Button();
            this.IDOK = new System.Windows.Forms.Button();
            this.IDCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // 名称
            // 
            this.名称.AutoSize = true;
            this.名称.Location = new System.Drawing.Point(12, 37);
            this.名称.Name = "名称";
            this.名称.Size = new System.Drawing.Size(53, 12);
            this.名称.TabIndex = 0;
            this.名称.Text = "工程名称";
            // 
            // projNameText
            // 
            this.projNameText.Location = new System.Drawing.Point(75, 34);
            this.projNameText.Name = "projNameText";
            this.projNameText.Size = new System.Drawing.Size(313, 21);
            this.projNameText.TabIndex = 2;
            // 
            // projPathText
            // 
            this.projPathText.Location = new System.Drawing.Point(75, 84);
            this.projPathText.Name = "projPathText";
            this.projPathText.ReadOnly = true;
            this.projPathText.Size = new System.Drawing.Size(313, 21);
            this.projPathText.TabIndex = 3;
            // 
            // pathBrowser
            // 
            this.pathBrowser.Location = new System.Drawing.Point(393, 82);
            this.pathBrowser.Name = "pathBrowser";
            this.pathBrowser.Size = new System.Drawing.Size(45, 23);
            this.pathBrowser.TabIndex = 4;
            this.pathBrowser.Text = "...";
            this.pathBrowser.UseVisualStyleBackColor = true;
            this.pathBrowser.Click += new System.EventHandler(this.pathBrowser_Click);
            // 
            // IDOK
            // 
            this.IDOK.Location = new System.Drawing.Point(232, 140);
            this.IDOK.Name = "IDOK";
            this.IDOK.Size = new System.Drawing.Size(75, 23);
            this.IDOK.TabIndex = 5;
            this.IDOK.Text = "确定";
            this.IDOK.UseVisualStyleBackColor = true;
            this.IDOK.Click += new System.EventHandler(this.IDOK_Click);
            // 
            // IDCancel
            // 
            this.IDCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.IDCancel.Location = new System.Drawing.Point(313, 140);
            this.IDCancel.Name = "IDCancel";
            this.IDCancel.Size = new System.Drawing.Size(75, 23);
            this.IDCancel.TabIndex = 6;
            this.IDCancel.Text = "取消";
            this.IDCancel.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "存储位置";
            // 
            // FormNewPrj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.IDCancel;
            this.ClientSize = new System.Drawing.Size(450, 177);
            this.Controls.Add(this.IDCancel);
            this.Controls.Add(this.IDOK);
            this.Controls.Add(this.pathBrowser);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.projPathText);
            this.Controls.Add(this.projNameText);
            this.Controls.Add(this.名称);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormNewPrj";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建工程";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label 名称;
        private System.Windows.Forms.Button pathBrowser;
        private System.Windows.Forms.Button IDOK;
        private System.Windows.Forms.Button IDCancel;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox projNameText;
        public System.Windows.Forms.TextBox projPathText;
    }
}