using System;
using System.Windows.Forms;
using Pro_Swapper.API;
namespace Pro_Swapper
{
    public partial class GridItem : UserControl
    {

        private int ItemIndex = 0;
        private int OptionMenuIndex = 0;
        private global.GridItemType GridItemType;

        public GridItem(int index, global.GridItemType grid)
        {
            InitializeComponent();
            GridItemType = grid;

            switch (grid)
            {
                case global.GridItemType.Item:
                    ItemIndex = index;
                    pictureBox1.Image = global.ItemIcon(api.apidata.items[index].ToImage);
                    label1.Text = api.apidata.items[index].SwapsTo;
                    break;

                case global.GridItemType.SwapOption:

                    OptionMenuIndex = index;
                    pictureBox1.Image = global.ItemIcon(api.apidata.OptionMenu[index].MainIcon);
                    label1.Text = api.apidata.OptionMenu[index].Title;
                    break;
            }

            
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            switch (GridItemType)
            {
                case global.GridItemType.Item:
                    new OodleSwap(ItemIndex).Show();
                    break;

                case global.GridItemType.SwapOption:
                    new SwapOption(api.apidata.OptionMenu[OptionMenuIndex]).Show();
                    break;
            }
        }
    }
}
