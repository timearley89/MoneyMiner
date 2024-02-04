using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MoneyMiner.Controls;
using MoneyMiner.Properties;
using MoneyMiner.Windows;

namespace MoneyMiner.Windows
{
    public partial class Unlocks : Form
    {
        ItemView[] myItems;
        public Unlocks(ItemView[] items)
        {
            InitializeComponent();
            this.myItems = items;
        }

        private void Unlocks_Load(object sender, EventArgs e)
        {

            for (int i = 0; i < myItems.Length; i++)
            {
                Label mylabel = new();
                mylabel.AutoSize = false;
                mylabel.Dock = DockStyle.Fill;
                mylabel.Margin = new Padding(5);
                mylabel.BackColor = Colors.colBorders;
                mylabel.Font = new Font("Bahnschrift", 16, FontStyle.Bold);
                mylabel.Text = $"X{myItems[i].myUnlockList[1, (myItems[i].latestUnlock + 1 < myItems[i].myUnlockList.Length ? myItems[i].latestUnlock + 1 : myItems[i].myUnlockList.Length - 1)]}";
                mylabel.TextAlign = ContentAlignment.MiddleCenter;
                tableLayoutPanel1.Controls.Add(mylabel, i, 2);
                Label mylabel2 = new();
                mylabel2.AutoSize = false;
                mylabel2.Dock = DockStyle.Fill;
                mylabel2.Margin = new Padding(5);
                mylabel2.BackColor = Colors.colBorders;
                mylabel2.Font = new Font("Bahnschrift", 16, FontStyle.Bold);
                mylabel2.TextAlign = ContentAlignment.MiddleCenter;
                mylabel2.Text = $"{myItems[i].myUnlockList[0, (myItems[i].latestUnlock + 1 < myItems[i].myUnlockList.Length ? myItems[i].latestUnlock + 1 : myItems[i].myUnlockList.Length - 1)]}x";
                tableLayoutPanel1.Controls.Add(mylabel2, i, 1);
                PictureBox pctIcon = new();
                pctIcon.Dock = DockStyle.Fill;
                pctIcon.Margin = new Padding(5);
                pctIcon.Image = myItems[i].myIcon;
                pctIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                tableLayoutPanel1.Controls.Add(pctIcon, i, 0);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
