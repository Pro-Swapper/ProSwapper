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
            Text = title;
            richTextBox1.Text = error;
            Icon = Main.appIcon;
            Region = Native.RoundedFormRegion(Width, Height, 50);
            ExitButton.Click += delegate
            {
                if (close)
                    Process.GetCurrentProcess().Kill();
                else
                    Close();
            };

            this.MouseDown += delegate (object sender, MouseEventArgs e)
            {
                global.MoveForm(e, Handle);
            };
        }
    }
}
