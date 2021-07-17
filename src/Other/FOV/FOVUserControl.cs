using System;
using System.Windows.Forms;

namespace Pro_Swapper.FOV
{
    public partial class FOVUsercontrol : UserControl
    {
        public FOVUsercontrol()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int percentage = (int)Math.Round((decimal)trackBar1.Value / trackBar1.Maximum * 100);
            label4.Text = "Current Value: " + percentage + "%";
            label4.Tag = percentage;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IniEditor.SetRes(trackBar1.Value.ToString(), FOV.currentScreen.Width.ToString(), 2);
            MessageBox.Show("Set FOV to " + label4.Tag, "Pro Swapper FOV", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
