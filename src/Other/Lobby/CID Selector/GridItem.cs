using System;
using System.Drawing;
using System.Windows.Forms;
using static Pro_Swapper.API.api;

namespace Pro_Swapper.CID_Selector
{
    public partial class GridItem : UserControl
    {
        private SkinSearch.Datum skin { get; set; }
        private SkinSearch.Datum searchedskin { get; set; }

        public GridItem(SkinSearch.Datum Skin, SkinSearch.Datum Searchedskin)
        {
            InitializeComponent();
            skin = Skin;
            searchedskin = Searchedskin;
            label1.Text = skin.name;
            backgroundWorker1.DoWork += delegate
            {
                pictureBox1.Image = global.ItemIcon(skin.images.smallIcon);
            };
            backgroundWorker1.RunWorkerAsync();
            backgroundWorker1.Dispose(); 
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        contextMenuStrip1.Show(this, new Point(e.X, e.Y));//places the menu at the pointer position
                    }
                    break;
                case MouseButtons.Left:
                    Application.OpenForms["Lobby"].Controls["textBox2"].Text = skin.id;
                    Application.OpenForms["Lobby"].Controls["textBox1"].Text = searchedskin.id;
                    Lobby.CurrentCID = new Item();
                    Lobby.CurrentCID.SwapsFrom = searchedskin.name;
                    Lobby.CurrentCID.SwapsTo = skin.name;
                    Lobby.CurrentCID.FromImage = searchedskin.images.icon;
                    Lobby.CurrentCID.ToImage = skin.images.icon;
                    Lobby.CurrentCID.Zlib = true;
                    var asset = new Asset();
                    asset.AssetPath = "FortniteGame/AssetRegistry.bin";
                    asset.UcasFile = "pakchunk0-WindowsClient.pak";//Asset registry is always in pakchunk0 coz ue4 moment
                    asset.Search = new string[1] { $"{searchedskin.id}.{searchedskin.id}" };
                    asset.Replace = new string[1] { $"{skin.id}.{skin.id}" };
                    Lobby.CurrentCID.Asset = new Asset[1] { asset };

                    Application.OpenForms["CIDSelection"].Close();
                    break;
            }
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
    }
}
