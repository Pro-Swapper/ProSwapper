using System;
using System.Windows.Forms;

namespace Pro_Swapper.FOV
{
	public partial class StretchedUserControl : UserControl
	{
		public StretchedUserControl()
		{
			InitializeComponent();
		}

		private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
			if (e.KeyData != Keys.Back)
                e.SuppressKeyPress = !int.TryParse(Convert.ToString((char)e.KeyData), out int _);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			IniEditor.SetRes(yres.Text, xres.Text, 0);
			MessageBox.Show("Set Resolution to " + xres.Text + " x " + yres.Text, "Pro Swapper Resolution Changer", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}


		public static class Calc
		{
			//https://dotnetfiddle.net/nBzr0i
			public static int gcd(int answerNumerator, int answerDenominator)
			{
				// assigned x and y to the answer Numerator/Denominator, as well as an  
				// empty integer, this is to make code more simple and easier to read
				int x = Math.Abs(answerNumerator);
				int y = Math.Abs(answerDenominator);
				int m;
				// check if numerator is greater than the denominator, 
				// make m equal to denominator if so
				if (x > y)
					m = y;
				else
					// if not, make m equal to the numerator
					m = x;
				// assign i to equal to m, make sure if i is greater
				// than or equal to 1, then take away from it
				for (int i = m; i >= 1; i--)
				{
					if (x % i == 0 && y % i == 0)
					{
						//return the value of i
						return i;
					}
				}

				return 1;
			}

			public static Resolution Reduce(int answerNumerator, int answerDenominator)
			{
				try
				{
					//assign an integer to the gcd value
					int gcdNum = gcd(answerNumerator, answerDenominator);
					if (gcdNum != 0)
					{
						answerNumerator = answerNumerator / gcdNum;
						answerDenominator = answerDenominator / gcdNum;
					}

					if (answerDenominator < 0)
					{
						answerDenominator = answerDenominator * -1;
						answerNumerator = answerNumerator * -1;
					}
				}
				catch (Exception exp)
				{
					// display the following error message 
					// if the fraction cannot be reduced
					throw new InvalidOperationException("Cannot reduce Fraction: " + exp.Message);
				}

				return new Resolution(answerNumerator, answerDenominator);
			}
		}

        private void yres_Leave(object sender, EventArgs e)
        {
			
		}

        public struct Resolution
        {
			public int x;
			public int y;

			public Resolution(int X, int Y)
            {
				x = X;
				y = Y;
            }
        }

        private void yres_TextChanged(object sender, EventArgs e)
        {
			try
			{
				Resolution aspectratio = Calc.Reduce(int.Parse(xres.Text), int.Parse(yres.Text));
				label2.Text = $"Aspect Ratio: {aspectratio.x}:{aspectratio.y}";
			}
			catch
			{
				label2.Text = $"Calculating Aspect Ratio....";
			}
		}
    }
}
