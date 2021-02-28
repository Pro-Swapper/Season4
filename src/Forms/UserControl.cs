using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using Newtonsoft.Json;
namespace Pro_Swapper
{
    public partial class UserControl : System.Windows.Forms.UserControl
    {
        public UserControl(string tab)
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            HorizontalScroll.Enabled = false;
            skinsflowlayout.BackColor = global.ItemsBG;
            int buttonx = 134;
            int buttony = 141;
            
            switch (tab)
            {
                case "Dashboard":
                    Dashboard();
                    new Thread(SetNews).Start();
                    skinsflowlayout.Dispose();
                    break;
                default:
                    
                    foreach (global.Item item in global.ItemList)
                        {
                        
                        if (item.itemType != tab) continue;
                            AddItem(item.ID, buttonx, buttony, item, numberButton_Click);
                        }
                    break;
            }
        }
        #region Dashboard
        private PictureBox news = new PictureBox();
        private Label label3 = new Label();
        private Label label2 = new Label();
        private RichTextBox patchnotes = new RichTextBox();
        private RichTextBox newstext = new RichTextBox();



        //https://json2csharp.com/
        public class Data
        {
            public string image { get; set; }
        }
        public class fnapi
        {
            public int status { get; set; }
            public Data data { get; set; }
        }
        private void SetNews()
        {
            try
            {
                CheckForIllegalCrossThreadCalls = false;
                news.ImageLocation = JsonConvert.DeserializeObject<fnapi>(new WebClient().DownloadString("https://fortnite-api.com/v2/news/br")).data.image;
            }
            catch { }
        }
        private void Dashboard()
        {
            // 
            // news
            // 
            this.news.Cursor = Cursors.Arrow;
            this.news.ErrorImage = null;
            this.news.InitialImage = null;
            this.news.Location = new Point(479, 113);
            this.news.Name = "news";
            this.news.Size = new Size(501, 357);
            this.news.SizeMode = PictureBoxSizeMode.Zoom;
            this.news.TabIndex = 1;
            this.news.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = Color.Transparent;
            this.label3.Font = new Font("Century Gothic", 48F, FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = Color.White;
            this.label3.Location = new Point(21, 33);
            this.label3.Name = "label3";
            this.label3.Size = new Size(403, 77);
            this.label3.TabIndex = 2;
            this.label3.Text = "Patch Notes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = Color.Transparent;
            this.label2.Font = new Font("Century Gothic", 48F, FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = Color.White;
            this.label2.Location = new Point(663, 33);
            this.label2.Name = "label2";
            this.label2.Size = new Size(200, 77);
            this.label2.TabIndex = 3;
            this.label2.Text = "News";
            // 
            // patchnotes
            // 
            this.patchnotes.BorderStyle = BorderStyle.None;
            this.patchnotes.Cursor = Cursors.Arrow;
            this.patchnotes.Font = new Font("Segoe UI", 12F);
            this.patchnotes.ForeColor = SystemColors.ControlLightLight;
            this.patchnotes.Location = new Point(23, 182);
            this.patchnotes.Name = "patchnotes";
            this.patchnotes.ReadOnly = true;
            this.patchnotes.Size = new Size(383, 405);
            this.patchnotes.TabIndex = 4;
            this.patchnotes.TabStop = false;
            this.patchnotes.Text = "";
            // 
            // newstext
            // 
            this.newstext.BorderStyle = BorderStyle.None;
            this.newstext.Cursor = Cursors.Arrow;
            this.newstext.Font = new Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newstext.ForeColor = SystemColors.ControlLightLight;
            this.newstext.Location = new Point(479, 476);
            this.newstext.Name = "newstext";
            this.newstext.ReadOnly = true;
            this.newstext.Size = new Size(501, 127);
            this.newstext.TabIndex = 5;
            this.newstext.TabStop = false;
            this.newstext.Text = "";
            // 
            // Dashboard
            // 
            this.AutoScaleMode = AutoScaleMode.Inherit;
            this.Controls.Add(this.newstext);
            this.Controls.Add(this.news);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.patchnotes);
            this.Name = "Dashboard";

            BackColor = global.MainMenu;
            patchnotes.BackColor = global.MainMenu;
            label2.ForeColor = global.TextColor;
            label3.ForeColor = global.TextColor;
            newstext.BackColor = BackColor;
            patchnotes.Text = "Update " + global.version + Environment.NewLine + Program.apidata.patchnotes;
            newstext.Text = Program.apidata.newstext;
        }
        #endregion

        void numberButton_Click(object sender, EventArgs e)=> new swap((int)((PictureBox)sender).Tag).Show();
        void AddItem(int i, int buttonx, int buttony,global.Item item, EventHandler type)
        {
            PictureBox button = new PictureBox();
            button.InitialImage = null;
            button.SizeMode = PictureBoxSizeMode.Zoom;
            button.ImageLocation = "https://cdn.discordapp.com/attachments/" + item.imgurl;
            button.Tag = i;
            button.Size = new Size(buttonx, buttony);
            button.Click += type;
            button.Cursor = Cursors.Hand;

            Label lbl = new Label();
            lbl.Text = item.swapsTo;
            lbl.ForeColor = global.TextColor;
            lbl.Font = new Font("Segoe UI", 8.5f, FontStyle.Regular);
            lbl.Location = new Point(35, 149);


            Panel panel = new Panel();
            panel.Size = new Size(buttonx, buttony + 35);
            panel.Controls.Add(button);
            panel.Controls.Add(lbl);
            skinsflowlayout.Controls.Add(panel);
        }
    }
}