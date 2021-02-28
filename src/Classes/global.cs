using System;
using System.Text;
using System.IO;
using System.Reflection;
using System.IO.Compression;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Net;
using System.Drawing.Imaging;

namespace Pro_Swapper
{
    public class global
    {
        public static string version = Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(2, 5);
        #region RoundedCorners
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        public static Region Rounded(int Width, int Height)
        {
            return Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
        }
        #endregion


        public static Image ItemIcon(string url)
        {
            string rawpath = ProSwapperFolder + @"Images\";
            CreateDir(rawpath);
            string path = rawpath + url;
            string imageurl = "https://i.imgur.com/" + url + ".png";
        //Downloads image if doesnt exists
        start: if (!File.Exists(path))
                new WebClient().DownloadFile(imageurl, path);


            try
            {
                Image img;
                using (Bitmap bmpTemp = new Bitmap(path))
                {
                    img = new Bitmap(bmpTemp);
                }
                if (IsImage(img))
                {
                    return img;
                }
                else
                {
                    img.Dispose();
                    throw new Exception();
                }


            }
            catch
            {
                File.Delete(path);
                goto start;
            }
        }
        private static bool IsImage(Image imagevar)
        {
            try
            {
                Image imgInput = imagevar;
                Graphics gInput = Graphics.FromImage(imgInput);
                ImageFormat thisFormat = imgInput.RawFormat;
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Settings Writer
        public static string settingspath()
        {
            string path = ProSwapperFolder + @"Config\" + version + "_config.txt";
            CreateDir(ProSwapperFolder + @"Config\");
            if (!File.Exists(path))
                {
                using (StreamWriter a = new StreamWriter(path))
                {
                    foreach (Setting foo in Enum.GetValues(typeof(Setting)))
                    {
                        a.WriteLine(foo + "=");
                    }
                }
               WriteSetting("0,33,113;64,85,170;65,105,255;255,255,255", Setting.theme);
                }
                return path;
        }

        public static Color MainMenu, Button, TextColor, ItemsBG;
        public static string ProSwapperFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Pro_Swapper\";
        private static void CreateDir(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
        public static void WriteSetting(string newText, Setting value)
        {
            string line;
            int counter = 1;
            string text = File.ReadAllText(settingspath());
            using (StringReader reader = new StringReader(text))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith(value + "="))
                    {
                        lineChanger(value + "=" + newText, settingspath(), counter);
                        break;
                    }
                    counter++;
                }
            }
        }
        private static void lineChanger(string newText, string fileName, int line_to_edit)
        {
            string[] arrLine = File.ReadAllLines(fileName);
            arrLine[line_to_edit - 1] = newText;
            File.WriteAllLines(fileName, arrLine);
        }
        public static string ReadSetting(Setting value)
        {
            string line;
            using (StreamReader file = new StreamReader(settingspath()))
            {
                for (int counter = 0; (line = file.ReadLine()) != null; counter++)
                {
                    if (line.StartsWith(value + "="))
                        return line.Replace(value + "=","");
                }
                return null;
            }
        }
        public enum Setting
        {
            Paks1420,
            theme,
            lastopened,
            swaplogs1420
        }
        public static string Decompress(string input)
        {
            byte[] decompressed = Decompress(Convert.FromBase64String(input));
            return Encoding.UTF8.GetString(decompressed);
        }
        public static byte[] Decompress(byte[] input)
        {
            using (var source = new MemoryStream(input))
            {
                byte[] lengthBytes = new byte[4];
                source.Read(lengthBytes, 0, 4);

                var length = BitConverter.ToInt32(lengthBytes, 0);
                using (var decompressionStream = new GZipStream(source,
                    CompressionMode.Decompress))
                {
                    var result = new byte[length];
                    decompressionStream.Read(result, 0, length);
                    return result;
                }
            }
        }

        public static string GetLine(string text, int lineNo)
        {
            string[] lines = text.Replace("\r", "").Split('\n');
            return lines.Length >= lineNo ? lines[lineNo - 1] : null;
        }

    public static bool IsOodle(string path)
        {
            if (File.Exists(path + @"\\pakChunkEarly-WindowsClient.ucas"))
                return true;
            else
                return false;
        }

        public static List<Item> ItemList = new List<Item>();
        public class Item
        {
            public int ID { get; set; }
            public string swapsFrom { get; set; }
            public string swapsTo { get; set; }
            public string URL { get; set; }
            public string imgurl { get; set; }
            public string itemType { get; set; }
            public string swapsfromImageURL { get; set; }
            public Item(int itemID, string itemtype, string swapsfrom, string swapsto, string imageURL, string swapsFromImageURL, string url)
            {
                ID = itemID;
                itemType = itemtype;
                URL = url;
                imgurl = imageURL;
                swapsfromImageURL = swapsFromImageURL;
                swapsFrom = swapsfrom;
                swapsTo = swapsto;
            }
        }
    }
}
