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
    public partial class PrestigeEarned : Form
    {
        public PrestigeEarned(string PrestigeMessage)
        {
            InitializeComponent();
            lblPrestigeText.Text = PrestigeMessage;
        }
    }
}
