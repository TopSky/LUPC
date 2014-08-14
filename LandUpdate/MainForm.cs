﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace LandUpdate
{
    public partial class mainForm : Form
    {
        private IMapControl2 m_mapControl;
        private bool m_isProjOpen;
        private int m_currentLayerNum;

        public ArrayList m_monitorLayers;
        public ArrayList m_jbntLayers;
        public ArrayList m_ydspLayers;
        public ArrayList m_redLineLayers;
        public ArrayList m_preImgLayers;
        public ArrayList m_postImgLayers;

        public ArrayList m_lyerEnvelop;
        public ILayer    m_dltbLayer;
        public ILayer    m_lxdwLayer;
        public ILayer    m_xzdwLayer;
        public ILayer    m_xzqLayer;

        //下面这些Envelope需要全局吗，？
        //需要
        IEnvelope m_monitorEnv;
        IEnvelope m_dltbEnv;
        IEnvelope m_xzqEnv;
        IEnvelope m_jbntEnv;
        IEnvelope m_lxdwEnv;
        IEnvelope m_xzdwEnv;
        IEnvelope m_redLineEnv;
        IEnvelope m_ydspEnv;

        //public ProjPathChoose m_newPrjChooseDlg;
        public ProjectManage m_prjMan;
        
        //public ILayer jctbLayer
        public mainForm()
        {
            InitializeComponent();
            initialControls();
            
            initialSetting();
            
        }

        private void initialSetting()
        {
            m_isProjOpen = false;
            m_currentLayerNum = 0;
            m_prjMan = new ProjectManage();
            m_prjMan.initPrj();

            m_monitorLayers = new ArrayList();
            m_jbntLayers = new ArrayList();
            m_ydspLayers = new ArrayList();
            m_preImgLayers = new ArrayList();
            m_postImgLayers = new ArrayList();
            m_dltbLayer = null;
            m_lxdwLayer = null;
            m_xzdwLayer = null;
            m_xzqLayer = null;
            m_redLineLayers = new ArrayList();

            this.beforeFieldMenuItem.Enabled = false;
            this.MobileMenuItem.Enabled = false;
        }
        private void initialControls() 
        {
            m_mapControl = axMapControl1.Object as IMapControl2;
            m_mapControl.Map.Name = "工程数据";
            //初始化工具栏，界面上已经绑定
            
            //axTOCControl1.SetBuddyControl(axMapControl1);
            //axToolbarControl1.SetBuddyControl(axMapControl1);
            //axToolbarControl1.AddItem("esriControls.ControlsNewDocCommand");
            //axToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand");
            //axToolbarControl1.AddItem("esriControls.ControlsAddDataCommand");
            //axToolbarControl1.AddItem("esriControls.ControlsSaveAsDocCommand");
            //axToolbarControl1.AddItem("esriControls.ControlsMapNavigationToolbar");
            //axToolbarControl1.AddItem("esriControls.ControlsMapIdentifyTool");
         }

        #region 工程管理

        //新建工程
        private void newProject_Click(object sender, EventArgs e)
        {
            //如果有工程打开，则先提示是否保存工程，再清空相关参数，再新建工程，这段代码没写
            if (m_isProjOpen == true)
            {
                DialogResult dr = MessageBox.Show("工程"+m_prjMan.m_projectName+"已打开，是否保存后关闭？","提示",MessageBoxButtons.YesNoCancel);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    saveProject_Click(sender, e);
                    //是否清理图层
                    m_mapControl.ClearLayers();
                    //axMapControl1.ClearLayers();
                    axTOCControl1.Update();
                    initialSetting();
                }
                else if (dr == DialogResult.No)
                {
                    //是否清理图层
                    m_mapControl.ClearLayers();
                    //axMapControl1.ClearLayers();
                    axTOCControl1.Update();
                    initialSetting();
                }
                else
                {
                    return;
                }
            }
            FormNewPrj m_newPrjChooseDlg = new FormNewPrj();
            DialogResult dr1 = m_newPrjChooseDlg.ShowDialog();
            if (dr1 == DialogResult.OK)
            {
                try
                {
                    m_prjMan.m_projectPath = m_newPrjChooseDlg.projPathText.Text;
                    m_prjMan.m_projectName = m_newPrjChooseDlg.projNameText.Text;
                    m_prjMan.CreatePrjPaths();
                    m_prjMan.WriteProjectFile();
                    m_isProjOpen = true;
                    this.beforeFieldMenuItem.Enabled = true;
                    this.MobileMenuItem.Enabled = true;
                    this.txtPrjName.Text = m_prjMan.m_projectName;
                    MessageBox.Show("创建工程成功！");

                }
                catch(Exception ex)
                {
                    MessageBox.Show("创建工程失败！"+ex.Message);
                }
            }
        }

        //打开工程
        private void openProject_Click(object sender, EventArgs e)
        {
            //如果有工程打开，则先提示是否保存工程，再清空相关参数，再新建工程，这段代码没写
            if (m_isProjOpen == true)
            {
                DialogResult dr = MessageBox.Show("工程" + m_prjMan.m_projectName + "已打开，是否保存后关闭？", "提示", MessageBoxButtons.YesNoCancel);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    saveProject_Click(sender, e);
                    //清理图层
                    m_mapControl.ClearLayers();
                    //m_mapControl.Refresh();
                    axTOCControl1.Update();
                    initialSetting();
                }
                else if (dr == DialogResult.No)
                {
                    //清理图层
                    m_mapControl.ClearLayers();
                    //m_mapControl.Refresh();
                    axTOCControl1.Update();
                    initialSetting();
                }
                else
                {
                    return;
                }
            }

            string mxdFilePath;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "土地变更工程(*.bgw)|*.bgw";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                if (m_prjMan.LoadProject(FileName))
                {
                    this.txtPrjName.Text = m_prjMan.m_projectName;
                    m_isProjOpen = true;
                    this.beforeFieldMenuItem.Enabled = true;
                    this.MobileMenuItem.Enabled = true;

                    mxdFilePath = m_prjMan.m_projectPath + "\\" + m_prjMan.m_projectName + "\\" + m_prjMan.m_projectName + ".mxd";
                    if (File.Exists(mxdFilePath))
                    {
                        if (m_mapControl.CheckMxFile(mxdFilePath))
                        {
                            m_mapControl.LoadMxFile(mxdFilePath);

                            //将数据载入pMapDocument并与map控件联系起来
                            //MapDocumentClass mapDoc = new MapDocumentClass();
                            //mapDoc.Open(mxdFilePath, "");
                            ////IMapDocument对象中可能有多个Map对象，遍历每个map对象
                            //for (int i = 0; i < mapDoc.MapCount; i++)
                            //{
                            //    m_mapControl.Map = mapDoc.get_Map(i);
                            //}
                            //刷新地图
                            m_mapControl.Refresh();
                        }
                    }
                    MessageBox.Show("工程打开成功！");
                }
                else
                {
 
                }

            }
        }

        //保存工程
        private void saveProject_Click(object sender, EventArgs e)
        {

            m_prjMan.WriteProjectFile();
            string mxdFileName = m_prjMan.m_projectPath + "\\" + m_prjMan.m_projectName + "\\" + m_prjMan.m_projectName + ".mxd";

            IMxdContents pMxdC;
            pMxdC = m_mapControl.Map as IMxdContents;
            
            IMapDocument pMapDocument = new MapDocumentClass();
            if (!File.Exists(mxdFileName))
            {
                pMapDocument.New(mxdFileName);
            }
            else
            {
                pMapDocument.Open(mxdFileName);
            }
            //IActiveView pActiveView = m_mapControl.ActiveView;
            //pMapDocument.SetActiveView(pActiveView);
            pMapDocument.ReplaceContents(pMxdC);
            pMapDocument.Save(true, true);
            pMapDocument.Close();
        }

        //退出
        private void exitProject_Click(object sender, EventArgs e)
        {
            saveProject_Click(sender, e);
            this.Close();
        }

        #endregion

        #region 外业前数据准备
        #region 导入数据
        //导入监测图斑图层
        private void importMonitorShape_Click(object sender, EventArgs e)
        {
            ICommand command = new ControlsAddDataCommandClass();
            command.OnCreate(m_mapControl);
            command.OnClick();

            int newAddedNum = m_mapControl.LayerCount - m_currentLayerNum;

            if (newAddedNum >= 1)
            {
                ILayer pLayer = null;
                for (int i = 0; i < newAddedNum; i++)
                {
                    pLayer = m_mapControl.get_Layer(i);
                    pLayer.Name = "监测图斑" + (m_monitorLayers.Count+ 1).ToString();
                    m_monitorLayers.Add(pLayer);
                }
                pLayer = null;
                this.axTOCControl1.Update();
                m_currentLayerNum = m_mapControl.LayerCount;
            }

        }
        //导入基本农田数据
        private void importBasicNTData_Click(object sender, EventArgs e)
        {
            ICommand command = new ControlsAddDataCommandClass();
            command.OnCreate(m_mapControl);
            command.OnClick();

            int newAddedNum = m_mapControl.LayerCount - m_currentLayerNum;

            if (newAddedNum >= 1)
            {
                ILayer pLayer = null;
                for (int i = 0; i < newAddedNum; i++)
                {
                    pLayer = m_mapControl.get_Layer(i);
                    pLayer.Name = "基本农田" + (m_jbntLayers.Count + 1).ToString();
                    m_jbntLayers.Add(pLayer);
                }
                pLayer = null;
                this.axTOCControl1.Update();
                m_currentLayerNum = m_mapControl.LayerCount;
            }
        }
        //导入前时相卫片
        private void importPreImg_Click(object sender, EventArgs e)
        {
            ICommand command = new ControlsAddDataCommandClass();
            command.OnCreate(m_mapControl);
            command.OnClick();

            int newAddedNum = m_mapControl.LayerCount - m_currentLayerNum;

            if (newAddedNum >= 1)
            {
                ILayer pLayer = null;
                for (int i = 0; i < newAddedNum; i++)
                {
                    pLayer = m_mapControl.get_Layer(i);
                    pLayer.Name = "前时相卫片" + (m_preImgLayers.Count + 1).ToString();
                    m_preImgLayers.Add(pLayer);
                }
                pLayer = null;
                this.axTOCControl1.Update();
                m_currentLayerNum = m_mapControl.LayerCount;
            }
        }
        //导入后时相卫片
        private void importPostImg_Click(object sender, EventArgs e)
        {
            ICommand command = new ControlsAddDataCommandClass();
            command.OnCreate(m_mapControl);
            command.OnClick();

            int newAddedNum = m_mapControl.LayerCount - m_currentLayerNum;

            if (newAddedNum >= 1)
            {
                ILayer pLayer = null;
                for (int i = 0; i < newAddedNum; i++)
                {
                    pLayer = m_mapControl.get_Layer(i);
                    pLayer.Name = "后时相卫片" + (m_postImgLayers.Count + 1).ToString();
                    m_postImgLayers.Add(pLayer);
                }
                pLayer = null;
                this.axTOCControl1.Update();
                m_currentLayerNum = m_mapControl.LayerCount;
            }
        }
        //导入规划红线
        private void importRedPlanLine_Click(object sender, EventArgs e)
        {
            ICommand command = new ControlsAddDataCommandClass();
            command.OnCreate(m_mapControl);
            command.OnClick();

            int newAddedNum = m_mapControl.LayerCount - m_currentLayerNum;

            if (newAddedNum >= 1)
            {
                ILayer pLayer = null;
                for (int i = 0; i < newAddedNum; i++)
                {
                    pLayer = m_mapControl.get_Layer(i);
                    pLayer.Name = "规划红线" + (m_redLineLayers.Count + 1).ToString();
                    m_redLineLayers.Add(pLayer);
                }
                pLayer = null;
                this.axTOCControl1.Update();
                m_currentLayerNum = m_mapControl.LayerCount;
            }
        }
        //导入乡镇名称
        private void importTownName_Click(object sender, EventArgs e)
        {

        }
        //导入用地审批数据
        private void importYDSPData_Click(object sender, EventArgs e)
        {
            ICommand command = new ControlsAddDataCommandClass();
            command.OnCreate(m_mapControl);
            command.OnClick();

            int newAddedNum = m_mapControl.LayerCount - m_currentLayerNum;

            if (newAddedNum >= 1)
            {
                ILayer pLayer = null;
                for (int i = 0; i < newAddedNum; i++)
                {
                    pLayer = m_mapControl.get_Layer(i);
                    pLayer.Name = "用地审批" + (m_ydspLayers.Count + 1).ToString();
                    m_ydspLayers.Add(m_mapControl.get_Layer(i));
                }
                pLayer = null;
                this.axTOCControl1.Update();
                m_currentLayerNum = m_mapControl.LayerCount;
            }
        }
        #region 导入基础库
        //导入地类图斑图层
        private void importDLTB_Click(object sender, EventArgs e)
        {

            ICommand command = new ControlsAddDataCommandClass();
            command.OnCreate(m_mapControl);
            command.OnClick();

            int newAddedNum = m_mapControl.LayerCount - m_currentLayerNum;
            if (newAddedNum == 0)
            {
                MessageBox.Show("未导入地类图斑！");
            }
            else if (newAddedNum == 1)
            {
                m_dltbLayer = m_mapControl.get_Layer(0);
                m_dltbLayer.Name = "地类图斑";
                m_currentLayerNum = m_mapControl.LayerCount;
                this.axTOCControl1.Update();
            }
            else if (newAddedNum > 1)
            {
                for (int i = 0; i < newAddedNum; i++)
                {
                    m_mapControl.DeleteLayer(0);
                }
                MessageBox.Show("地类图斑只能导入一层！请重新导入！");
            }
        }
        //导入行政区图层
        private void importXZQ_Click(object sender, EventArgs e)
        {
            ICommand command = new ControlsAddDataCommandClass();
            command.OnCreate(m_mapControl);
            command.OnClick();

            int newAddedNum = m_mapControl.LayerCount - m_currentLayerNum;
            if (newAddedNum == 0)
            {
                MessageBox.Show("未导入行政区！");
            }
            else if (newAddedNum == 1)
            {
                m_xzqLayer = m_mapControl.get_Layer(0);
                m_xzqLayer.Name = "行政区";
                m_currentLayerNum = m_mapControl.LayerCount;
                this.axTOCControl1.Update();
            }
            else if (newAddedNum > 1)
            {
                for (int i = 0; i < newAddedNum; i++)
                {
                    //m_mapControl.DeleteLayer(m_mapControl.LayerCount - i - 1);//这种写法明显错误，m_mapControl.LayerCount在DeleteLayer的时候会变化的
                    m_mapControl.DeleteLayer(0);//循环删除最上层就好了
                }
                MessageBox.Show("行政区只能导入一层！请重新导入！");
            }
            else
                return;
        }
        //导入零星地物
        private void importLXDW_Click(object sender, EventArgs e)
        {
            ICommand command = new ControlsAddDataCommandClass();
            command.OnCreate(m_mapControl);
            command.OnClick();

            int newAddedNum = m_mapControl.LayerCount - m_currentLayerNum;
            if (newAddedNum == 0)
            {
                MessageBox.Show("未导入零星地物！");
            }
            else if (newAddedNum == 1)
            {
                m_lxdwLayer = m_mapControl.get_Layer(0);
                m_lxdwLayer.Name = "零星地物";
                m_currentLayerNum = m_mapControl.LayerCount;
                this.axTOCControl1.Update();
            }
            else if (newAddedNum > 1)
            {
                for (int i = 0; i < newAddedNum; i++)
                {
                    //m_mapControl.DeleteLayer(m_mapControl.LayerCount - i - 1);
                    m_mapControl.DeleteLayer(0);//循环删除最上层就好了
                }
                MessageBox.Show("零星地物只能导入一层！请重新导入！");
            }
            else
            {
                MessageBox.Show("零星地物导入失败！请重新导入！");
                return;
            }

        }
        //导入线状地物
        private void importXZDW_Click(object sender, EventArgs e)
        {
            ICommand command = new ControlsAddDataCommandClass();
            command.OnCreate(m_mapControl);
            command.OnClick();

            int newAddedNum = m_mapControl.LayerCount - m_currentLayerNum;
            if (newAddedNum == 0)
            {
                MessageBox.Show("未导入线状地物！");
            }
            else if (newAddedNum == 1)
            {
                m_xzdwLayer = m_mapControl.get_Layer(0);
                m_xzdwLayer.Name = "线状地物";
                m_currentLayerNum = m_mapControl.LayerCount;
                this.axTOCControl1.Update();
            }
            else if (newAddedNum > 1)
            {
                for (int i = 0; i < newAddedNum; i++)
                {
                    //m_mapControl.DeleteLayer(m_mapControl.LayerCount - i - 1);
                    m_mapControl.DeleteLayer(0);//循环删除最上层就好了
                }
                MessageBox.Show("线状地物只能导入一层！请重新导入！");
            }
            else
            {
                MessageBox.Show("线状地物导入失败！请重新导入！");
                return;
            }

        }
        #endregion
        #endregion
        //统计计算
        private void jctbStatics_Click(object sender, EventArgs e)
        {
            //1、数据完备性判断，必选图层为JCTB和DLTB，其他不是必选
            if (m_monitorLayers.Count <= 0)
            {
                MessageBox.Show("请导入监测图斑图层！");
                return;
            }
            if (m_dltbLayer == null)
            {
                MessageBox.Show("请导入基础库中的DLTB图层，否则无法进行统计计算！");
                return;
            }
            //2、数据准备，拷贝原始数据到BASEDATA
            m_monitorEnv = Function.getLayersExtent(m_monitorLayers);
            m_lyerEnvelop = new ArrayList();
            m_lyerEnvelop.Add(m_monitorEnv);

            IFeatureClass monitorSumFC = Function.MergeFeatureClasses(m_monitorLayers);
            if (!Function.AddSelectedFileds2Layer(ref monitorSumFC))
            {
                MessageBox.Show("为监测图斑图层添加字段失败！");
                return;
            }
            string shpPath = m_prjMan.m_projectPath + "\\" + m_prjMan.m_projectName + "\\" + ProjectManage.m_baseData;// +"\\" + "jctb.shp";
            Function.saveFeatureClass(monitorSumFC, shpPath);
            string shpSrcPath = shpPath + "\\" + "GPL0.shp";
            string newName = shpPath + "\\" + "jctb_OLD.shp";
            Function.reNameShpFile(shpSrcPath, newName);


            if (!CopyLayerToBaseDataFolder(m_dltbLayer, m_dltbEnv, shpPath))//dltb
            {
                MessageBox.Show("数据拷贝失败，无法进行统计计算！");
                return;
            }
            else
            {
                newName = shpPath + "\\" + "dltb.shp";
                Function.reNameShpFile(shpSrcPath, newName);
            }
            if (!CopyLayerToBaseDataFolder(m_lxdwLayer, m_lxdwEnv, shpPath))//lxdw
            {
                MessageBox.Show("数据拷贝失败，无法进行统计计算！");
                return;
            }
            else
            {
                newName = shpPath + "\\" + "lxdw.shp";
                Function.reNameShpFile(shpSrcPath, newName);
            }
            if (!CopyLayerToBaseDataFolder(m_xzdwLayer, m_xzdwEnv, shpPath))//xzdw
            {
                MessageBox.Show("数据拷贝失败，无法进行统计计算！");
                return;
            }
            else
            {
                newName = shpPath + "\\" + "xzdw.shp";
                Function.reNameShpFile(shpSrcPath, newName);
            }
            if (!CopyLayerToBaseDataFolder(m_xzqLayer, m_xzqEnv, shpPath))//xzq
            {
                MessageBox.Show("数据拷贝失败，无法进行统计计算！");
                return;
            }
            else
            {
                newName = shpPath + "\\" + "xzq.shp";
                Function.reNameShpFile(shpSrcPath, newName);
            }
            if (!CopyLayerToBaseDataFolder(m_jbntLayers, m_jbntEnv, shpPath))//jbnt
            {
                MessageBox.Show("数据拷贝失败，无法进行统计计算！");
                return;
            }
            else
            {
                newName = shpPath + "\\" + "jbnt.shp";
                Function.reNameShpFile(shpSrcPath, newName);
            }
            if (!CopyLayerToBaseDataFolder(m_ydspLayers, m_ydspEnv, shpPath))//YDSP
            {
                MessageBox.Show("数据拷贝失败，无法进行统计计算！");
                return;
            }
            else
            {
                newName = shpPath + "\\" + "spsj.shp";
                Function.reNameShpFile(shpSrcPath, newName);
            }
            if (!CopyLayerToBaseDataFolder(m_redLineLayers, m_redLineEnv, shpPath))//GHHX
            {
                MessageBox.Show("数据拷贝失败，无法进行统计计算！");
                return;
            }
            else
            {
                newName = shpPath + "\\" + "RedLine.shp";
                Function.reNameShpFile(shpSrcPath, newName);
            }

            Function.CalJCTBStatistic(monitorSumFC, m_dltbLayer as IFeatureClass, m_lxdwLayer as IFeatureClass, m_xzdwLayer as IFeatureClass);
            Function.SC_JCTBandXZQ(monitorSumFC, (m_xzqLayer as IFeatureLayer).FeatureClass);
            Function.SC_JCTBandJBNT(monitorSumFC, Function.MergeFeatureClasses(m_jbntLayers));
            Function.SC_JCTBandYDSP(monitorSumFC, Function.MergeFeatureClasses(m_ydspLayers));

            Function.saveFeatureClass(monitorSumFC, shpPath);

            newName = shpPath + "\\" + "jctb_new.shp";
            Function.reNameShpFile(shpSrcPath, newName);


            //1合并监测图层，以及其他可合并的图层

            //2：将字段加入监测图层
            //3统计计算各字段值
            //4将文件存储入basedata
            //5将各值更新
            //6显示计算结果


        }
        //生成移动端数据
        private void generateMobileData_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 统计计算前数据准备，把DLTB\XZDW\LXDW\XZQ\GHHX\JBNT\YDSP等数据拷贝到BASEDATA文件夹
        /// </summary>
        /// <param name="srLayer">原始layer</param>
        /// <param name="srEnvelope">原始layer的Envelope</param>
        /// <param name="outputPathName">输出路径</param>
        /// <returns></returns>
        private bool CopyLayerToBaseDataFolder(ILayer srLayer, IEnvelope srEnvelope, string outputPathName )
        {
            try
            {
                if (srLayer != null)
                {
                    ArrayList pArrayList = new ArrayList();
                    pArrayList.Add(srLayer);
                    srEnvelope = Function.getLayersExtent(pArrayList);

                    IFeatureLayer pFeatLyr = srLayer as IFeatureLayer;
                    IFeatureClass pFeatCls = pFeatLyr.FeatureClass;
                    Function.saveFeatureClass(pFeatCls, outputPathName);
                }
                else
                {
                    srEnvelope = new EnvelopeClass();
                    srEnvelope.SetEmpty();
                }
                m_lyerEnvelop.Add(srEnvelope);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool CopyLayerToBaseDataFolder(ArrayList srArrayList, IEnvelope srEnvelope, string outputPathName)
        {
            try
            {
                if (srArrayList.Count >= 0)
                {
                    srEnvelope = Function.getLayersExtent(srArrayList);

                    IFeatureClass pFeatCls = Function.MergeFeatureClasses(srArrayList);
                    Function.saveFeatureClass(pFeatCls, outputPathName);
                }
                else
                {
                    srEnvelope = new EnvelopeClass();
                    srEnvelope.SetEmpty();
                }
                m_lyerEnvelop.Add(srEnvelope);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //调用MergeGP 将图层合并
        private void MergeLayers(ArrayList srArrayList, string outputPathName)
        {
        }
        #endregion

    }
    
}
