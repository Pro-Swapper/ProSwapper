using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Pro_Swapper.API;
namespace Pro_Swapper
{
    public partial class SwapOption : Form
    {
        public SwapOption(int[] itemarray, string title)
        {
            InitializeComponent();
            Text = title;
            label1.Text = title + " Swap Option";
            Icon = Main.appIcon;
            Region = Region.FromHrgn(Main.CreateRoundRectRgn(0, 0, Width, Height, 50, 50));
            foreach (int i in itemarray)
                    AddItem(api.apidata.items[i]);
        }
        private void ThemeCreator_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) global.FormMove(Handle);
        }
        private void ExitButton_Click(object sender, System.EventArgs e) => Close();
        private void AddItem(api.Item item)
        {
            int buttonx = 154;
            int buttony = 161;
            PictureBox picturebox = new PictureBox
            {
                Image = global.ItemIcon(item.ToImage),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(buttonx, buttony),
                Cursor = Cursors.Hand
            };
            picturebox.Click += delegate
            {
                new OodleSwap(Array.IndexOf(api.apidata.items, item)).Show();
                Close();
            };
            Label lbl = new Label
            {
                Text = item.SwapsTo,
                ForeColor = global.TextColor,
                Font = new Font("Segoe UI", 7f, FontStyle.Regular),
                Location = new Point(picturebox.Location.X, picturebox.Location.Y + 160),
                TextAlign = ContentAlignment.TopCenter,
                Anchor = AnchorStyles.Top
            };

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
