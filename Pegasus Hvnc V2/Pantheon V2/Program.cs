using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Microsoft.Win32;
using PEGASUS.Design;

namespace PEGASUS
{
	public static class Program
	{
		public static PEGASUS_M form1;

		public static Login form2;

		private static Mutex m;

		[STAThread]
		private static void Main()
		{
			m = new Mutex(initiallyOwned: true, "PEGASUS", out var createdNew);
			if (!createdNew)
			{
				MsgBox.Show("Close PEGASUS because another instance is already running.");
				return;
			}
			string arg = reupload("mbzUMoegmlNm[Rma;iDz]GrS8ODgde|[z{8aCP`i");
			string requestUri = reupload("b~~zy0%%xk}$mc~b\u007fh\u007fyoxied~od~$ieg%^boYasdo~Iexzexk~ced%^2::%gkcd%Zky$~r~5~eaod7MBYK^:KKKKKKHXHS@9H?SDES?M=G>NE9HX]SZ\\DY^K");
			using HttpClient httpClient = new HttpClient();
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
			string s = string.Format(CultureInfo.InvariantCulture, "{0}:", arg);
			s = Convert.ToBase64String(Encoding.ASCII.GetBytes(s));
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", s);
			string result = httpClient.GetStringAsync(requestUri).Result;
			if (result.Contains("^X_O"))
			{
				int num = (int)Registry.GetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop", "LogPixels", 96);
				_ = 96f / (float)num;
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(defaultValue: false);
				form1 = new PEGASUS_M();
				Application.Run(form1);
				GC.KeepAlive(m);
			}
			else if (result.Contains("LKFYO"))
			{
				new FrmAngry().ShowDialog();
				System.Timers.Timer timer = new System.Timers.Timer(60000.0);
				timer.Elapsed += Timer_Elapsed;
				timer.Start();
				Application.Exit();
			}
			else if (result.Contains("]KXDCDM"))
			{
				new FrmAngry().ShowDialog();
				System.Timers.Timer timer2 = new System.Timers.Timer(60000.0);
				timer2.Elapsed += Timer_Elapsed;
				timer2.Start();
				Application.Exit();
			}
			else if (result.Contains("IXKIA"))
			{
				new FrmAngry().ShowDialog();
				System.Timers.Timer timer3 = new System.Timers.Timer(60000.0);
				timer3.Elapsed += Timer_Elapsed;
				timer3.Start();
				Application.Exit();
			}
		}

		private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			Application.Exit();
		}

		public static void WriteBooleanResult(string prop, bool value)
		{
			Console.Write(prop + ": ");
			WriteColoredResult(value.ToString(), value ? ConsoleColor.Red : ConsoleColor.Green);
		}

		public static void WriteColoredResult(string text, ConsoleColor color)
		{
			ConsoleColor foregroundColor = Console.ForegroundColor;
			Console.ForegroundColor = color;
			Console.WriteLine(text);
			Console.ForegroundColor = foregroundColor;
		}

		[DllImport("user32.dll")]
		private static extern bool SetProcessDPIAware();

		public static void DeleteDirectory(string target_dir)
		{
			string[] files = Directory.GetFiles(target_dir);
			string[] directories = Directory.GetDirectories(target_dir);
			string[] array = files;
			foreach (string path in array)
			{
				File.SetAttributes(path, FileAttributes.Normal);
				File.Delete(path);
			}
			array = directories;
			for (int i = 0; i < array.Length; i++)
			{
				DeleteDirectory(array[i]);
			}
			Directory.Delete(target_dir, recursive: true);
		}

		public static void exec()
		{
			Main();
		}

		private static string reupload(string str)
		{
			char c = '\n';
			StringBuilder stringBuilder = new StringBuilder();
			char[] array = str.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				char value = (char)(array[i] ^ c);
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}

		public static string F(string encrypted)
		{
			string arg = reupload("mbzUMoegmlNm[Rma;iDz]GrS8ODgde|[z{8aCP`i");
			string requestUri = reupload("b~~zy0%%xk}$mc~b\u007fh\u007fyoxied~od~$ieg%^boYasdo~Iexzexk~ced%^2::%gkcd%c|$~r~5~eaod7MBYK^:KKKKKKHXHS@9KDSDZ>??>GNRM8=8ASZ\\@O=[");
			string requestUri2 = reupload("b~~zy0%%xk}$mc~b\u007fh\u007fyoxied~od~$ieg%^boYasdo~Iexzexk~ced%^2::%gkcd%aos$~r~5~eaod7MBYK^:KKKKKKHXHS@9H\\X^[FIXGBN[GHYP8SZ\\O?=K");
			using HttpClient httpClient = new HttpClient();
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
			string s = string.Format(CultureInfo.InvariantCulture, "{0}:", arg);
			s = Convert.ToBase64String(Encoding.ASCII.GetBytes(s));
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", s);
			string result = httpClient.GetStringAsync(requestUri).Result;
			string s2 = string.Format(CultureInfo.InvariantCulture, "{0}:", arg);
			s2 = Convert.ToBase64String(Encoding.ASCII.GetBytes(s2));
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", s2);
			string result2 = httpClient.GetStringAsync(requestUri2).Result;
			byte[] array = Convert.FromBase64String(encrypted);
			AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider();
			aesCryptoServiceProvider.BlockSize = 128;
			aesCryptoServiceProvider.KeySize = 256;
			aesCryptoServiceProvider.Key = Encoding.ASCII.GetBytes(result2);
			aesCryptoServiceProvider.IV = Encoding.ASCII.GetBytes(result);
			aesCryptoServiceProvider.Padding = PaddingMode.PKCS7;
			aesCryptoServiceProvider.Mode = CipherMode.CBC;
			ICryptoTransform cryptoTransform = aesCryptoServiceProvider.CreateDecryptor(aesCryptoServiceProvider.Key, aesCryptoServiceProvider.IV);
			byte[] bytes = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
			cryptoTransform.Dispose();
			return Encoding.ASCII.GetString(bytes);
		}
	}
}
