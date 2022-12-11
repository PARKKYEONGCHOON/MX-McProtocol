using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Diagnostics;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace IntelligentFactory
{
    public enum LOG_TYPE
    {
        SYSTEM,
        ERROR,
        COMM,
        IO,
        RESULT,
        CONFIG,
        MOTION
    }

    //public class ILog
    //{
    //    private LOG_TYPE m_Type;
    //    public LOG_TYPE Type
    //    {
    //        get { return m_Type; }
    //        set { m_Type = value; }
    //    }

    //    private string m_strDesc;
    //    public string Desc
    //    {
    //        get { return m_strDesc; }
    //        set { m_strDesc = value; }
    //    }

    //    private bool m_bIsNew = false;
    //    public bool IsNew
    //    {
    //        get { return m_bIsNew; }
    //        set { m_bIsNew = value; }
    //    }

    //    public ILog(LOG_TYPE type, string strDesc)
    //    {
    //        m_Type = type;
    //        m_strDesc = strDesc;
    //        m_bIsNew = true;
    //    }
    //}

    public class ILog
    {
        private LOG_TYPE m_etype;
        public LOG_TYPE Type
        {
            get
            {
                return m_etype;
            }

            set
            {
                m_etype = value;
            }
        }

        private string m_strLog;
        public string StrLog
        {
            get
            {
                return m_strLog;
            }

            set
            {
                m_strLog = value;
            }
        }

        private bool m_bDisplay;
        public bool IsDisplay
        {
            get
            {
                return m_bDisplay;
            }

            set
            {
                m_bDisplay = value;
            }
        }

        public ILog(LOG_TYPE etype, string strLog)
        {
            this.m_etype = etype;
            this.m_strLog = strLog;
            this.m_bDisplay = true;
        }
    }

    public static class ILogger
    {   
        private const int MAX_TRY_WRITE_LOG_FILE = 10;
        private static List<string> m_lstFilebuffer = new List<string>();
        private static List<ILog> m_lstLogbuffer = new List<ILog>();        

        private static DateTime timestampOld;
        public static bool m_bStartLog = true;
        private static string m_strAppName;

        public delegate void LogEvent(ILog item);
        public static event LogEvent EventLog;

        private static string m_strLogPath = Application.StartupPath + "\\";
        private static string m_strDateTimeLogPath = Application.StartupPath + "\\LOG\\";
        private static string m_strSystemLogPath = Application.StartupPath + "\\LOG\\SYSTEM\\";
        private static string m_strResultLogPath = Application.StartupPath + "\\LOG\\RESULT\\";

        public static Color GetColor(LOG_TYPE type)
        {
            switch (type)
            {
                case LOG_TYPE.SYSTEM:
                    return Color.White;
                case LOG_TYPE.IO:
                    return Color.Lime;
                case LOG_TYPE.ERROR:
                    return Color.Red;
                case LOG_TYPE.COMM:
                    return Color.Blue;
                case LOG_TYPE.RESULT:
                    return Color.Aquamarine;
                case LOG_TYPE.CONFIG:
                    return Color.Yellow;
                case LOG_TYPE.MOTION:
                    return Color.Aquamarine;
            }

            return Color.Silver;
        }

        public static void Init()
        {
            try
            {
                InitDirectory();
                Add(LOG_TYPE.SYSTEM, "[OK] {0}==>{1}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception Desc)
            {
                Add(LOG_TYPE.ERROR, "[FAILED] {0}==>{1}   Ex ==> {2}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, Desc.Message);
            }

        }
        public static bool InitDirectory()
        {
            try
            {
                DirectoryInfo dirLog = new DirectoryInfo(m_strLogPath);
                if (dirLog.Exists == false) dirLog.Create();

                //m_strDateTimeLogPath = Application.StartupPath + "\\LOG\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
                //DirectoryInfo dirDateTime = new DirectoryInfo(m_strDateTimeLogPath);
                //if (dirDateTime.Exists == false) dirDateTime.Create();

                //m_strSystemLogPath = m_strDateTimeLogPath + "SYSTEM\\";
                //DirectoryInfo dirSystem = new DirectoryInfo(m_strSystemLogPath);
                //if (dirSystem.Exists == false) dirSystem.Create();

                //m_strResultLogPath = m_strDateTimeLogPath + "RESULT\\";
                //DirectoryInfo dirResult = new DirectoryInfo(m_strResultLogPath);
                //if (dirResult.Exists == false) dirResult.Create();

                return true;
            }
            catch (Exception Desc)
            {
                ILogger.Add(LOG_TYPE.ERROR, "[FAILED] {0}==>{1}   Ex ==> {2}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, Desc.Message);
                return false;
            }
        }

        public static void SetPath(string strPath)
        {
            string strDir = strPath;
            if (Directory.Exists(strDir) == false)
            {
                Directory.CreateDirectory(strPath);
            }

            m_strLogPath = strDir;
        }

        public static string GetPath()
        {
            return m_strLogPath;
        }

        public static void SetAppName(string strAppName)
        {
            m_strAppName = strAppName;
        }

        public static string GetAppName()
        {
            return m_strAppName;
        }
        
        public static void AddEvent( LogEvent newMethod )
        {
            EventLog += newMethod;
        }

        static private object lockObject = new object();
        
        public static string LogStrToFile(LOG_TYPE Type, string strLog)
        {
            string strLogTotal = "";
            lock (lockObject)
            {
                strLog = strLog.TrimEnd('\0');

                DateTime timestampNew = DateTime.Now;
                if (ILogger.m_bStartLog == true)
                {
                    ILogger.timestampOld = timestampNew;
                    ILogger.m_bStartLog = false;
                }

                TimeSpan timeSpanDelay;
                timeSpanDelay = timestampNew - ILogger.timestampOld;

                int nDelay = (int)timeSpanDelay.TotalMilliseconds;

                string strLogType = string.Format("{0,6} ", "[" + Type.ToString().ToUpper() + "]");
                string strDelay = string.Format("{0,6}", nDelay);

                strLogTotal = string.Format("{0} : {1} : {2}", timestampNew.ToString("yy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture), strLogType, strLog);
                Debug.WriteLine(strLogTotal);

                if (GetPath() != "")
                {
                    string strAbnormalFilePath = GetPath() + '\\' + timestampNew.ToString("yyMMdd") + (m_strAppName != null ? "_" : "") + m_strAppName + "_Alarm.log";
                    
                    string filename = GetPath() + '\\' + timestampNew.ToString("yyMMdd") + (m_strAppName != null ? "_" : "") + m_strAppName + ".log";

                    if (m_lstFilebuffer.Count > 0)
                    {
                        foreach (string strItem in m_lstFilebuffer)
                        {
                            try
                            {
                                WriteFileLog(filename, strItem);

                                if (Type == LOG_TYPE.ERROR)
                                {
                                    WriteFileLog(strAbnormalFilePath, strItem);
                                }
                            }
                            catch (Exception e)
                            {
                                WriteLog("Log File Write Fail : " + e.Message);
                            }
                        }
                        m_lstFilebuffer.Clear();
                        try
                        {
                            WriteFileLog(filename, strLogType + strLogTotal);

                            if (Type == LOG_TYPE.ERROR)
                            {
                                WriteFileLog(strAbnormalFilePath, strLogType + strLogTotal);
                            }
                        }
                        catch (Exception e)
                        {
                            WriteLog("Log File Write Fail : " + e.Message);
                        }
                    }
                    else
                    {
                        try
                        {
                            WriteFileLog(filename, strLogType + strLogTotal);
                        }
                        catch (Exception e)
                        {
                            WriteLog("Log File Write Fail : " + e.Message);
                        }
                    }
                }
                else
                {
                    m_lstFilebuffer.Add(strLogType + strLogTotal);
                }

                ILogger.timestampOld = timestampNew;

                if (strLogTotal.Length > 3)
                {
                    strLogTotal = strLogTotal.Remove(0, 3);
                }

            }
            return strLogTotal;
        }

        private static void WriteFileLog(string strFileName, string strLog)
        {
            int i = 0;
            while (true)
            {
                try
                {                    
                    using (StreamWriter writer = File.AppendText(strFileName))
                    {
                        writer.WriteLine(strLog);
                    }
                    break;
                }
                catch (IOException)
                {
                    Thread.Sleep(10);
                    i++;
                    if (i >= MAX_TRY_WRITE_LOG_FILE)
                    {
                        throw new IOException("Log file \"" + strFileName + "\" not accessible after 5 tries");
                    }
                }
            }
        }

        public static bool WriteLog(string format, params object[] args)
        {
            string strLog = string.Format(format, args);
            return Add(LOG_TYPE.SYSTEM, strLog);
        }

        public static bool Add(LOG_TYPE type, string format, params object[] args)
        {
            string strLog = "";
            try
            {
                strLog = string.Format(format, args);
            }
            catch (Exception Desc)
            {
                strLog = format;
            }

            string str = LogStrToFile(type, strLog);

            ILog item = new ILog(type, str);

            if (EventLog != null)
            {
                if (m_lstLogbuffer.Count > 0)
                {
                    /*
                    foreach (LogItem Log in m_lstLogbuffer)
                    {
                        EventLog(Log);
                    }
                     */
                    for (int i = 0; i < m_lstLogbuffer.Count; i++)
                    {
                        EventLog(m_lstLogbuffer[i]);
                    }
                    m_lstLogbuffer.Clear();
                    EventLog(item);
                }
                else
                {
                    EventLog(item);
                }
            }
            else
            {
                m_lstLogbuffer.Add(item);
            }

            return true;
        }

        public static void LoggerFileDelete(int FileCount)
        {
            DirectoryInfo dinfo = new DirectoryInfo(GetPath());
            if (!dinfo.Exists) dinfo.Create();

            if (FileCount < dinfo.GetFiles().Length)
            {
                List<FileInfo> files = new List<FileInfo>();
                foreach (FileInfo f in dinfo.GetFiles())
                {
                    if (f.Extension == ".log") files.Add(f);
                }

                files.Sort(new CompareFileInfoEntries());

                for (int i = 0; i <= files.Count - FileCount; i++)
                {
                    File.Delete(dinfo.FullName +"\\"+ files[i].Name);
                    WriteLog("Delete File ==> " + dinfo.FullName + "\\" + files[i].Name);
                }
            }
        }

        public static void LoggerFileDelete(string strDir, string strExt, int FileCount)
        {
            DirectoryInfo dinfo = new DirectoryInfo(strDir);
            if (!dinfo.Exists) dinfo.Create();

            if (FileCount < dinfo.GetFiles().Length)
            {
                List<FileInfo> files = new List<FileInfo>();
                foreach (FileInfo f in dinfo.GetFiles())
                {
                    if (f.Extension == strExt) files.Add(f);
                }

                files.Sort(new CompareFileInfoEntries());

                for (int i = 0; i <= files.Count - FileCount; i++)
                {
                    File.Delete(dinfo.FullName + "\\" + files[i].Name);
                    WriteLog("Delete File ==> " + dinfo.FullName + "\\" + files[i].Name);
                }
            }
        }   
    }

    public class CompareFileInfoEntries : IComparer<FileInfo>
    {
        public int Compare(FileInfo f1, FileInfo f2)
        {
            return (DateTime.Compare(f1.CreationTime, f2.CreationTime));
        }
    }
}
