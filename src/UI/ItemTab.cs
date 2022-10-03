using System.Windows.Forms;
using Pro_Swapper.API;
using System.Linq;
namespace Pro_Swapper
{
    public partial class ItemTab : UserControl
    {
        public ItemTab(string tab)
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            Tab = tab;
            HorizontalScroll.Enabled = false;
            skinsflowlayout.BackColor = global.ItemsBG;
            var Items = api.apidata.items.Where(x => x.Type.Equals(tab)).Where(x => x.ShowMain != false);
            if (Items.Count() > 0)
                skinsflowlayout.Controls.AddRange(Items.Select(x => new GridItem(x)).ToArray());

            if (api.apidata.OptionMenu.Length > 0)
            {
                var SwapOptions = api.apidata.OptionMenu.Where(x => x.Type.Equals(tab));
                if (SwapOptions.Count() > 0)
                    skinsflowlayout.Controls.AddRange(SwapOptions.Select(x => new GridItem(x)).ToArray());
            }


            if (skinsflowlayout.Controls.Count == 0)
                MessageBox.Show($@"{tab} is currently disabled, please be patient for the developer(s) of Pro Swapper to add this feature. If you would like to request a feature please send a message on the Discord server", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public string Tab { get; set; }

        private void ItemTab_MouseDown(object sender, MouseEventArgs e) => global.MoveForm(e, Main.Mainform.Handle);
    }
}