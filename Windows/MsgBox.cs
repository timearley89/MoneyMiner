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
    public partial class MsgBox : Form
    {
        public MsgBox(string Message)
        {
            InitializeComponent();
            lblMsgText.Text = Message;
        }
        public MsgBox(string Message, string Caption)
        {
            InitializeComponent();
            lblMsgText.Text = Message;
            this.Text = Caption;
        }
    }
}
