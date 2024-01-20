using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstClicker.Controls
{
    public partial class ItemView : UserControl
    {

        public decimal myCost;
        public int myQty;
        public decimal mySalary;
        public int myID;
        public int purchaseAmount = 1;
        public decimal myCostMult;
        public decimal calculatedCost;

        public ItemView(int myID, string myName, decimal _myCost, decimal _myCostMult, int _myQty = 0)
        {
            InitializeComponent();
            this.myCost = _myCost * myID;
            this.myQty = _myQty;
            this.myCostMult = _myCostMult;
            this.lblCost.Text = $"Cost: ${decimal.Round(myCost, 2).ToString()}";
            this.lblQuantity.Text = $"Qty: {myQty.ToString()}";

            this.Name = myName;
            this.grpItem.Text = this.Name;
            this.mySalary = myCost / 10; //temporary
            this.lblSalPerSec.Text = $"Per Sec: ${decimal.Round(this.mySalary, 2).ToString()}";
            this.lblTotalSal.Text = $"Total Salary: ${decimal.Round((decimal)(this.mySalary * this.myQty), 2)}";
        }

        private void ItemView_Load(object sender, EventArgs e)
        {
            this.calculatedCost = myCost * purchaseAmount;
            ButtonColor(false);
        }
        public void UpdateLabels()
        {
            this.calculatedCost = myCost * (decimal)Math.Pow((double)myCostMult, purchaseAmount - 1);
            this.lblCost.Text = $"Cost: ${decimal.Round(calculatedCost, 2).ToString()}";
            this.lblQuantity.Text = $"Qty: {myQty.ToString()}";
            this.lblTotalSal.Text = $"Total Salary: ${decimal.Round((decimal)(this.mySalary * this.myQty), 2)}";
            this.btnBuy.Text = $"Purchase x{this.purchaseAmount}";
        }

        private void ItemView_Paint(object sender, PaintEventArgs e)
        {
            //this.calculatedCost = myCost * ((decimal)Math.Pow((double)myCostMult, purchaseAmount));
            //this.btnBuy.Text = $"Purchase x{purchaseAmount}";
            //this.lblCost.Text = $"Cost: ${decimal.Round(calculatedCost, 2).ToString()}";
            
            this.lblQuantity.Text = $"Qty: {myQty.ToString()}";
            this.lblTotalSal.Text = $"Total Salary: ${decimal.Round((decimal)(this.mySalary * this.myQty), 2)}";
            this.grpItem.Text = this.Name;
        }

        public void ButtonColor(bool afford)
        {
            if (afford)
            {
                this.btnBuy.BackColor = Color.Green;
            }
            else
            {
                this.btnBuy.BackColor = Color.Gray;
            }
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            //passes itself as a parameter to the parent form, to avoid change-watcher system for array. I'm sure better ideas exist.
            ((frmMain)(this.Parent).Parent).BuyClicked(this, this.purchaseAmount);
            this.ItemView_Paint(this, new PaintEventArgs(CreateGraphics(), this.DisplayRectangle));

        }
        
    }
}
