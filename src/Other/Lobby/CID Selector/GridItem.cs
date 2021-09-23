using System;
using System.Drawing;
using System.Windows.Forms;
using static Pro_Swapper.API.api;
using static Pro_Swapper.CID_Selector.SkinSearch;
using System.Linq;
namespace Pro_Swapper.CID_Selector
{
    public partial class GridItem : UserControl
    {
        private Datum skin { get; set; }
        
        public GridItem(Datum Skin)
        {
            InitializeComponent();
            skin = Skin;
            label1.Text = skin.name;
            backgroundWorker1.DoWork += delegate
            {
                if (skin.images.smallIcon != null)
                {
                    pictureBox1.Image = global.ItemIcon(skin.images.smallIcon);
                }
            };
            backgroundWorker1.RunWorkerAsync();
            backgroundWorker1.Dispose(); 
        }

        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string lastshop;
            var shophistory = skin.shopHistory;
            if (shophistory == null)
                lastshop = "Not an item shop skin";
            else
                lastshop = global.CalculateTimeSpan(skin.shopHistory.Last());

            if (skin.introduction == null)
                skin.introduction = new Introduction() { chapter = "", backendValue = 0, season = "", text = "" };

            MessageBox.Show($"ID: {skin.id}\nDescription:{skin.description}\nBackend Value:{skin.type.backendValue}\nIntroduced: \n{skin.introduction.text}\nRarity: {skin.rarity.displayValue}\nLast seen in shop: {lastshop}", "Item Info for " + skin.name, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        contextMenuStrip1.Show(this, new Point(e.X, e.Y));//places the menu at the pointer position
                    }
                    break;
                case MouseButtons.Left:
                    CIDSelection.CIDSelectionfrm.SetSkinCID(skin);
                    break;
            }
        }
    }
}
