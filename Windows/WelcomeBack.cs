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
    public partial class WelcomeBack : Form
    {
        public WelcomeBack(string WelcomeMessage)
        {
            InitializeComponent();
            lblWelcomeText.Text = WelcomeMessage;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WelcomeBack_Load(object sender, EventArgs e)
        {
        }
    }
}
