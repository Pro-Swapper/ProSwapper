using System;
using System.Drawing;
using System.Windows.Forms;
using Pro_Swapper.API;
using System.Linq;
namespace Pro_Swapper
{
    public partial class UserControl : System.Windows.Forms.UserControl
    {
        public UserControl(string tab)
        {
            InitializeComponent();
            foreach (api.Item item in api.apidata.items.Where(x => x.Type.Equals(tab)))
                AddItem(Array.IndexOf(api.apidata.items, item), item);

            foreach (api.Swapoption swapoption in api.apidata.swapoptions.Where(x => x.Type.Equals(tab)))
                AddSwapOption(swapoption.ItemIndexs.ToArray(), swapoption.Title);

            if (skinsflowlayout.Controls.Count == 0) 
                MessageBox.Show($"{tab} is currently disabled, please be patient for the developer(s) of Pro Swapper to add this feature. If you would like to request a feature please send a message on the Discord server", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            HorizontalScroll.Enabled = false;
            skinsflowlayout.BackColor = global.ItemsBG;
        }
        private void AddItem(int i, api.Item item)
        {
            if (item.ShowMain == false)
                return;

            int buttonx = 134;
            int buttony = 141;
            PictureBox picturebox = new PictureBox
            {
                Image = global.ItemIcon(item.ToImage),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(buttonx, buttony),
                Cursor = Cursors.Hand
            };
            picturebox.Click += delegate
            {
                new OodleSwap(i).Show();
            };
            Label lbl = new Label
            {
                Text = item.SwapsTo,
                ForeColor = global.TextColor,
                Font = new Font("Segoe UI", 8f, FontStyle.Regular),
                Location = new Point(picturebox.Location.X, picturebox.Location.Y + 140),
                TextAlign = ContentAlignment.TopCenter,
                Anchor = AnchorStyles.Top
            };

            Panel panel = new Panel
            {
                Size = new Size(buttonx + 10, buttony + 30)
            };
            panel.Controls.Add(picturebox);
            panel.Controls.Add(lbl);
            skinsflowlayout.Controls.Add(panel);
        }


        private void AddSwapOption(int[] indexs, string title)
        {
            int buttonx = 134;
            int buttony = 141;
            PictureBox picturebox = new PictureBox
            {
                Image = global.ItemIcon(api.apidata.items[indexs[0]].ToImage),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(buttonx, buttony),
                Cursor = Cursors.Hand
            };
            picturebox.Click += delegate
            {
                new SwapOption(indexs, title).Show();
            };
            Label lbl = new Label
            {
                Text = title,
                ForeColor = global.TextColor,
                Font = new Font("Segoe UI", 8f, FontStyle.Regular),
                Location = new Point(picturebox.Location.X, picturebox.Location.Y + 140),
                TextAlign = ContentAlignment.TopCenter,
                Anchor = AnchorStyles.Top
            };

            Panel panel = new Panel
            {
                Size = new Size(buttonx + 10, buttony + 30)
            };
            panel.Controls.Add(picturebox);
            panel.Controls.Add(lbl);
            skinsflowlayout.Controls.Add(panel);
        }
    }
}