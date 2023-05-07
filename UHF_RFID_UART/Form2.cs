using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using UHF_RFID_UART;

namespace UHF_RFID_UART
{
    public partial class Form2 : Form
    {
        Button[] buttonsArray;
        TextBox ctrl;
        public Form2(int min, int max,TextBox control)
        {
             ctrl = control;
            InitializeComponent();

            buttonsArray = new Button[max - min+1];
            fillButtonsArray();
        }
        int step = 0;
        void fillButtonsArray()
        {
            Size blockSize = new Size((int)Math.Sqrt(this.Width* (this.Height) / buttonsArray.Length),
                (int)Math.Sqrt(this.Width * (this.Height-25) / buttonsArray.Length));
            int rows = this.Width / blockSize.Width;
            int columns = this.Height / blockSize.Height;
            for (int n = 0; n < buttonsArray.Length; n++)
            {
                buttonsArray[n] = new Button();
                buttonsArray[n].Size = blockSize;
                buttonsArray[n].Location = new Point(0, 0);
                buttonsArray[n].Tag = n.ToString();
            }
            int diff = 0;
            for (int n = 1; n < buttonsArray.Length; n++)
            {
                if(n-diff < columns)
                {
                    buttonsArray[n].Location = new Point(
                       buttonsArray[n - 1].Location.X + buttonsArray[n].Width,
                       buttonsArray[diff].Location.Y + buttonsArray[n].Height
                        ) ;
                }
                else
                {
                    diff = n;
                    buttonsArray[n].Location = new Point(
                     buttonsArray[0].Location.X ,
                     buttonsArray[diff-columns].Location.Y + buttonsArray[n].Height
                      );
                }
                
                this.Controls.Add(buttonsArray[n]);
                this.buttonsArray[n].Click += new System.EventHandler(this.addresFieldPress);

            }
            for (int n = 0; n < buttonsArray.Length; n++)
            {
                buttonsArray[n].Location = new Point(buttonsArray[n].Location.X-buttonsArray[n].Width,
                    buttonsArray[n].Location.Y - buttonsArray[n].Height);
            }
        }
        private void addresFieldPress(object sender, EventArgs e)
        {
            ctrl.Text = ((Button)sender).Tag.ToString();
            this.Hide();
        }
        private void topBar_MouseDown(object sender, MouseEventArgs e)
        {

          

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
