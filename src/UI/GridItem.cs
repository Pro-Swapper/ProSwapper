using System.Windows.Forms;
using Pro_Swapper.API;
namespace Pro_Swapper
{
    public partial class GridItem : UserControl
    {
        public GridItem(api.Item i)
        {
            InitializeComponent();
            backgroundWorker1.DoWork += delegate
            {
                pictureBox1.Image = global.ItemIcon(i.ToImage);
            };
            backgroundWorker1.RunWorkerAsync();
            pictureBox1.Click += delegate
            {
               new OodleSwap(i).Show();
            };
            label1.Text = i.SwapsTo.Split('|')[0];
            backgroundWorker1.Dispose();
        }


        public GridItem(api.OptionMenu i)
        {
            InitializeComponent();
            backgroundWorker1.DoWork += delegate
            {
                pictureBox1.Image = global.ItemIcon(i.MainIcon);
            };
            backgroundWorker1.RunWorkerAsync();
            pictureBox1.Click += delegate
            {
                new SwapOption(i).Show();
            };
            label1.Text = i.Title.Split('|')[0];
            backgroundWorker1.Dispose();
        }

        public GridItem(string imageURL, string Text, string ClickURL)
        {
            InitializeComponent();
            backgroundWorker1.DoWork += delegate
            {
                pictureBox1.Load(imageURL);
            };
            backgroundWorker1.RunWorkerAsync();
            label1.Text = Text;
            pictureBox1.Click += delegate
            {
                global.OpenUrl(ClickURL);
            };
        }
    }
}
