using System.Drawing;
using System.Windows.Forms;
namespace Pro_Swapper
{
    public partial class Message : Form
    {
        public static bool CloseOnExit { get; set; }
        public Message(string title, string error, bool close)
        {
            InitializeComponent();
            label1.Text = title;
            richTextBox1.Text = error;
            Icon = Main.appIcon;
            Region = Region.FromHrgn(Main.CreateRoundRectRgn(0, 0, Width, Height, 50, 50));
            CloseOnExit = close;
        }
        private void ThemeCreator_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Main.FormMove(Handle);
        }
        private void ExitButton_Click(object sender, System.EventArgs e)
        {
            if (CloseOnExit)
                Main.Cleanup();
            else
                Close();
        }
    }
}
