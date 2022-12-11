using IntelligentFactory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace InControls.PLC.Mitsubishi
{
	// ####################################################################################
	// 通信に使用するコマンドを表現するインナークラス
	public class McCommand
	{
		public McFrame FrameType { get; set; }         // フレーム種別
		private uint SerialNumber { get; set; }         // 
		private uint NetworkNumber { get; set; }        // 网络编号
		private uint PcNumber { get; set; }             // PC编号/PLC编号
		private uint IoNumber { get; set; }             // 请求目标模块IO编号
		private uint ChannelNumber { get; set; }        // 请求目标模块站编号
		private uint CpuTimer { get; set; }             // CPU监视定时器
		private int ResultCode { get; set; }            // 返回代码（如果没有返回，则为0xcccc）
		public byte[] Response { get; private set; }    // 応答データ

		public McCommand(McFrame iFrame)
		{
			FrameType = iFrame;
			SerialNumber = 0x0001u;
			NetworkNumber = 0x0000u;
			PcNumber = 0x00FFu;
			IoNumber = 0x03FFu;
			ChannelNumber = 0x0000u;
			CpuTimer = 0x0010u;
		}



		public byte[] SetCommand(uint iMainCommand, uint iSubCommand, byte[] iData)
		{
			var dataLength = (uint)(iData.Length + 6);
			var ret = new List<byte>(iData.Length + 20);
			uint frame = (FrameType.HasFlag(McFrame.MC3E)) ? 0x0050u :
						 (FrameType.HasFlag(McFrame.MC4E)) ? 0x0054u : 0x0000u;
			ret.Add((byte)frame);
			ret.Add((byte)(frame >> 8));
			if (FrameType.HasFlag(McFrame.MC4E)) {
				ret.Add((byte)(SerialNumber >> 8));
				ret.Add((byte)SerialNumber);
				ret.Add(0x00);
				ret.Add(0x00);
			}
			ret.Add((byte)NetworkNumber);
			ret.Add((byte)PcNumber);

			ret.Add((byte)(IoNumber >> 8));
			ret.Add((byte)IoNumber);

			ret.Add((byte)ChannelNumber);

			ret.Add((byte)(dataLength >> 8));
			ret.Add((byte)dataLength);

			ret.Add((byte)(CpuTimer >> 8));
			ret.Add((byte)CpuTimer);

			ret.Add((byte)(iMainCommand >> 8));
			ret.Add((byte)iMainCommand);

			ret.Add((byte)(iSubCommand >> 8));
			ret.Add((byte)iSubCommand);
			ret.AddRange(iData);
			return ret.ToArray();
		}

		public byte[] SetCommandMC3E(uint iMainCommand, uint iSubCommand, byte[] iData)
		{
			var dataLength = (uint)(iData.Length + 6);
			List<byte> ret = new List<byte>(iData.Length + 20);
			try
            {
				uint frame = 0x0050u;
				ret.Add((byte)frame);
				ret.Add((byte)(frame >> 8));

				ret.Add((byte)NetworkNumber);

				ret.Add((byte)PcNumber);

				ret.Add((byte)IoNumber);
				ret.Add((byte)(IoNumber >> 8));
				ret.Add((byte)ChannelNumber);
				ret.Add((byte)dataLength);
				ret.Add((byte)(dataLength >> 8));

				ret.Add((byte)CpuTimer);
				ret.Add((byte)(CpuTimer >> 8));
				ret.Add((byte)iMainCommand);
				ret.Add((byte)(iMainCommand >> 8));
				ret.Add((byte)iSubCommand);
				ret.Add((byte)(iSubCommand >> 8));

				ret.AddRange(iData);
			}
			catch (Exception Desc)
			{
				ILogger.Add(LOG_TYPE.ERROR, "[FAILED] {0}==>{1} Execption ==> {2}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, Desc.Message);
			}

			return ret.ToArray();
		}

		public byte[] SetCommandMC4E(uint iMainCommand, uint iSubCommand, byte[] iData)
		{
			var dataLength = (uint)(iData.Length + 6);
			var ret = new List<byte>(iData.Length + 20);
			uint frame = 0x0054u;
			try
            {
				ret.Add((byte)frame);
				ret.Add((byte)(frame >> 8));
				ret.Add((byte)SerialNumber);
				ret.Add((byte)(SerialNumber >> 8));
				ret.Add(0x00);
				ret.Add(0x00);
				ret.Add((byte)NetworkNumber);
				ret.Add((byte)PcNumber);
				ret.Add((byte)IoNumber);
				ret.Add((byte)(IoNumber >> 8));
				ret.Add((byte)ChannelNumber);
				ret.Add((byte)dataLength);
				ret.Add((byte)(dataLength >> 8));
				ret.Add((byte)CpuTimer);
				ret.Add((byte)(CpuTimer >> 8));
				ret.Add((byte)iMainCommand);
				ret.Add((byte)(iMainCommand >> 8));
				ret.Add((byte)iSubCommand);
				ret.Add((byte)(iSubCommand >> 8));

				ret.AddRange(iData);
			}
			catch (Exception Desc)
			{
				ILogger.Add(LOG_TYPE.ERROR, "[FAILED] {0}==>{1} Execption ==> {2}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, Desc.Message);
			}
			
			return ret.ToArray();
		}

		/// <summary>
		/// 按ASCII格式的3E格式构造
		/// 注意：需要颠倒int的顺序
		/// </summary>
		public byte[] SetCommand(uint iMainCommand, uint iSubCommand, string data)
		{
			var dataLength = (uint)(data.Length + 12);

			StringBuilder ret = new StringBuilder();
			ret.Append("5000");     // 头部
			ret.AppendFormat("{0:X2}", NetworkNumber);
			ret.AppendFormat("{0:X2}", PcNumber);
			ret.AppendFormat("{0:X4}", IoNumber);
			ret.AppendFormat("{0:X2}", ChannelNumber);
			ret.AppendFormat("{0:X4}", dataLength);
			ret.AppendFormat("{0:X4}", CpuTimer);
			ret.AppendFormat("{0:X4}", iMainCommand);
			ret.AppendFormat("{0:X4}", iSubCommand);
			ret.Append(data);

			return ASCIIEncoding.ASCII.GetBytes(ret.ToString());
		}

		public byte[] SetCommandMC1E(byte Subheader, byte[] iData)
		{
			List<byte> ret = new List<byte>(iData.Length + 4);
			ret.Add(Subheader);
			ret.Add((byte)this.PcNumber);
			ret.Add((byte)CpuTimer);
			ret.Add((byte)(CpuTimer >> 8));
			ret.AddRange(iData);
			return ret.ToArray();
		}

		public int SetResponse(byte[] iResponse)
		{
			int min;
			try
            {
				switch (FrameType)
				{
					//case McFrame.MC1E:
					//	min = 2;
					//	if (min <= iResponse.Length)
					//	{
					//		//There is a subheader, end code and data.                                    

					//		ResultCode = (int)iResponse[min - 2];
					//		Response = new byte[iResponse.Length - 2];
					//		Buffer.BlockCopy(iResponse, min, Response, 0, Response.Length);
					//	}
					//	break;
					case McFrame.MC3E:
						min = 11;
						if (min <= iResponse.Length)
						{
							var btCount = new[] { iResponse[min - 4], iResponse[min - 3] };
							var btCode = new[] { iResponse[min - 2], iResponse[min - 1] };
							int rsCount = BitConverter.ToUInt16(btCount, 0);
							ResultCode = BitConverter.ToUInt16(btCode, 0);
							Response = new byte[rsCount - 2];
							Buffer.BlockCopy(iResponse, min, Response, 0, Response.Length);
						}
						break;
					case McFrame.MC4E:
						min = 15;
						if (min <= iResponse.Length)
						{
							var btCount = new[] { iResponse[min - 4], iResponse[min - 3] };
							var btCode = new[] { iResponse[min - 2], iResponse[min - 1] };
							int rsCount = BitConverter.ToUInt16(btCount, 0);
							ResultCode = BitConverter.ToUInt16(btCode, 0);
							Response = new byte[rsCount - 2];
							Buffer.BlockCopy(iResponse, min, Response, 0, Response.Length);
						}
						break;
					default:
						throw new Exception("Frame type not supported.");

				}
			}
			catch (Exception Desc)
			{
				ILogger.Add(LOG_TYPE.ERROR, "[FAILED] {0}==>{1} Execption ==> {2}", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, Desc.Message);
			}
			
			return ResultCode;
		}

		public int SetResponseWrite(byte[] iResponse)
		{
			int min;

			// 일단 하
			min = 15;
			if (min <= iResponse.Length)
			{
				var btCount = new[] { iResponse[min - 4], iResponse[min - 3] };
				var btCode = new[] { iResponse[min - 2], iResponse[min - 1] };
				int rsCount = BitConverter.ToUInt16(btCount, 0);
				ResultCode = BitConverter.ToUInt16(btCode, 0);
				Response = new byte[rsCount - 2];
				Buffer.BlockCopy(iResponse, min, Response, 0, Response.Length);
			}
			return ResultCode;
		}

		public int SetResponse(byte[] responseBytes, McFrame mcFrame)
		{
			if (mcFrame.HasFlag(McFrame.ASCII_FLAG))
				return SetResponse(ASCIIEncoding.ASCII.GetString(responseBytes));

			ResultCode = 0xcccc;

			int min = (FrameType.HasFlag(McFrame.MC3E)) ? 11 : 15;
			if (min <= responseBytes.Length) {
				var btCount = new[] { responseBytes[min - 3], responseBytes[min - 4] };
				var btCode = new[] { responseBytes[min - 1], responseBytes[min - 2] };
				int rsCount = BitConverter.ToUInt16(btCount, 0);
				if (FrameType.HasFlag(McFrame.ASCII_FLAG)) rsCount = rsCount / 2;

				ResultCode = BitConverter.ToUInt16(btCode, 0);
				Response = new byte[rsCount - 2];
				Buffer.BlockCopy(responseBytes, min, Response, 0, Response.Length);
			}
			return ResultCode;
		}

		/// <summary>
		/// 按ASCII格式的3E格式构造
		/// 注意：需要颠倒int的顺序
		/// </summary>
		public int SetResponse(string responseText)
		{
			Debug.WriteLine(responseText);

			byte[] buffer = new byte[responseText.Length / 2];
			for (int i = 0; i < buffer.Length; i++) {
				byte b;
				byte.TryParse(responseText.Substring(i * 2, 2), NumberStyles.HexNumber, null, out b);
				buffer[i] = b;
			}
			return SetResponse(buffer, McFrame.MC3E);
		}

        public bool IsIncorrectResponse(byte[] iResponse)
        {
            if (iResponse.Length == 0)
                return false;

            var min = (FrameType.HasFlag(McFrame.MC3E)) ? 11 : 15;
            var btCount = new[] { iResponse[min - 3], iResponse[min - 4] };
            var btCode = new[] { iResponse[min - 1], iResponse[min - 2] };
            var rsCount = BitConverter.ToUInt16(btCount, 0) - 2;
            var rsCode = BitConverter.ToUInt16(btCode, 0);
            return (rsCode == 0 && rsCount != (iResponse.Length - min));
        }

        public bool IsIncorrectResponse(byte[] iResponse, int minLenght)
		{
			//TEST add 1E frame
			switch (this.FrameType)
			{
				case McFrame.MC3E:
				case McFrame.MC4E:
					var btCount = new[] { iResponse[minLenght - 4], iResponse[minLenght - 3] };
					var btCode = new[] { iResponse[minLenght - 2], iResponse[minLenght - 1] };
					var rsCount = BitConverter.ToUInt16(btCount, 0) - 2;
					var rsCode = BitConverter.ToUInt16(btCode, 0);
					return (rsCode == 0 && rsCount != (iResponse.Length - minLenght));

				default:
					throw new Exception("Type Not supported");

			}
		}
	}
}
