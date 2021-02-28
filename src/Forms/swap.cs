using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Drawing;
using System.Threading;
namespace Pro_Swapper
{
    public partial class swap : Form
    {
        private static global.Item SelectedItem;
        public swap(int item)
        {
            InitializeComponent();
            BackColor = global.ItemsBG;
            log.BackColor = global.ItemsBG;
            SwapB.ForeColor = global.TextColor;
            SwapB.BackColor = global.Button;
            log.ForeColor = global.TextColor;
            SelectedItem = global.ItemList[item];
            Text = SelectedItem.swapsFrom + " --> " + SelectedItem.swapsTo;
            image.ImageLocation = "https://cdn.discordapp.com/attachments/" + SelectedItem.swapsfromImageURL;
            if (global.ReadSetting(global.Setting.swaplogs1420).Contains(SelectedItem.swapsFrom + " To " + SelectedItem.swapsTo + ","))
            {
                label3.ForeColor = Color.Lime;
                label3.Text = "ON";
                SwapB.Text = "Revert";
            }
            else
            {
                label3.ForeColor = Color.Red;
                label3.Text = "OFF";
                SwapB.Text = "Convert";
            }
        }
        private void ConvertWork(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Thread.CurrentThread.Priority = ThreadPriority.Highest;
                CheckForIllegalCrossThreadCalls = false;
                log.Clear();
                string paksfolder = global.ReadSetting(global.Setting.Paks1420);
                if (File.Exists(paksfolder + "\\pakchunk0-WindowsClient.pak"))
                {
                    Stopwatch s = new Stopwatch();
                    s.Start();
                    log.Text = "Starting...";
                    try
                    {
                        string filepath = paksfolder + "\\pakchunk999" + SelectedItem.ID + "-WindowsClient";
                        if (SwapB.Text == "Convert")
                        {
                            //Convert
                            Program.DeleteIfExists(filepath + ".pak");
                            Program.DeleteIfExists(filepath + ".sig");
                            using (WebClient webClient = new WebClient())
                                webClient.DownloadFile("https://cdn.discordapp.com/attachments/" + SelectedItem.URL, filepath + ".pak");
                            File.Copy(global.ReadSetting(global.Setting.Paks1420) + "\\pakchunk1003-WindowsClient.sig", filepath + ".sig");
                            File.SetAttributes(filepath + ".pak", FileAttributes.Hidden | FileAttributes.System);
                            File.SetAttributes(filepath + ".sig", FileAttributes.Hidden | FileAttributes.System);
                            label3.Text = "ON";
                            label3.ForeColor = Color.Lime;
                            SwapB.Text = "Revert";
                            s.Stop();
                            log.Clear();
                            log.Text = "[+] Converted item in " + s.Elapsed.TotalMilliseconds + "ms";
                            global.WriteSetting(global.ReadSetting(global.Setting.swaplogs1420) + SelectedItem.swapsFrom + " To " + SelectedItem.swapsTo + ",", global.Setting.swaplogs1420);
                        }
                        else
                        {
                            //Revert
                            if (File.Exists(filepath + ".pak"))
                                File.SetAttributes(filepath + ".pak", FileAttributes.Normal);

                            if (File.Exists(filepath + ".sig"))
                                File.SetAttributes(filepath + ".sig", FileAttributes.Normal);

                            Program.DeleteIfExists(filepath + ".pak");
                            Program.DeleteIfExists(filepath + ".sig");
                            label3.Text = "OFF";
                            label3.ForeColor = Color.Red;
                            SwapB.Text = "Convert";
                            s.Stop();
                            log.Clear();
                            log.Text = "[-] Reverted item in " + s.Elapsed.TotalMilliseconds + "ms";
                            global.WriteSetting(global.ReadSetting(global.Setting.swaplogs1420).Replace(SelectedItem.swapsFrom + " To " + SelectedItem.swapsTo + ",", ""), global.Setting.swaplogs1420);
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Text = "Restart the swapper or refer to this error:" + ex.Message;
                    }
                }
                else
                    MessageBox.Show("Select your Fortnite 14.20 Paks Location in Settings! You may have not installed Fortnite 14.20 correctly.", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }).Start();
        }
    }
}