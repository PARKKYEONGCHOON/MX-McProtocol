using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace IntelligentFactory
{
    public class IThreadStatus
    {
        public enum RESULT { Idle, Run, End, Stop, Error };
        private RESULT m_Result;
        private string m_strName;
        private string m_strProc;
        private bool m_bRun;
        private bool m_bStopReq;
        public bool m_bFirst = false;

        public IThreadStatus(string strName = null)
        {
            this.m_strName = strName;
        }

        public void WriteInfo(string str)
        {
            //Logger.WriteLog(LOG.Thread, string.Format("[Thread] {0} {1} {2}", m_strName, m_strProc, str));
            Debug.WriteLine("[Thread] {0} {1} {2}", m_strName, m_strProc, str);
        }

        public bool Start(string strName = null)
        {
            if (strName != null)
            {
                m_strName = strName;
            }

            m_strProc = "Start";

            if (m_bRun)
            {
                WriteInfo("Already Running");
                return false;
            }

            m_bRun = true;
            m_bStopReq = false;

            m_Result = RESULT.Run;
            WriteInfo("Run");

            if (EventStart != null)
            {
                EventStart(this, new EventArgs());
            }
            return true;
        }

        public void End()
        {
            if (m_bStopReq)
            {
                m_Result = RESULT.Stop;
                m_strProc = "Stop";
                WriteInfo("By Stop Request");
            }
            else if (m_Result == RESULT.Error)
            {
                m_strProc = "Error";
                WriteInfo("By Error");
            }
            else
            {
                m_Result = RESULT.End;
                m_strProc = "End";
                WriteInfo("Process");
            }

            m_bRun = false;
            m_bStopReq = false;
            if (EventEnd != null)
            {
                EventEnd(this, new EventArgs());
            }
        }

        public bool Stop(long nTimeLimit)
        {
            if (m_bRun == false)
            {
                return true;
            }

            long tickCountStart = Environment.TickCount;

            m_bStopReq = true;

            while (m_bRun)
            {
                Thread.Sleep(1);
                if (Environment.TickCount - tickCountStart > nTimeLimit)
                {
                    WriteInfo("Can't Stop");
                    return false;
                }
            }

            m_bFirst = false;
            m_bStopReq = false;
            return true;
        }

        public void Error(string strError)
        {
            m_Result = RESULT.Error;
            strError = "Error";
            Debug.WriteLine("[Thread] {0} {1} {2}", m_strName, m_strProc, strError);
            //Logger.WriteLog(LOG.Alarm, string.Format("[Thread] {0} {1} {2}", m_strName, m_strProc, strError));
        }

        public bool IsExit()
        {
            if (m_bStopReq) return true;

            if (m_Result == RESULT.Run)
                return false;

            return true;
        }

        public bool IsEnd()
        {
            return !m_bRun;
        }

        #region Event Register
        public EventHandler<EventArgs> EventStart;
        public EventHandler<EventArgs> EventEnd;
        #endregion
    }
}

