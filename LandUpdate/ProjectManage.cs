
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace LandUpdate
{
    
    public class ProjectManage
    {

        public const string m_projectFileVersion = "TopSkyVersion1.0";
        public const string m_projectExt = ".bgw";
        public const string m_baseData = "BaseData";
        public const string m_afterF = "afterField";
        public const string m_afterBG = "afterBG";
        public const string m_mobile = "mobile";
        public const string m_mobileVector = "Vector";
        public const string m_mobileRaster = "Raster";
        public const string m_shpString = ".shp";

        public const string MonitorShapeNum = "MonitorShapeNum";
        public const string ghhxShpNum = "ghhxShpNum";
        public const string spsjShpNum = "spsjShpNum";
        public const string jbntShpNum = "jbntShpNum";
        public const string MonitorShapeTotalArea = "MonitorShapeTotalArea";
        public const string TotalGengDiZhanyong = "TotalGengDiZhanyong";
        public const string TotalNongtianZhanyong = "TotalNongtianZhanyong";
        public const string TotalGuihuashenpi = "TotalGuihuashenpi";
        public const string TotalJianSheYongDi = "TotalJianSheYongDi";
        public const string TotalTBWeiFaMianji = "TotalTBWeiFaMianji";



        const string m_monitorShapeFileName = "jctb";   //监测图斑内部文件名
        const string m_LandTypeFileName = "LandType";   //地类图斑内部名称
        const string m_planRedLineFileName = "PlanRLine";   //规划红线
        const string m_GovAreaFileName = "GovArea";     //行政区
        const string m_baseFarmFileName = "BaseFarm";   //基本农田
        const string m_useAllowedFileName = "UseAllowed";   //用地审批
        const string m_fewObjFileName = "FewObj";   //零星地物
        const string m_lineObjFileName = "LineObj";   //线状地物

        public ArrayList m_preAbsoluteImgPath;//前时相img影像全路径
        public ArrayList m_postAbsoluteImgPath;//后时相img影像全路径

        public int m_MonitorShapeNum;
        public int m_ghhxShpNum;
        public int m_spsjShpNum;
        public int m_jbntShpNum;

        public double m_MonitorShapeTotalArea;
        public double m_TotalGengDiZhanyong;
        public double m_TotalNongtianZhanyong;
        public double m_TotalGuihuashenpi;
        public double m_TotalJianSheYongDi;
        public double m_TotalTBWeiFaMianji;

        public string m_projectName;    //工程名字
        public string m_projectPath;    //工程路径
        public int m_layerNum;          //图层数量

        public enum luShpType
        {
            jctbType = 0,       //监测图斑
            ghhxType = 1,       //规划红线
            lxdwType = 2,       //零星地物
            xzdwtype = 3,       //线状地物
            jbntType = 4,       //基本农田
            spsjType = 5,       //审批数据

        }

        public int GetSameFileNum(luShpType lyrType)
        {
            string baseDataPath = m_projectPath + m_projectName  +"//" + m_projectName + "//" + m_baseData + "//";
            if(lyrType == luShpType.jctbType)
            {
                for(int i=1; ; i++)
                {
                    if (!File.Exists(baseDataPath + m_monitorShapeFileName + i.ToString() + ".shp"))
                        return i;
                }
            }
            else if (lyrType == luShpType.ghhxType)
            {
                for (int i = 1; ; i++)
                {
                    if (!File.Exists(baseDataPath + m_planRedLineFileName + i.ToString() + ".shp"))
                        return i;
                }
            }
            else if (lyrType == luShpType.jbntType)
            {
                for (int i = 1; ; i++)
                {
                    if (!File.Exists(baseDataPath + m_baseFarmFileName + i.ToString() + ".shp"))
                        return i;
                }
            }
            else if (lyrType == luShpType.spsjType)
            {
                for (int i = 1; ; i++)
                {
                    if (!File.Exists(baseDataPath + m_useAllowedFileName + i.ToString() + ".shp"))
                        return i;
                }
            }
            else
            {
                return -1;
            }          

        }

        public bool DirectCopySHP(string srcShpfilePathName, string dstFilePathName)
        {

            int dotIndex = srcShpfilePathName.LastIndexOf(".");
            string srcFileName = srcShpfilePathName.Substring(0, dotIndex);
            dotIndex = dstFilePathName.LastIndexOf(".");
            string dstFileName = dstFilePathName.Substring(0, dotIndex);

            if (File.Exists(srcShpfilePathName))
                File.Copy(srcShpfilePathName, dstFilePathName);
            else { MessageBox.Show("shp文件不存在！"); return false; }
            
            string tmpsrcFileName = srcFileName + ".dbf";
            string tmpDstFileName = dstFileName + ".dbf";

            if (File.Exists(tmpsrcFileName))
                File.Copy(tmpsrcFileName, tmpDstFileName);
            else { MessageBox.Show("dbf文件不存在！"); return false; }

            tmpsrcFileName = srcFileName + ".shx";
            tmpDstFileName = dstFileName + ".shx";
            if (File.Exists(tmpsrcFileName))
                File.Copy(tmpsrcFileName, tmpDstFileName);
            else { MessageBox.Show("shx文件不存在！"); return false; }

            tmpsrcFileName = srcFileName + ".sbn";
            tmpDstFileName = dstFileName + ".sbn";
            if (File.Exists(tmpsrcFileName))
                File.Copy(tmpsrcFileName, tmpDstFileName);
            else { MessageBox.Show("sbn文件不存在！");}

            tmpsrcFileName = srcFileName + ".sbx";
            tmpDstFileName = dstFileName + ".sbx";
            if (File.Exists(tmpsrcFileName))
                File.Copy(tmpsrcFileName, tmpDstFileName);
            else { MessageBox.Show("sbx文件不存在！"); }

            tmpsrcFileName = srcFileName + ".shp.xml";
            tmpDstFileName = dstFileName + ".shp.xml";
            if (File.Exists(tmpsrcFileName))
                File.Copy(tmpsrcFileName, tmpDstFileName);
            else { MessageBox.Show("shp.xml文件不存在！"); }

            tmpsrcFileName = srcFileName + ".prj";
            tmpDstFileName = dstFileName + ".prj";
            if (File.Exists(tmpsrcFileName))
                File.Copy(tmpsrcFileName, tmpDstFileName);
            else { MessageBox.Show("prj文件不存在！"); }
            
            return true;
            
        }

        public bool SHPFileCopy(string srcShpfilePathName, string dstFilePath, luShpType lyrType)
        {
            string tmpDstFileName;
            if (lyrType == luShpType.jctbType)
            {
                tmpDstFileName = dstFilePath + m_monitorShapeFileName + GetSameFileNum(lyrType).ToString() + ".shp";
                return DirectCopySHP(srcShpfilePathName, tmpDstFileName);                
            }
            else if (lyrType == luShpType.ghhxType)
            {
                tmpDstFileName = dstFilePath + m_planRedLineFileName + GetSameFileNum(lyrType).ToString() + ".shp";
                return DirectCopySHP(srcShpfilePathName, tmpDstFileName);
            }
            else if (lyrType == luShpType.jbntType)
            {
                tmpDstFileName = dstFilePath + m_baseFarmFileName + GetSameFileNum(lyrType).ToString() + ".shp";
                return DirectCopySHP(srcShpfilePathName, tmpDstFileName);
            }

            else if (lyrType == luShpType.spsjType)
            {
                tmpDstFileName = dstFilePath + m_useAllowedFileName + GetSameFileNum(lyrType).ToString() + ".shp";
                return DirectCopySHP(srcShpfilePathName, tmpDstFileName);
            }
            else
                return false;
                
        }

        public  bool IsMDBPath(string dataPath)
        {
            if (dataPath.Contains(".mdb") || dataPath.Contains(".MDB"))
                return true;
            return false;
        }

  
        

        public void initPrj()
        {
            m_preAbsoluteImgPath = null;
            m_postAbsoluteImgPath = null;
            m_MonitorShapeNum = 0;
            m_ghhxShpNum = 0;
            m_spsjShpNum = 0;
            m_jbntShpNum = 0;
            m_projectName = "";    //工程名字
            m_projectPath = "";    //工程路径

            m_layerNum = 0;
            m_MonitorShapeNum = 0;
            m_MonitorShapeTotalArea = 0;
            m_TotalGengDiZhanyong = 0;
            m_TotalNongtianZhanyong = 0;
            m_TotalGuihuashenpi = 0;
            m_TotalJianSheYongDi = 0;
            m_TotalTBWeiFaMianji = 0;
        }

        public bool CreatePrjPaths()
        {
            string projectFullPath = m_projectPath +"\\"+ m_projectName;
            if (projectFullPath.Trim() != String.Empty)
            {
                if (System.IO.Directory.Exists(projectFullPath))
                {
                    MessageBox.Show("该工程已经存在，继续创建将覆盖之前的工程。");
                    return false;
                }
                else
                {
                    System.IO.Directory.CreateDirectory(projectFullPath);
                    //System.IO.Directory.CreateDirectory(projectFullPath + "\\" + m_projectName + "\\" + m_baseData);
                    //System.IO.Directory.CreateDirectory(projectFullPath + "\\" + m_projectName + "\\" + m_mobile);
                    System.IO.Directory.CreateDirectory(projectFullPath + "\\" + m_baseData);
                    System.IO.Directory.CreateDirectory(projectFullPath + "\\" + m_mobile);
                    return true;
                }
            }
            return false;

        }
        
        public  bool WriteProjectFile()
        {
            string projectFullName = m_projectPath+"\\" + m_projectName + "\\" + m_projectName+ m_projectExt;               
            //System.IO.StreamWriter file = new System.IO.StreamWriter(projectFullName);

            //文件打开后不能保存？
            System.IO.FileStream prjFile = new System.IO.FileStream(projectFullName, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(prjFile);            
            // Write file version
            sw.WriteLine(m_projectFileVersion);

            sw.WriteLine(MonitorShapeNum);
            sw.WriteLine(m_MonitorShapeNum.ToString());       

             sw.WriteLine(ghhxShpNum);
             sw.WriteLine(m_ghhxShpNum.ToString());   

             sw.WriteLine(jbntShpNum);
             sw.WriteLine(m_jbntShpNum.ToString());

             sw.WriteLine(spsjShpNum);
             sw.WriteLine(m_spsjShpNum.ToString());               

            sw.WriteLine(MonitorShapeTotalArea);
            sw.WriteLine(m_MonitorShapeTotalArea.ToString());

            sw.WriteLine(TotalGengDiZhanyong);
            sw.WriteLine(m_TotalGengDiZhanyong.ToString());

            sw.WriteLine(TotalNongtianZhanyong);
            sw.WriteLine(m_TotalNongtianZhanyong.ToString());

            sw.WriteLine(TotalGuihuashenpi);
            sw.WriteLine(m_TotalGuihuashenpi.ToString());

            sw.WriteLine(TotalJianSheYongDi);
            sw.WriteLine(m_TotalJianSheYongDi.ToString());


            sw.WriteLine(TotalTBWeiFaMianji);
            sw.WriteLine(m_TotalTBWeiFaMianji.ToString());      

            sw.Close();
            prjFile.Close();
            return true;
       
         }



        public bool LoadProject(string projFileName)
        {
            m_projectName = System.IO.Path.GetFileNameWithoutExtension(projFileName);
            m_projectPath = System.IO.Path.GetDirectoryName(projFileName);
            int a = m_projectPath.LastIndexOf("\\");
            m_projectPath = m_projectPath.Substring(0, a);
            //m_projectPath += "\\";
            
            
            //string projectFullName = m_projectPath + m_projectName + m_projectExt;
            //一个工程打开两遍，下面的语句会报错IOException
            System.IO.FileStream prjFile = null;
            try
            {
                prjFile = new System.IO.FileStream(projFileName, FileMode.OpenOrCreate);
            }
            catch
            {
                MessageBox.Show("该工程已经被打开！");
                return false;
            }
            StreamReader sr = new StreamReader(prjFile);

            string tmp = sr.ReadLine();
            if (tmp == m_projectFileVersion)
            {
                tmp = sr.ReadLine();
                tmp = sr.ReadLine();
                m_MonitorShapeNum = Convert.ToInt32(tmp);

                tmp = sr.ReadLine();
                tmp = sr.ReadLine();
                m_ghhxShpNum = Convert.ToInt32(tmp);

                tmp = sr.ReadLine();
                tmp = sr.ReadLine();
                m_jbntShpNum = Convert.ToInt32(tmp);

                tmp = sr.ReadLine();
                tmp = sr.ReadLine();
                m_spsjShpNum = Convert.ToInt32(tmp);

                tmp = sr.ReadLine();
                tmp = sr.ReadLine();                
                m_MonitorShapeTotalArea = Convert.ToDouble(tmp);

                tmp = sr.ReadLine();
                tmp = sr.ReadLine();
                m_TotalGengDiZhanyong = Convert.ToDouble(tmp);

                tmp = sr.ReadLine();
                tmp = sr.ReadLine();
                m_TotalNongtianZhanyong = Convert.ToDouble(tmp);

                tmp = sr.ReadLine();
                tmp = sr.ReadLine();
                m_TotalGuihuashenpi = Convert.ToDouble(tmp);

                tmp = sr.ReadLine();
                tmp = sr.ReadLine();
                m_TotalJianSheYongDi = Convert.ToDouble(tmp);

                tmp = sr.ReadLine();
                tmp = sr.ReadLine();
                m_TotalTBWeiFaMianji = Convert.ToDouble(tmp);

                sr.Close();
                prjFile.Close();
                //MessageBox.Show("打开工程成功！");
                return true;
            }
            else
            {
                sr.Close();
                prjFile.Close();
                MessageBox.Show("该工程已经被破坏！");
                
                return false;
            }
            sr.Close();
            prjFile.Close();
            return false;
        }

        
       
        public bool saveProject(string path)
        {
            return false;
 
        }
        
        //lyrExist ,依次：lxdw, xzsw, jctb, xzq, dltb, jbnt,redline 
        public string formCorrdString(IEnvelope env)
        {                        
            string coordString = env.XMin.ToString() + "," + env.YMin.ToString() + "," + env.XMax.ToString() + "," + env.YMax.ToString();
            return coordString;
 
        }

        public bool WriteMapConfigFile(string mapConfigFilePath, ArrayList lyrExist)
        {
            string mobilePath = m_projectPath + m_projectName + "\\" + m_projectName + "\\" + m_mobile;
            string projectFullName = m_projectPath + m_projectName + "\\" + m_projectName + "\\" + m_baseData + "\\" + "mapConfig.ini";
            //System.IO.StreamWriter file = new System.IO.StreamWriter(projectFullName);
            //string dataPath = "";
            string basePath = m_projectPath + m_projectName + "\\" + m_projectName + "\\" + m_baseData + "\\";
            string fullString = "";
            System.IO.FileStream prjFile = new System.IO.FileStream(projectFullName, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(projectFullName);
            // Write file version
            sw.WriteLine("[UCMap]");

            sw.WriteLine(mobilePath);
            int layerNum = 0;
            for (int i = 0; i < lyrExist.Count; i++)
            {
                IEnvelope tmpEnv = lyrExist[i] as IEnvelope;
                if (!tmpEnv.IsEmpty)
                    layerNum++;
            }
            sw.WriteLine(layerNum.ToString());

            IEnvelope tmpMapEnv = lyrExist[0] as IEnvelope;    //jctb
            string coordString = formCorrdString(tmpMapEnv);

            fullString = basePath + "\\" + "jctb.shp" + "," + "null" + "," + "1" + coordString;

            sw.WriteLine(fullString);


            tmpMapEnv = lyrExist[1] as IEnvelope;  //dltb
            if (!tmpMapEnv.IsEmpty) ;

            {
                coordString = formCorrdString(tmpMapEnv);
                fullString = basePath + "\\" + "dltb.shp" + "," + "null" + "," + "1" + coordString;
                sw.WriteLine(fullString);
            }

            tmpMapEnv = lyrExist[2] as IEnvelope;  // m_lxdwLayer
            if (!tmpMapEnv.IsEmpty) ;

            {
                coordString = formCorrdString(tmpMapEnv);
                fullString = basePath + "\\" + "lxdw.shp" + "," + "null" + "," + "1" + coordString;
                sw.WriteLine(fullString);
            }

            tmpMapEnv = lyrExist[3] as IEnvelope;  // m_xzqLayer
            if (!tmpMapEnv.IsEmpty) ;

            {
                coordString = formCorrdString(tmpMapEnv);
                fullString = basePath + "\\" + "xzq.shp" + "," + "null" + "," + "1" + coordString;
                sw.WriteLine(fullString);
            }

            tmpMapEnv = lyrExist[4] as IEnvelope;  // m_xzqLayer
            if (!tmpMapEnv.IsEmpty) ; //  m_jbntEnv

            {
                coordString = formCorrdString(tmpMapEnv);
                fullString = basePath + "\\" + "jbnt.shp" + "," + "null" + "," + "1" + coordString;
                sw.WriteLine(fullString);
            }

            tmpMapEnv = lyrExist[5] as IEnvelope;  // m_xzqLayer
            if (!tmpMapEnv.IsEmpty) ; //  m_ydspLayers

            {
                coordString = formCorrdString(tmpMapEnv);
                fullString = basePath + "\\" + "ydsp.shp" + "," + "null" + "," + "1" + coordString;
                sw.WriteLine(fullString);
            }


            tmpMapEnv = lyrExist[6] as IEnvelope;  // m_xzqLayer
            if (!tmpMapEnv.IsEmpty) ; //  m_redLineLayers

            {
                coordString = formCorrdString(tmpMapEnv);
                fullString = basePath + "\\" + "redLine.shp" + "," + "null" + "," + "1" + coordString;
                sw.WriteLine(fullString);
            }
            
            sw.Close();
            return true;


        }
    }
}
