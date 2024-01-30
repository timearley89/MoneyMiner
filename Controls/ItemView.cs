using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace FirstClicker.Controls
{


    [Serializable]
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
        public int latestUnlock;
        public System.Windows.Forms.Timer myTimer;
        public int mySalaryTimeMS;
        public int myprogressvalue;
        public bool displaySalPerSec;

        public ItemView(int myID, string myName, double _myCost, double _myCostMult, double _mySalary, int _salaryTime, int _latestUnlock = -1, int progressvalue = 0)
        {
            InitializeComponent();
            this.myID = myID;
            this.Colors = new MyColors();
            this.BackColor = Colors.colBackground;
            this.grpItem.BackColor = Colors.colBackground;
            this.ForeColor = Colors.colTextPrimary;
            this.myCost = _myCost;
            this.baseCost = _myCost;
            //this.myQty = _myQty;
            this.myCostMult = _myCostMult;
            this.Name = myName;
            this.mySalary = _mySalary;
            this.latestUnlock = _latestUnlock;
            this.mySalaryTimeMS = _salaryTime;
            this.progressMining.Maximum = _salaryTime;
            this.displaySalPerSec = false;
            if (progressvalue > this.progressMining.Maximum)
            {
                this.myprogressvalue = _salaryTime;
            }
            else
            {
                this.myprogressvalue = progressvalue;
            }
            this.UpdateLabels();
        }
        public ItemView(ItemData data)
        {
            InitializeComponent();
            this.myCost = data.myCost;
            this.myQty = data.myQty;
            this.mySalary = data.mySalary;
            this.myID = data.myID;
            this.purchaseAmount = data.purchaseAmount;
            this.myCostMult = data.myCostMult;
            this.calculatedCost = data.calculatedCost;
            this.baseCost = data.baseCost;
            this.Colors = data.myColors;
            this.Name = data.myName;
            this.latestUnlock = data.latestUnlock;
            this.mySalaryTimeMS = data.mySalaryTimeMS;
            this.progressMining.Maximum = this.mySalaryTimeMS;
            this.displaySalPerSec = data.displaySalPerSec;
            if (data.progressvalueMS > this.progressMining.Maximum)
            {
                this.myprogressvalue = this.progressMining.Maximum;
            }
            else
            {
                this.myprogressvalue = data.progressvalueMS;
            }
        }
        private void ItemView_Load(object sender, EventArgs e)
        {
            tooltipTotalSal.SetToolTip(lblTotalSal, "Toggle between Total Salary & Total Salary Per Second");
            myTimer = new System.Windows.Forms.Timer();
            myTimer.Interval = 100;
            progressMining.Minimum = 0;
            progressMining.Maximum = mySalaryTimeMS; //if max==salarytime, we go through 11 cycles and either 1st or last update don't get drawn.
            if (mySalaryTimeMS < myTimer.Interval)
            {
                NormalizeSalary(this, myTimer.Interval);
            }
            if (this.myprogressvalue > 0 && this.myQty > 0)
            {
                progressMining.Value = this.myprogressvalue <= progressMining.Maximum ? this.myprogressvalue : progressMining.Maximum;
            }
            else { this.myprogressvalue = 0; }
            progressMining.Enabled = false;
            if (this.Parent != null && (this.Parent).Parent != null)
            {
                myTimer.Tick += new EventHandler(salaryTimer_Tick);

            }

            lblTotalSal.BackColor = Colors.colButtonEnabled;
            lblTotalSal.BorderStyle = BorderStyle.FixedSingle;
            lblQuantity.BackColor = Colors.colButtonEnabled;
            lblQuantity.BorderStyle = BorderStyle.FixedSingle;
            lblCost.BackColor = Colors.colButtonEnabled;
            lblCost.BorderStyle = BorderStyle.FixedSingle;
            lblSalPerSec.BackColor = Colors.colButtonEnabled;
            lblSalPerSec.BorderStyle = BorderStyle.FixedSingle;
            grpItem.BackColor = Colors.colBorders;
            BackColor = Colors.colBackground;
            lblTotalSal.ForeColor = Colors.colTextPrimary;    //black
            lblQuantity.ForeColor = Colors.colTextPrimary;
            lblCost.ForeColor = Colors.colTextPrimary;
            lblSalPerSec.ForeColor = Colors.colTextPrimary;
            grpItem.ForeColor = Colors.colTextPrimary;
            lblTimeLeft.BackColor = Colors.colButtonEnabled;
            lblTimeLeft.ForeColor = Colors.colTextPrimary;
        }
        /// <summary>
        /// Calculates and sets the properties in the given item for it's salary per timeinterval.
        /// Use this when an item's salarytimeMS is less than 100mS to prevent updating too quickly and blocking the UI thread.
        /// </summary>
        /// <param name="item">The ItemView item to be normalized.</param>
        /// <param name="timeinMS">Time period to earn salary, default 100mS.</param>
        public static void NormalizeSalary(ItemView item, int timeinMS = 100)
        {
            //salary / time = salpersec
            item.mySalary = (item.mySalary / (item.mySalaryTimeMS / 1000.0d)) * (timeinMS / 1000);
            item.mySalaryTimeMS = timeinMS;
        }
        public static double GetTotalSalPerSec(ItemView item)
        {
            return (item.mySalary / (item.mySalaryTimeMS / 1000.0d)) * item.myQty;
        }
        public static double GetIndSalPerSec(ItemView item)
        {
            return (item.mySalary / (item.mySalaryTimeMS / 1000.0d));
        }
        private void salaryTimer_Tick(object? sender, EventArgs e)
        {
            ((frmMain)((this.Parent).Parent)).itemTimer_Tick(this, e);

        }
        public void UpdateLabels()
        {
            //no calculations, just update labels/buttons.

            this.lblCost.Text = $"Cost: ${(double.Round(calculatedCost, 2) > 1000000.0d ? (frmMain.Stringify(calculatedCost.ToString("R"), StringifyOptions.LongText)) : double.Round(calculatedCost, 2).ToString("N"))}";
            this.lblQuantity.Text = $"Qty: {myQty:N0}";
            this.grpItem.Text = this.Name;
            this.lblTimeLeft.Text = $"Time: {double.Round(((1.0d - ((double)this.myprogressvalue / (double)this.mySalaryTimeMS)) * this.mySalaryTimeMS) / 1000, 1).ToString("N1")}s";
            this.btnBuy.Text = $"Purchase x{this.purchaseAmount:N0}";
            if (this.displaySalPerSec)
            {
                //Display calculated salary per second
                this.lblTotalSal.Text = $"Salary / Sec: ${(ItemView.GetTotalSalPerSec(this) > 1000000.0d ? frmMain.Stringify((ItemView.GetTotalSalPerSec(this)).ToString("R"), StringifyOptions.LongText) : double.Round(ItemView.GetTotalSalPerSec(this), 2).ToString("N"))}";
                this.lblSalPerSec.Text = $"Salary/S: ${(double.Round(ItemView.GetIndSalPerSec(this), 2) > 1000000.0d ? frmMain.Stringify(ItemView.GetIndSalPerSec(this).ToString("R"), StringifyOptions.LongText) : double.Round(ItemView.GetIndSalPerSec(this), 2).ToString("N"))}";
            }
            else
            {
                //Display total salary per payout period
                this.lblTotalSal.Text = $"Total Salary: ${(this.mySalary * this.myQty > 1000000.0d ? frmMain.Stringify((this.mySalary * this.myQty).ToString("R"), StringifyOptions.LongText) : double.Round(this.mySalary * this.myQty, 2).ToString("N"))}";
                this.lblSalPerSec.Text = $"Salary: ${(double.Round(this.mySalary, 2) > 1000000.0d ? frmMain.Stringify(this.mySalary.ToString("R"), StringifyOptions.LongText) : double.Round(this.mySalary, 2).ToString("N"))}";//double.Round(this.mySalary,
            }
        }

        private void ItemView_Paint(object sender, PaintEventArgs e)
        {
            //this causes unexpected problems, because the main form raises this event on mouseover or focus switch.
        }

        public void ButtonColor(double myMoney, int purchaseQty, double costMultiplier)
        {
            //calculates whether purchase can be done given current balance, purchase amount, and cost multiplier, based on current cost of this item.
            this.btnBuy.BackColor = this.CanAfford(myMoney, purchaseQty, costMultiplier) && purchaseQty > 0 ? Colors.colButtonEnabled : Colors.colButtonDisabled;
            this.btnBuy.ForeColor = this.CanAfford(myMoney, purchaseQty, costMultiplier) && purchaseQty > 0 ? Colors.colTextPrimary : Colors.colTextSecondary;
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
                throw new NullReferenceException(); //this control has no parent!? WTF!? Luckily haven't seen this one yet, and we never should...
            }


        }

        private void lblSalPerSec_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalSal_Click(object sender, EventArgs e)
        {
            //simple toggle
            //this.displaySalPerSec = !this.displaySalPerSec;
            ((frmMain)(this.Parent).Parent).ToggleItemSalaryDisplays();
        }

        
    }
    [Serializable]
    public class ItemData
    {
        public double myCost;
        public int myQty;
        public double mySalary;
        public int myID;
        public int purchaseAmount;
        public double myCostMult;
        public double calculatedCost;
        public double baseCost;
        public MyColors myColors;
        public string myName;
        public int latestUnlock;
        public int mySalaryTimeMS;
        public int progressvalueMS;
        public bool displaySalPerSec;

        public ItemData(ItemView item)
        {
            this.myCost = item.myCost;
            this.myQty = item.myQty;
            this.mySalary = item.mySalary;
            this.myID = item.myID;
            this.purchaseAmount = item.purchaseAmount;
            this.myCostMult = item.myCostMult;
            this.calculatedCost = item.calculatedCost;
            this.baseCost = item.baseCost;
            this.myColors = item.Colors;
            this.myName = item.Name;
            this.latestUnlock = item.latestUnlock;
            this.mySalaryTimeMS = item.mySalaryTimeMS;
            this.progressvalueMS = item.myprogressvalue;
            this.displaySalPerSec = item.displaySalPerSec;
        }
        public ItemData(double myCost, int myQty, double mySalary, int myID, int purchaseAmount, double myCostMult, double calculatedCost, double baseCost, MyColors myColors, string myName, int mySalaryTimeMS, int latestUnlock = -1, int myprogressvalue = 0)
        {
            this.myCost = myCost;
            this.myQty = myQty;
            this.mySalary = mySalary;
            this.myID = myID;
            this.purchaseAmount = purchaseAmount;
            this.myCostMult = myCostMult;
            this.calculatedCost = calculatedCost;
            this.baseCost = baseCost;
            this.myColors = myColors;
            this.myName = myName;
            this.latestUnlock = latestUnlock;
            this.mySalaryTimeMS = mySalaryTimeMS;
            this.progressvalueMS = myprogressvalue;
            this.displaySalPerSec = false;
        }
    }

    
}
