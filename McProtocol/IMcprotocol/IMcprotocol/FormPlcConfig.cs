using InControls.PLC.Mitsubishi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntelligentFactory
{
    public partial class FormPlcConfig : Form
    {
        McProtocol mcprotocol = new McProtocol();

        private IGlobal Global = IGlobal.Instance;

        public FormPlcConfig()
        {
            InitializeComponent();  
        }

        private void FormPlcConfig_Load(object sender, EventArgs e)
        {
            bool bIsPingOk = mcprotocol.PingTest();
            {
                mcprotocol.InitIO();
                mcprotocol.Open();
                mcprotocol.StartThreadRead();
            }

            //Global.System.McProtocol.GetAddress(out string strIp, out int nPort);
            //tbServerip.Text = strIp;
            //tbServerPort.Text = nPort.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //Global.System.McProtocol.SaveConfig();

            ILogger.Add(LOG_TYPE.SYSTEM, "[OK] {0}==>{1}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ILogger.Add(LOG_TYPE.SYSTEM, "[OK] {0}==>{1}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                string strServerIp = tbServerip.Text;
                int nServerPort = int.Parse(tbServerPort.Text);

               // Global.System.McProtocol.SetAddress(strServerIp, nServerPort);

                ILogger.Add(LOG_TYPE.SYSTEM, "[OK] {0}==>{1}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception Desc)
            {
                ILogger.Add(LOG_TYPE.ERROR, "[FAILED] {0}==>{1}   Ex ==> {2}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, Desc.Message);
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                short[] shWriteData;
                string strWriteValue = tbWriteValue.Text;
                int nAddress = int.Parse(tbAddress.Text);

                mcprotocol.ClientBinary.SetDevice(nAddress.ToString(), int.Parse(strWriteValue));

                var rtn = mcprotocol.ClientBinary.GetDevice(nAddress.ToString(), out int nData);

                lbReadValue.Text = nData.ToString();

                ILogger.Add(LOG_TYPE.SYSTEM, "[OK] {0}==>{1}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception Desc)
            {
                ILogger.Add(LOG_TYPE.ERROR, "[FAILED] {0}==>{1}   Ex ==> {2}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, Desc.Message);
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                short[] shReadData = new short[1]; ;
                string strReadValue = tbWriteValue.Text;
                int nAddress = int.Parse(tbAddress.Text);
                if (cbWordCount.SelectedIndex == 0)
                {
                    //var rtn = Global.System.McProtocol.ExecuteRead("D", nAddress, 1, ref shReadData);

                    var rtn = mcprotocol.ClientBinary.GetDevice(nAddress.ToString(), out int nData);

                    lbReadValue.Text = nData.ToString();
                }
                else
                {
                    int nValueW = 0;
                    int nData = 0;
                    int[] nDataArr = new int[2];
                    var rtn = mcprotocol.ClientBinary.ReadDeviceBlock(nAddress.ToString(), 2, nDataArr);

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

                    lbReadValue.Text = nValueW.ToString();
                }

                ILogger.Add(LOG_TYPE.SYSTEM, "[OK] {0}==>{1}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception Desc)
            {
                ILogger.Add(LOG_TYPE.ERROR, "[FAILED] {0}==>{1}   Ex ==> {2}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, Desc.Message);
            }
        }


    }
}