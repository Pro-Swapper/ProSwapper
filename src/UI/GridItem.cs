using System;
using System.Windows.Forms;
using Pro_Swapper.API;
namespace Pro_Swapper
{
    public partial class GridItem : UserControl
    {
        public GridItem(api.Item i)
        {
            InitializeComponent();
            pictureBox1.Image = global.ItemIcon(i.ToImage);
            pictureBox1.Click += delegate
            {
                new OodleSwap(i).Show();
            };
            label1.Text = i.SwapsTo;
        }


        public GridItem(api.OptionMenu i)
        {
            InitializeComponent();
            pictureBox1.Image = global.ItemIcon(i.MainIcon);
            pictureBox1.Click += delegate
            {
                new SwapOption(i).Show();
            };
            label1.Text = i.Title;
        }
    }
}
