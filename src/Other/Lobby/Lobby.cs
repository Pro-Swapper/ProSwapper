using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Pro_Swapper.CID_Selector;
using Newtonsoft.Json;
using Pro_Swapper.API;
using static Pro_Swapper.API.api;
namespace Pro_Swapper
{
    public partial class Lobby : Form
    {
        public static api.Item CurrentCID = null;

        public static CIDSelection cidform;
        public Lobby(UI.Splash splash)
        {
            InitializeComponent();
            string FNCosmeticsPath = global.ProSwapperFolder + "lobby.ProSwapper";
            Icon = Main.appIcon;
            BackColor = global.MainMenu;


            CurrentCID = new Item();
            var asset = new Asset();
            asset.AssetPath = "FortniteGame/AssetRegistry.bin";
            asset.UcasFile = "pakchunk0-WindowsClient.pak";//Asset registry is always in pakchunk0 coz ue4 moment
            asset.Search = new string[1] { $"" };
            asset.Replace = new string[1] { $"" };
            CurrentCID.Asset = new Asset[1] { asset };

            if (global.allskins == null)
            {
                SkinSearch.Root allitems = new();
                download: double TimeNow = global.GetEpochTime();
                if (global.CurrentConfig.LobbyLastOpened + 86400 < TimeNow)
                {
                    //More than 24hrs have passed
                    allitems = msgpack.MsgPacklz4<SkinSearch.Root>($"{api.FNAPIEndpoint}v2/cosmetics/br?responseFormat=msgpack_lz4&responseOptions=ignore_null");
                    File.WriteAllText(FNCosmeticsPath, JsonConvert.SerializeObject(allitems));
                    global.CurrentConfig.LobbyLastOpened = TimeNow;
                    global.SaveConfig();
                }
                else
                {
                    if (!File.Exists(FNCosmeticsPath))
                    {
                        global.CurrentConfig.LobbyLastOpened = 0;
                        goto download;
                    }
                    allitems = JsonConvert.DeserializeObject<SkinSearch.Root>(File.ReadAllText(FNCosmeticsPath));

                }
                    

                global.allskins = new SkinSearch.Root();
                global.allskins.data = allitems.data.Where(x => CIDSelection.actuallyUsingBackends.Any(x.type.backendValue.Equals)).ToArray();
            }
            splash.Invoke(new Action(() => { splash.Close(); }));
        }




        private void button1_Click(object sender, EventArgs e)
        {
            string Search = textBox1.Text;
            string Replace = textBox2.Text;

            if (CurrentCID.Asset[0].Search[0].Split('.')[0] == Search && CurrentCID.Asset[0].Replace[0].Split('.')[0] == Replace)
            {
                if (CurrentCID.Asset[0].Search[0].Length == 0)
                    return;
                
                new ZlibSwap(CurrentCID).ShowDialog();
                return;
            }
            if (CurrentCID.Asset[0].Search[0].Split('.')[0] != Search || CurrentCID.Asset[0].Replace[0].Split('.')[0] != Replace)
            {
                if (Search.Length < Replace.Length)
                {
                    MessageBox.Show("The ID you own must be longer than the ID you replace it for!", "Pro Swapper Lobby", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Item CustomID = new Item();
                CustomID.SwapsFrom = Search;
                CustomID.SwapsTo = Replace;
                CustomID.FromImage = "K9KWU5h.png";
                CustomID.ToImage = "K9KWU5h.png";
                CustomID.Zlib = true;
                var asset = new Asset();
                asset.AssetPath = "FortniteGame/AssetRegistry.bin";
                asset.UcasFile = "pakchunk0-WindowsClient.pak";//Asset registry is always in pakchunk0 coz ue4 moment
                asset.Search = new string[1] { $"{Search}.{Replace}" };
                asset.Replace = new string[1] { $"{Replace}.{Replace}" };
                CustomID.Asset = new Asset[1] { asset };
                new ZlibSwap(CustomID).ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cidform = new CIDSelection(this);
            cidform.ShowDialog();
        }

        private void saveAsPluginjsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog a = new SaveFileDialog())
            {
                a.Title = "Save as a Pro Swapper plugin (.json)";
                a.DefaultExt = "json";
                a.FileName = $"{CurrentCID.SwapsFrom} To {CurrentCID.SwapsTo} Lobby Swap";
                a.Filter = "Pro Swapper Plugin (*.json)|*.json|All files (*.*)|*.*";
                
                if (a.ShowDialog() == DialogResult.OK)
                {
                    string plugin = JsonConvert.SerializeObject(CurrentCID, Formatting.Indented, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
                    File.WriteAllText(a.FileName, plugin);
                }
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string lobbyswapperpath = $"{global.CurrentConfig.Paks}\\Pro Swapper Lobby";
            if (Directory.Exists(lobbyswapperpath))
            {
                DialogResult result = MessageBox.Show("Do you want to remove all swapped lobby items?", "Remove Lobby Swapper Items?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    RevertAllLobbySwaps();
                    MessageBox.Show("Reverted all lobby swapped items", "Pro Swapper Lobby", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } 
            }
            else
            {
                MessageBox.Show("You have no lobby swapped items!", "Pro Swapper Lobby", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        public static void RevertAllLobbySwaps(bool IsVerify = false)
        {
            string lobbyswapperpath = $"{global.CurrentConfig.Paks}\\Pro Swapper Lobby";
            if (Directory.Exists(lobbyswapperpath))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Directory.Delete(lobbyswapperpath, true);
                if (!IsVerify)
                {
                    string swaplogs = global.CurrentConfig.swaplogs;
                    if (swaplogs.Length > 0)
                    {
                        string[] swappeditems = swaplogs.Remove(swaplogs.Length - 1).Split(',');
                        string newconfig = "";
                        foreach (string item in swappeditems)
                        {
                            if (!item.Contains("(Lobby)"))
                                newconfig += item + ",";
                        }
                        global.CurrentConfig.swaplogs = newconfig;
                        global.SaveConfig();
                    }
                }
            }
        }
    }
}
