using System;
using System.Linq;
using System.Windows.Forms;
using static Pro_Swapper.API.api;
using static Pro_Swapper.CID_Selector.CIDSelection.BackendTypes;
namespace Pro_Swapper.CID_Selector
{
    public partial class CIDSelection : Form
    {
        public static SkinSearch.Datum SearchedSkin = null;
        private BackendTypes currentBackEndType = AthenaCharacter;
        public static CIDSelection CIDSelectionfrm;

        //Use only the following backend types
        public static readonly string[] actuallyUsingBackends = { AthenaCharacter.ToString(), AthenaBackpack.ToString(), AthenaDance.ToString(), AthenaMusicPack.ToString(), AthenaPickaxe.ToString(), AthenaGlider.ToString() };
        public enum BackendTypes
        {
            AthenaCharacter,//
            AthenaBackpack,//
            AthenaDance,//
            AthenaPickaxe,//
            BannerToken,
            AthenaSkyDiveContrail,
            AthenaGlider,//
            AthenaEmoji,
            AthenaLoadingScreen,
            AthenaPetCarrier,
            AthenaPet,
            AthenaMusicPack,//
            AthenaSpray,
            AthenaToy,
            AthenaItemWrap,
           // All,
        }
        public static Lobby lobbyform;

        private enum SearchingBy
        {
            OwnedItem,
            WantItem
        }
        private SearchingBy searchingBy = SearchingBy.OwnedItem;

        public CIDSelection(Lobby lobbyfrm)
        {
            InitializeComponent();
            CIDSelectionfrm = this;
            button1.Tag = searchingBy;
            button1_Click(button1, new EventArgs());
            lobbyform = lobbyfrm;
            Icon = Main.appIcon;
            BackColor = global.MainMenu;
            flowLayoutPanel1.BackColor = global.MainMenu;
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            SearchedSkin = ParseSkinInfo(textBox1.Text);
            if (SearchedSkin != null)
            {
                string currentBackendType = currentBackEndType.ToString();
                SortItemList(out SkinSearch.Datum[] itemlist);
                GridItem[] skinlist = null;
                switch (searchingBy)
                {
                    case SearchingBy.OwnedItem:
                        skinlist = itemlist.Where(x => x.type.backendValue == currentBackendType && x.id.Length <= SearchedSkin.id.Length).Select(x => new GridItem(x)).ToArray();
                        label1.Text = $"Found {skinlist.Length} cosmetics that can be swapped for {SearchedSkin.name}";
                        break;
                    case SearchingBy.WantItem:
                        skinlist = itemlist.Where(x => x.type.backendValue == currentBackendType && x.type.backendValue == currentBackendType && SearchedSkin.id.Length <= x.id.Length).Select(x => new GridItem(x)).ToArray();
                        
                        label1.Text = $"Found {skinlist.Length} cosmetics that can be swapped to {SearchedSkin.name}";
                        break;
                }
                flowLayoutPanel1.Controls.AddRange(skinlist);

            }
            else
                label1.Text = $"That cosmetic cannot be found ({textBox1.Text})";
        }


        public void SetSkinCID(SkinSearch.Datum skin)
        {
            switch (searchingBy)
            {
                case SearchingBy.OwnedItem:
                    lobbyform.Controls["textBox2"].Text = skin.id;
                    lobbyform.Controls["textBox1"].Text = SearchedSkin.id;
                    Lobby.CurrentCID = new Item();
                    Lobby.CurrentCID.SwapsFrom = SearchedSkin.name;
                    Lobby.CurrentCID.SwapsTo = skin.name;
                    Lobby.CurrentCID.FromImage = SearchedSkin.images.icon;
                    Lobby.CurrentCID.ToImage = skin.images.icon;
                    Lobby.CurrentCID.Zlib = true;
                    var asset = new Asset();
                    asset.AssetPath = "FortniteGame/AssetRegistry.bin";
                    asset.UcasFile = "pakchunk0-WindowsClient.pak";//Asset registry is always in pakchunk0 coz ue4 moment
                    asset.Search = new string[1] { $"{SearchedSkin.id}.{SearchedSkin.id}" };
                    asset.Replace = new string[1] { $"{skin.id}.{skin.id}" };
                    Lobby.CurrentCID.Asset = new Asset[1] { asset };
                    Lobby.cidform.Close();
                    break;
                case SearchingBy.WantItem:
                    lobbyform.Controls["textBox2"].Text = SearchedSkin.id;
                    lobbyform.Controls["textBox1"].Text = skin.id;
                    Lobby.CurrentCID = new Item();
                    Lobby.CurrentCID.SwapsFrom = skin.name;
                    Lobby.CurrentCID.SwapsTo = SearchedSkin.name;
                    Lobby.CurrentCID.FromImage = skin.images.icon;
                    Lobby.CurrentCID.ToImage = SearchedSkin.images.icon;
                    Lobby.CurrentCID.Zlib = true;
                    var asset2 = new Asset();
                    asset2.AssetPath = "FortniteGame/AssetRegistry.bin";
                    asset2.UcasFile = "pakchunk0-WindowsClient.pak";//Asset registry is always in pakchunk0 coz ue4 moment
                    asset2.Search = new string[1] { $"{skin.id}.{skin.id}" };
                    asset2.Replace = new string[1] { $"{SearchedSkin.id}.{SearchedSkin.id}" };
                    Lobby.CurrentCID.Asset = new Asset[1] { asset2 };
                    Lobby.cidform.Close();

                    break;
            }
            
        }

        private SkinSearch.Datum ParseSkinInfo(string searchinfo)
        {
            //Using our local list to get our stuff so it's SUPER fast.
            string BackendType = currentBackEndType.ToString();
            try
            {
                if (searchinfo.Contains("ID"))
                {
                    searchinfo = searchinfo.ToLower();
                    return global.allskins.data.First(x => x.type.backendValue == BackendType && x.id.ToLower() == searchinfo);
                } 
                else
                {
                    searchinfo = searchinfo.ToLower();
                    return global.allskins.data.First(x => x.type.backendValue == BackendType && x.name.ToLower().StartsWith(searchinfo));
                }
            }
            catch
            {
                MessageBox.Show("That skin doesn't exist! Are you sure you spelt it right?", "Pro Swapper Lobby");
            }
            return null;
        }

        //Basically reload the current items with the new sort
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)=> button2_Click(null, new EventArgs());

        private void SortItemList(out SkinSearch.Datum[] itemlist)
        {
            switch (comboBox1.Text)
            {
                case "Alphabetical":
                    itemlist = global.allskins.data.OrderBy(x => x.name).ToArray();
                    break;

                case "Rarity":
                    itemlist = global.allskins.data.OrderBy(x => x.rarity.backendValue).ToArray();
                    break;
                case "Last Seen in shop":
                    itemlist = global.allskins.data.Where(x => x.shopHistory != null).OrderBy(x => x.shopHistory.Last()).ToArray();
                    break;
                case "Season":
                default:
                    itemlist = global.allskins.data;
                    break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            /*
             * Skins
               Backblings
               Emotes
               Pickaxes
               Music
             * 
             */
            switch (comboBox2.Text)
            {
                case "Skins":
                    currentBackEndType = AthenaCharacter;
                    break;
                case "Backblings":
                    currentBackEndType = AthenaBackpack;
                    break;
                case "Emotes":
                    currentBackEndType = AthenaDance;
                    break;
                case "Pickaxes":
                    currentBackEndType = AthenaPickaxe;
                    break;
                case "Music":
                    currentBackEndType = AthenaMusicPack;
                    break;
                case "Glider":
                    currentBackEndType = AthenaGlider;
                    break;
            }
            button2_Click(null, new EventArgs());
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button2_Click(null, new EventArgs());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Tag)
            {
                case SearchingBy.OwnedItem:

                    searchingBy = SearchingBy.WantItem;
                    btn.Tag = SearchingBy.WantItem;
                    btn.Text = "Search by cosmetic you own";
                    label4.Text = "Search with either Item ID or Name:\nType below the cosmetic you WANT";
                    break;

                case SearchingBy.WantItem:
                    searchingBy = SearchingBy.OwnedItem;
                    btn.Tag = SearchingBy.OwnedItem;
                    btn.Text = "Search by cosmetic you want";
                    label4.Text = "Search with either Item ID or Name:\nType below the cosmetic you OWN";
                    break;
            }
            button2_Click(null, new EventArgs());
        }
    }
}
