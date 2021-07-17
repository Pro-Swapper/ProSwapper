using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Pro_Swapper.FOV
{
    public partial class FOV : Form
    {
        public static Rectangle currentScreen;

        private FOVUsercontrol FOVUC = new FOVUsercontrol();
        private StretchedUserControl StretchedUC = new StretchedUserControl();
        private bool IsShowingFOV = true;

        public FOV()
        {
            InitializeComponent();
            Icon = Main.appIcon;
            Region = Region.FromHrgn(Main.CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            if (!File.Exists(global.CurrentConfig.ConfigIni))
                global.CurrentConfig.ConfigIni = IniEditor.fndir;//Gets default GameUserSettings.ini file location


            textBox1.Text = global.CurrentConfig.ConfigIni;
            panel1.Controls.Add(FOVUC);
            currentScreen = Screen.FromControl(this).Bounds;
            label6.Text = "Current Monitor: " + currentScreen.Width + "x" + currentScreen.Height;
        }

        private void button1_Click(object sender, EventArgs e) => Close();

        private void textBox1_TextChanged(object sender, EventArgs e) => global.CurrentConfig.ConfigIni = textBox1.Text;

        private void label6_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                global.FormMove(Handle);
                currentScreen = Screen.FromControl(this).Bounds;
                label6.Text = "Current Monitor: " + currentScreen.Width + "x" + currentScreen.Height;
             } 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            if (IsShowingFOV)
            {
                panel1.Controls.Add(StretchedUC);
                button3.Text = "Show FOV Changer";
                IsShowingFOV = false;
            }
            else
            {
                panel1.Controls.Add(FOVUC);
                button3.Text = "Show Stretched Resolution";
                IsShowingFOV = true;
            }
                
        }
    }
}
