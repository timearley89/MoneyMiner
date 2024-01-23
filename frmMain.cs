using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FirstClicker.Controls;

namespace FirstClicker
{

    public partial class frmMain : Form
    {

        public MyColors Colors;
        public double myMoney; //max value currently is Decimal.MaxValue, which is just over 79 Octillion. Need to restructure this.
        public double salary;
        public double clickAmount;
        public double thislifetimeMoney;    //needs to accumulate all money earned, just without subtracting from when item is purchased
        public double lastlifetimeMoney;
        public double prestigePoints;
        public double prestigeMultiplier = 2.0d; //2% initially
        private int purchAmount = 1;
        public int matsMined;
        public int incrperclick = 1;

        public ItemView[] myItems;
        public List<UpgradeButton> upgradeButtons;

        public frmMain(double lastlifeMoney = 0.0d, double prestPoints = 0.0d)
        {
            this.Colors = new MyColors();
            this.BackColor = Colors.colTertiary;
            
            InitializeComponent();
            if (this.thislifetimeMoney == default)
            {
                this.thislifetimeMoney = lastlifeMoney;
            }
            if (this.prestigePoints == default)
            {
                this.prestigePoints = prestPoints;
            }
            btnMine.BackColor = Colors.colSecondary;
            if (this.lastlifetimeMoney == default) { this.lastlifetimeMoney = lastlifeMoney; }
            matsMined = 0;
            myMoney = 1000.00d;    //debug only if not $0.00
            salary = 0.00d;
            clickAmount = 0.25d; //initial value, temporary
            myItems = [new ItemView(1, "Wood", 3.738d, 1.07d, 1.67d),
                new ItemView(2, "Stone", 60d, 1.15d, 20d),
                new ItemView(3, "Iron", 720d, 1.14d, 90d),
                new ItemView(4, "Steel", 8640d, 1.13d, 360d),
                new ItemView(5, "Diamond", 103680d, 1.12d, 2160d),
                new ItemView(6, "Uranium", 1244160d, 1.11d, 6480d),
                new ItemView(7, "Antimatter", 14929920d, 1.10d, 19440d),
                new ItemView(8, "Black Hole", 179159040d, 1.09d, 58320d)];
            upgradeButtons = new List<UpgradeButton>();    //basic buttons for now - create new custom control for upgrades containing a button and labels for display

            if (this.prestigePoints > 0.0d)
            {
                this.clickAmount *= ((prestigePoints / (100.0d / prestigeMultiplier)) + 1);
            }
            foreach (var item in myItems) 
            {
                //upgradeButtons.Add(new UpgradeButton());
                if (this.prestigePoints > 0.0d)
                {
                    item.mySalary *= ((prestigePoints / (100.0d / prestigeMultiplier)) + 1);
                }
            } //add upgrade button for each item and adjust salary for prestigepoints.
            //(ID, Name, baseCost, Multiplier, Salary)
            //8 available items right now

            

            //We should implement autoupgrades that double individual salaries at ownership thresholds, like, for example:
            //double when qty = 25, 50, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000
            
            //Also, a window to keep track of stats, like amount made, upgrades purchased, prestige points, etc

            //Need to implement save states/loading, and calculate time between last save/current load, add to playtime stat, and multiply by $/sec to
            //add to money, thus making it a true 'idle' game. Right now it's just more of a clicker.

            //Upgrades are declared as: (string Name, double Cost, int ItemID, double Multiplier). They can only be created or altered through their constructors.
            List<Upgrade> MainUpgradeList =
            [
                new Upgrade("Double Tap", 1000.0d, 0, 3), //Click Upgrades
                new Upgrade("Click Amplifier", 7500.0d, 0, 3),
                new Upgrade("Mega-Clicking", 15000.0d, 0, 3),
                new Upgrade("Click Physics", 40000.0d, 0, 3),
                new Upgrade("Parallel Clicking", 100000.0d, 0, 3),
                new Upgrade("Birch Wood", 250000.0d, 1, 3), //Item1 Upgrades
                new Upgrade("Pine Wood", 20000000000000.0d, 1, 3),
                new Upgrade("Oak Wood", 2000000000000000000.0d, 1, 3),
                new Upgrade("Cherry Wood", 25000000000000000000000.0d, 1, 3),
                new Upgrade("Sequoia Wood", 1000000000000000000000000000.0d, 1, 7),
                new Upgrade("Sandstone", 500000.0d, 2, 3), //Item2 Upgrades
                new Upgrade("Granite", 50000000000000.0d, 2, 3),
                new Upgrade("Limestone", 5000000000000000000.0d, 2, 3),
                new Upgrade("Marble", 50000000000000000000000.0d, 2, 3),
                new Upgrade("Slate", 5000000000000000000000000000.0d, 2, 7),
                new Upgrade("Picked Iron", 1000000.0d, 3, 3), //Item3 Upgrades
                new Upgrade("Cast Iron", 100000000000000.0d, 3, 3),
                new Upgrade("Molded Iron", 7000000000000000000.0d, 3, 3),
                new Upgrade("Forged Iron", 100000000000000000000000.0d, 3, 3),
                new Upgrade("Cold-Fused Iron", 25000000000000000000000000000.0d, 3, 7),
                new Upgrade("Basic Steel", 5000000.0d, 4, 3), //Item4 Upgrades
                new Upgrade("Tempered Steel", 500000000000000.0d, 4, 3),
                new Upgrade("Rolled Steel", 10000000000000000000.0d, 4, 3),
                new Upgrade("Steel Alloy", 200000000000000000000000.0d, 4, 3),
                new Upgrade("Venutian Steel", 100000000000000000000000000000.0d, 4, 7),
                new Upgrade("Flawed Diamond", 10000000.0d, 5, 3), //Item5 Upgrades
                new Upgrade("Improved Diamond", 1000000000000000.0d, 5, 3),
                new Upgrade("Flawless Diamond", 20000000000000000000.0d, 5, 3),
                new Upgrade("Synthetic Diamond", 300000000000000000000000.0d, 5, 3),
                new Upgrade("Quantum Diamond", 250000000000000000000000000000.0d, 5, 7),
                new Upgrade("Waste Uranium", 25000000.0d, 6, 3), //Item6 Upgrades
                new Upgrade("Mined Uranium", 2000000000000000.0d, 6, 3),
                new Upgrade("Refined Uranium", 35000000000000000000.0d, 6, 3),
                new Upgrade("Synthetic Uranium", 400000000000000000000000.0d, 6, 3),
                new Upgrade("Quantum Uranium", 500000000000000000000000000000.0d, 6, 7),
                new Upgrade("Low Yield Antimatter", 500000000.0d, 7, 3), //Item7 Upgrades
                new Upgrade("Mid Yield Antimatter", 5000000000000000.0d, 7, 3),
                new Upgrade("High Yield Antimatter", 50000000000000000000.0d, 7, 3),
                new Upgrade("Perfect Antimatter", 400000000000000000000000.0d, 7, 3),
                new Upgrade("CPT Reversed Antimatter", 1000000000000000000000000000000.0d, 7, 7),
                new Upgrade("Plank Black Hole", 10000000000.0d, 8, 3), //Item8 Upgrades
                new Upgrade("Primordial Black Hole", 7000000000000000.0d, 8, 3),
                new Upgrade("Rogue Black Hole", 75000000000000000000.0d, 8, 3),
                new Upgrade("Supermassive Black Hole", 600000000000000000000000.0d, 8, 7),
                new Upgrade("Universal Black Hole", 5000000000000000000000000000000.0d, 8, 7),
                new Upgrade("Tax Adjustment", 1000000000000.0d, 15, 3), //All-Item Upgrades
                new Upgrade("Ledger Spoofing", 50000000000000000.0d, 15, 3),
                new Upgrade("Illegal Workers", 500000000000000000000.0d, 15, 3),
                new Upgrade("Off-Shore Mining", 900000000000000000000000.0d, 15, 3),
                new Upgrade("Cult Following", 1000000000000000000000000000000000.0d, 15, 7),
                new Upgrade("Material Lobbying", 100000000000000000.0d, 20, 1.01), //Prestige Upgrades
                new Upgrade("Investor Fraud", 1000000000000000000000.0d, 20, 1.01),
                new Upgrade("Controlled Striking", 10000000000000000000000000.0d, 20, 1.02),
                new Upgrade("Space Investing", 1000000000000000000000000000000000000.0d, 20, 1.05),
                new Upgrade("Planetary Ransom", 1000000000000000000000000000000000000000.0d, 20, 1.06)
                ];
            MainUpgradeList = (List<Upgrade>)MainUpgradeList.OrderBy(x => x.Cost).ToList();
            //Upgrades should probably be broken out into an xml file or similar.
            

            foreach (Upgrade upgrade in MainUpgradeList)
            {
                UpgradeButton btn = new UpgradeButton();
                btn.Text = upgrade.Description + $"\n${upgrade.Cost:N0}";
                btn.myUpgrade = upgrade;
                btn.BackColor = Colors.colDisable;
                btn.Enabled = false;
                upgradeButtons.Add(btn);
            }

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
                //itemID==0 is clickAmount upgrade button.
                //itemID==15 is allitem upgrade, itemID==20 is prestigemultiplier upgrade.
                
                upgradeButtons[i].Click += new EventHandler(upgradeClicked);
                upgradeButtons[i].Width = (int)(UpgradePanel.Width * 0.85);  //button width is 85% of panel width
                upgradeButtons[i].Height = (int)(upgradeButtons[i].Width * 0.40); //button height is 40% of button width
                upgradeButtons[i].BackColorChanged += new EventHandler(btncolorchanged);
                upgradeButtons[i].Enabled = false;
                UpgradePanel.Controls.Add(upgradeButtons[i]);   //add all upgrade buttons to upgradepanel
            }
            foreach (ItemView item in myItems) { item.UpdateLabels(); }
            //frmMain_UpdateLabels();
            this.timerPerSec.Start();
            this.timerVisualUpdate.Start();
        }
        public void btncolorchanged(Object? sender, EventArgs e)
        {
            //MessageBox.Show("");
        }
        public void upgradeClicked(Object? sender, EventArgs e)
        {
            if (sender == null) { return; } //basic protection

            else
            {
                UpgradeButton btnsender = (UpgradeButton)sender;
                int btnitemID = btnsender.myUpgrade.itemID;
                if (btnitemID >= 1 && btnitemID <= myItems.Length)
                {
                    //btnvars itemID is between 1 and the last itemID in myItems, so ItemUpgrade
                    if (myMoney >= btnsender.myUpgrade.Cost)
                    {
                        btnsender.Enabled = false;
                        btnsender.BackColor = Colors.colPrimary;
                        myMoney -= btnsender.myUpgrade.Cost;
                        myItems[btnitemID - 1].mySalary *= btnsender.myUpgrade.Multiplier;
                        btnsender.myUpgrade = Upgrade.SetPurchased(btnsender.myUpgrade);
                        btnsender.Text = $"{btnsender.myUpgrade.Description}\nPurchased!";
                    }
                    else
                    {
                        btnsender.Enabled = false;
                        btnsender.BackColor = Colors.colDisable;
                    }
                }
                else if (btnitemID == 0)
                {
                    //clickAmount upgrade, may be removed entirely, not sure yet
                    if (myMoney >= btnsender.myUpgrade.Cost)
                    {
                        btnsender.Enabled = false;
                        btnsender.BackColor = Colors.colPrimary;
                        myMoney -= btnsender.myUpgrade.Cost;
                        clickAmount *= btnsender.myUpgrade.Multiplier;
                        //had to make 'SetPurchased' a static method that returned an object reference. For some reason it wasn't updating the object passed to it before...
                        btnsender.myUpgrade = Upgrade.SetPurchased(btnsender.myUpgrade);
                        btnsender.Text = $"{btnsender.myUpgrade.Description}\nPurchased!";
                    }
                    else
                    {
                        btnsender.Enabled = false;
                        btnsender.BackColor = Colors.colDisable;
                    }
                }
                else if (btnitemID == 15)
                {
                    //All Items Upgrade
                    if (myMoney >= btnsender.myUpgrade.Cost)
                    {
                        btnsender.Enabled = false;
                        btnsender.BackColor = Colors.colPrimary;
                        myMoney -= btnsender.myUpgrade.Cost;
                        foreach (ItemView item in myItems)
                        {
                            item.mySalary *= btnsender.myUpgrade.Multiplier;
                        }
                        btnsender.myUpgrade = Upgrade.SetPurchased(btnsender.myUpgrade);
                        btnsender.Text = $"{btnsender.myUpgrade.Description}\nPurchased!";
                    }
                    else
                    {
                        btnsender.Enabled = false;
                        btnsender.BackColor = Colors.colDisable;
                    }
                    
                }
                else if (btnitemID == 20)
                {
                    //prestige points upgrade
                    if (myMoney >= btnsender.myUpgrade.Cost)
                    {
                        btnsender.Enabled = false;
                        btnsender.BackColor = Colors.colPrimary;
                        double newmult = (btnsender.myUpgrade.Multiplier * 100) - 100;
                        prestigeMultiplier += newmult;
                        btnsender.myUpgrade = Upgrade.SetPurchased(btnsender.myUpgrade);
                        btnsender.Text = $"{btnsender.myUpgrade.Description}\nPurchased!";
                    }
                    else
                    {
                        btnsender.Enabled = false;
                        btnsender.BackColor = Colors.colDisable;
                    }
                }
            }
        }

        public double calcPrestige(double lastlifeMoney, double thislifeMoney)
        {
            double newlifeMoney = lastlifeMoney + thislifeMoney;
            double term1 = newlifeMoney / Math.Pow((400000000000.0d / 9), 0.5d);
            double term2 = lastlifeMoney / Math.Pow((4000000000.0d / 9), 0.5d);
            return term1 - term2 >= 0 ? double.Round(term1 - term2, MidpointRounding.ToZero) : 0;
        }

        public void frmMain_UpdateLabels()
        {
            lblMoney.Text = $"Money: ${double.Round(myMoney, 2):N}";
            lblSalary.Text = $"${double.Round(salary, 2):N} Per Second, ${double.Round(clickAmount, 2):N} Per Click";
            lblIncrPerClick.Text = $"Mined Per Click: {incrperclick:N0}";
            lblMatsMined.Text = $"Materials Mined: {matsMined:N0}";
            if (this.purchAmount == 1 || this.purchAmount == 10 || this.purchAmount == 100)
            {
                btnPurchAmount.Text = $"Buy: x{this.purchAmount}";
            }
            else
            {
                btnPurchAmount.Text = "Buy: Max";
            }

            foreach (UpgradeButton btn in upgradeButtons)
            {
                if (btn.myUpgrade.Purchased) {
                    btn.Enabled = false; 
                    btn.BackColor = Colors.colTertiary; 
                    btn.ForeColor = Color.White; 
                    continue; }
                //if we can afford it and haven't bought it, enable it and turn it green, if not, disable it and turn it gray. real simple.
                else if (!(btn.myUpgrade.Purchased)) 
                { 
                    //if not purchased
                    if (myMoney >= btn.myUpgrade.Cost)
                    {
                        //can afford
                        btn.Enabled = true; 
                        btn.BackColor = Colors.colPrimary; 
                    }
                    else 
                    { 
                        //can't afford
                        btn.Enabled = false; 
                        btn.BackColor = Colors.colDisable; 
                    }
                } 
                

            }
        }

        private void frmMain_Click(object sender, EventArgs e)
        {
            //MessageBox.Show($"Max: {calcMax(myMoney, myItems[0]).ToString()} items, Cost for {purchAmount}: ${costcalcnew(myItems[0], purchAmount)}"); --debug
            //calcMax seems to work perfectly. costcalcnew works perfectly.
        }

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            //this.lblMoneyAmt.Text = myMoney.ToString(); --debug
        }

        private void timerPerSec_Tick(object sender, EventArgs e)
        {
            //update button colors based on affordability
            //update money amount based on quantity of items owned and any multipliers
            //update labels and buttons as needed
            double tempsal = 0.00d;    //iterate through items owned and calculate total salary per cycle.
            foreach (ItemView view in myItems)
            {
                tempsal += (view.mySalary * view.myQty);    //if qty is 0, salary increment will be 0.
            }
            salary = tempsal;
            myMoney += salary;  
            thislifetimeMoney += salary;
        }
        public void BuyClicked(ItemView sender)
        {
            if (sender.CanAfford(this.myMoney, sender.purchaseAmount, sender.myCostMult))
            {
                myMoney -= sender.calculatedCost;
                sender.myQty += sender.purchaseAmount; //how to implement progressive upgrades(i.e. value checking)?
                sender.myCost = sender.myCost * Math.Pow(sender.myCostMult, sender.purchaseAmount);
            }
            sender.UpdateLabels();
            //frmMain_UpdateLabels();
            sender.ButtonColor(myMoney, sender.purchaseAmount, sender.myCostMult);
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
                purchAmount = -1;
            }
            else if (purchAmount == -1)
            {
                purchAmount = 1;
            }
            //frmMain_UpdateLabels(); //this function updates the button text already

            if (purchAmount == 1 || purchAmount == 10 || purchAmount == 100)
            {
                for (int i = 0; i < myItems.Length; i++)
                {
                    //we just need to update the item with the new amount, and then update the labels and colors.
                    myItems[i].purchaseAmount = purchAmount;
                    myItems[i].CalcCost(myItems[i].purchaseAmount, myItems[i].myCostMult);
                    myItems[i].ButtonColor(myMoney, myItems[i].purchaseAmount, myItems[i].myCostMult);
                    myItems[i].UpdateLabels();
                }
            }
            else
            {
                for (int i = 0; i < myItems.Length; i++)
                {
                    int temppurchamount = (int)calcMax(myMoney, myItems[i]);
                    //myItems[i].CalcCost(temppurchamount, myItems[i].myCostMult);
                    myItems[i].calculatedCost = this.costcalcnew(myItems[i], temppurchamount);
                    myItems[i].purchaseAmount = temppurchamount;
                    myItems[i].ButtonColor(myMoney, myItems[i].purchaseAmount, myItems[i].myCostMult);
                    myItems[i].UpdateLabels();
                }
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
            thislifetimeMoney += clickAmount;
            //frmMain_UpdateLabels();
        }

        private void timerVisualUpdate_Tick(object sender, EventArgs e)
        {
            foreach (ItemView item in myItems)
            {
                if (this.purchAmount == -1) { item.purchaseAmount = (int)calcMax(myMoney, item); }  //only used for max calculation updates
                item.ButtonColor(myMoney, item.purchaseAmount, item.myCostMult);
                if (item.calculatedCost == 0.00d) { item.calculatedCost = costcalcnew(item, 1); }   //prevent showing $0 in item cost field
                item.UpdateLabels();
            }
            //TODO: Add upgrade labelupdates and buttoncolor updates
            frmMain_UpdateLabels();
        }

        public long calcMax(double balance, ItemView item)
        {
            long maxbuy = (long)Math.Floor((Math.Log(((balance * (item.myCostMult - 1)) / (item.baseCost * Math.Pow(item.myCostMult, item.myQty))) + 1) / Math.Log(item.myCostMult)));
            return maxbuy;
        }
        public double costcalcnew(ItemView item, int purchqty)
        {
            return ((item.baseCost) * (((Math.Pow(item.myCostMult, item.myQty) * (Math.Pow(item.myCostMult, purchqty) - 1)) / (item.myCostMult - 1))));

        }

        private void btnPrestige_Click(object sender, EventArgs e)
        {
            //In order for this to work, I need to refactor the main game logic into it's own gameobject that takes parameters for prestige amount, and default params(overrideable) for money, upgrades, purchased items, etc.
            double tempprestige = this.calcPrestige(lastlifetimeMoney, thislifetimeMoney);
            this.timerPerSec.Stop();
            this.timerVisualUpdate.Stop();
            DialogResult dres = MessageBox.Show($"Current Prestige: {this.prestigePoints:N0}. \nPrestige to Gain: {tempprestige:N0}. Prestige?", "Reset to earn prestige?", MessageBoxButtons.YesNo);
            if (dres == DialogResult.Yes)
            {
                
                frmMain newLife = new(thislifetimeMoney + lastlifetimeMoney, tempprestige);
                newLife.Show();
                this.timerPerSec.Stop();
                this.timerVisualUpdate.Stop();
                this.Hide();
            }
            else if (dres == DialogResult.No)
            {
                timerPerSec.Start();
                timerVisualUpdate.Start();
            }
        }
    }
    internal struct Upgrade(string description, double cost, int ID, double multiplier)
    {
        //public getters and struct declaration means we can read the struct and it's properties from anywhere, even a different namespace.
        //private fields and internal constructor mean that only the struct can access it's fields, and only this assembly can create new upgrades.
        public string Description { get { return _description; } }
        public double Cost { get { return _cost; } }
        public int itemID { get { return _itemID; } }
        public double Multiplier { get { return _multiplier; } }
        public bool Purchased { get { return _purchased; } }

        private string _description = description;
        private double _cost = cost;
        private int _itemID = ID;
        private double _multiplier = multiplier;
        public bool _purchased;

        internal static Upgrade SetPurchased(Upgrade myUpgrade)
        {
            myUpgrade._purchased = true;
            return myUpgrade;
        }
    }
    public class UpgradeButton : Button
    {
        internal Upgrade myUpgrade { get; set; }
        
    }
    public class MyColors
    {
        public Color colPrimary = Color.Green;
        public Color colSecondary = Color.SandyBrown;
        public Color colTertiary = Color.Tan;
        public Color colDisable = Color.Gray;
    }
}
