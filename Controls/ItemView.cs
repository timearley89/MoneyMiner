﻿using System;
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
        public Stopwatch mystopwatch;
        public int mySalaryTimeMS = 1000;
        
        public ItemView(int myID, string myName, double _myCost, double _myCostMult, double _mySalary, int _latestUnlock = -1)
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
        }
        private void ItemView_Load(object sender, EventArgs e)
        {
            myTimer = new System.Windows.Forms.Timer();
            mystopwatch = new Stopwatch();
            myTimer.Interval = mySalaryTimeMS / 10;
            progressMining.Value = 0;
            progressMining.Maximum = (mySalaryTimeMS / 10) * 9; //if max==salarytime, we go through 11 cycles and either 1st or last update don't get drawn.
            progressMining.Minimum = 0;
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
            this.lblTotalSal.Text = $"Total Salary: ${(this.mySalary * this.myQty > 1000000.0d ? frmMain.Stringify((this.mySalary * this.myQty).ToString("R"), StringifyOptions.LongText) : double.Round(this.mySalary * this.myQty, 2).ToString("N"))}";//double.Round((this.mySalary * this.myQty), 2):N
            this.lblSalPerSec.Text = $"Salary: ${(double.Round(this.mySalary, 2) > 1000000.0d ? frmMain.Stringify(this.mySalary.ToString("R"), StringifyOptions.LongText) : double.Round(this.mySalary, 2).ToString("N"))}";//double.Round(this.mySalary, 2):N
            this.grpItem.Text = this.Name;

            this.btnBuy.Text = $"Purchase x{this.purchaseAmount:N0}";
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
        public int timeRemainingMS;

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
            //this.timeRemainingMS = item.mySalaryTimeMS - (int)((double)item.progressMining.Value / ((double)item.progressMining.Maximum));
        }
        public ItemData(double myCost, int myQty, double mySalary, int myID, int purchaseAmount, double myCostMult, double calculatedCost, double baseCost, MyColors myColors, string myName, int latestUnlock = -1)
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
        }
    }

    
}
