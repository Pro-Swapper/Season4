using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace Pro_Swapper
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            new Thread(() =>
            {
                CheckForIllegalCrossThreadCalls = false;
                Thread.CurrentThread.IsBackground = true;
                Thread.CurrentThread.Priority = ThreadPriority.Highest;

                pictureBox7.Image = global.ItemIcon("0G7O3O2");
                pictureBox1.Image = global.ItemIcon("8Z9xRUU");
                pictureBox2.Image = global.ItemIcon("EHbHFjp");
                discord.Image = global.ItemIcon("ECo8w6F");
            }).Start();

            Region = global.Rounded(Width, Height);
            Icon = Main.appIcon;
            paksBox.Text = global.ReadSetting(global.Setting.Paks1420);
            BackColor = global.MainMenu;
            button2.BackColor = global.Button;
            button2.ForeColor = global.TextColor;

            button3.BackColor = global.Button;
            button3.ForeColor = global.TextColor;

            button5.BackColor = global.Button;
            button5.ForeColor = global.TextColor;

            button4.BackColor = global.Button;
            button4.ForeColor = global.TextColor;


            button10.BackColor = global.Button;
            button10.ForeColor = global.TextColor;
            Restart.BackColor = global.Button;
            Restart.ForeColor = global.TextColor;

            label13.ForeColor = global.TextColor;
            label1.ForeColor = global.TextColor;
            if (!Directory.Exists(paksBox.Text))
            NoPaks();

        }
        private void NoPaks()
        {
                MessageBox.Show("You haven't selected your Fortnite 14.20 Paks! Click the folder icon to set them!", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button1_Click(object sender, EventArgs e) => Close();
        private void SettingsForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Main.FormMove(Handle);
        }


        private void pictureBox7_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog paks = new FolderBrowserDialog())
            {
                paks.RootFolder = Environment.SpecialFolder.MyComputer;
                paks.Description = "Select your Fortnite Season 4 Paks folder";
                paks.ShowNewFolderButton = false;
                if (paks.ShowDialog() == DialogResult.OK)
                {
                    if (global.IsOodle(paks.SelectedPath))
                        MessageBox.Show("You selected the wrong Fortnite path! You need to select your Fortnite Season 4 (or older) path! If you don't have Fortnite Season 4 please download it! (It will take ~98GB of storage)", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        paksBox.Text = paks.SelectedPath;
                        global.WriteSetting(paks.SelectedPath, global.Setting.Paks1420);
                    }
                }
            }
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            if (paksBox.TextLength > 1)
                Process.Start(paksBox.Text);
            else
                NoPaks();

        }
        private void Restart_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.FriendlyName);
            Main.Cleanup();
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            string[] paks = Directory.GetFiles(global.ReadSetting(global.Setting.Paks1420));
            foreach (string file in paks)
            {
                if (file.Contains("pakchunk999"))
                    File.Delete(file);
            }
            
            global.WriteSetting("", global.Setting.swaplogs1420);
            MessageBox.Show("All items reset!", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            int converteditemno = global.ReadSetting(global.Setting.swaplogs1420).Length - global.ReadSetting(global.Setting.swaplogs1420).Replace(",", "").Length;
            if (converteditemno > 0)
                MessageBox.Show("You currently have " + converteditemno + " item(s) converted. The items you have converted are: " + global.ReadSetting(global.Setting.swaplogs1420), "Converted Items List", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("You have no items converted!", "Converted Items List", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (File.Exists(global.ReadSetting(global.Setting.Paks1420) + @"\pakchunk0-WindowsClient.sig"))
                MessageBox.Show("You have the right pak files selected.", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Please select the correct location of your paks folder", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)=> MessageBox.Show("Pro Swapper made by Kye#5000. https://github.com/kyeondiscord", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
        private void button10_Click(object sender, EventArgs e) => new ThemeCreator().ShowDialog();
        private void pictureBox1_Click(object sender, EventArgs e)=> Process.Start("https://youtube.com/proswapperofficial");
        private void pictureBox2_Click(object sender, EventArgs e)=> Process.Start("https://twitter.com/Pro_Swapper");
        private void discord_Click(object sender, EventArgs e) => Process.Start(Convert.ToString(Program.apidata.discordurl));
    }
}