using System;
using System.Collections.Generic;
using System.Data;
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
        public static string[] actuallyUsingBackends = { AthenaCharacter.ToString(), AthenaBackpack.ToString(), AthenaDance.ToString(), AthenaMusicPack.ToString(), AthenaPickaxe.ToString() };
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
            SearchedSkin = ParseSkinInfo(textBox1.Text);
            if (SearchedSkin != null)
            {
                flowLayoutPanel1.Controls.Clear();
                List<SkinSearch.Datum> itemlist = new List<SkinSearch.Datum>();
                SortItemList(ref itemlist);
                var skinlist = itemlist.Where(x => x.id.Length <= SearchedSkin.id.Length).Where(x => x.images.icon != null).ToList();
                
                for (int i = 0; i < skinlist.Count; i++)
                {
                    if (currentBackEndType.ToString() == skinlist[i].type.backendValue)
                        flowLayoutPanel1.Controls.Add(new GridItem(skinlist[i]));
                }    
                    
                label1.Text = $"Found {flowLayoutPanel1.Controls.Count} cosmetics compatible with {SearchedSkin.name}";
            }
            else
            {
                flowLayoutPanel1.Controls.Clear();
                label1.Text = $"That cosmetic cannot be found ({textBox1.Text})";
            }
        }


        private SkinSearch.Datum ParseSkinInfo(string searchinfo)
        {
            //Using our local list to get our stuff so it's SUPER fast.
            try
            {
                if (searchinfo.Contains("ID"))
                {
                    foreach (var item in global.allskins.data)
                    {
                        BackendTypes thisbackendtype = (BackendTypes)Enum.Parse(typeof(BackendTypes), item.type.backendValue);
                        if (currentBackEndType == thisbackendtype && item.id.ToLower() == searchinfo.ToLower())
                            return item;
                    }
                    goto error;
                }
                else
                {
                    foreach (var item in global.allskins.data)
                    {
                        BackendTypes thisbackendtype = (BackendTypes)Enum.Parse(typeof(BackendTypes), item.type.backendValue);
                        if (currentBackEndType == thisbackendtype && item.name.ToLower().StartsWith(searchinfo.ToLower()))
                            return item;  
                    }
                    goto error;
                }
                    
            }
            catch
            {
                goto error;
            }
            error: MessageBox.Show("That skin doesn't exist! Are you sure you spelt it right?", "Pro Swapper Lobby");
            return null;
        }

        //Basically reload the current items with the new sort
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)=> button2_Click(this, new EventArgs());

        private void SortItemList(ref List<SkinSearch.Datum> itemlist)
        {
            switch (comboBox1.Text)
            {
                case "Alphabetical":
                    itemlist = global.allskins.data.OrderBy(x => x.name).ToList();
                    break;

                case "Rarity":
                    itemlist = global.allskins.data.OrderBy(x => x.rarity.backendValue).ToList();
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
