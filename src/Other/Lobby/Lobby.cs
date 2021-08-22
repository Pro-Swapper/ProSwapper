using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Pro_Swapper.CID_Selector;
using Newtonsoft.Json;
using Pro_Swapper.API;
namespace Pro_Swapper
{
    public partial class Lobby : Form
    {
        public static api.Item CurrentCID = null;

        public static CIDSelection cidform;
        public Lobby(UI.Splash splash)
        {
            InitializeComponent();
            Icon = Main.appIcon;
            BackColor = global.MainMenu;
            if (global.allskins == null)
            {
                SkinSearch.Root allitems = msgpack.MsgPacklz4<SkinSearch.Root>($"{api.FNAPIEndpoint}v2/cosmetics/br?responseFormat=msgpack_lz4&responseOptions=ignore_null");
                var datalist = new List<SkinSearch.Datum>();
                foreach (var item in allitems.data)
                {
                    if (CIDSelection.actuallyUsingBackends.Any(item.type.backendValue.Equals))
                        datalist.Add(item);
                }
                global.allskins = new SkinSearch.Root();
                global.allskins.data = datalist;
            }
            Action safeClose = delegate { splash.Close(); };
            splash.Invoke(safeClose);
        }




        private void button1_Click(object sender, EventArgs e)
        {
           if (CurrentCID != null)
            {
                new ZlibSwap(CurrentCID).ShowDialog();
            }
           else
            {
                MessageBox.Show("You haven't chosen a CID");
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

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show("Press the button above to select the lobby swap skins");
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


        public static void RevertAllLobbySwaps()
        {
            string lobbyswapperpath = $"{global.CurrentConfig.Paks}\\Pro Swapper Lobby";
            if (Directory.Exists(lobbyswapperpath))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Directory.Delete(lobbyswapperpath, true);
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
