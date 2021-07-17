using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
namespace Pro_Swapper.Splash
{
    public partial class SplashSettings : UserControl
    {
        public Settings settings;

        private readonly string SplashSettingsPath = $"{SplashUI.SplashPath}\\Settings.json";
        public SplashSettings()
        {
            InitializeComponent();
        }

            public class Settings
            {
                public string title { get; set; }
                public string executable { get; set; }
                public string parameters { get; set; }
                public string logo_position { get; set; }
                public string use_cmdline_parameters { get; set; }
                public string working_directory { get; set; }
                public string wait_for_game_process_exit { get; set; }
                public string hide_splash_screen { get; set; }
                public string hide_ui_controls { get; set; }
            }


        private void SplashSettings_Load(object sender, EventArgs e)
        {
            BackColor = global.MainMenu;
            settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(SplashSettingsPath));
            logoposBox.Text = settings.logo_position;
            DisableSplashScreenBox.Text = global.IntToBool(settings.hide_splash_screen);
            HideUIControlsBox.Text = global.IntToBool(settings.hide_ui_controls);
            UpdateLogoImage();
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.logo_position = logoposBox.Text;
            UpdateLogoImage();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.hide_splash_screen = global.BoolToInt(DisableSplashScreenBox.Text);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.hide_ui_controls = global.BoolToInt(HideUIControlsBox.Text);
        }
        public void Save()
        {
            string newsettings = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(SplashSettingsPath, newsettings);
        }

        private void UpdateLogoImage()
        {
            switch (settings.logo_position)
            {
                case "top-left":
                    LogoImage.Image = Properties.Resources.top_left;
                    break;
                case "bottom-right":
                    LogoImage.Image = Properties.Resources.bottom_right;
                    break;
            }
        }
    }
}
