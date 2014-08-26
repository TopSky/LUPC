using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.ArcMapUI;  
using ESRI.ArcGIS.Catalog;  
using ESRI.ArcGIS.DataSourcesFile;

using ESRI.ArcGIS.ConversionTools;         //添加引用
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.AnalysisTools;
using ESRI.ArcGIS.DataManagementTools;


  

namespace LandUpdate
{
   public enum luLandType
    {
        // 摘要:
        //     gengdi
        luGengdi = 0,
        //
        // 摘要:
        //     weiliyongdi
        luWeiliyong = 1,
        //
        // 摘要:
        //     jiansheyongdi.
        luJiansheyong = 2,
        //
        luOtherNongyong = 3,

        luOthers = 4,
        
    }
    

    public class Function
    {
        //static const string XZBH = "XZBH";
        //static const string GDZY = "GDZY";
        //static const string GDMJBL = "GDMJBL";
        //static const string WLYDZY = "WLYDZY";
        //static const string JSYDZY = "JSYDZY";
        //static const string JYBGMJ = "JYBGMJ";
        //static const string SDBGMJ = "SDBGMJ";
        //static const string JBNTZY = "JBNTZY";
        //static const string YDSPCH = "YDSPCH";
        //static const string BGDLLB = "BGDLLB";
        //static const string WYSM = "WYSM";
        //static const string TBWFMJ = "TBWFMJ";
        //static const string TBSPGDZY = "TBSPGDZY";
        //static const string TBWFGD = "TBWFGD";
        //static const string TBZYSP = "TBZYSP";
        //static const string BGHTBBHLB = "BGHTBBHLB";
        //static const string LDPS = "LDPS";
        const string XZBH = "XZBH";
        const string GDZY = "GDZY";
        const string GDMJBL = "GDMJBL";
        const string WLYDZY = "WLYDZY";
        const string JSYDZY = "JSYDZY";
        const string JYBGMJ = "JYBGMJ";
        const string SDBGMJ = "SDBGMJ";
        const string JBNTZY = "JBNTZY";
        const string YDSPCH = "YDSPCH";
        const string BGDLLB = "BGDLLB";
        const string YYLX = "YYLX";
        const string WYSM = "WYSM";
        const string TBWFMJ = "TBWFMJ";
        const string TBSPGDZY = "TBSPGDZY";
        const string TBWFGD = "TBWFGD";
        const string TBZYSP = "TBZYSP";
        const string BGHTBBHLB = "BGHTBBHLB";
        const string LDPS = "LDPS";


        public static string GetCurShpName(string baseDataPath)
        {
            return "";
        }

        public static bool reNameShpFile(string shpSrcPath, string newName)
        {
            int index1 = shpSrcPath.LastIndexOf(".");
            int index2 = newName.LastIndexOf(".");
            string baseNameSrc = shpSrcPath.Substring(0, index1);
            string baseNameDst = newName.Substring(0, index2);

            string shxNameSrc = baseNameSrc + ".shx";
            string dbfNameSrc = baseNameSrc + ".dbf";
            string prjNameSrc = baseNameSrc + ".prj";
            string sbnNameSrc = baseNameSrc + ".sbn";
            string sbxNameSrc = baseNameSrc + ".sbx";
            string shxXmlNameSrc = baseNameSrc + ".shp.xml";

            string shxNameDst = baseNameDst + ".shx";
            string dbfNameDst = baseNameDst + ".dbf";
            string prjNameDst = baseNameDst + ".prj";
            string sbnNameDst = baseNameDst + ".sbn";
            string sbxNameDst = baseNameDst + ".sbx";
            string shxXmlNameDst = baseNameDst + ".shp.xml";


            if (File.Exists(shxNameSrc))
                File.Move(shxNameSrc, shxNameDst);

            if (File.Exists(dbfNameSrc))
                File.Move(dbfNameSrc, dbfNameDst);

            if (File.Exists(prjNameSrc))
                File.Move(prjNameSrc, prjNameDst);

            if (File.Exists(sbnNameSrc))
                File.Move(sbnNameSrc, sbnNameDst);

            if (File.Exists(sbxNameSrc))
                File.Move(sbxNameSrc, sbxNameDst);

            if (File.Exists(shxXmlNameSrc))
                File.Move(shxXmlNameSrc, shxXmlNameDst);


            if (File.Exists(shpSrcPath))
                File.Move(shpSrcPath, newName);
            return true;
        }



        public static IEnvelope getLayersExtent(ArrayList lyrs)
        {
           // double XMin; double XMax; double YMin; double YMax;
            IEnvelope pExtent = new EnvelopeClass();
            pExtent.SetEmpty();            
            IEnvelope pCurExtent = new EnvelopeClass();
            pCurExtent.SetEmpty();

            if (lyrs.Count == 1)
            {
                IGeoDataset pGeoDataset = (ESRI.ArcGIS.Geodatabase.IGeoDataset)lyrs[0];
                pExtent = pGeoDataset.Extent;
            }
            else if (lyrs.Count > 1)
            {
                for (int i = 0; i < lyrs.Count; i++)
                {
                    IGeoDataset pGeoDataset = (ESRI.ArcGIS.Geodatabase.IGeoDataset)lyrs[i];
                    pCurExtent = pGeoDataset.Extent;
                    pExtent.Union(pCurExtent);
                }
            }
            return pExtent;
        }

        #region//使用insert cursor插入要素
        /// <summary>
        /// 使用insert cursor插入要素
        /// </summary>
        /// <param name="stringWorkspaceOut">目标数据库工作空间</param>
        /// <param name="stringFeatureClassOut">目标数据库的要素类</param>
        /// <param name="stringWorkspaceIn">待合并数据库工作空间</param>
        /// <param name="stringFeatureClassIn">待合并数据库的要素类</param>
        public static IFeatureClass MergeFeatureClasses(ArrayList arrFeatureLyrs)
        {
            
            IFeatureClass featureClassOut1 = (arrFeatureLyrs[0] as IFeatureLayer).FeatureClass;
            if (arrFeatureLyrs.Count == 1)
                return featureClassOut1;

            IObjectCopy pObjectCopy = new ObjectCopyClass();
            //object copyFC = featureClassOut1;
            object preFC = new object();
            //IClone pClone =new
            preFC = pObjectCopy.Copy(arrFeatureLyrs[0]);//对象的深度复制
            //pObjectCopy.Overwrite(copyFC, preFC);//对象的深度复制
            IFeatureClass outFC = (preFC as IFeatureLayer).FeatureClass;

            ArrayList arrError = new ArrayList();
            //IFeatureClass outFC = new FeatureClassClass();


            int count1 = featureClassOut1.FeatureCount(null);
            
             
            
           // IFeatureClass featureClassIn = 
            int intFeatureCount = 0;

            IFeatureCursor featureCursorInsert = outFC.Insert(true);
            IFeatureBuffer featureBufferInsert = outFC.CreateFeatureBuffer();

            // 遍历待合并数据库要素类中的所有要素
            for (int i = 1; i < arrFeatureLyrs.Count; i++)
            {
                IFeatureClass featureClassIn = (arrFeatureLyrs[i] as IFeatureLayer).FeatureClass;
                //count2 = featureClassIn.FeatureCount(null);
                IQueryFilter qf;
                IFeatureCursor featureCursorSearch = featureClassIn.Search(null, true);
                IFeature feature = featureCursorSearch.NextFeature();
                while (feature != null)
                {
                    try
                    {
                        featureBufferInsert.Shape = feature.Shape;
                    }
                    catch
                    {

                        try
                        {
                            if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon || feature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                            {
                                IPointCollection pPointC;
                                IPoint pPoint;
                                pPointC = (IPointCollection)feature.Shape;
                                for (int j = 0; j < pPointC.PointCount; j++)
                                {
                                    pPoint = pPointC.get_Point(j);
                                    pPoint.M = 0;
                                    pPointC.UpdatePoint(j, pPoint);
                                }
                                feature.Shape = (IGeometry)pPointC;
                                featureBufferInsert.Shape = feature.Shape;
                            }
                        }
                        catch
                        {
                            //记录合并失败的要素
                            string strFeatureID = feature.OID.ToString();
                            string strClass = feature.Class.AliasName;
                            arrError.Add(strClass + "-" + strFeatureID + "; ");

                            //指向下一个要素
                            feature = featureCursorSearch.NextFeature();
                            continue;
                        }

                    }
                    AddFields(featureBufferInsert, feature);
                    try
                    {
                        featureCursorInsert.InsertFeature(featureBufferInsert);
                    }
                    catch
                    {
                       // Console.WriteLine(featureClassOut);
                    }

                    // 每添加100个要素刷新一次feature Cursor
                    if (++intFeatureCount == 100)
                    {
                        featureCursorInsert.Flush();
                        intFeatureCount = 0;
                    }

                    // 指向下一个要素
                    feature = featureCursorSearch.NextFeature();
                }

                // 刷新feature Cursor
                featureCursorInsert.Flush();
            }
            return outFC;
        }
        #endregion

        #region//为要素添填充字段
        /// <summary>
        /// 为要素添填充属性
        /// </summary>
        /// <param name="featureBuffer">要素缓存</param>
        /// <param name="feature">待填充的要素</param>
        public static void AddFields(IFeatureBuffer featureBuffer, IFeature feature)
        {
            IRowBuffer rowBuffer = (IRowBuffer)featureBuffer;
            IFields fieldsNew = rowBuffer.Fields;

            int i;
            int intFieldIndex;
            IFields fields = feature.Fields;
            IField field;

            for (i = 0; i < fieldsNew.FieldCount; i++)
            {
                field = fieldsNew.get_Field(i);
                //				if (field.Editable == true && (field.Type != esriFieldType.esriFieldTypeGeometry) && (field.Type != esriFieldType.esriFieldTypeOID) 
                //					&& (field.Name != "SHAPE_Length") && (field.Name != "SHAPE_Area"))
                if (field.Editable == true)
                {
                    intFieldIndex = feature.Fields.FindField(field.Name);
                    if (intFieldIndex != -1)
                    {
                        featureBuffer.set_Value(i, feature.get_Value(intFieldIndex));
                    }
                }

            }
        }
        #endregion



        #region//将监测图斑图层与行政区图层进行叠加，
        /// <summary>
        /// 将村级行政区
        /// </summary>
        /// <param name="stringWorkspaceOut">目标数据库工作空间</param>
        /// <param name="stringFeatureClassOut">目标数据库的要素类</param>
        /// <param name="stringWorkspaceIn">待合并数据库工作空间</param>
        /// <param name="stringFeatureClassIn">待合并数据库的要素类</param>
        public static bool assignXZQ(IFeatureClass jctbFC, IFeatureClass xzqFC)
        {
            return false;
        }
        #endregion
        //public static IFeatureClass merge2TownXZQ(IFeatureClass xzqFC)
        //{
        //    //IFeatureClass townXZFC = new IFeatureClass();
        //    int i;
        //    double totalArea = 0;
        //    string strLayerName;
        //    string resData;

        //    IFeatureCursor fCursor = null;//遍历游标
        //    IFeature xzqFea = null;//监测图斑要素
            
        //    //遍历监测图斑要素类中的所有要素
        //    fCursor = xzqFC.Search(null, false);
        //    if (fCursor != null)
        //    {
        //        xzqFea = fCursor.NextFeature();
        //        while (xzqFea != null)
        //        {

        //        }

        //}
        //将计算出的值填入监测图斑的字段
        //IFeature
        public static bool setStaticsValue(ref IFeature jctbFea, ArrayList dlbmArr, ArrayList areaArr)
        {
            if(dlbmArr.Count != areaArr.Count) {MessageBox.Show("地类编码与面积个数不匹配"); return false;}

            int indexGengdi = jctbFea.Fields.FindField(GDZY);            
            if(indexGengdi <0) {MessageBox.Show("找不到耕地字段！");}
            int indexGDMJBL = jctbFea.Fields.FindField(GDMJBL);
            if(indexGDMJBL <0) {MessageBox.Show("找不到耕地比例字段！");}

            int indexWLYDZY = jctbFea.Fields.FindField(WLYDZY);            
            if(indexWLYDZY <0) {MessageBox.Show("找不到未利用地占用字段！");}
            int indexJSYDZY = jctbFea.Fields.FindField(JSYDZY);
            if(indexJSYDZY <0) {MessageBox.Show("找不到建设用地字段！");}

            int indexJBNTZY = jctbFea.Fields.FindField(JBNTZY);            
            if(indexJBNTZY <0) {MessageBox.Show("找不到基本农田占用字段！");}
            int indexYDSPCH = jctbFea.Fields.FindField(YDSPCH);
            if(indexYDSPCH <0) {MessageBox.Show("找不到用地审批重合面积字段！");}

            int indexTBWFMJ = jctbFea.Fields.FindField(TBWFMJ);            
            if(indexTBWFMJ <0) {MessageBox.Show("找不到图斑违法面积字段！");}
            string tmp; int dlbmInt; luLandType dlType;
           // if (jctbFea.FindField(fieldName) != -1)
            for(int i=0; i<dlbmArr.Count; i++)
            {
                tmp = dlbmArr[i].ToString();
                dlbmInt = Convert.ToInt32(tmp);
                dlType = getLandTypeByDLBM(dlbmInt);
                if(luLandType.luGengdi == dlType)
                {
                    jctbFea.set_Value(indexGengdi, areaArr[i]);
                }
                else if(luLandType.luJiansheyong == dlType)
                {
                    jctbFea.set_Value(indexJSYDZY, areaArr[i]);
                }
                else if(luLandType.luWeiliyong == dlType)
                {
                    jctbFea.set_Value(indexWLYDZY, areaArr[i]); 
                }
                else if(luLandType.luOtherNongyong == dlType)
                {

                }
                else
                {}
            }
            return true;
        }

        public static bool CalJCTBStatistic(IFeatureClass jctbFC, IFeatureClass dltbFC, IFeatureClass lxdw, IFeatureClass xzdw)
        {
            int i;
            double totalArea = 0;
            string strLayerName;
            string resData;

            IFeatureCursor fCursor = null;//遍历游标
            IFeature jctbFea = null;//监测图斑要素
            int jctbCount = jctbFC.FeatureCount(null);

            ArrayList dlbmArr = new ArrayList();
            ArrayList areaArr = new ArrayList();
    
            //遍历监测图斑要素类中的所有要素
            fCursor = jctbFC.Search(null, false);
            if (fCursor != null)
            {
                jctbFea = fCursor.NextFeature();
                while (jctbFea != null)
                {
                    //监测图斑面积累加，计算总面积
                    totalArea += double.Parse(jctbFea.get_Value(jctbFea.Fields.FindField("JCMJ")).ToString());
                    //统计与监测图斑相交的地类图斑类别及面积
                    dlbmArr.Clear(); areaArr.Clear();
                    Function.CalIntersectData(jctbFea, dltbFC, ref dlbmArr, ref areaArr);

                    setStaticsValue(ref jctbFea, dlbmArr, areaArr);
                    
                    jctbFea.Store();
                    jctbFea = fCursor.NextFeature();
                }
                return true;
            }
            return false;
        }       

        //public static void saveFeatureClass(IFeatureClass pFeatureClass, string fileName)  
        //{  
           
        //    try  
        //    {  
        //        string sFileName = System.IO.Path.GetFileName(fileName);  
        //        string sFilePath = System.IO.Path.GetDirectoryName(fileName);  
  
        //        IDataset pDataset = pFeatureClass as IDataset;  
                  
        //        IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactoryClass();  
        //        IFeatureWorkspace pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(sFilePath, 0) as IFeatureWorkspace;  
  
        //        IWorkspace pWorkspace = pFeatureWorkspace as IWorkspace;  
                
        //        pDataset.Copy(sFileName, pFeatureWorkspace as IWorkspace);  
  
        //    }  
        //    catch { MessageBox.Show("错误"); }  
        //}



        public static bool saveFeatureClass(IFeatureClass pInFeatureClass, string outputPathName)
        {

            Geoprocessor gp = new Geoprocessor();
            
            gp.OverwriteOutput = true;                     //允许运算结果覆盖现有文件

            FeatureClassToShapefile convert = new FeatureClassToShapefile(); //定义 convert工具
            //IGpValueTableObject vtobject = new GpValueTableObject();
            //vtobject.SetColumns(2);
            //object obj = pInFeatureClass;
            //vtobject.AddRow(ref obj);
            convert.Input_Features = pInFeatureClass;    //输入对象
            convert.Output_Folder = outputPathName;     //输出对象
           // Export 
            gp.Execute(convert,null);                //执行
            return true;
        }
        //调用Merge工具不成功
        public static bool MergeLayers(ArrayList srArrayList, string strOutputPath)
        {
            //样例是可以执行成功的，但是如果是ArrayList，报错
            Geoprocessor gp = new Geoprocessor();
            gp.OverwriteOutput = true;
            Merge mergeLayersTool = new Merge();
            //string strInput = @"F:\Land\data\jctb\371082荣成市\20130037108201荣成市\371082jctb.shp;F:\Land\data\jctb\371082荣成市\20130037108201荣成市\371082thzd.shp";
            //string strOutput = @"F:\Land\data\jctb\371082荣成市\20130037108201荣成市\Merge\a.shp";

            ////makefeaturelayer,copyfeatures两个工具可以用

            
            IGpValueTableObject vtobject = new GpValueTableObject();
            vtobject.SetColumns(2);
            object[] objArray = new object[srArrayList.Count];
            for (int i = 0; i < srArrayList.Count; i++)
            {
                ILayer plyr = srArrayList[i] as ILayer;
                IFeatureLayer pFeatLyr = plyr as IFeatureLayer;
                object obj = pFeatLyr.FeatureClass;
                objArray[i] = obj;
            }
            for (int i = 0; i < objArray.Length; i++)
            {
                object obj = objArray[i];
                vtobject.AddRow(ref obj);
            }
            
            mergeLayersTool.inputs = vtobject;    //输入对象
            mergeLayersTool.output = strOutputPath;     //输出对象
            // Export 
            try
            {
                gp.Execute(mergeLayersTool, null);                //执行
                return true;
            }
            catch
            {
                return false;
            }
        }
        //用这个Merge可以
        public static IFeatureClass MergeLayers(ArrayList srArrayList,string strOutputPath,string strOutputName)
        {
            ILayer pLayer;
            IFeatureLayer pFeatureLayer;
            IFeatureClass pFeatureClass;
            IWorkspaceName pNewWSName;
            IBasicGeoprocessor pBasicGeop;
            IFeatureClassName pFeatureClassName;
            IDatasetName pDatasetName;
            IFeatureClass pOutputFeatClass;
            IArray pArray;
            ITable pTable;
            //合并图层的集合
            pArray = new ArrayClass();
            for (int i = 0; i < srArrayList.Count; i++)
            {
                pLayer = srArrayList[i] as ILayer;
                pArray.Add(pLayer);
            }
            //定义输出图层的fields表
            pLayer = srArrayList[0] as ILayer;
            pTable = (ITable)pLayer;
            pFeatureLayer = (IFeatureLayer)pLayer;
            pFeatureClass = pFeatureLayer.FeatureClass;
            
            //输出文件类型
            pFeatureClassName = new FeatureClassNameClass();
            pFeatureClassName.FeatureType = esriFeatureType.esriFTSimple;
            pFeatureClassName.ShapeFieldName = "Shape";
            pFeatureClassName.ShapeType = pFeatureClass.ShapeType;
            //输出shapefile的名称和位置
            pNewWSName = new WorkspaceNameClass();
            pNewWSName.WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory";
            pNewWSName.PathName = strOutputPath;
            pDatasetName = (IDatasetName)pFeatureClassName;
            pDatasetName.Name = strOutputName;
            pDatasetName.WorkspaceName = pNewWSName;

            //合并图层
            pBasicGeop = new BasicGeoprocessorClass();
            pOutputFeatClass = pBasicGeop.Merge(pArray, pTable, pFeatureClassName);

            return pOutputFeatClass;
        }
        public static string getDataPath(string strTitle)  
        {  
            string strDatapath = "";  
            IEnumGxObject enumObj;  
            IGxDialog pgxdlg = new GxDialogClass();  
            IGxObjectFilterCollection pFilterCol = pgxdlg as IGxObjectFilterCollection;  
            pFilterCol.AddFilter(new GxFilterDatasetsAndLayers(), false);  
            pgxdlg.Title = strTitle;  
            pgxdlg.AllowMultiSelect = false;  
            pgxdlg.DoModalOpen(0, out enumObj);  
  
            if (enumObj != null)  
            {  
                IGxObject pGxObj = enumObj.Next();  
                if(pGxObj != null)  
                {  
                    strDatapath = pGxObj.FullName;  
                }  
            }  
            return strDatapath;  
        }  

 
        
        //AddFeatureClass2MDB()


        //合并多个相同字段图层，并保存为shp文件
    


        public static string GetLayerName(ILayer srcLayer)
        {
            IDatasetName pDN = (IDatasetName)srcLayer;
             return pDN.WorkspaceName.PathName;
        }
        public static string GetLayerPath(ILayer srcLayer)
        {
            IDatasetName pDN = (IDatasetName)srcLayer;
            return pDN.WorkspaceName.PathName;
        }

        public static luLandType getFeatureLandType(IFeature iFeature)
        {
            string dlbmString = iFeature.get_Value(iFeature.Fields.FindField("DLBM")).ToString();
            int bm = Convert.ToInt32(dlbmString);
            return getLandTypeByDLBM(bm);
            
        }

        public static luLandType getLandTypeByDLBM(int dlbm)
        {
            //建设用地101、102、105、106、107、113、118、201、202、203、204、205
            //耕地：011、012、013
            //其他农用地：021、022、023、031、032、033、041、042、104、114、122、123、117
            //未利用地：111、112、115、116、119、124、125、126、127、043
            //农用地：为耕地和其他农用地之和
            //104/114为农用地（原为建设用地和未利用地）
            //113为建设用地（原为未利用地）
            if (dlbm == 101 || dlbm == 102 || dlbm == 105 || dlbm == 106 || dlbm == 107 || dlbm == 113 || dlbm == 118 || dlbm == 201 || dlbm == 202 || dlbm == 203 || dlbm == 204 || dlbm == 205)
                return luLandType.luJiansheyong;
            else if (dlbm == 011 || dlbm == 012 || dlbm == 013)
                return luLandType.luGengdi;
            else if (dlbm == 111 || dlbm == 112 || dlbm == 115 || dlbm == 116 || dlbm == 119 || dlbm == 124 || dlbm == 125 || dlbm == 126 || dlbm == 127 || dlbm == 043)
                return luLandType.luWeiliyong;
            else if (dlbm == 021 || dlbm == 022 || dlbm == 023 || dlbm == 031 || dlbm == 032 || dlbm == 033 || dlbm == 041 || dlbm == 042 || dlbm == 104 || dlbm == 114 || dlbm == 122 || dlbm == 123 || dlbm == 117)
                return luLandType.luOtherNongyong;
            else
                return luLandType.luOthers;
        }

        

        #region 为监测图斑图层增加预先定义的字段


        public static bool AddSelectedFileds2Layer(ref IFeatureClass feaLayer)
        {
            bool ret = false;
            ret = AddFiled2Layer(ref feaLayer, XZBH, esriFieldType.esriFieldTypeString, 12);
            if (ret == false) { MessageBox.Show("添加XZBH失败！"); return ret; }
            ret = AddFiled2Layer(ref feaLayer, GDZY, esriFieldType.esriFieldTypeDouble, 8);
            if (ret == false) { MessageBox.Show("添加GDZY失败！"); return ret; }
            ret = AddFiled2Layer(ref feaLayer, GDMJBL, esriFieldType.esriFieldTypeDouble, 8);
            if (ret == false) { MessageBox.Show("添加GDMJBL失败！"); return ret; }
            ret = AddFiled2Layer(ref feaLayer, WLYDZY, esriFieldType.esriFieldTypeDouble, 8);
            if (ret == false) { MessageBox.Show("添加WLYDZY失败！"); return ret; }
            ret = AddFiled2Layer(ref feaLayer, JSYDZY, esriFieldType.esriFieldTypeDouble, 8);
            if (ret == false) { MessageBox.Show("添加JSYDZY失败！"); return ret; }
            ret = AddFiled2Layer(ref feaLayer, JYBGMJ, esriFieldType.esriFieldTypeDouble, 8);
            if (ret == false) { MessageBox.Show("添加JYBGMJ失败！"); return ret; }
            ret = AddFiled2Layer(ref feaLayer, SDBGMJ, esriFieldType.esriFieldTypeDouble, 8);
            if (ret == false) { MessageBox.Show("添加SDBGMJ失败！"); return ret; }
            ret = AddFiled2Layer(ref feaLayer, JBNTZY, esriFieldType.esriFieldTypeDouble, 8);
            if (ret == false) { MessageBox.Show("添加JBNTZY失败！"); return ret; }
            ret = AddFiled2Layer(ref feaLayer, YDSPCH, esriFieldType.esriFieldTypeDouble, 8);
            if (ret == false) { MessageBox.Show("添加YDSPCH失败！"); return ret; }
            ret = AddFiled2Layer(ref feaLayer, BGDLLB, esriFieldType.esriFieldTypeString, 128);
            if (ret == false) { MessageBox.Show("添加BGDLLB失败！"); return ret; }
            ret = AddFiled2Layer(ref feaLayer, YYLX, esriFieldType.esriFieldTypeString, 256);
            if (ret == false) { MessageBox.Show("添加YYLX失败！"); return ret; }
            ret = AddFiled2Layer(ref feaLayer, WYSM, esriFieldType.esriFieldTypeString, 256);
            if (ret == false) { MessageBox.Show("添加WYSM失败！"); return ret; }
            ret = AddFiled2Layer(ref feaLayer, TBWFMJ, esriFieldType.esriFieldTypeDouble, 8);
            if (ret == false) { MessageBox.Show("添加TBWFMJ失败！"); return ret; }
            ret = AddFiled2Layer(ref feaLayer, TBSPGDZY, esriFieldType.esriFieldTypeDouble, 8);
            if (ret == false) { MessageBox.Show("添加TBSPGDZY失败！"); return ret; }
            ret = AddFiled2Layer(ref feaLayer, TBWFGD, esriFieldType.esriFieldTypeDouble, 8);
            if (ret == false) { MessageBox.Show("添加TBWFGD失败！"); return ret; }
            ret = AddFiled2Layer(ref feaLayer, TBZYSP, esriFieldType.esriFieldTypeDouble, 8);
            if (ret == false) { MessageBox.Show("添加TBZYSP失败！"); return ret; }
            ret = AddFiled2Layer(ref feaLayer, BGHTBBHLB, esriFieldType.esriFieldTypeString, 128);
            if (ret == false) { MessageBox.Show("添加BGHTBBHLB失败！"); return ret; }
            ret = AddFiled2Layer(ref feaLayer, LDPS, esriFieldType.esriFieldTypeString, 128);
            if (ret == false) { MessageBox.Show("添加LDPS失败！"); return ret; }
            return true;
        }

        public static bool AddFiled2Layer(ref IFeatureClass jctbFC, string fieldName, esriFieldType fType, int length)
        {
            //IFeatureClass jctbFC = null;
            //jctbFC = (feaLayer as IFeatureLayer).FeatureClass;            
            IField pFieldExtra;
            IFieldEdit pFieldEdit;
            pFieldExtra = new FieldClass();
            pFieldEdit = (IFieldEdit)pFieldExtra;
            pFieldEdit.Name_2 = fieldName;
            pFieldEdit.AliasName_2 = "统计数据";
            pFieldEdit.Type_2 = fType;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Length_2 = length;
            if (jctbFC.FindField(fieldName) == -1)
            {
                jctbFC.AddField(pFieldExtra);
                return true;
            }
            else
            {
                //MessageBox.Show("已经存在该属性，不用添加！");
                return true;
            }                          
            
        }
        #endregion 

      


        #region 计算监测图斑与地类图斑相交的统计数据
        /// <summary>
        /// 计算监测图斑与地类图斑相交的统计数据
        /// </summary>
        /// <param name="pFea">图斑要素</param>
        /// <param name="pFC">地类图斑要素类</param>
        public static bool CalIntersectData(IFeature pFea, IFeatureClass pFC, ref ArrayList dlbmArr, ref ArrayList areaArr)
        {
            
            string strDLBM = "";
            IArea pArea = null;
            IFeature dltbFea = null;
            IPolygon dltbPly = null;
            ITopologicalOperator pTopologicalOperator = null;

            IPolygon jctbPly = pFea.ShapeCopy as IPolygon;
            //获取与监测图斑相交的地类图斑要素
            ArrayList arrFeature = SearchFeature(pFC, jctbPly, esriSpatialRelEnum.esriSpatialRelIntersects, false);
            for (int i = 0; i < arrFeature.Count; i++)
            {
                dltbFea = (IFeature)arrFeature[i];
                dltbPly = (IPolygon)dltbFea.ShapeCopy;
                //计算相交面积
                pTopologicalOperator = jctbPly as ITopologicalOperator;
                IGeometry Geo = pTopologicalOperator.Intersect(dltbPly, esriGeometryDimension.esriGeometry2Dimension);
                IFeatureBuffer FeaBuffer = pFC.CreateFeatureBuffer();
                FeaBuffer.Shape = Geo;
                pArea = (IArea)FeaBuffer.Shape;
                if (pArea.Area > 0.0000001)
                {
                    //获取地类图斑的地类编码
                    strDLBM = dltbFea.get_Value(dltbFea.Fields.FindField("DLBM")).ToString();
                    dlbmArr.Add(strDLBM);
                    areaArr.Add(pArea.Area);                    
                }                
            }
            return true;
        }
        #endregion

        #region 查询一个图层中，与 pGeometry 交叉的要素
        /// <summary>
        /// 查询一个图层中，与 pGeometry 交叉的要素
        /// </summary>
        /// <param name="pMap"></param>
        /// <param name="pGeometry"></param>
        /// <param name="justOne"></param>
        /// <param name="allLayers"></param>
        /// <returns></returns>
        public static ArrayList SearchFeature(IFeatureLayer pFeaLay, IGeometry pGeometry, esriSpatialRelEnum queryMethod, bool justOne)
        {
            SpatialFilterClass spaFilter = new SpatialFilterClass();
            spaFilter.Geometry = pGeometry;
            spaFilter.SpatialRel = queryMethod;//esriSpatialRelEnum.esriSpatialRelIntersects;//交叉
            spaFilter.GeometryField = pFeaLay.FeatureClass.ShapeFieldName;
            return SearchFeature(pFeaLay, spaFilter, justOne);
        }

        /// <summary>
        /// 查询一个图层中，与 pGeometry 交叉的要素
        /// </summary>
        /// <param name="pMap"></param>
        /// <param name="pGeometry"></param>
        /// <param name="justOne"></param>
        /// <param name="allLayers"></param>
        /// <returns></returns>
        public static ArrayList SearchFeature(IFeatureClass pFeatureClass, IGeometry pGeometry, esriSpatialRelEnum queryMethod, bool justOne)
        {
            SpatialFilterClass spaFilter = new SpatialFilterClass();
            spaFilter.Geometry = pGeometry;
            spaFilter.SpatialRel = queryMethod;//esriSpatialRelEnum.esriSpatialRelIntersects;//交叉
            spaFilter.GeometryField = pFeatureClass.ShapeFieldName;
            return SearchFeature(pFeatureClass, spaFilter, justOne);
        }

        #endregion 

        #region 找一个图层中符合条件的要素
        /// <summary>
        /// 找一个图层中符合条件的要素
        /// </summary>
        /// <param name="pFeaLay"></param>
        /// <param name="pFilter"></param>
        /// <param name="justOne"></param>
        /// <returns></returns>
        public static ArrayList SearchFeature(IFeatureLayer pFeaLay, IQueryFilter pFilter, bool justOne)
        {
            ArrayList result = new ArrayList();
            IFeatureCursor pFeaCur = pFeaLay.Search(pFilter, false);
            IFeature pFeature = pFeaCur.NextFeature();
            if (pFeature != null)
            {
                result.Add(pFeature);
                if (!justOne)
                {
                    pFeature = pFeaCur.NextFeature();
                    while (pFeature != null)
                    {
                        result.Add(pFeature);
                        pFeature = pFeaCur.NextFeature();
                    }
                }
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeaCur);
            return result;
        }
        #endregion

        #region 找一个要素类中符合条件的要素
        /// <summary>
        /// 找一个要素类中符合条件的要素
        /// </summary>
        /// <param name="pFeaLay"></param>
        /// <param name="pFilter"></param>
        /// <param name="justOne"></param>
        /// <returns></returns>
        public static ArrayList SearchFeature(IFeatureClass pFeatureClass, IQueryFilter pFilter, bool justOne)
        {
            ArrayList result = new ArrayList();
            IFeatureCursor pFeaCur = pFeatureClass.Search(pFilter, false);
            IFeature pFeature = pFeaCur.NextFeature();
            if (pFeature != null)
            {
                result.Add(pFeature);
                if (!justOne)
                {
                    pFeature = pFeaCur.NextFeature();
                    while (pFeature != null)
                    {
                        result.Add(pFeature);
                        pFeature = pFeaCur.NextFeature();
                    }
                }
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeaCur);
            return result;
        }
        #endregion

        /*
         * 目前导入的图层是存储在ArrayList里
         * 问题是打开工程时，MXD里已经有需要的图层了，不需要重新导入
         * 解决方案是：遍历Map里的Layer.Name，如果符合条件，则记录图层的index；在统计计算时再调取使用。
         * 注意一点：图层顺序变化时需要重新遍历Map，这会导致多次遍历的问题。而且照顾不到图层名称不规范，或者多个JCTB图层的问题
         * 初步想法，暂不实现
         */

        #region 统计计算
        /*
         * 统计计算新算法：
         * 1、用Intersect工具做相交，JCTB与所有图层，保存在临时文件夹里,bin/debug/tmpResult
         * 2、
         */

        public static bool SC_JCTBandTDLY(IFeatureClass fc_jctb, IFeatureClass fc_dltb,IFeatureClass fc_xzdw,IFeatureClass fc_lxdw)
        {
            
            return false;
        }

        public static bool SC_JCTBandJBNT(IFeatureClass fc_jctb,IFeatureClass fc_jbnt)
        {
            object obj1 = fc_jctb;
            object obj2 = fc_jbnt;
            string strOutputPath = Application.StartupPath+"\\tempResult";
            string strOutputName = "jbnt.shp";
            bool bSuccess = GPIntersectTool(obj1, obj2, strOutputPath, strOutputName);
            IFeatureClass fc_newJBNT = OpenShpFile(strOutputPath, "jbnt");
            if (fc_newJBNT != null)
            {
                IFeatureCursor pJCTBCursor = null;//遍历游标
                IFeature jctbFeat = null;//监测图斑要素
               
                //遍历监测图斑要素类中的所有要素
                pJCTBCursor = fc_jctb.Search(null, false);
                if (pJCTBCursor != null)
                {
                    jctbFeat = pJCTBCursor.NextFeature();
                    while (jctbFeat != null)
                    {
                        string strJcbh = jctbFeat.get_Value(jctbFeat.Fields.FindField("JCBH")).ToString();
                        IQueryFilter pQueryfilter = new QueryFilterClass();
                        pQueryfilter.WhereClause = "JCBH = '"+strJcbh+"'";
                        IFeatureCursor pJBNTCursor = fc_newJBNT.Search(pQueryfilter, false);
                        int indexJBNTZY = jctbFeat.Fields.FindField(JBNTZY);
                        
                        if (pJBNTCursor == null)
                        {
                            jctbFeat.set_Value(indexJBNTZY, 0);
                        }
                        else
                        {
                            IFeature pJBNTfeat = pJBNTCursor.NextFeature();
                            double jbntzyArea = 0;
                            while (pJBNTfeat != null)
                            {
                                IArea pArea = (IArea)pJBNTfeat.ShapeCopy;
                                jbntzyArea += pArea.Area;
                                pJBNTfeat = pJBNTCursor.NextFeature();
                            }
                            jctbFeat.set_Value(indexJBNTZY, jbntzyArea);
                        }

                        jctbFeat.Store();
                        jctbFeat = pJCTBCursor.NextFeature();
                    }
                }
                return true;
            }
            return false;
        }

        public static bool SC_JCTBandXZQ(IFeatureClass fc_jctb, IFeatureClass fc_XZQ)
        {
            object obj1 = fc_jctb;
            object obj2 = fc_XZQ;
            string strOutputPath = Application.StartupPath + "\\tempResult"; 
            string strOutputName = "xzq.shp";
            bool bSuccess = GPIntersectTool(obj1, obj2, strOutputPath, strOutputName);
            IFeatureClass fc_newXZQ = OpenShpFile(strOutputPath, "xzq");
            if (fc_newXZQ != null)
            {
                IFeatureCursor pJCTBCursor = null;//遍历游标
                IFeature jctbFeat = null;//监测图斑要素
                //遍历监测图斑要素类中的所有要素
                pJCTBCursor = fc_jctb.Search(null, false);
                if (pJCTBCursor != null)
                {
                    jctbFeat = pJCTBCursor.NextFeature();
                    while (jctbFeat != null)
                    {
                        string strJcbh = jctbFeat.get_Value(jctbFeat.Fields.FindField("JCBH")).ToString();
                        IQueryFilter pQueryfilter = new QueryFilterClass();
                        pQueryfilter.WhereClause = "JCBH = '" + strJcbh + "'";
                        IFeatureCursor pXZQCursor = fc_newXZQ.Search(pQueryfilter, false);
                        int indexXZBH = jctbFeat.Fields.FindField(XZBH);

                        if (pXZQCursor == null)
                        {
                            jctbFeat.set_Value(indexXZBH, "-1");
                        }
                        else
                        {
                            IFeature pXZQfeat = pXZQCursor.NextFeature();
                            string strXZQDM = "";
                            while (pXZQfeat != null)
                            {
                                strXZQDM += pXZQfeat.get_Value(pXZQfeat.Fields.FindField("XZQDM_1")).ToString()+";";
                                
                                pXZQfeat = pXZQCursor.NextFeature();
                            }
                            strXZQDM = strXZQDM.Substring(0, strXZQDM.Length - 1);
                            jctbFeat.set_Value(indexXZBH, strXZQDM);
                        }

                        jctbFeat.Store();
                        jctbFeat = pJCTBCursor.NextFeature();
                    }
                }
                return true;
            }
            return false;
        }

        public static bool SC_JCTBandYDSP(IFeatureClass fc_jctb, IFeatureClass fc_ydsp)
        {
            object obj1 = fc_jctb;
            object obj2 = fc_ydsp;
            string strOutputPath = Application.StartupPath + "\\tempResult"; 
            string strOutputName = "spsj.shp";
            bool bSuccess = GPIntersectTool(obj1, obj2, strOutputPath, strOutputName);
            IFeatureClass fc_newYDSP = OpenShpFile(strOutputPath, "spsj");
            if (fc_newYDSP != null)
            {
                IFeatureCursor pJCTBCursor = null;//遍历游标
                IFeature jctbFeat = null;//监测图斑要素

                //遍历监测图斑要素类中的所有要素
                pJCTBCursor = fc_jctb.Search(null, false);
                if (pJCTBCursor != null)
                {
                    jctbFeat = pJCTBCursor.NextFeature();
                    while (jctbFeat != null)
                    {
                        string strJcbh = jctbFeat.get_Value(jctbFeat.Fields.FindField("JCBH")).ToString();
                        IQueryFilter pQueryfilter = new QueryFilterClass();
                        pQueryfilter.WhereClause = "JCBH = '" + strJcbh + "'";
                        IFeatureCursor pYDSPCursor = fc_newYDSP.Search(pQueryfilter, false);
                        int indexYDSPCH = jctbFeat.Fields.FindField(YDSPCH);

                        if (pYDSPCursor == null)
                        {
                            jctbFeat.set_Value(indexYDSPCH, 0);
                        }
                        else
                        {
                            IFeature pYDSPfeat = pYDSPCursor.NextFeature();
                            double ydspchArea = 0;
                            while (pYDSPfeat != null)
                            {
                                IArea pArea = (IArea)pYDSPfeat.ShapeCopy;
                                ydspchArea += pArea.Area;
                                pYDSPfeat = pYDSPCursor.NextFeature();
                            }
                            jctbFeat.set_Value(indexYDSPCH, ydspchArea);
                        }

                        jctbFeat.Store();
                        jctbFeat = pJCTBCursor.NextFeature();
                    }
                }
                return true;
            }
            return false;
        }

        public static bool GPIntersectTool(object IntersectObj,object srcObj ,string strOutputPath,string strOutputName)
        {
            IGpValueTableObject vtobject = new GpValueTableObject();
            vtobject.SetColumns(2);
            vtobject.AddRow(ref IntersectObj);
            vtobject.AddRow(ref srcObj);

            string strOutput = strOutputPath + "\\" + strOutputName;

            Geoprocessor gp = new Geoprocessor();
            gp.OverwriteOutput = true;
            Intersect intersectTool = new Intersect();
            intersectTool.in_features = vtobject;    //输入对象
            intersectTool.out_feature_class = strOutput;     //输出对象
            try
            {
                // Export 
                gp.Execute(intersectTool, null);                //执行
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static IFeatureClass OpenShpFile(string sPath, string sShapeName)
        {
            IFeatureClass rltFClass = null;
            IWorkspaceFactory pWSFact;
            IWorkspace pWS;
            IFeatureWorkspace pFWS;
            try
            {
                pWSFact = new ShapefileWorkspaceFactoryClass();
                pWS = pWSFact.OpenFromFile(sPath, 0);
                pFWS = pWS as IFeatureWorkspace; 
                rltFClass = pFWS.OpenFeatureClass(sShapeName);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            return rltFClass;
        }
        #endregion

    }
}
