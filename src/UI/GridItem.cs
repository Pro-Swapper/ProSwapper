using System.Threading.Tasks;
using System.Windows.Forms;
using Pro_Swapper.API;
namespace Pro_Swapper
{
    public partial class GridItem : UserControl
    {
        public GridItem(api.Item i)
        {
            InitializeComponent();
            Task.Run(() => pictureBox1.Image = global.ItemIcon(i.ToImage));
            pictureBox1.Click += delegate
            {
                new SwapForm(i).Show();
            };
            label1.Text = i.SwapsTo.Split('|')[0];
        }


        public GridItem(api.OptionMenu i)
        {
            InitializeComponent();
            Task.Run(() => pictureBox1.Image = global.ItemIcon(i.MainIcon));

            pictureBox1.Click += delegate
            {
                new SwapOption(i).Show();
            };
            label1.Text = i.Title.Split('|')[0];
        }
    }
}
