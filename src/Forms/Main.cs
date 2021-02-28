using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using DiscordRPC;
namespace Pro_Swapper
{
    public partial class Main : Form
    {
        public static Icon appIcon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
        public Main()
        {
            InitializeComponent();
            #region Set Global Themes
            ThemeCreator.ColorTranslate(global.ReadSetting(global.Setting.theme).Split(';'), out string[] panel1d, out string[] panel2d, out string[] panel3d, out string[] panel4d);
            global.MainMenu = Color.FromArgb(255, int.Parse(panel1d[0]), int.Parse(panel1d[1]), int.Parse(panel1d[2]));
            global.ItemsBG = Color.FromArgb(255, int.Parse(panel2d[0]), int.Parse(panel2d[1]), int.Parse(panel2d[2]));
            global.Button = Color.FromArgb(255, int.Parse(panel3d[0]), int.Parse(panel3d[1]), int.Parse(panel3d[2]));
            global.TextColor = Color.FromArgb(255, int.Parse(panel4d[0]), int.Parse(panel4d[1]), int.Parse(panel4d[2]));
            #endregion
            RPC.rpctimestamp = Timestamps.Now;
            RPC.InitializeRPC();
            NewPanel("Dashboard");
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            new Thread(LoadIcons).Start();
            Icon = appIcon;
            Region = global.Rounded(Width, Height);
            versionlabel.Text = global.version;
            #region ThemeSet
            BackColor = global.MainMenu;
            panel1.BackColor = global.MainMenu;
            bunifuFlatButton1.BackColor = global.Button;
            bunifuFlatButton1.IconZoom = 85;
            bunifuFlatButton1.Normalcolor = global.Button;
            bunifuFlatButton1.OnHovercolor = global.Button;
            bunifuFlatButton1.Activecolor = global.Button;
            bunifuFlatButton1.Textcolor = global.TextColor;
            bunifuFlatButton1.OnHoverTextColor = global.TextColor;

            bunifuFlatButton2.BackColor = global.Button;
            bunifuFlatButton2.Normalcolor = global.Button;
            bunifuFlatButton2.OnHovercolor = global.Button;
            bunifuFlatButton2.Activecolor = global.Button;
            bunifuFlatButton2.Textcolor = global.TextColor;
            bunifuFlatButton2.OnHoverTextColor = global.TextColor;
            bunifuFlatButton2.IconZoom = 90;

            bunifuFlatButton3.BackColor = global.Button;
            bunifuFlatButton3.Normalcolor = global.Button;
            bunifuFlatButton3.OnHovercolor = global.Button;
            bunifuFlatButton3.Activecolor = global.Button;
            bunifuFlatButton3.Textcolor = global.TextColor;
            bunifuFlatButton3.OnHoverTextColor = global.TextColor;
            bunifuFlatButton3.IconZoom = 60;

            bunifuFlatButton4.BackColor = global.Button;
            bunifuFlatButton4.Normalcolor = global.Button;
            bunifuFlatButton4.OnHovercolor = global.Button;
            bunifuFlatButton4.Activecolor = global.Button;
            bunifuFlatButton4.Textcolor = global.TextColor;
            bunifuFlatButton4.OnHoverTextColor = global.TextColor;
            bunifuFlatButton4.IconZoom = 100;

            bunifuFlatButton5.BackColor = global.Button;
            bunifuFlatButton5.Normalcolor = global.Button;
            bunifuFlatButton5.OnHovercolor = global.Button;
            bunifuFlatButton5.Activecolor = global.Button;
            bunifuFlatButton5.Textcolor = global.TextColor;
            bunifuFlatButton5.OnHoverTextColor = global.TextColor;
            bunifuFlatButton5.IconZoom = 85;

            bunifuFlatButton6.BackColor = global.Button;
            bunifuFlatButton6.Normalcolor = global.Button;
            bunifuFlatButton6.OnHovercolor = global.Button;
            bunifuFlatButton6.Activecolor = global.Button;
            bunifuFlatButton6.Textcolor = global.TextColor;
            bunifuFlatButton6.OnHoverTextColor = global.TextColor;
            bunifuFlatButton6.IconZoom = 85;
            versionlabel.ForeColor = global.TextColor;
            #endregion


            string iteminfo = global.Decompress(Program.apidata.items);
            int numLines = iteminfo.Split('\n').Length;
            for (int i = 0; i < numLines; i++)
            {
                string[] items = global.GetLine(iteminfo, i + 1).Split('|');
                global.ItemList.Add(new global.Item(i, items[0], items[1], items[2], items[3], items[4], items[5]));
            }
        }
        #region FormMoveable
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public static void FormMove(IntPtr Handle)
        {
            ReleaseCapture();
            SendMessage(Handle, 0xA1, 0x2, 0);
        }
        public void Main_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                FormMove(Handle);
        }
        #endregion
        #region LoadIcons
        private void LoadIcons()
        {
            CheckForIllegalCrossThreadCalls = false;
            pictureBox1.Image = global.ItemIcon("aBiQwFU");
            bunifuFlatButton1.Iconimage = global.ItemIcon("JCYJliG");
            bunifuFlatButton2.Iconimage = global.ItemIcon("0YAShwW");
            bunifuFlatButton3.Iconimage = global.ItemIcon("STNJqdv");
            bunifuFlatButton4.Iconimage = global.ItemIcon("cakmQuO");
            bunifuFlatButton5.Iconimage = global.ItemIcon("4z0xnJ5");
            bunifuFlatButton6.Iconimage = global.ItemIcon("CueE0Wg");
        }
        #endregion


        
        private void NewPanel(string tab)
        {
            string state = tab;
            switch (state)
            {
                case "Skin":
                    state = "Skins";
                    break;

                case "Backbling":
                    state = "Backblings";
                    break;

                case "Lobby":
                    state = "Lobby Backgrounds";
                    break;

                case "LS":
                    state = "Loading Screens";
                    break;

                case "Music":
                    state = "Music Packs";
                    break;
                    //Watching Dashboard is fine coz english works
                    /*
                case "Dashboard":
                    break;*/
            }
            RPC.SetState(state, true);
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(new UserControl(tab));
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e) => Cleanup();
        private void button1_Click(object sender, EventArgs e) => Cleanup();
        private void bunifuFlatButton1_Click(object sender, EventArgs e) => NewPanel("Skin");
        private void bunifuFlatButton2_Click(object sender, EventArgs e) => NewPanel("Backbling");
        private void bunifuFlatButton3_Click(object sender, EventArgs e)=> NewPanel("Lobby");
        private void bunifuFlatButton5_Click(object sender, EventArgs e) => NewPanel("LS");
        private void bunifuFlatButton4_Click(object sender, EventArgs e)=> NewPanel("Music");
        private void button2_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;
        private void pictureBox1_Click(object sender, EventArgs e) => NewPanel("Dashboard");

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            RPC.SetState("Settings", true); 
            new Settings().Show();
        }
        public static void Cleanup()=> Process.GetCurrentProcess().Kill();
    }
}