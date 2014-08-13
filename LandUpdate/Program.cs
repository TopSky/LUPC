using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Catalog;  

namespace LandUpdate
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (checkLicense())
            {
                Application.Run(new mainForm());
            }
        }

        private static bool checkLicense()
        {
            //ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine);
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);

            IAoInitialize m_AoInitialize = new AoInitializeClass();

            esriLicenseStatus licenseStatus = (esriLicenseStatus)m_AoInitialize.IsProductCodeAvailable(esriLicenseProductCode.esriLicenseProductCodeEngine);
            if (licenseStatus == esriLicenseStatus.esriLicenseAvailable)
            {
                licenseStatus = (esriLicenseStatus)m_AoInitialize.IsExtensionCodeAvailable(esriLicenseProductCode.esriLicenseProductCodeEngine, esriLicenseExtensionCode.esriLicenseExtensionCode3DAnalyst);
                if (licenseStatus == esriLicenseStatus.esriLicenseAvailable)
                {
                    licenseStatus = (esriLicenseStatus)m_AoInitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB);//esriLicenseProductCodeEngine);
                    if (licenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
                    {
                        System.Windows.Forms.MessageBox.Show("The initialization failed. This application cannot run!");
                        return false;
                    }
                    else
                    {
                        licenseStatus = (esriLicenseStatus)m_AoInitialize.CheckOutExtension(esriLicenseExtensionCode.esriLicenseExtensionCodeDesigner);
                        if (licenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
                        {
                            System.Windows.Forms.MessageBox.Show("Unable to check out the Designer extension. This application cannot run!");
                            return false;
                        }
                    }
                }
                return true;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("The ArcGIS Engine product is unavailable. This application cannot run!");
                return false;
            }
        }

    }
}
