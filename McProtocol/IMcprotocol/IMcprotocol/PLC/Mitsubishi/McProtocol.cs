using InControls.PLC.MCPackage;
using IntelligentFactory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace InControls.PLC.Mitsubishi
{
    public class McProtocol
    {
        private IGlobal Global = IGlobal.Instance;

        public McProtocolTcp ClientBinary = null;

        private string strHostIP;
        private int strHostPort;

        #region io
        public ISignal InputAlive = null;
        public ISignal InputInspectionRequest = null;
        public ISignal InputIndexNumber = null;

        public ISignal OutputAlive = null;
        public ISignal OutputOperationMode = null;
        public ISignal OutputWarningAlarm = null;
        public ISignal OutputErrorAlarm = null;
        public ISignal OutputInspectionStart = null;
        public ISignal OutputInspectionComplete = null;
        public ISignal OutputInspectionJudgement = null;

        private List<ISignal> m_ListInput = new List<ISignal>();
        public List<ISignal> ListInput
        {
            get { return m_ListInput; }
            set { m_ListInput = value; }
        }

        private List<ISignal> m_ListOutput = new List<ISignal>();
        public List<ISignal> ListOutput
        {
            get { return m_ListOutput; }
            set { m_ListInput = value; }
        }

        #region Thread IO
        private IThreadStatus m_ThreadStatusRead = new IThreadStatus();
        public IThreadStatus ThreadStatusRead
        {
            get { return m_ThreadStatusRead; }
        }

        private Stopwatch m_Timeout = new Stopwatch();
        public Stopwatch PlcConnectionTimeout
        {
            get => m_Timeout;
            set
            {
                m_Timeout = value;
            }
        }

        public void StartThreadRead()
        {
            Thread t = new Thread(new ParameterizedThreadStart(ThreadRead));
            t.Start(m_ThreadStatusRead);
        }

        public void StopThreadRead()
        {
            if (!ThreadStatusRead.IsExit())
            {
                ThreadStatusRead.Stop(100);
            }
        }

        int m_nAliveTime = 0;
        private void ThreadRead(object ob)
        {
            IThreadStatus ThreadStatus = (IThreadStatus)ob;

            ThreadStatus.Start("Read Io");
            ILogger.Add(LOG_TYPE.SYSTEM, "Read the Io Signal");

            try
            {
                while (!ThreadStatus.IsExit())
                {
                    if ((Environment.TickCount - m_nAliveTime) > 1000)
                    {
                        m_nAliveTime = Environment.TickCount;

                    }

                    Thread.Sleep(10);

                    try
                    {                        
                        if (IsConnected)
                        {
                            for (int i = 0; i < ListInput.Count; i++)
                            {
                                ISignal InputSignal = ListInput[i];

                                string strAddr = InputSignal.Address;

                                int nWordCount = InputSignal.WordCount;
                                int nValueW = 0;
                                int nData = 0;
                                int[] nDataArr = new int[nWordCount];

                                if (nWordCount == 1)
                                {
                                    var rtn = ClientBinary.GetDevice(strAddr, out nData);
                                    
                                    InputSignal.Current = nData;
                                }
                                else
                                {
                                    var rtn = ClientBinary.ReadDeviceBlock(strAddr, nWordCount, nDataArr);

                                    List<short> listValue = new List<short>();
                                    int nSum = 0;
                                    int shBuffTemp = 0;
                                    int nBuffTemp = 0;

                                    for (int k = 0; k < nDataArr.Length; k++)
                                    {
                                        shBuffTemp = nDataArr[k];
                                        ushort ushBuffTemp = (ushort)shBuffTemp;
                                        nBuffTemp = (int)ushBuffTemp;

                                        if (k == 0)
                                        {
                                        }
                                        else
                                        {
                                            nBuffTemp = ((int)nBuffTemp << (16 * k));
                                        }

                                        nSum += nBuffTemp;
                                    }

                                    nValueW = nSum;

                                    InputSignal.Current = nValueW;
                                }
                            }
                        }

                        if (!PlcConnectionTimeout.IsRunning)
                        {
                            PlcConnectionTimeout.Start();
                            //IGlobal.Instance.System.McProtocol.On(OutputAlive);
                        }

                        if (m_Timeout.ElapsedMilliseconds >= 3000)
                        {
                            if (OutputAlive.Current == ISignal.SIGNAL_ON)
                            {
                                //IGlobal.Instance.System.McProtocol.Off(OutputAlive);
                            }
                            else
                            {
                                //IGlobal.Instance.System.McProtocol.On(OutputAlive);
                            }
                            m_Timeout.Restart();
                        }
                    }
                    catch (Exception Desc)
                    {
                        ILogger.Add(LOG_TYPE.ERROR, "[FAILED] {0}==>{1}   Ex ==> {2}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, Desc.Message);
                    }
                }
            }
            catch (Exception Desc)
            {
                ILogger.Add(LOG_TYPE.ERROR, "[FAILED] {0}==>{1}   Ex ==> {2}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, Desc.Message);
            }

        }

        public bool On(ISignal signal)
        {
            try
            {
                if (IsConnected)
                {
                    if (signal != null)
                    {
                        ClientBinary.SetDevice(signal.Address, ISignal.SIGNAL_ON);
                        ILogger.Add(LOG_TYPE.IO, "[{0}] {1} ==> {2}", signal.Name, signal.Current.ToString() == "1" ? "ON" : "OFF", "ON");
                    }
                }
            }
            catch (Exception Desc)
            {
                ILogger.Add(LOG_TYPE.ERROR, "[FAILED] {0}==>{1}   Ex ==> {2}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, Desc.Message);
                return false;
            }

            return true;
        }

        public bool Off(ISignal signal)
        {
            try
            {
                if (IsConnected)
                {
                    if (signal != null)
                    {
                        ClientBinary.SetDevice(signal.Address, ISignal.SIGNAL_OFF);

                        ILogger.Add(LOG_TYPE.IO, "[{0}] {1} ==> {2}", signal.Name, signal.Current.ToString() == "0" ? "OFF" : "ON", "OFF");
                    }
                }
            }
            catch (Exception Desc)
            {
                ILogger.Add(LOG_TYPE.ERROR, "[FAILED] {0}==>{1}   Ex ==> {2}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, Desc.Message);
                return false;
            }

            return true;
        }

        public bool InitIO()
        {
            try
            {
                ListInput.Clear();
                ListOutput.Clear();

                OutputAlive = new ISignal("OutputAlive", "1180", ISignal.DEV_TYPE.LW, 1);
                ListInput.Add(OutputAlive);
                OutputOperationMode = new ISignal("OutputOperationMode", "1181", ISignal.DEV_TYPE.LW, 1);
                ListInput.Add(OutputOperationMode);
                // 검사 종료시 Complete끌때 Start도 같이 끄기 
                OutputInspectionStart = new ISignal("OutputInspectionStart", "1183", ISignal.DEV_TYPE.LW, 1);
                ListInput.Add(OutputInspectionStart);
                OutputInspectionComplete = new ISignal("OutputInspectionComplete", "1184", ISignal.DEV_TYPE.LW, 1);
                ListInput.Add(OutputInspectionComplete);
                OutputInspectionJudgement = new ISignal("OutputInspectionJudgement", "1185", ISignal.DEV_TYPE.LW, 1);
                ListInput.Add(OutputInspectionJudgement);

                OutputWarningAlarm = new ISignal("OutputWarningAlarm", "1186", ISignal.DEV_TYPE.LW, 1);
                ListInput.Add(OutputWarningAlarm);
                OutputErrorAlarm = new ISignal("OutputErrorAlarm", "1187", ISignal.DEV_TYPE.LW, 1);
                ListInput.Add(OutputErrorAlarm);


                InputAlive = new ISignal("InputAlive", "1190", ISignal.DEV_TYPE.LW, 1);
                ListInput.Add(InputAlive);
                InputInspectionRequest = new ISignal("InputInspectionRequest", "1192", ISignal.DEV_TYPE.LW, 1);
                ListInput.Add(InputInspectionRequest);

                //InputLightAlarm = new ISignal("InputLightAlarm", "1183", ISignal.DEV_TYPE.LW, 1);
                //ListInput.Add(InputLightAlarm);
                //InputHeavyAlarm = new ISignal("InputHeavyAlarm", "1184", ISignal.DEV_TYPE.LW, 1);
                //ListInput.Add(InputHeavyAlarm);

                // 1 : A / 2 : B / 4 : C / 8 : D
                InputIndexNumber = new ISignal("InputIndexNumber", "1198", ISignal.DEV_TYPE.LW, 1);
                ListInput.Add(InputIndexNumber);

                ILogger.Add(LOG_TYPE.SYSTEM, "[OK] {0}==>{1}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name); 
                return true;
            }
            catch (Exception Desc)
            {
                ILogger.Add(LOG_TYPE.ERROR, "[FAILED] {0}==>{1}   Ex ==> {2}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, Desc.Message);
                return false;
            }
        }
        #endregion

        #endregion

        public bool IsConnected
        {
            get
            {
                if (ClientBinary != null)
                {
                    if (ClientBinary.Client.Connected != null)
                        return ClientBinary.Client.Connected;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
        }

        public void Open()
        {
            ClientBinary = new McProtocolTcp(strHostIP, strHostPort);
            ClientBinary.Open();            
        }

        public void SetAddress(string strIp, int nPort)
        {
            strHostIP = strIp;
            strHostPort = nPort;
            ILogger.Add(LOG_TYPE.SYSTEM, "[OK] {0}==>{1}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
        }

        public void GetAddress(out string strIp, out int nPort)
        {
            strIp = strHostIP;
            nPort = strHostPort;
        }

        public bool PingTest()
        {
            try
            {
                Ping pingSender = new Ping();
                int nTimeout = 1000;
                // IP를 따로 빼야함
                PingReply reply = pingSender.Send(strHostIP, nTimeout);

                if (reply.Status == IPStatus.Success)
                {
                    ILogger.Add(LOG_TYPE.SYSTEM, "[OK] - {0}", "PLC Server Ping TEST");
                    return true;
                }
                ILogger.Add(LOG_TYPE.ERROR, "[Fail] - {0}", "PLC Server Ping TEST");
            }
            catch
            {
                ILogger.Add(LOG_TYPE.ERROR, "[Fail] - {0}", "PLC Server Ping TEST");
            }

            return false;
        }
    }
}
