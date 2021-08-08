using System;
using System.Drawing;
using System.Windows.Forms;
using Pro_Swapper.API;
using System.Threading.Tasks;
namespace Pro_Swapper
{
    public partial class SwapOption : Form
    {
        public SwapOption(api.OptionMenu optionmenu)
        {
            InitializeComponent();
            RPC.SetState(optionmenu.Title.Split('|')[0] + " Swap Option", true);
            Icon = Main.appIcon;
            Region = Region.FromHrgn(Main.CreateRoundRectRgn(0, 0, Width, Height, 50, 50));
            
                Text = optionmenu.Title;
                label1.Text = optionmenu.Title;
                IsSwapOption = optionmenu.IsSwapOption;
                foreach (int i in optionmenu.ItemIndexs)
                    AddItem(api.apidata.items[i]);
        }

        //Either a swap option or style option
        private bool IsSwapOption = false;

        private void ThemeCreator_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) global.FormMove(Handle);
        }
        private void ExitButton_Click(object sender, EventArgs e) => Close();
        private void AddItem(api.Item item)
        {
            int buttonx = 154;
            int buttony = 161;
            PictureBox picturebox = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(buttonx, buttony),
                Cursor = Cursors.Hand
            };
                picturebox.Click += delegate
                {
                    new OodleSwap(item).Show();
                    Close();
                };
            Label lbl = new Label
            {
                ForeColor = global.TextColor,
                Font = new Font("Segoe UI", 7f, FontStyle.Regular),
                Location = new Point(picturebox.Location.X, picturebox.Location.Y + 160),
                TextAlign = ContentAlignment.TopCenter,
                Anchor = AnchorStyles.Top
            };


            if (IsSwapOption)
            {
                lbl.Text = item.SwapsFrom;
                picturebox.Image = global.ItemIcon(item.FromImage);
            }
            else
            {
                lbl.Text = item.SwapsTo;
                picturebox.Image = global.ItemIcon(item.ToImage);
            }
            Panel panel = new Panel
            {
                Size = new Size(buttonx + 10, buttony + 50)
            };
            panel.Controls.Add(picturebox);
            panel.Controls.Add(lbl);
            flowLayoutPanel1.Controls.Add(panel);
        }
    }
}
