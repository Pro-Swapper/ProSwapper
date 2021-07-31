using System;
using System.Drawing;
using System.Windows.Forms;
using static Pro_Swapper.API.api;

namespace Pro_Swapper.CID_Selector
{
    public partial class GridItem : UserControl
    {
        private SkinSearch.Datum skin { get; set; }
        
        public GridItem(SkinSearch.Datum Skin)
        {
            InitializeComponent();
            skin = Skin;
            label1.Text = skin.name;
            backgroundWorker1.DoWork += delegate
            {
                pictureBox1.Image = global.ItemIcon(skin.images.smallIcon);
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
                lastshop = Settings.CalculateTimeSpan(skin.shopHistory[skin.shopHistory.Count - 1]);
            
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
                    CIDSelection.lobbyform.Controls["textBox2"].Text = skin.id;
                    CIDSelection.lobbyform.Controls["textBox1"].Text = CIDSelection.SearchedSkin.id;
                    Lobby.CurrentCID = new Item();
                    Lobby.CurrentCID.SwapsFrom = CIDSelection.SearchedSkin.name;
                    Lobby.CurrentCID.SwapsTo = skin.name;
                    Lobby.CurrentCID.FromImage = CIDSelection.SearchedSkin.images.icon;
                    Lobby.CurrentCID.ToImage = skin.images.icon;
                    Lobby.CurrentCID.Zlib = true;
                    var asset = new Asset();
                    asset.AssetPath = "FortniteGame/AssetRegistry.bin";
                    asset.UcasFile = "pakchunk0-WindowsClient.pak";//Asset registry is always in pakchunk0 coz ue4 moment
                    asset.Search = new string[1] { $"{CIDSelection.SearchedSkin.id}.{CIDSelection.SearchedSkin.id}" };
                    asset.Replace = new string[1] { $"{skin.id}.{skin.id}" };
                    Lobby.CurrentCID.Asset = new Asset[1] { asset };
                    Lobby.cidform.Close();
                    break;
            }
        }
    }
}
