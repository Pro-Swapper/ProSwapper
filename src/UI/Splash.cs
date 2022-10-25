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
            Region = Native.RoundedFormRegion(Width, Height);
            WindowState = FormWindowState.Normal;
        }
        private void Splash_MouseDown(object sender, MouseEventArgs e) => global.MoveForm(e, Handle);
        private void Splash_Load(object sender, System.EventArgs e) => Activate();
    }
}
