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
        public int matsMined;
        public int incrperclick = 1;

        public ItemView[] myItems;
        public List<Button> upgradeButtons;
        public string[][] upgradeArrays;

        public frmMain()
        {
            InitializeComponent();
            matsMined = 0;
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
            upgradeButtons = new List<Button>();    //basic buttons for now - create new custom control for upgrades containing a button and some
                                                    //labels for information like price, description, etc. Keep them invisible until user buys at least
                                                    //one related item. Buttons (and items) should also be sorted within their list by ItemView.myID.
                                                    //Or should we just use the index of the button within the array, and update the tag?
            upgradeButtons.Add(new Button());   //Add upgrade button for clickamount upgrades.
            foreach (var item in myItems) { upgradeButtons.Add(new Button()); } //add upgrade button for each item.
            //(ID, Name, Cost, Multiplier, Quantity)
            //8 available items right now

            //We should implement autoupgrades that double individual salaries at ownership thresholds, like, for example:
            //double when qty = 10, 25, 100, 200, 500, 1000
            //maybe clickamount should increase when money thresholds are reached:
            //money = $10, $100, $1000, etc, clickamount*=2?
            //Also we should have upgrades. Expensive, but worth it, for both click amount and salary.
            //Also, a prestige system, which uses prestige points earned to further multiply salary per second per item and initial click amount.
            //Also, a window to keep track of stats, like amount made, upgrades purchased, prestige points, etc
            //Need to implement save states/loading, and calculate time between last save/current load, add to playtime stat, and multiply by $/sec to
            //add to money, thus making it a true 'idle' game. Right now it's just more of a clicker.

            //Upgrades should probably be broken out into an xml file or similar.
            string[] clickUpgrades = { "Doubletap", "Click Amplifier", "Mega-clicking", "Click Physics", "Parallel Clicking" };
            string[] item1Upgrades = { "Birch Wood", "Pine Wood", "Oak Wood", "Cherry Wood", "Sequioa Wood" };
            string[] item2Upgrades = { "Sandstone", "Granite", "Limestone", "Marble", "Slate" };
            string[] item3Upgrades = { "Picked Iron", "Cast Iron", "Molded Iron", "Rolled Iron", "Forged Iron" };
            string[] item4Upgrades = { "Weak Steel", "Better Steel", "Rolled Steel", "Strong Steel", "Venutian Steel" };
            string[] item5Upgrades = { "Flawed Diamond", "Improved Diamond", "Flawless Diamond", "Synthetic Diamond", "Quantum Diamond" };
            string[] item6Upgrades = { "Waste Uranium", "Mined Uranium", "Refined Uranium", "Synthetic Uranium", "Quantum Uranium" };
            string[] item7Upgrades = { "Low Yield Antimatter", "Mid Yield Antimatter", "High Yield Antimatter", "Perfect Antimatter", "CPT Reversed Antimatter" };
            string[] item8Upgrades = { "Plank Black Hole", "Primordial Black Hole", "Rogue Black Hole", "Supermassive Black Hole", "Universal Black Hole" };
            upgradeArrays = new string[][] { clickUpgrades, item1Upgrades, item2Upgrades, item3Upgrades, item4Upgrades, item5Upgrades, item6Upgrades, item7Upgrades, item8Upgrades };
            //when calculating ownership, use an int representing number of upgrades owned. This can be retroactively used to determine next upgrade to buy. Cost is a whole different thing. Maybe a parseable string?
            //Or a decimal[] containing base costs for upgrades, to be multiplied by itemID's to determine upgrade costs?
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Populate form with possible items from array, then update their labels.
            for (int i = 1; i <= myItems.Length; i++)
            {
                itemPanel.Controls.Add(myItems[i - 1]);  //add all items to itempanel
            }

            for (int i = 0; i < upgradeButtons.Count; i++)
            {
                upgradeButtons[i].Tag = new int[] { i, 0 }; //tag contains itemID, upgradelevel. itemID==0 is clickAmount upgrade button.
                upgradeButtons[i].Click += new EventHandler(upgradeClicked);
                upgradeButtons[i].Width = (int)(UpgradePanel.Width * 0.85);  //button width is 85% of panel width
                upgradeButtons[i].Height = (int)(upgradeButtons[i].Width * 0.40); //button height is 40% of button width
                upgradeButtons[i].Text = upgradeArrays[i][0];   //Button text is the first upgrade available in each item's upgrades array
                UpgradePanel.Controls.Add(upgradeButtons[i]);   //add all upgrade buttons to upgradepanel
            }
            foreach (ItemView item in myItems) { item.UpdateLabels(); }
            //frmMain_UpdateLabels();
            this.timerPerSec.Start();
            this.timerVisualUpdate.Start();
        }
        public void upgradeClicked(object? sender, EventArgs e)
        {
            if (sender == null || ((Button)sender).Tag == null) { return; } //basic protection
                                                                            //if you can afford it:
                                                                            //determine from 'tag' property which item it's for, and which upgrade you're trying to buy. Maybe 'Upgrade' should
                                                                            //be it's own class/object? Then it could store all relevant data, and be passed through the 'tag' property. 
                                                                            //if it's all valid, purchase the upgrade and apply the new multiplier to the relevant item's salary property.
                                                                            //then iterate the button text and price to reflect the next upgrade in the relevant array.

            //TO DO --- ADD UPGRADE COST LOGIC
            else
            {
                int[] btnvars = ((Button)sender).Tag as int[];
                if (btnvars[0] >= 1 && btnvars[0] <= myItems.Length)
                {
                    //btnvars itemID is between 1 and the last itemID in myItems, so ItemUpgrade
                    if (btnvars[1] < upgradeArrays[btnvars[0]].Length - 1)
                    {
                        btnvars[1]++;
                        myItems[btnvars[0] - 1].mySalary *= 2;  //double related salary
                        ((Button)sender).Text = upgradeArrays[btnvars[0]][btnvars[1]];
                        //update cost of upgrade
                    }
                    else if (btnvars[1] == upgradeArrays[btnvars[0]].Length - 1)
                    {
                        //max upgrade reached
                        btnvars[1]++;
                        myItems[btnvars[0] - 1].mySalary *= 2;
                        ((Button)sender).Text = "Maximum Reached";
                        ((Button)sender).Enabled = false;
                    }
                }
                else if (btnvars[0] == 0)
                {
                    //clickAmount upgrade
                    if (btnvars[1] < upgradeArrays[0].Length - 1)
                    {
                        //max owned upgrade is (5)-1=4
                        btnvars[1]++;
                        clickAmount *= 2;   //double clickamount
                        ((Button)sender).Text = upgradeArrays[0][btnvars[1]];
                        //update cost of upgrade
                    }
                    else if (btnvars[1] == upgradeArrays[0].Length - 1)
                    {
                        btnvars[1]++;
                        clickAmount *= 2;
                        ((Button)sender).Text = "Maximum Reached";
                        ((Button)sender).Enabled = false;
                    }
                }

            }
        }
        public void frmMain_UpdateLabels()
        {
            lblMoney.Text = $"Money: ${decimal.Round(myMoney, 2):N}";
            lblSalary.Text = $"${decimal.Round(salary, 2):N} Per Second, ${decimal.Round(clickAmount, 2):N} Per Click";
            lblIncrPerClick.Text = $"Mined Per Click: {incrperclick:N0}";
            lblMatsMined.Text = $"Materials Mined: {matsMined:N0}";
            btnPurchAmount.Text = $"Buy: x{this.purchAmount}";
        }

        private void frmMain_Click(object sender, EventArgs e)
        {

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
        }
        public void BuyClicked(ItemView sender)
        {
            if (sender.CanAfford(this.myMoney, purchAmount, costMult))
            {
                myMoney -= sender.calculatedCost;
                sender.myQty += sender.purchaseAmount; //how to implement progressive upgrades(i.e. value checking)?
                sender.myCost = sender.myCost * (decimal)Math.Pow((double)costMult, sender.purchaseAmount);
            }
            sender.UpdateLabels();
            //frmMain_UpdateLabels();
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
            //frmMain_UpdateLabels(); //this function updates the button text already

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

        private void btnMine_Click(object sender, EventArgs e)
        {
            if (matsMined != 0 && matsMined + incrperclick >= (Math.Round((double)matsMined / 100.0d, MidpointRounding.ToPositiveInfinity) * 100) && incrperclick < 10 && matsMined != (Math.Round((double)matsMined / 100.0d, MidpointRounding.ToPositiveInfinity) * 100))
            {
                matsMined += incrperclick;
                incrperclick++;
                clickAmount = (clickAmount / (incrperclick - 1)) * incrperclick;

            } //for now, when matsMined reaches another multiple of 100, amountperclick is incremented. clickAmount reflects this.
            //incrperclick has a max of 10 for now.
            else { matsMined += incrperclick; }

            this.myMoney += clickAmount;
            //frmMain_UpdateLabels();
        }

        private void timerVisualUpdate_Tick(object sender, EventArgs e)
        {
            foreach (ItemView item in myItems)
            {
                item.ButtonColor(myMoney, purchAmount, costMult);
                item.UpdateLabels();
            }
            //TODO: Add upgrade labelupdates and buttoncolor updates
            frmMain_UpdateLabels();
        }
    }
}
