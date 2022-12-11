using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using IntelligentFactory;
using System.Reflection;

namespace InControls.PLC.Mitsubishi
{
	// ########################################################################################
	public class McProtocolTcp : McProtocolBinary
	{
		System.Diagnostics.Stopwatch _watch = new System.Diagnostics.Stopwatch();

		public TcpClient Client { get; set; }
		private NetworkStream Stream { get; set; }

		// ====================================================================================
		public McProtocolTcp() : this("", 0) { }
		public McProtocolTcp(string iHostName, int iPortNumber)
			: base(iHostName, iPortNumber)
		{
			Client = new TcpClient();
		}

		override protected void DoConnect()
		{
			TcpClient c = Client;
			if (!c.Connected) 
			{
				// Keep Alive機能の実装
				var ka = new List<byte>(sizeof(uint) * 3);
				ka.AddRange(BitConverter.GetBytes(1u));
				ka.AddRange(BitConverter.GetBytes(45000u));
				ka.AddRange(BitConverter.GetBytes(5000u));
				c.Client.IOControl(IOControlCode.KeepAliveValues, ka.ToArray(), null);
				try 
				{
					c.Connect(HostName, PortNumber);
					Stream = c.GetStream();
				} 
				catch (SocketException e1) 
				{
					throw new Exception("连接到PLC（" + HostName + ":" + PortNumber + "）失败！", e1);
				}
			}
		}

		// ====================================================================================
		override protected void DoDisconnect()
		{
			TcpClient c = Client;
			if (c.Connected) {
				c.Close();
			}
		}
		// ================================================================================
		override protected byte[] Execute(byte[] iCommand)
		{
			var buff = new byte[256];
			try
            {
				NetworkStream ns = Stream;
				ns.Write(iCommand, 0, iCommand.Length);
				ns.Flush();

				_watch.Restart();

				ns.ReadTimeout = 5000;

				using (var ms = new MemoryStream())
				{

					do
					{
						int sz = ns.Read(buff, 0, buff.Length);
						if (sz == 0)
						{
							Debug.WriteLine("读TCP流失败，网络已断");
						}
						else
						{
							ms.Write(buff, 0, sz);
						}
					} while (ns.DataAvailable && _watch.ElapsedMilliseconds > 10);

					return ms.ToArray();
				}
			}
			catch (Exception Desc)
			{
				ILogger.Add(LOG_TYPE.ERROR, "[FAILED] {0}==>{1} Execption ==> {2}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, Desc.Message);
				return buff;
			}
		}
	}
}
