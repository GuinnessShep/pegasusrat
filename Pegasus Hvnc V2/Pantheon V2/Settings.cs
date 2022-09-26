using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using PEGASUS.Diadyktio;
using PEGASUS.Properties;

namespace PEGASUS
{
	public static class Settings
	{
		public static List<string> Blocked = new List<string>();

		public static object LockBlocked = new object();

		public static object LockReceivedSendValue = new object();

		public static string CertificatePath = Path.GetTempPath() + "\\PEGASUSCertificate.p12";

		public static X509Certificate2 PEGASUSCertificate;

		public static readonly string Version = versioning();

		public static object LockListviewClients = new object();

		public static object LockListviewLogs = new object();

		public static object LockListviewThumb = new object();

		public static bool ReportWindow = false;

		public static List<Clients> ReportWindowClients = new List<Clients>();

		public static object LockReportWindowClients = new object();

		public static string WebHook = Convert.ToString(PEGASUS.Properties.Settings.Default.WebHook);

		public static string TelAPI = Convert.ToString(PEGASUS.Properties.Settings.Default.TelAPI);

		public static string TelID = Convert.ToString(PEGASUS.Properties.Settings.Default.TelID);

		public static string IP = "";

		public static string pool = Convert.ToString(PEGASUS.Properties.Settings.Default.pool);

		public static string wallet = Convert.ToString(PEGASUS.Properties.Settings.Default.wallet);

		public static string password = Convert.ToString(PEGASUS.Properties.Settings.Default.password);

		public static string algo = Convert.ToString(PEGASUS.Properties.Settings.Default.algo);

		public static string threads = Convert.ToString(PEGASUS.Properties.Settings.Default.threads);

		public static string hook = Convert.ToString(PEGASUS.Properties.Settings.Default.hook);

		public static string message = Convert.ToString(PEGASUS.Properties.Settings.Default.message);

		public static string times = Convert.ToString(PEGASUS.Properties.Settings.Default.times);

		public static long SentValue { get; set; }

		public static long ReceivedValue { get; set; }

		public static string versioning()
		{
			return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
		}
	}
}
