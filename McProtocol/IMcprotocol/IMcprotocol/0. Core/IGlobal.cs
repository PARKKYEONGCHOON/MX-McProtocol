using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

using Microsoft.Win32;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

using System.Xml;
using IntelligentFactory;
using System.CodeDom;
using IntelligentFactory;

namespace IntelligentFactory
{   
    // Version 2.0.0 : Main Data Property => Global Data Class 로 변경 (Reversion)
    public class IGlobal
    {
        static readonly object syncLock = new object();
        static IGlobal m_Instance = null;
        public static IGlobal Instance
        {
            get
            {
                lock (syncLock)
                {
                    if (m_Instance == null) m_Instance = new IGlobal();
                    return m_Instance;
                }
            }
        }
        public int m_nInspIndex = 0;
        
        #region Event Register                
        #endregion

        public IGlobal()
        {
            m_Instance = this;

            string strLogPath = InitLogDirectory();
            ILogger.SetPath(strLogPath);
            //ILogger.SetPath(Application.StartupPath);
            //ILogger.Init();
        }

        private string InitLogDirectory()
        {
            string strLogPath = Application.StartupPath;
            try
            {
                return strLogPath = Application.StartupPath + "\\" + "Log\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM");
            }
            catch
            {
                return strLogPath;
            }
        }

        public bool Close()
        {
            try
            {
                                  
                return true;
            }
            catch (Exception Desc)
            {
                ILogger.Add(LOG_TYPE.ERROR, "[FAILED] {0}==>{1} Execption ==> {2}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, Desc.Message);
                return false;
            }
        }

        public static bool InitRecipeDirectory(string strRecipeName)
        {
            try
            {
                string sDirPath;
                sDirPath = Application.StartupPath + "\\Recipe\\" + strRecipeName;
                DirectoryInfo di = new DirectoryInfo(sDirPath);
                if (di.Exists == false)
                {
                    di.Create();

                    //Logger.WriteLog(LOG.SYS, "[OK] {0}==>{1}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                }
                else
                {
                    //Logger.WriteLog(LOG.Normal, "Already Exist Folder ==> " + strName);
                }
                return true;
            }
            catch (Exception Desc)

            {
                //Logger.WriteLog(LOG.ERR, "[FAILED] {0}==>{1}   Execption ==> {2}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, Desc.Message);
                return false;
            }
        }
        public static bool InitDirectory(string strName)
        {
            try
            {
                string sDirPath;
                sDirPath = Application.StartupPath + "\\" + strName;
                DirectoryInfo di = new DirectoryInfo(sDirPath);
                if (di.Exists == false)
                {
                    di.Create();

                    if (strName == "Config")
                    {
                        //devLightController.WriteInitFile();
                    }

                    //Logger.WriteLog(LOG.SYS, "[OK] {0}==>{1}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                }
                else
                {
                    //Logger.WriteLog(LOG.Normal, "Already Exist Folder ==> " + strName);
                }
                return true;
            }
            catch (Exception Desc)

            {
                //Logger.WriteLog(LOG.ERR, "[FAILED] {0}==>{1}   Execption ==> {2}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, Desc.Message);
                return false;
            }
        }
    }
}
