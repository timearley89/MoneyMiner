using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoneyMiner.Windows
{
    public partial class Stats : Form
    {
        public Stats(string statstext)
        {
            InitializeComponent();
            lblStats.Text = statstext;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
