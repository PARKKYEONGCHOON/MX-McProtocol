using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentFactory
{
    public static class DEFINE
    {
        public const double PIXEL_RESOLUTION_UM = 4.3125;
        public const double PIXEL_RESOLUTION_MM = 0.0043125;

        /*
         * bal.m_System.Recipe.ROI.Add(rtRB);
                    Global.m_System.Recipe.ROI.Add(rtLB);
                    Global.m_System.Recipe.ROI.Add(rtRT);
                    Global.m_System.Recipe.ROI.Add(rtLT);
         */
        public const int ROI_RB = 0;
        public const int ROI_LB = 1;
        public const int ROI_RT = 2;
        public const int ROI_LT = 3;


        public const int AXIS_MAX_COUNT = 3;

        public const int GRABEER_MAX_CNT = 1;

        public const int INTERLOCK_Z_AXIS = 280000;

        public const int AXIS_Z_RECV_POS_OFFSET = 27000;
        public const int AXIS_R_ALIGN_POS_OFFSET = 1000;

        public enum Direction { LeftToRight, RightToLeft, ToptoBottom, BottomToTop }

        public enum MOVE_POS : int
        {
            X_READY = 0,
            X_STATION = 1,
            X_STATION_BACK = 2,
            X_ULD_READY = 3,
            X_ULD_PUSH = 4,
            Y_CENTER = 5,
            Z_HANDLER = 6,
            Z_PLACE = 7,
            Z_VISION = 8,
            R_ALIGN = 9,
            R_FREE
        }

        public const string EQSendRequest = "EQSendRequest";
        public const string EQReadyQuery = "EQReadyQuery";
        public const string EQSendOut = "EQSendOut";
        public const string EQSendEnd = "EQSendEnd";
        public const string EQAbortTransfer = "EQAbortTransfer";

        public const string QCSendRequest = "QCSendRequest";
        public const string QCReadyQuery = "QCReadyQuery";
        public const string QCSendOut = "QCSendOut";
        public const string QCSendEnd = "QCSendEnd";
        public const string QCAbortTransfer = "QCAbortTransfer";

        //public const string QCSendRequest = "QCSendRequest";
        //public const string QCReadyQueryOK = "QCReadyQuery";
        //public const string QCReadyQueryNG = "QCReadyQuery";
        //public const string QCSendOut = "QCSendOut";
        //public const string QCSendEnd = "QCSendEnd";
        //public const string QCAbortTransfer = "QCAbortTransfer";


        public const string AutoRun = "AutoRun";
        public const string AutoStop = "AutoStop";
        public const string ErrorReset = "ErrorReset";
        public const string LotEnd = "LotEnd";
        public const string DoorLock = "DoorLock";
        public const string JobChange = "JobChange";
        public const string EmgStop = "EmgStop";
        public const string Buzzer = "Buzzer";
        public const string StatusQuery = "StatusQuery";
        public const string ResultQuery = "ResultQuery";
        public const string OK = "OK";
        public const string NG = "NG";
        public const string GOOD = "GOOD";
        public const string BYPASS = "Bypass";
        public const string TEST = "Test";
        public const string ON = "Von";
        public const string OFF = "Voff";
        public const string ACK = "ACK";

        //public const string SendRequest = "SendRequest";
        //public const string ReadyQuery = "ReadyQuery";
        //public const string SendOut = "SendOut";
        //public const string SendEnd = "SendEnd";
        //public const string AbortTransfer = "AbortTransfer";
        //public const string AutoRun = "AutoRun";
        //public const string AutoStop = "AutoStop";
        //public const string ErrorReset = "ErrorReset";
        //public const string LotEnd = "LotEnd";
        //public const string DoorLock = "DoorLock";
        //public const string JobChange = "JobChange";
        //public const string EmgStop = "EmgStop";
        //public const string StatusQuery = "StatusQuery";
        //public const string ResultQuery = "ResultQuery";
        //public const string OK = "OK";
        //public const string NG = "NG";
        //public const string GOOD = "GOOD";
        //public const string BYPASS = "Bypass";
        //public const string TEST = "Test";
        //public const string ON = "On";
        //public const string OFF = "Off";


        //public enum MSG_RECV
        //{
        //    SendRequest = 0,
        //    ReadyQuery,
        //    SendOut,
        //    SendEnd,
        //    AbortTransfer,
        //    AutoRun,
        //    AutoStop,
        //    ErrorReset,
        //    LotEnd,
        //    DoorLock,
        //    JobChange,
        //    EmgStop,
        //    StatusQuery,
        //    ResultQuery
        //}
    }
}
