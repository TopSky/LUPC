namespace LandUpdate
{
    partial class mainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProject = new System.Windows.Forms.ToolStripMenuItem();
            this.openProject = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProject = new System.Windows.Forms.ToolStripMenuItem();
            this.exitProject = new System.Windows.Forms.ToolStripMenuItem();
            this.beforeFieldMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importDataMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.importMonitorShape = new System.Windows.Forms.ToolStripMenuItem();
            this.importBaseDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.importDLTB = new System.Windows.Forms.ToolStripMenuItem();
            this.importXZDW = new System.Windows.Forms.ToolStripMenuItem();
            this.importLXDW = new System.Windows.Forms.ToolStripMenuItem();
            this.importXZQ = new System.Windows.Forms.ToolStripMenuItem();
            this.importBasicNTData = new System.Windows.Forms.ToolStripMenuItem();
            this.importPreImg = new System.Windows.Forms.ToolStripMenuItem();
            this.importPostImg = new System.Windows.Forms.ToolStripMenuItem();
            this.importRedPlanLine = new System.Windows.Forms.ToolStripMenuItem();
            this.importYDSPData = new System.Windows.Forms.ToolStripMenuItem();
            this.importTownName = new System.Windows.Forms.ToolStripMenuItem();
            this.jctbStatics = new System.Windows.Forms.ToolStripMenuItem();
            this.generateMobileData = new System.Windows.Forms.ToolStripMenuItem();
            this.MobileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据分发ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.外业数据汇总ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtPrjName = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.beforeFieldMenuItem,
            this.MobileMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip2";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProject,
            this.openProject,
            this.saveProject,
            this.exitProject});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.文件ToolStripMenuItem.Text = "工程管理";
            // 
            // newProject
            // 
            this.newProject.Name = "newProject";
            this.newProject.Size = new System.Drawing.Size(124, 22);
            this.newProject.Text = "新建工程";
            this.newProject.Click += new System.EventHandler(this.newProject_Click);
            // 
            // openProject
            // 
            this.openProject.Name = "openProject";
            this.openProject.Size = new System.Drawing.Size(124, 22);
            this.openProject.Text = "打开工程";
            this.openProject.Click += new System.EventHandler(this.openProject_Click);
            // 
            // saveProject
            // 
            this.saveProject.Name = "saveProject";
            this.saveProject.Size = new System.Drawing.Size(124, 22);
            this.saveProject.Text = "保存工程";
            this.saveProject.Click += new System.EventHandler(this.saveProject_Click);
            // 
            // exitProject
            // 
            this.exitProject.Name = "exitProject";
            this.exitProject.Size = new System.Drawing.Size(124, 22);
            this.exitProject.Text = "退出";
            this.exitProject.Click += new System.EventHandler(this.exitProject_Click);
            // 
            // beforeFieldMenuItem
            // 
            this.beforeFieldMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importDataMenu,
            this.jctbStatics,
            this.generateMobileData});
            this.beforeFieldMenuItem.Name = "beforeFieldMenuItem";
            this.beforeFieldMenuItem.Size = new System.Drawing.Size(104, 21);
            this.beforeFieldMenuItem.Text = "外业前数据准备";
            // 
            // importDataMenu
            // 
            this.importDataMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importMonitorShape,
            this.importBaseDatabase,
            this.importBasicNTData,
            this.importPreImg,
            this.importPostImg,
            this.importRedPlanLine,
            this.importYDSPData,
            this.importTownName});
            this.importDataMenu.Name = "importDataMenu";
            this.importDataMenu.Size = new System.Drawing.Size(160, 22);
            this.importDataMenu.Text = "导入数据";
            // 
            // importMonitorShape
            // 
            this.importMonitorShape.Name = "importMonitorShape";
            this.importMonitorShape.Size = new System.Drawing.Size(172, 22);
            this.importMonitorShape.Text = "导入监测图斑";
            this.importMonitorShape.Click += new System.EventHandler(this.importMonitorShape_Click);
            // 
            // importBaseDatabase
            // 
            this.importBaseDatabase.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importDLTB,
            this.importXZDW,
            this.importLXDW,
            this.importXZQ});
            this.importBaseDatabase.Name = "importBaseDatabase";
            this.importBaseDatabase.Size = new System.Drawing.Size(172, 22);
            this.importBaseDatabase.Text = "导入基础库";
            // 
            // importDLTB
            // 
            this.importDLTB.Name = "importDLTB";
            this.importDLTB.Size = new System.Drawing.Size(124, 22);
            this.importDLTB.Text = "地类图斑";
            this.importDLTB.Click += new System.EventHandler(this.importDLTB_Click);
            // 
            // importXZDW
            // 
            this.importXZDW.Name = "importXZDW";
            this.importXZDW.Size = new System.Drawing.Size(124, 22);
            this.importXZDW.Text = "线状地物";
            this.importXZDW.Click += new System.EventHandler(this.importXZDW_Click);
            // 
            // importLXDW
            // 
            this.importLXDW.Name = "importLXDW";
            this.importLXDW.Size = new System.Drawing.Size(124, 22);
            this.importLXDW.Text = "零星地物";
            this.importLXDW.Click += new System.EventHandler(this.importLXDW_Click);
            // 
            // importXZQ
            // 
            this.importXZQ.Name = "importXZQ";
            this.importXZQ.Size = new System.Drawing.Size(124, 22);
            this.importXZQ.Text = "行政区";
            this.importXZQ.Click += new System.EventHandler(this.importXZQ_Click);
            // 
            // importBasicNTData
            // 
            this.importBasicNTData.Name = "importBasicNTData";
            this.importBasicNTData.Size = new System.Drawing.Size(172, 22);
            this.importBasicNTData.Text = "导入基本农田数据";
            this.importBasicNTData.Click += new System.EventHandler(this.importBasicNTData_Click);
            // 
            // importPreImg
            // 
            this.importPreImg.Name = "importPreImg";
            this.importPreImg.Size = new System.Drawing.Size(172, 22);
            this.importPreImg.Text = "导入前时相卫片";
            this.importPreImg.Click += new System.EventHandler(this.importPreImg_Click);
            // 
            // importPostImg
            // 
            this.importPostImg.Name = "importPostImg";
            this.importPostImg.Size = new System.Drawing.Size(172, 22);
            this.importPostImg.Text = "导入后时相卫片";
            this.importPostImg.Click += new System.EventHandler(this.importPostImg_Click);
            // 
            // importRedPlanLine
            // 
            this.importRedPlanLine.Name = "importRedPlanLine";
            this.importRedPlanLine.Size = new System.Drawing.Size(172, 22);
            this.importRedPlanLine.Text = "导入规划红线";
            this.importRedPlanLine.Click += new System.EventHandler(this.importRedPlanLine_Click);
            // 
            // importYDSPData
            // 
            this.importYDSPData.Name = "importYDSPData";
            this.importYDSPData.Size = new System.Drawing.Size(172, 22);
            this.importYDSPData.Text = "导入用地审批数据";
            this.importYDSPData.Click += new System.EventHandler(this.importYDSPData_Click);
            // 
            // importTownName
            // 
            this.importTownName.Name = "importTownName";
            this.importTownName.Size = new System.Drawing.Size(172, 22);
            this.importTownName.Text = "导入乡镇名称";
            this.importTownName.Click += new System.EventHandler(this.importTownName_Click);
            // 
            // jctbStatics
            // 
            this.jctbStatics.Name = "jctbStatics";
            this.jctbStatics.Size = new System.Drawing.Size(160, 22);
            this.jctbStatics.Text = "统计计算";
            this.jctbStatics.Click += new System.EventHandler(this.jctbStatics_Click);
            // 
            // generateMobileData
            // 
            this.generateMobileData.Name = "generateMobileData";
            this.generateMobileData.Size = new System.Drawing.Size(160, 22);
            this.generateMobileData.Text = "生成移动端数据";
            this.generateMobileData.Click += new System.EventHandler(this.generateMobileData_Click);
            // 
            // MobileMenuItem
            // 
            this.MobileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.数据分发ToolStripMenuItem,
            this.外业数据汇总ToolStripMenuItem});
            this.MobileMenuItem.Name = "MobileMenuItem";
            this.MobileMenuItem.Size = new System.Drawing.Size(80, 21);
            this.MobileMenuItem.Text = "移动端管理";
            // 
            // 数据分发ToolStripMenuItem
            // 
            this.数据分发ToolStripMenuItem.Name = "数据分发ToolStripMenuItem";
            this.数据分发ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.数据分发ToolStripMenuItem.Text = "数据分发";
            // 
            // 外业数据汇总ToolStripMenuItem
            // 
            this.外业数据汇总ToolStripMenuItem.Name = "外业数据汇总ToolStripMenuItem";
            this.外业数据汇总ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.外业数据汇总ToolStripMenuItem.Text = "外业数据汇总";
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.axToolbarControl1.Location = new System.Drawing.Point(0, 25);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(1008, 28);
            this.axToolbarControl1.TabIndex = 2;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axTOCControl1.Location = new System.Drawing.Point(0, 0);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(181, 677);
            this.axTOCControl1.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 53);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.axTOCControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.axMapControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 677);
            this.splitContainer1.SplitterDistance = 181;
            this.splitContainer1.TabIndex = 5;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(0, 0);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(823, 677);
            this.axMapControl1.TabIndex = 4;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 53);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(995, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.txtPrjName});
            this.statusStrip1.Location = new System.Drawing.Point(0, 708);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabel1.Text = "工程名称：";
            // 
            // txtPrjName
            // 
            this.txtPrjName.Name = "txtPrjName";
            this.txtPrjName.Size = new System.Drawing.Size(80, 17);
            this.txtPrjName.Text = "未打开工程！";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "国土变更调查辅助软件";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProject;
        private System.Windows.Forms.ToolStripMenuItem openProject;
        private System.Windows.Forms.ToolStripMenuItem saveProject;
        private System.Windows.Forms.ToolStripMenuItem exitProject;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem importDataMenu;
        private System.Windows.Forms.ToolStripMenuItem importMonitorShape;
        private System.Windows.Forms.ToolStripMenuItem importBaseDatabase;
        private System.Windows.Forms.ToolStripMenuItem importBasicNTData;
        private System.Windows.Forms.ToolStripMenuItem importPreImg;
        private System.Windows.Forms.ToolStripMenuItem importPostImg;
        private System.Windows.Forms.ToolStripMenuItem importRedPlanLine;
        private System.Windows.Forms.ToolStripMenuItem importTownName;
        private System.Windows.Forms.ToolStripMenuItem importYDSPData;
        private System.Windows.Forms.ToolStripMenuItem jctbStatics;
        private System.Windows.Forms.ToolStripMenuItem generateMobileData;
        private System.Windows.Forms.ToolStripMenuItem importDLTB;
        private System.Windows.Forms.ToolStripMenuItem importXZQ;
        private System.Windows.Forms.ToolStripMenuItem importLXDW;
        private System.Windows.Forms.ToolStripMenuItem importXZDW;
        private System.Windows.Forms.ToolStripMenuItem 数据分发ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 外业数据汇总ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        public System.Windows.Forms.ToolStripMenuItem beforeFieldMenuItem;
        public System.Windows.Forms.ToolStripMenuItem MobileMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel txtPrjName;
    }
}

