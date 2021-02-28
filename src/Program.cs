using System;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Threading;
using Newtonsoft.Json;
namespace Pro_Swapper
{
    static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string filename = AppDomain.CurrentDomain.FriendlyName;
            if (!filename.Contains("Pro") && !filename.Contains("Swapper"))
                ThrowError("This version of Pro Swapper has been modified (renamed) " + filename + " , please download the original Pro Swapper at https://proswapper.xyz/download");
            new Thread(CloseFN).Start();

            #region Checks
            try
            {
                using (WebClient web = new WebClient())
                {
                    string endpoint = "/s4.json";
                    string apidownloaded = string.Empty;
                    try
                    {
                        apidownloaded = web.DownloadString("https://pro-swapper.github.io/api" + endpoint);
                    }
                    catch
                    {
                        apidownloaded = web.DownloadString("https://raw.githubusercontent.com/Pro-Swapper/api/main" + endpoint);
                    }
                    apidata = JsonConvert.DeserializeObject<api>(apidownloaded);
                }
            }
            catch (Exception ex)
            {
                ThrowError("Pro Swapper needs an internet connection to run, if you are already connected to the internet Pro Swapper severs may be blocked in your country, please use a VPN or try disabling your firewall, if you are already doing this please refer to this error: \n\n" + ex);
            }
            string apiversion = apidata.version;
            //Does these if user isn't dev
            string thishr = DateTime.Now.ToString("MMddHH");
            if (global.ReadSetting(global.Setting.lastopened) != thishr)
            {
                string discordurl = apidata.discordurl;
                Process.Start(discordurl);//opens discord
                global.WriteSetting(thishr, global.Setting.lastopened);
            }

            if (!apiversion.Contains(global.version)) //if outdated
            {
                MessageBox.Show("New Pro Swapper Update found! We will redirect you to the new download!", "Pro Swapper Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start("https://link-to.net/86737/proswapperseason4");
                Pro_Swapper.Main.Cleanup();
            }

            if (apiversion.Contains("OFFLINE"))
                ThrowError("Pro Swapper is currently not working. Take a look at our Discord for any announcments");

            #endregion
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }


        #region CloseFN
        public static void CloseFN()
        {
            try
            {
                foreach (Process a in Process.GetProcesses())
                {
                    if (a.ProcessName.StartsWith("Fortnite") | a.ProcessName.Contains("EasyAntiCheat") | a.ProcessName.Contains("FortniteClient") | a.ProcessName.Contains("EpicGamesLauncher") | a.ProcessName.Contains("UnrealCEFSubProcess") | a.ProcessName.Equals("umodel") | a.ProcessName.Equals("FModel"))
                    {
                        if (a.ProcessName == "FortniteClient-Win64-Shipping")
                        {
                            MessageBox.Show("Closed Fortnite (Fortnite needs to be closed to use Pro Swapper)!", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        a.Kill();
                    }
                }
            }
            catch
            {
               ThrowError("Please close Fortnite and other related things!");
            }
        }
        #endregion


        // https://json2csharp.com/
        public class api
        {
            public string newstext { get; set; }
            public string patchnotes { get; set; }
            public string version { get; set; }
            public string discordurl { get; set; }
            public string items { get; set; }
        }
        public static api apidata;

        public static void ThrowError(string ex)=> new Error(ex).ShowDialog();
        public static void DeleteIfExists(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}