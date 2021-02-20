using System;
using System.Net;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Collections.Generic;
namespace Pro_Swapper
{
    public partial class ItemSearcher : Form
    {

        public ItemSearcher()
        {
            InitializeComponent();
            itemtype.SelectedItem = "All";
            Icon = Main.appIcon;
            BackColor = global.MainMenu;
            button2.BackColor = global.Button;
            button2.ForeColor = global.TextColor;
            button1.BackColor = global.Button;
            button1.ForeColor = global.TextColor;
            label1.ForeColor = global.TextColor;
            label2.ForeColor = global.TextColor;
            label3.ForeColor = global.TextColor;
            label4.ForeColor = global.TextColor;
        }
        public class Icons
        {
            public string icon { get; set; }
            public object featured { get; set; }
            public object series { get; set; }
        }

        public class Root
        {
            public string id { get; set; }
            public string path { get; set; }
            public Icons icons { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string shortDescription { get; set; }
            public string backendType { get; set; }
            public string rarity { get; set; }
            public string backendRarity { get; set; }
            public string set { get; set; }
            public string setText { get; set; }
            public object series { get; set; }
            public object variants { get; set; }
            public List<string> gameplayTags { get; set; }
        }




        enum ItemTypes
        {
            Character,
            Dance,
            Backpack,
            Pickaxe,
            All,
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string data = string.Empty;
            string ItemType = string.Empty;
            image.Image = null;
            featured.ImageLocation = null;
            Results.Text = "";
            namedesc.Text = "";
            try
            {
                using (WebClient web = new WebClient())
                {
                    switch (itemtype.Text)
                    {
                        case "All":
                            ItemType = ItemTypes.All.ToString();
                            data = web.DownloadString("https://benbotfn.tk/api/v1/cosmetics/br/search?lang=en&searchLang=en&name=" + textBox1.Text.Replace(" ", "%20"));
                            break;

                        case "Skin":
                            ItemType = ItemTypes.Character.ToString();
                            break;

                        case "Emote":
                            ItemType = ItemTypes.Dance.ToString();
                            break;

                        case "Backbling":
                            ItemType = ItemTypes.Backpack.ToString();
                            break;

                        case "Pickaxe":
                            ItemType = ItemTypes.Pickaxe.ToString();
                            break;


                    }
                    if (ItemType != "All")
                        data = web.DownloadString("https://benbotfn.tk/api/v1/cosmetics/br/search?lang=en&backendType=Athena" + ItemType + "&searchLang=en&name=" + textBox1.Text.Replace(" ", "%20"));

                    Root apidata = new JavaScriptSerializer().Deserialize<Root>(data);
                    Results.Text = apidata.id;
                    namedesc.Text = apidata.shortDescription + ": " + apidata.name + Environment.NewLine + apidata.description;
                    image.ImageLocation = apidata.icons.icon;
                    featured.ImageLocation = (string)apidata.icons.featured;
                }
                
            }
            catch (WebException)
            {
                MessageBox.Show("That item doesn't exist! Did you spell it right?", "Pro Swapper Item Searcher");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Pro Swapper Item Searcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Results.Text != "")
            {
                Clipboard.SetText(Results.Text);
                MessageBox.Show("Copied " + Results.Text + " to your clipboard!");
            }
        }
    }
}
