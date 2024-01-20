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

        public decimal myMoney; //max value currently is Decimal.MaxValue, which is just over 79 Octillion.
        public decimal salary;
        public decimal clickAmount;
        private decimal costMult = 1.1m;
        private int purchAmount = 1;

        public ItemView[] myItems;

        public frmMain()
        {
            InitializeComponent();
            myMoney = 0.00m;
            salary = 0.00m;
            clickAmount = 0.05m;
            myItems = [new ItemView(1, "Wood", 0.5m, costMult, 0),
                new ItemView(2, "Stone", 5.5m, costMult, 0),
                new ItemView(3, "Iron", 55.5m, costMult, 0),
                new ItemView(4, "Steel", 555.5m, costMult, 0),
                new ItemView(5, "Diamond", 5555.5m, costMult, 0)];
            //5 available items right now
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //for now there are 5 basic items that can be bought.
            for (int i = 1; i <= myItems.Length; i++)
            {
                flowLayoutPanel1.Controls.Add(myItems[i - 1]);
            }
            this.timer1.Start();
        }

        private void frmMain_Click(object sender, EventArgs e)
        {
            this.myMoney += clickAmount;
            lblMoneyAmt.Text = decimal.Round(myMoney, 2).ToString();
            lblSalary.Text = $"Salary: {decimal.Round(salary, 2).ToString()} Per Second, {clickAmount.ToString()} Per Click";
        }

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            //this.lblMoneyAmt.Text = myMoney.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //update button colors based on affordability
            //update money amount based on quantity of items owned and any multipliers
            //update lblMoneyAmt.Text as needed
            decimal tempsal = 0.00m;
            foreach (ItemView view in myItems)
            {
                tempsal += (view.mySalary * view.myQty);    //if qty is 0, salary increment will be 0.
            }
            salary = tempsal;
            myMoney += salary;
            lblMoneyAmt.Text = decimal.Round(myMoney, 2).ToString();
            lblSalary.Text = $"Salary: {decimal.Round(salary, 2).ToString()} Per Second, {clickAmount.ToString()} Per Click";
            foreach (ItemView view in myItems)
            {
                view.ButtonColor(view.calculatedCost <= myMoney);
                //we do this at the end of the tick() event so that the button color doesn't wait til the next tick to update if money is enough on this tick.
            }
        }
        public void BuyClicked(ItemView sender, int purchaseQuantity)
        {
            if (myMoney - sender.calculatedCost >= 0.00m)
            {
                myMoney -= sender.calculatedCost;
                sender.myQty += purchaseQuantity;
                sender.myCost = sender.calculatedCost * costMult; //increase cost of each item purchased, iteratively if buying more than one.
            }
 
            

            lblMoneyAmt.Text = decimal.Round(myMoney, 2).ToString();
            sender.UpdateLabels();
            lblSalary.Text = $"Salary: {decimal.Round(salary, 2).ToString()} Per Second, {decimal.Round(clickAmount, 2).ToString()} Per Click";
            sender.ButtonColor(sender.myCost <= myMoney);   //update button color after purchasing as well
        }

        private void btnPurchAmount_Click(object sender, EventArgs e)
        {
            if (purchAmount == 1)
            {
                purchAmount = 10;
                this.btnPurchAmount.Text = $"Buy: x{purchAmount}";
            }
            else if (purchAmount == 10)
            {
                purchAmount = 100;
                this.btnPurchAmount.Text = $"Buy: x{purchAmount}";
            }
            else if (purchAmount == 100)
            {
                purchAmount = 1;
                this.btnPurchAmount.Text = $"Buy: x{purchAmount}";
            }
            //we only wanna loop through once, so we'll set a condition for anything other than 'max'.
            //--should we display the combined salarypersecond as well? or keep it as SPS for one of each item?
            if (purchAmount != 999)
            {
                for (int i = 0; i < myItems.Length; i++)
                {
                    //set lblCost.Text to the total iterative cost of purchasing.
                    myItems[i].purchaseAmount = purchAmount;
                    myItems[i].calculatedCost = myItems[i].myCost * (decimal)Math.Pow((double)costMult, purchAmount - 1);
                    //myItems[i].Controls.Find("lblCost", true)[0].Text = "Cost: $" + decimal.Round(myItems[i].calculatedCost, 2).ToString();
                    myItems[i].UpdateLabels();
                    myItems[i].ButtonColor(myItems[i].calculatedCost <= myMoney);
                }
            }
            else
            {
                //how would we reverse multiplying by an exponent? Log?

                //we need to determine the highest amount of each item we could buy without going over our balance.
                //for now, iterate til over max. Keep in mind this will get computationally expensive later.
                foreach (ItemView item in myItems)
                {
                    
                }
            }
        }
    }
}
