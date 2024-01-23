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
        public MyColors Colors;
        public double myCost;
        public int myQty = 0;
        public double mySalary;
        public int myID;
        public int purchaseAmount = 1;
        public double myCostMult;
        public double calculatedCost;
        public double baseCost;

        public ItemView(int myID, string myName, double _myCost, double _myCostMult, double _mySalary)
        {
            InitializeComponent();
            this.Colors = new MyColors();
            this.myCost = _myCost;
            this.baseCost = _myCost;
            //this.myQty = _myQty;
            this.myCostMult = _myCostMult;
            this.Name = myName;
            this.mySalary = _mySalary;
            this.UpdateLabels();
        }

        private void ItemView_Load(object sender, EventArgs e)
        {
            
        }
        public void UpdateLabels()
        {
            //no calculations, just update labels/buttons.
            this.lblCost.Text = $"Cost: ${double.Round(calculatedCost, 2):N}";
            this.lblQuantity.Text = $"Qty: {myQty:N0}";
            this.lblTotalSal.Text = $"Total Salary: ${double.Round((this.mySalary * this.myQty), 2):N}";
            this.lblSalPerSec.Text = $"Salary: ${double.Round(this.mySalary, 2):N}";
            this.grpItem.Text = this.Name;
            
            this.btnBuy.Text = $"Purchase x{this.purchaseAmount}";
        }

        private void ItemView_Paint(object sender, PaintEventArgs e)
        {
            //this causes unexpected problems, because the main form raises this event on mouseover or focus switch.
        }

        public void ButtonColor(double myMoney, int purchaseQty, double costMultiplier)
        {
            //calculates whether purchase can be done given current balance, purchase amount, and cost multiplier, based on current cost of this item.
            this.btnBuy.BackColor = this.CanAfford(myMoney, purchaseQty, costMultiplier) && purchaseQty > 0 ? Colors.colPrimary : Colors.colDisable;
        }
        public bool CanAfford(double myMoney, int purchaseQty, double costMultiplier)
        {
            return myMoney >= this.CalcCost(purchaseQty, costMultiplier);
        }
        public double CalcCost(int purchaseQty, double costMultiplier)
        {
            //just returns the cost for purchasing x amount of this item and updates internal variables accordingly.
            if (purchaseQty <= 0) 
            {
                this.calculatedCost = 0.00d; 
                this.purchaseAmount = 0; 
            }
            else
            {
                //the issue here is that the cost for purchasing more than one has to be recursively calculated, otherwise it gives the 
                //cost for buying the Nth item, but not the total. Use a while loop.
                //this.calculatedCost = this.myCost * (decimal)Math.Pow((double)costMultiplier, purchaseQty - 1);

                double temptotalcost = 0.00d;
                int tempqty = purchaseQty;
                while (tempqty >= 1)
                {
                    temptotalcost += this.myCost * Math.Pow(costMultiplier, tempqty - 1);
                    tempqty--;
                }
                this.calculatedCost = temptotalcost;
                this.purchaseAmount = purchaseQty;
            }
            return this.calculatedCost;
        }
        private void btnBuy_Click(object sender, EventArgs e)
        {
            //passes itself as a parameter to the parent form, to avoid change-watcher system for array. I'm sure better ideas exist.
            if (this.Parent != null && (this.Parent).Parent != null)
            {
                ((frmMain)(this.Parent).Parent).BuyClicked(this);
            }
            else
            {
                throw new NullReferenceException();
            }
            

        }
        
    }
}
