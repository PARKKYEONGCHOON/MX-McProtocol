using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentFactory
{
    public static class IAlarm
    {
        public const int ALARM_TEST    = 0x9999;
        public const int ALARM_SYSTEM  = 0x1000;
        public const int ALARM_UDP     = 0x2000;
        public const int ALARM_LIGHTC  = 0x3000;
        public const int ALARM_CAMERA  = 0x4000;
        public const int ALARM_LICENSE = 0x5000;


        #region 알람코드 정리
        public const int ALARM_PARTICLE_LIGHT = 101;
        public const int ALARM_PARTICLE_HEAVY = 102;        
        
        public const int ALARM_PARTICLE_GRAB_TIMEOUT = 121;

        public const int ALARM_CAMERA_CONNECTION_ERROR = 131;
        public const int ALARM_PLC_CONNECTION_ERROR = 132;
        public const int ALARM_LIGHT_CONNECTION_ERROR = 132;
        #endregion

        public struct AlarmNode
        {
            public int m_nCode;
            public string m_strDesc;

            public AlarmNode(int nCode, string strDesc)
            {
                this.m_nCode = nCode;
                this.m_strDesc = strDesc;
            }

            public override string ToString()
            {
                return string.Format("AlarmNode Code [{0:N}] ==> {1}", m_nCode, m_strDesc);
            }
        }

        private static List<AlarmNode> m_lstAlarm = new List<AlarmNode>();

        public static List<AlarmNode> ListAlarm
        {
            get
            {
                return m_lstAlarm;
            }
        }

        public static int GetLastCode()
        {
            int nLast = m_lstAlarm.Count - 1;
            return m_lstAlarm[nLast].m_nCode;
        }

        public static string GetLastDesc()
        {
            int nLast = m_lstAlarm.Count - 1;
            return m_lstAlarm[nLast].m_strDesc;
        }
        // Add        
        public static void Add(int nCode, string strDesc)
        {
            try
            {
                bool bExistSameCode = false;
                foreach (AlarmNode Node in m_lstAlarm)
                {
                    if (Node.m_nCode == nCode)
                    {
                        bExistSameCode = true;
                        break;
                    }
                }

                if (!bExistSameCode)
                {
                    string strInfo = string.Format("Alarm ==> Code [{0}] : {1}", nCode, strDesc);
                    ILogger.Add(LOG_TYPE.ERROR, strInfo);
                    AlarmNode Node = new AlarmNode(nCode, strDesc);
                    m_lstAlarm.Add(Node);
                }
            }
            catch(Exception e)
            {
                ILogger.Add(LOG_TYPE.ERROR, string.Format("Add Alarm ex ==> {0}", e.Message));
            }
        }

        public static void AddAlarm(int nCode, string format, params object[] args)
        {
            bool bExistSameCode = false;
            foreach (AlarmNode Node in m_lstAlarm)
            {
                if (Node.m_nCode == nCode)
                {
                    bExistSameCode = true;
                    break;
                }
            }

            if (!bExistSameCode)
            {
                string strLog = "";
                try
                {
                    strLog = string.Format(format, args);
                }
                catch (Exception ex)
                {
                    strLog = format;
                }

                string strInfo = string.Format("Alarm ==> Code [{0}] : {1}", nCode, strLog);
                ILogger.Add(LOG_TYPE.ERROR, strInfo);
                AlarmNode Node = new AlarmNode(nCode, strLog);
                m_lstAlarm.Add(Node);
            }
        }

        public static void RemoveAlarm(int nCode)
        {
            int nIndex = -1;
            foreach (AlarmNode Node in m_lstAlarm)
            {
                if (Node.m_nCode == nCode)
                {
                    nIndex = m_lstAlarm.IndexOf(Node);
                    break;
                }
            }

            if (nIndex >= 0)
                m_lstAlarm.RemoveAt(nIndex);
        }

        public static void Clear()
        {
            m_lstAlarm.Clear();
        }

        public static bool IsExistAlarm()
        {
            return m_lstAlarm.Count > 0;
        }
    }
}
