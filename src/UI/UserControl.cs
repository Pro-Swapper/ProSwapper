using System;
using System.Drawing;
using System.Windows.Forms;
namespace Pro_Swapper
{
    public partial class UserControl : System.Windows.Forms.UserControl
    {
        public UserControl(string tab)
        {
            InitializeComponent();
            int itemcount = 0;//Define the number of items in this form.
            for (int i = 0; i < global.items.Items.Count; i++)//not using foreach coz like we don't need the i var outside of it and ye i felt like it
            {
                if (global.items.Items[i].Type == tab)
                {
                    AddItem(i, global.items.Items[i], numberButton_Click);//add item onto form
                    itemcount++;//add item count
                }
            }
            if (itemcount == 0) MessageBox.Show(tab + " are currently disabled, this is due to Fortnite patching them. There is no estimated time to when " + tab + " will be available again.", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            HorizontalScroll.Enabled = false;
            skinsflowlayout.BackColor = global.ItemsBG;
        }
        void numberButton_Click(object sender, EventArgs e)=> new swap((int)((PictureBox)sender).Tag).Show();
        void AddItem(int i,Items.Item item, EventHandler type)
        {
            int buttonx = 134;
            int buttony = 141;
            PictureBox picturebox = new PictureBox
            {
                Image = global.ItemIcon(item.ToImage),
                SizeMode = PictureBoxSizeMode.Zoom,
                Tag = i,
                Size = new Size(buttonx, buttony),
                Cursor = Cursors.Hand
            };
            picturebox.Click += type;
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
    }
}