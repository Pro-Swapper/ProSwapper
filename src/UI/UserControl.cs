using System;
using System.Drawing;
using System.Windows.Forms;
using Pro_Swapper.API;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Pro_Swapper
{
    public partial class UserControl : System.Windows.Forms.UserControl
    {
        public UserControl(string tab)
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            HorizontalScroll.Enabled = false;
            skinsflowlayout.BackColor = global.ItemsBG;
            Tag = tab;
        }
        private async Task<Panel> AddItem(int i, api.Item item)
        {
            CheckForIllegalCrossThreadCalls = false;
            if (item.ShowMain == false)
                return null;

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
            return await Task.FromResult(panel);
        }

        private async Task<Panel> AddOption(api.OptionMenu option)
        {
            int buttonx = 134;
            int buttony = 141;
            PictureBox picturebox = new PictureBox
            {
                Image = global.ItemIcon(option.MainIcon),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(buttonx, buttony),
                Cursor = Cursors.Hand
            };
            picturebox.Click += delegate
            {
                new SwapOption(option).Show();
            };
            Label lbl = new Label
            {
                Text = option.Title,
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
            return await Task.FromResult(panel);
        }
        private async void UserControl_Load(object sender, EventArgs e)
        {
            List<Task<Panel>> icons = new List<Task<Panel>>();

            foreach (api.Item item in api.apidata.items.Where(x => x.Type.Equals(Tag)))
                icons.Add(Task.Run(() => AddItem(Array.IndexOf(api.apidata.items, item), item)));            
            foreach (api.OptionMenu option in api.apidata.OptionMenu.Where(x => x.Type.Equals(Tag)))
                icons.Add(Task.Run(() => AddOption(option)));
            icons.RemoveAll(x => x.Result == null);
            await Task.WhenAll(icons);

            foreach (var a in icons)
                skinsflowlayout.Controls.Add(a.Result);

            if (skinsflowlayout.Controls.Count == 0)
                MessageBox.Show($"{Tag} is currently disabled, please be patient for the developer(s) of Pro Swapper to add this feature. If you would like to request a feature please send a message on the Discord server", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}