using System;
using System.Windows.Forms;

namespace Pro_Swapper
{
    public partial class Error : Form
    {
        public Error(string error)
        {
            InitializeComponent();
            richTextBox1.Text = error;
            Icon = Main.appIcon;
            Region = Region = global.Rounded(Width, Height);
        }
        private void ThemeCreator_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Main.FormMove(Handle);
        }
        private void button1_Click(object sender, EventArgs e) => Main.Cleanup();
    }
}