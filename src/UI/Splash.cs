using System.Drawing;
using System.Windows.Forms;

namespace Pro_Swapper.UI
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
            BackgroundImage = Properties.Resources.pro_swapper_splash;
            pictureBox1.Image = Properties.Resources.Eclipse_0_7s_104px__1_;
            Icon = Main.appIcon;
            Region = Region.FromHrgn(Main.CreateRoundRectRgn(0, 0, Width, Height, 50,50));
            WindowState = FormWindowState.Normal;
        }

        private void Splash_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) global.FormMove(Handle);
        }

        private void Splash_Load(object sender, System.EventArgs e)=> Activate();
    }
}
