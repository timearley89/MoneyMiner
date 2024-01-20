using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FirstClicker.Controls;

namespace FirstClicker
{

    public partial class frmMain : Form
    {

        public decimal myMoney; //max value currently is Decimal.MaxValue, which is just over 79 Octillion. Need to restructure this.
        public decimal salary;
        public decimal clickAmount;
        private decimal costMult = 1.05m;
        private int purchAmount = 1;

        public ItemView[] myItems;

        public frmMain()
        {
            InitializeComponent();
            myMoney = 0.00m;
            salary = 0.00m;
            clickAmount = 0.10m; //initial value, temporary
            myItems = [new ItemView(1, "Wood", 1.0m, costMult, 0),
                new ItemView(2, "Stone", 11.0m, costMult, 0),
                new ItemView(3, "Iron", 120.0m, costMult, 0),
                new ItemView(4, "Steel", 1320.0m, costMult, 0),
                new ItemView(5, "Diamond", 14520.0m, costMult, 0),
                new ItemView(6, "Uranium", 159720.0m, costMult, 0),
                new ItemView(7, "Antimatter", 1756920.0m, costMult, 0),
                new ItemView(8, "Black Hole", 19326120.0m, costMult, 0)];
            //(ID, Name, Cost, Multiplier, Quantity)
            //8 available items right now

            //We should implement autoupgrades that double individual salaries at ownership thresholds, like, for example:
            //double when qty = 10, 25, 100, 200, 500, 1000
            //maybe clickamount should increase when money thresholds are reached:
            //money = $10, $100, $1000, etc, clickamount*=2?
            //Also we should have upgrades. Expensive, but worth it, for both click amount and salary.
            //Also, a prestige system, which uses prestige points earned to further multiply salary per second per item and initial click amount.
            //Also, a window to keep track of stats, like amount made, upgrades purchased, prestige points, etc
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Populate form with possible items from array, then update their labels.
            for (int i = 1; i <= myItems.Length; i++)
            {
                flowLayoutPanel1.Controls.Add(myItems[i - 1]);
            }
            foreach (ItemView item in myItems) { item.UpdateLabels(); }
            frmMain_UpdateLabels();
            this.timer1.Start();
        }
        public void frmMain_UpdateLabels()
        {
            lblMoneyAmt.Text = $"${decimal.Round(myMoney, 2):N}";
            lblSalary.Text = $"${decimal.Round(salary, 2):N} Per Second, ${decimal.Round(clickAmount, 2):N} Per Click";
            btnPurchAmount.Text = $"Buy: x{this.purchAmount}";
        }

        private void frmMain_Click(object sender, EventArgs e)
        {
            this.myMoney += clickAmount;
            frmMain_UpdateLabels();
        }

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            //this.lblMoneyAmt.Text = myMoney.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //update button colors based on affordability
            //update money amount based on quantity of items owned and any multipliers
            //update labels and buttons as needed
            decimal tempsal = 0.00m;    //iterate through items owned and calculate total salary per cycle.
            foreach (ItemView view in myItems)
            {
                tempsal += (view.mySalary * view.myQty);    //if qty is 0, salary increment will be 0.
            }
            salary = tempsal;
            myMoney += salary;
            frmMain_UpdateLabels();
            foreach (ItemView view in myItems)
            {
                view.UpdateLabels();
                view.ButtonColor(myMoney, purchAmount, costMult);
                //we do this at the end of the tick() event so that the button color doesn't wait til the next tick to update if money is enough on this tick.
            }
        }
        public void BuyClicked(ItemView sender)
        {
            if (sender.CanAfford(this.myMoney, purchAmount, costMult))
            {
                myMoney -= sender.calculatedCost;
                sender.myQty += sender.purchaseAmount;
                sender.myCost = sender.myCost * (decimal)Math.Pow((double)costMult, sender.purchaseAmount);
            }
            sender.UpdateLabels();
            frmMain_UpdateLabels();
            sender.ButtonColor(myMoney, purchAmount, costMult);
        }

        private void btnPurchAmount_Click(object sender, EventArgs e)
        {
            //purchAmount should be made into an enum. Perfect use-case for it, and reduces possible errors from invalid values.
            if (purchAmount == 1)
            {
                purchAmount = 10;
            }
            else if (purchAmount == 10)
            {
                purchAmount = 100;
            }
            else if (purchAmount == 100)
            {
                purchAmount = 1;
            }
            frmMain_UpdateLabels(); //this function updates the button text already
            
            if (purchAmount == 1 || purchAmount == 10 || purchAmount == 100)
            {
                for (int i = 0; i < myItems.Length; i++)
                {
                    //we just need to update the item with the new amount, and then update the labels and colors.
                    myItems[i].CalcCost(purchAmount, costMult);
                    myItems[i].ButtonColor(myMoney, purchAmount, costMult);
                    myItems[i].UpdateLabels();
                }
            }
            else
            {
                //write method for calculating max purchase amount
            }
        }
    }
}
