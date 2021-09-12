using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace Pro_Swapper
{
    public partial class Message : Form
    {
        public Message(string title, string error, bool close)
        {
            InitializeComponent();
            label1.Text = title;
            richTextBox1.Text = error;
            Icon = Main.appIcon;
            Region = Region.FromHrgn(Main.CreateRoundRectRgn(0, 0, Width, Height, 50, 50));
            ExitButton.Click += delegate
            {
                if (close) 
                    Process.GetCurrentProcess().Kill();
                else 
                    Close();
            };

            this.MouseDown += delegate (object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left) 
                    global.FormMove(Handle);
            };
        }
        private void ThemeCreator_MouseDown(object sender, MouseEventArgs e)
        {
            
        }
    }
}
