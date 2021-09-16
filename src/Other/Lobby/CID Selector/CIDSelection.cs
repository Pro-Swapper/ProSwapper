using System;
using System.Linq;
using System.Windows.Forms;
using static Pro_Swapper.CID_Selector.CIDSelection.BackendTypes;
namespace Pro_Swapper.CID_Selector
{
    public partial class CIDSelection : Form
    {
        public static SkinSearch.Datum SearchedSkin = null;
        private BackendTypes currentBackEndType = AthenaCharacter;

        //Use only the following backend types
        public static readonly string[] actuallyUsingBackends = { AthenaCharacter.ToString(), AthenaBackpack.ToString(), AthenaDance.ToString(), AthenaMusicPack.ToString(), AthenaPickaxe.ToString() };
        public enum BackendTypes
        {
            AthenaCharacter,//
            AthenaBackpack,//
            AthenaDance,//
            AthenaPickaxe,//
            BannerToken,
            AthenaSkyDiveContrail,
            AthenaGlider,
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

        public CIDSelection(Lobby lobbyfrm)
        {
            InitializeComponent();
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
                GridItem[] skinlist = itemlist.Where(x => x.id.Length <= SearchedSkin.id.Length).Where(x => x.images.icon != null).Where(x => x.type.backendValue == currentBackendType).Select(x => new GridItem(x)).ToArray();
                flowLayoutPanel1.Controls.AddRange(skinlist);
                label1.Text = $"Found {flowLayoutPanel1.Controls.Count} cosmetics compatible with {SearchedSkin.name}";
            }
            else
                label1.Text = $"That cosmetic cannot be found ({textBox1.Text})";
        }


        private SkinSearch.Datum ParseSkinInfo(string searchinfo)
        {
            //Using our local list to get our stuff so it's SUPER fast.s
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
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)=> button2_Click(this, new EventArgs());

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
            }
            button2_Click(this, new EventArgs());
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button2_Click(this, new EventArgs());
        }
    }
}
