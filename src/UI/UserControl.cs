using System;
using System.Drawing;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Threading;
namespace Pro_Swapper
{
    public partial class UserControl : System.Windows.Forms.UserControl
    {
        private static dynamic jsontable { get; set; }
        public UserControl(string tab)
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            HorizontalScroll.Enabled = false;
            skinsflowlayout.BackColor = global.ItemsBG;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            int buttonx = 134;
            int buttony = 141;
            jsontable = new JavaScriptSerializer().Deserialize<dynamic>(Program.decompresseditems);
            ushort i = 1;
            global.ItemList.Clear();
                foreach (var jsondata in jsontable["Database"][tab])
                {
                    if (Convert.ToString(jsondata).Contains("SwapsFromImage"))
                    {
                        string[] search = { GetObject(tab, i + "SwapS1"), GetObject(tab, i + "SwapS2"), GetObject(tab, i + "SwapS3"), GetObject(tab, i + "SwapS4") };
                        string[] replace = { GetObject(tab, i + "SwapR1"), GetObject(tab, i + "SwapR2"), GetObject(tab, i + "SwapR3"), GetObject(tab, i + "SwapR4") };
                        string[] file = { GetObject(tab, i + "Swap1File"), GetObject(tab, i + "Swap2File"), GetObject(tab, i + "Swap3File"), GetObject(tab, i + "Swap4File") };
                        long[] offset = { Convert.ToInt64(GetObject(tab, i + "Swap1Offset")), Convert.ToInt64(GetObject(tab, i + "Swap2Offset")), Convert.ToInt64(GetObject(tab, i + "Swap3Offset")), Convert.ToInt64(GetObject(tab, i + "Swap4Offset")) };
                        global.Item item = new global.Item(i, GetObject(tab, i + "SwapsFrom"), GetObject(tab, i + "SwapsTo"), GetObject(tab, i + "SwapsFromImage"), GetObject(tab, i + "SwapsToImage"), new global.Swap(search, replace, file, offset), GetObject(tab, i + "Note"));
                        global.ItemList.Add(item);
                        AddItem(i, buttonx, buttony, item, numberButton_Click);
                        i++;
                    }
                }
                if (i == 1)
                    MessageBox.Show(tab + " are currently disabled", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void numberButton_Click(object sender, EventArgs e)=> new swap((int)((PictureBox)sender).Tag).Show();
        private static string GetObject(string itemtype, string objectname)
        {
            try
            {
                return Convert.ToString(jsontable["Database"][itemtype][objectname]);
            }
            catch
            {
                return "0";
            }

        }

        //void option_Click(object sender, EventArgs e) => new bkswap().Show();
        void AddItem(int i, int buttonx, int buttony,global.Item item, EventHandler type)
        {
            PictureBox picturebox = new PictureBox
            {
                Image = global.ItemIcon(item.SwapsToIcon),
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