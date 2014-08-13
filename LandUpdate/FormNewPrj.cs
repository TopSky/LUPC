using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;

namespace LandUpdate
{
    public partial class FormNewPrj : Form
    {
        public FormNewPrj()
        {
            InitializeComponent();
        }

        private void pathBrowser_Click(object sender, EventArgs e)
        {
            string path = "";
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.Description = "请选择工程存放的文件夹。";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                path = folderBrowserDialog.SelectedPath;
            }
            
            this.projPathText.Text = path;
        }

        private void IDOK_Click(object sender, EventArgs e)
        {
            if (projNameText.Text.Trim() == String.Empty || projPathText.Text.Trim() == String.Empty)
            {
                MessageBox.Show("请填写完整后重试！");
                this.DialogResult = DialogResult.None;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

   }
}
