using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FirstClicker.Controls;
using static FirstClicker.Upgrade;

namespace FirstClicker
{

    public partial class frmMain : Form
    {

        public MyColors Colors;
        public double myMoney;
        public double salary;
        public double clickAmount;
        public double thislifetimeMoney;
        public double lastlifetimeMoney;
        public double prestigePoints;
        public double prestigeMultiplier;
        private int purchAmount;
        public int matsMined;
        public int incrperclick;
        public int toolTipDelay;
        public int toolTipVisibleTime;
        public TimeSpan thislifeGameTime;
        public TimeSpan totalGameTime;

        public ItemView[] myItems;
        public List<UpgradeButton> upgradeButtons;
        internal List<Upgrade> MainUpgradeList;

        public frmMain(double lastlifeMoney = 0.0d, double prestPoints = 0.0d)
        {
            InitializeComponent();

            //set form colors
            this.Colors = new MyColors();
            this.BackColor = Colors.colBackground;
            btnMine.BackColor = Colors.colButtonEnabled;
            itemPanel.BackColor = Colors.colBorders;
            UpgradePanel.BackColor = Colors.colBorders;
            grpMoney.BackColor = Colors.colBorders;
            lblMatsMined.AutoSize = true;
            lblIncrPerClick.AutoSize = true;

            //We should implement autoupgrades that double individual salaries at ownership thresholds, like, for example:
            //double when qty = 25, 50, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000

            //Also, a window to keep track of stats, like amount made, upgrades purchased, prestige points, etc

            //Need to implement save states/loading, and calculate time between last save/current load, add to playtime stat, and multiply by $/sec to
            //add to money, thus making it a true 'idle' game. Right now it's just more of a clicker.


            //frmMain constructor now initializes all fields. All we have to do is slip in a loading method to deserialize our custom GameState object, and set each parameter accordingly.
            this.LoadGame();

            if (this.myItems == default || this.myItems.Length==0){
                myItems = [new ItemView(1, "Wood", 3.738d, 1.07d, 1.67d),
                    new ItemView(2, "Stone", 60d, 1.15d, 20d),
                    new ItemView(3, "Iron", 720d, 1.14d, 90d),
                    new ItemView(4, "Steel", 8640d, 1.13d, 360d),
                    new ItemView(5, "Diamond", 103680d, 1.12d, 2160d),
                    new ItemView(6, "Uranium", 1244160d, 1.11d, 6480d),
                    new ItemView(7, "Antimatter", 14929920d, 1.10d, 19440d),
                    new ItemView(8, "Black Hole", 179159040d, 1.09d, 58320d)];
            }
            if (this.upgradeButtons == default) { upgradeButtons = new List<UpgradeButton>(); }    //(ID, Name, baseCost, Multiplier, Salary)

            //Upgrades are declared as: (string Name, double Cost, int ItemID, double Multiplier). They can only be created or altered through their constructors.
            if (this.MainUpgradeList == default){ MainUpgradeList =
            [
                new Upgrade("Double Tap", 1000.0d, 0, 3), //Click Upgrades
                new Upgrade("Click Amplifier", 25000.0d, 0, 3),
                new Upgrade("Mega-Clicking", 75000.0d, 0, 3),
                new Upgrade("Click Physics", 150000.0d, 0, 3),
                new Upgrade("Parallel Clicking", 500000.0d, 0, 3),
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
        }
            
            if (this.toolTipVisibleTime == default) { toolTipVisibleTime = 2500; }
            if (this.toolTipDelay == default) { toolTipDelay = 500; }
            if (this.prestigeMultiplier == default) { prestigeMultiplier = 2.0d; }
            if (this.clickAmount == default) { clickAmount = 0.25; }
            if (this.salary == default) { salary = 0.0d; }
            if (this.matsMined == default) { matsMined = 0; }
            if (this.myMoney == default) { myMoney = 0.00d; }
            if (this.incrperclick == default) { incrperclick = 1; }
            if (this.purchAmount == default) { purchAmount = 1; }
            if (this.thislifetimeMoney == default){this.thislifetimeMoney = lastlifeMoney;}
            if (this.prestigePoints == default){ this.prestigePoints = prestPoints;}
            if (this.lastlifetimeMoney == default) { this.lastlifetimeMoney = lastlifeMoney; }
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

            foreach (Upgrade upgrade in MainUpgradeList)
            {
                UpgradeButton btn = new UpgradeButton();
                btn.Text = upgrade.Description + $"\n${(upgrade.Cost >= 1000000.0d ? Stringify(upgrade.Cost.ToString(), StringifyOptions.LongText) : double.Round(upgrade.Cost, 2).ToString("N"))}";
                
                btn.MouseHover += Btn_Hover;
                btn.Paint += Btn_Paint;
                btn.myUpgrade = upgrade;
                btn.CausesValidation = false;
                btn.BackColor = Colors.colButtonDisabled;
                btn.ForeColor = Colors.colTextSecondary;
                btn.Enabled = false;
                upgradeButtons.Add(btn);
            }

        }

        System.Windows.Forms.Timer toolTipTimer = new System.Windows.Forms.Timer();
        ToolTip? myTip;
        private void toolTipTick(object? sender, EventArgs e)
        {
            if (myTip != null) { myTip.Hide(this); }
            toolTipTimer.Stop();
            //hide the tooltip and stop the timer. If needed, it will be started again via the hover event.
        }

        private void Btn_Hover(object? sender, EventArgs e)
        {
            
            toolTipTimer.Interval = toolTipVisibleTime;
            toolTipTimer.Tick += new EventHandler(toolTipTick);

            //get mouse location
            //display some magical floating box that says {Description} multiplies {get-name-from-itemID()}'s salary by {multiplier}
            if (sender == null) { return; }
            if (myTip != null) { myTip.Hide(this); }
            myTip = new ToolTip();
            
            myTip.InitialDelay = toolTipDelay;
            myTip.IsBalloon = true;
            myTip.AutoPopDelay = toolTipVisibleTime;
            myTip.UseAnimation = true;
            UpgradeButton btn = (UpgradeButton)sender;
            Point mousepos = MousePosition;
            mousepos.Offset(0, 30); //offset the tooltip down 30px so the cursor can still click the buttons
            toolTipTimer.Start();
            if (btn.myUpgrade.itemID >= 1 && btn.myUpgrade.itemID <= myItems.Length)
            {
                //upgrade refers to an item
                myTip.Show($"{btn.myUpgrade.Description} multiplies {myItems[btn.myUpgrade.itemID - 1].Name}'s salary per second by {btn.myUpgrade.Multiplier}!", this, mousepos);
                //"Double-Tap multiplies Wood's salary per second by 3!"
            }
            else if (btn.myUpgrade.itemID == 0)
            {
                //upgrade is a clickamount upgrade
                myTip.Show($"{btn.myUpgrade.Description} multiplies 'Click-Mining' earnings by {btn.myUpgrade.Multiplier}!", this, mousepos);
            }
            else if (btn.myUpgrade.itemID == 15)
            {
                //upgrade is for all items
                myTip.Show($"{btn.myUpgrade.Description} multiplies all item salaries by {btn.myUpgrade.Multiplier}!", this, mousepos);
            }
            else if (btn.myUpgrade.itemID == 20)
            {
                //upgrade is a prestige point upgrade
                myTip.Show($"{btn.myUpgrade.Description} adds {((btn.myUpgrade.Multiplier * 100) - 100):N0}% gain per prestige point!", this, mousepos);
            }
            else
            {
                //we have no idea what this button does. ItemID not found.
                return;
            }

        }

        private void Btn_Paint(object? sender, PaintEventArgs e)
        {
            if (sender == null) { return; }
            UpgradeButton myButton = (UpgradeButton)sender;
            if (myButton.Enabled) { return; }
            SolidBrush myBrush;
            if (myButton.Enabled)
            {
                myBrush = new SolidBrush(Colors.colTextPrimary);
            }
            else
            {
                myBrush = new SolidBrush(Colors.colTextSecondary);
            }
            StringFormat strFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            //button text is erased, and now will be repainted onto the button in the correct color so that 'Disabling' the button won't render black text.
            e.Graphics.DrawString(myButton.Text, myButton.Font, myBrush, e.ClipRectangle, strFormat);
            myBrush.Dispose();
            strFormat.Dispose();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            //frmMain.LoadState(string defaultSaveFile) -- TODO

            //Populate form with possible items from array, then update their labels.
            for (int i = 1; i <= myItems.Length; i++)
            {
                itemPanel.Controls.Add(myItems[i - 1]);  //add all items to itempanel
            }


            for (int i = 0; i < upgradeButtons.Count; i++)      //SET UP UPGRADE BUTTONS
            {
                //itemID==0 is clickAmount upgrade button.
                //itemID==15 is allitem upgrade, itemID==20 is prestigemultiplier upgrade.

                upgradeButtons[i].Click += new EventHandler(upgradeClicked);
                upgradeButtons[i].Width = (int)(UpgradePanel.Width * 0.85);  //button width is 85% of panel width
                upgradeButtons[i].Height = (int)(upgradeButtons[i].Width * 0.40); //button height is 40% of button width
                //upgradeButtons[i].BackColorChanged += new EventHandler(btncolorchanged);

                upgradeButtons[i].BackColor = Colors.colButtonDisabled;
                upgradeButtons[i].ForeColor = Colors.colTextSecondary;
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
            //MessageBox.Show(""); --debug
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

                        btnsender.BackColor = Colors.colButtonPurchased;
                        btnsender.ForeColor = Colors.colTextPrimary;
                        myMoney -= btnsender.myUpgrade.Cost;
                        myItems[btnitemID - 1].mySalary *= btnsender.myUpgrade.Multiplier;
                        btnsender.myUpgrade = Upgrade.SetPurchased(btnsender.myUpgrade);
                        btnsender.Text = $"{btnsender.myUpgrade.Description}\nPurchased!";
                        btnsender.Enabled = false;
                    }
                    else
                    {

                        btnsender.BackColor = Colors.colButtonDisabled;
                        btnsender.ForeColor = Colors.colTextSecondary;
                        btnsender.Enabled = false;
                    }
                }
                else if (btnitemID == 0)
                {
                    //clickAmount upgrade, may be removed entirely, not sure yet
                    if (myMoney >= btnsender.myUpgrade.Cost)
                    {

                        btnsender.BackColor = Colors.colButtonPurchased;
                        btnsender.ForeColor = Colors.colTextPrimary;
                        myMoney -= btnsender.myUpgrade.Cost;
                        clickAmount *= btnsender.myUpgrade.Multiplier;
                        //had to make 'SetPurchased' a static method that returned an object reference. For some reason it wasn't updating the object passed to it before...
                        btnsender.myUpgrade = Upgrade.SetPurchased(btnsender.myUpgrade);
                        btnsender.Text = $"{btnsender.myUpgrade.Description}\nPurchased!";
                        btnsender.Enabled = false;
                    }
                    else
                    {

                        btnsender.BackColor = Colors.colButtonDisabled;
                        btnsender.ForeColor = Colors.colTextSecondary;
                        btnsender.Enabled = false;
                    }
                }
                else if (btnitemID == 15)
                {
                    //All Items Upgrade
                    if (myMoney >= btnsender.myUpgrade.Cost)
                    {

                        btnsender.BackColor = Colors.colButtonPurchased;
                        btnsender.ForeColor = Colors.colTextPrimary;
                        myMoney -= btnsender.myUpgrade.Cost;
                        btnsender.Enabled = false;
                        foreach (ItemView item in myItems)
                        {
                            item.mySalary *= btnsender.myUpgrade.Multiplier;
                        }
                        btnsender.myUpgrade = Upgrade.SetPurchased(btnsender.myUpgrade);
                        btnsender.Text = $"{btnsender.myUpgrade.Description}\nPurchased!";
                    }
                    else
                    {

                        btnsender.BackColor = Colors.colButtonDisabled;
                        btnsender.ForeColor = Colors.colTextSecondary;
                        btnsender.Enabled = false;
                    }

                }
                else if (btnitemID == 20)
                {
                    //prestige points upgrade
                    if (myMoney >= btnsender.myUpgrade.Cost)
                    {

                        btnsender.BackColor = Colors.colButtonPurchased;
                        btnsender.ForeColor = Colors.colTextPrimary;
                        btnsender.Enabled = false;
                        double newmult = (btnsender.myUpgrade.Multiplier * 100) - 100;
                        prestigeMultiplier += newmult;
                        btnsender.myUpgrade = Upgrade.SetPurchased(btnsender.myUpgrade);
                        btnsender.Text = $"{btnsender.myUpgrade.Description}\nPurchased!";
                    }
                    else
                    {

                        btnsender.BackColor = Colors.colButtonDisabled;
                        btnsender.ForeColor = Colors.colTextSecondary;
                        btnsender.Enabled = false;
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
            lblMoney.Text = $"Money: ${(myMoney >= 1000000.0d ? Stringify(myMoney.ToString(), StringifyOptions.LongText) : double.Round(myMoney, 2).ToString("N"))}";
            lblSalary.Text = $"Salary: ${(salary >= 1000000.0d ? Stringify(salary.ToString(), StringifyOptions.LongText) : double.Round(salary, 2).ToString("N"))} Per Second";
            lblClickAmount.Text = $"Mining: ${(clickAmount >= 1000000.0d ? Stringify(clickAmount.ToString(), StringifyOptions.LongText) : double.Round(clickAmount, 2).ToString("N"))} Per Click";
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
                if (btn.myUpgrade.Purchased)
                {


                    //specify color to override enabledchanged event handler method

                    btn.BackColor = Colors.colButtonPurchased;
                    btn.ForeColor = Colors.colTextPrimary;
                    btn.Enabled = false;
                }
                //if we can afford it and haven't bought it, enable it and turn it green, if not, disable it and turn it gray. real simple.
                else if (!(btn.myUpgrade.Purchased))
                {
                    //if not purchased
                    if (myMoney >= btn.myUpgrade.Cost)
                    {
                        //can afford

                        btn.BackColor = Colors.colButtonEnabled;
                        btn.ForeColor = Colors.colTextPrimary; //black
                        btn.Enabled = true;
                    }
                    else
                    {
                        //can't afford


                        btn.BackColor = Colors.colButtonDisabled;
                        btn.ForeColor = Colors.colTextSecondary; //white
                        btn.Enabled = false;
                    }
                }
                btn.Refresh();

            }
            //override x coordinate in itemPanel to center ItemViews
            for (int i = 0; i < itemPanel.Controls.Count; i++)
            {
                itemPanel.Controls[i].Anchor = AnchorStyles.None;
                //get left x coord of itempanel, add half of itempanel.width to find center of itempanel, then subtract half of item.width to find it's left x coord. Y remains unchanged.
                int xLocation = (itemPanel.Location.X + ((itemPanel.Size.Width - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth) / 2)) - (itemPanel.Controls[i].Size.Width / 2);

                Point newLocation = new Point(xLocation, itemPanel.Controls[i].Location.Y);
                //subtract itemPanel.X from xLocation to prevent adding extra padding due to distance of itemPanel from edge of form.
                Padding padding = new Padding(xLocation - itemPanel.Location.X, 10, 10, 10);
                itemPanel.Controls[i].Margin = padding;
                //itemPanel.Controls[i].Location = newLocation; Not needed afterall, causes flickering due to different locations anyway. Parent Control forces location anyway.
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
                sender.myQty += sender.purchaseAmount;
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

            DialogResult dres = MessageBox.Show($"Current Prestige: {this.prestigePoints:N0}. \nPrestige to Gain: {tempprestige:N0}. Prestige?", "Reset to earn prestige?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, false);
            if (dres == DialogResult.Yes)
            {
                this.timerPerSec.Stop();
                this.timerVisualUpdate.Stop();
                this.toolTipTimer.Stop();

                this.lastlifetimeMoney = thislifetimeMoney + lastlifetimeMoney;
                this.prestigePoints += tempprestige;
                this.myItems = new ItemView[0];
                this.upgradeButtons = new List<UpgradeButton>();
                this.MainUpgradeList = new List<Upgrade>();
                this.salary = default;
                this.myMoney = default;
                this.clickAmount = default;
                this.prestigeMultiplier = default;
                this.matsMined = default;
                this.incrperclick = default;
                this.thislifetimeMoney = default;
                this.thislifeGameTime = default;
                SaveGame();
                Program.RestartForPrestige = true;
                this.Close();
            }
            else if (dres == DialogResult.No)
            {
                timerPerSec.Start();
                timerVisualUpdate.Start();
            }
        }
        internal static string Stringify(string input, StringifyOptions option = StringifyOptions.LongText)
        {
            //IMPORTANT - Currently will not handle non-numerical input. Intended for 'lblMoney.Text = Stringify(myMoney.ToString():N, StringifyOptions.LongText);' or similar!
            switch (option)
            {
                case (StringifyOptions.LongText):
                    {
                        if (input.Contains("E")) { input = ReBig(input); }
                        //1,000.00
                        if (input.Length > 5 && input.Contains("."))
                        {
                            input = input.Split('.')[0];
                            //if the input is longer than 4 digits plus a decimal then we don't need the decimal for display.
                        }
                        //return string using classic long notation (#.### followed by Million, Billion, Trillion, etc)
                        string myOutput = "";
                        string[] TextStrings = { "Million", "Billion", "Trillion", "Quadrillion", "Quintillion", "Sextillion", "Septillion", "Octillion", "Nonillion", "Decillion", "Undecillion", "Duodecillion", "Tredecillion", "Quattordecillion", "Quindecillion", "Sexdecillion", "Septendecillion", "Octodecillion", "Novemdecillion", "Vigintillion",
                                                "Unvigintillion", "Duovigintillion", "Trevigintillion", "Quattorvigintillion", "Quinvigintillion", "Sexvigintillion", "Septenvigintillion", "Octovigintillion", "Novemvigintillion", "Trigintillion", "Untrigintillion", "Duotrigintillion", "Tretrigintillion", "Quattortrigintillion", "Quintrigintillion",
                                                "Sextrigintillion", "Septentrigintillion", "Octotrigintillion", "Novemtrigintillion", "Quadragintillion"};  //currently up to 9.99*10^125
                        //index 0 = 7-9 length, index 1 = 10-12 length, index 2 = 13-15 length, etc
                        int digitcount = 0;
                        for (int i = 0; i < input.Length; i++)
                        {
                            if (input[i] != ',') { digitcount++; }
                        }
                        //input 1,935,342.35 => split: 1,935,342
                        if (digitcount < 7) { return input; }
                        string trimmedinput = "";
                        int index = 0;
                        int digits = 0;
                        while (digits < 4)
                        {
                            if (input[index] != ',')
                            {
                                trimmedinput += input[index];
                                digits++;
                            }
                            index++;
                        }   //1935
                        //digitcount % 3 gives period placement, unless it's 0. If 0, put period at index 3.
                        trimmedinput = trimmedinput.Insert((digitcount % 3 == 0 ? 3 : digitcount % 3), ".");    //1.935
                        trimmedinput += " ";
                        //7/3=2.333, 9/3=3, 10/3=3.333, 12/3=4, cast to double, do division, round up to nearest integer, subtract 3.
                        int wordindex = ((int)double.Round((double)digitcount / 3, MidpointRounding.ToPositiveInfinity) - 3);
                        myOutput = trimmedinput + TextStrings[wordindex];
                        return myOutput;
                    }
                default:
                    {
                        return input;
                    }
            }
        }
        internal enum StringifyOptions
        {
            LongText = 32,
            ShortText = 64,
            ScientificNotation = 128
        }

        internal static string ReBig(string input)
        {
            //Removes scientific notation and returns a string as a literal representation of the number.
            if (input[0] == '-') { input = input.Remove(0, 1); }
            string[] parts = input.Split('E');  //7.5, +27
            if (parts[1].Contains('+')) { parts[1] = parts[1].Remove(0, 1); }   //7.5, 27

            //remove decimal if it exists, and subtract it's distance to end of string from parts[1]: {7.5, 27} => {75, 26}
            if (parts[0].Contains('.'))
            {
                int subtractor = (parts[0].Length - 1) - parts[0].IndexOf("."); //7.5 gives 2-1=1, 4.23 gives 3-1=2, 2.331 gives 4-1=3
                parts[1] = (Int32.Parse(parts[1]) - subtractor).ToString();     //27-1=26
                parts[0] = parts[0].Remove(parts[0].IndexOf("."), 1);           //7.5 => 75
            }
            string output = parts[0];
            for (int i = 0; i < Int32.Parse(parts[1]); i++) { output += "0"; }
            return output;
        }


        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SaveGame();
            Program.RestartForPrestige = false;
            
            
        }

        private void lblMoney_SizeChanged(object sender, EventArgs e)
        {
            lblMoney.Left = (this.grpMoney.Width - lblMoney.Size.Width) / 2;
        }

        private void lblSalary_SizeChanged(object sender, EventArgs e)
        {
            lblSalary.Left = (this.grpMoney.Width - lblSalary.Size.Width) / 2;
        }

        private void lblClickAmount_SizeChanged(object sender, EventArgs e)
        {
            lblClickAmount.Left = (this.grpMoney.Width - lblClickAmount.Size.Width) / 2;
        }

        //---TempBelowThisLine---//
        public void SaveGame()
        {
            GameState save = new GameState();
            save.myItems = this.myItems;
            save.MainUpgradeList = this.MainUpgradeList;
            save.upgradeButtons = this.upgradeButtons;
            save.toolTipVisibleTime = this.toolTipVisibleTime;
            save.toolTipDelay = this.toolTipDelay;
            save.prestigeMultiplier = this.prestigeMultiplier;
            save.clickAmount = this.clickAmount;
            save.salary = this.salary;
            save.matsMined = this.matsMined;
            save.myMoney = this.myMoney;
            save.incrperclick = this.incrperclick;
            save.purchAmount = this.purchAmount;
            save.thislifetimeMoney = this.thislifetimeMoney;
            save.prestigePoints = this.prestigePoints;
            save.lastlifetimeMoney = this.lastlifetimeMoney;
            save.totalgametime = this.totalGameTime;
            save.thislifegametime = this.thislifeGameTime;
            save.lastsavetimestamp = DateTime.Now;
            save.lastWindowState = this.WindowState;

            using StreamWriter sWriter = new StreamWriter(Environment.CurrentDirectory + @"\GameState.mmf", false, Encoding.UTF8);
            //serialize and write to disk
            
            
        }
        public void LoadGame()
        {
            FileStreamOptions fsOptions = new FileStreamOptions();
            GameState save;
            try
            {
                fsOptions.Mode = FileMode.Open;
                using StreamReader sReader = new StreamReader(Environment.CurrentDirectory + @"\GameState.mmf", Encoding.UTF8, false, fsOptions);
                //read from disk and deserialize

            }
            catch { return; }
            this.myItems = save.myItems;
            this.MainUpgradeList = save.MainUpgradeList;
            this.upgradeButtons = save.upgradeButtons;
            this.toolTipVisibleTime = save.toolTipVisibleTime;
            this.toolTipDelay = save.toolTipDelay;
            this.prestigeMultiplier = save.prestigeMultiplier;
            this.clickAmount = save.clickAmount;
            this.salary = save.salary;
            this.matsMined = save.matsMined;
            this.myMoney = save.myMoney;
            this.incrperclick = save.incrperclick;
            this.purchAmount = save.purchAmount;
            this.thislifetimeMoney = save.thislifetimeMoney;
            this.prestigePoints = save.prestigePoints;
            this.lastlifetimeMoney = save.lastlifetimeMoney;
            this.totalGameTime = save.totalgametime;
            this.thislifeGameTime = save.thislifegametime;
            this.WindowState = save.lastWindowState;
            TimeSpan sincelastsave = DateTime.Now.Subtract(save.lastsavetimestamp);
            this.thislifeGameTime.Add(sincelastsave);
            this.totalGameTime.Add(sincelastsave);
            if (sincelastsave.TotalSeconds > 5.0d) { myMoney += salary * sincelastsave.TotalSeconds; thislifetimeMoney += salary * sincelastsave.TotalSeconds; }
        }
    }

    public class GameState
    {
        
        internal ItemView[] myItems;
        internal List<Upgrade> MainUpgradeList;
        internal List<UpgradeButton> upgradeButtons;
        internal int toolTipVisibleTime;
        internal int toolTipDelay;
        internal double prestigeMultiplier;
        internal double clickAmount;
        internal double salary;
        internal int matsMined;
        internal double myMoney;
        internal int incrperclick;
        internal int purchAmount;
        internal double thislifetimeMoney;
        internal double prestigePoints;
        internal double lastlifetimeMoney;
        internal TimeSpan totalgametime;
        internal TimeSpan thislifegametime;
        internal DateTime lastsavetimestamp;
        internal FormWindowState lastWindowState;
        public GameState()
        {
            myItems = new ItemView[0];
            MainUpgradeList = new List<Upgrade>();
            upgradeButtons = new List<UpgradeButton>();
            toolTipVisibleTime = default;
            toolTipDelay = default;
            prestigeMultiplier = default;
            clickAmount = default;
            salary = default;
            matsMined = default;
            myMoney = default;
            incrperclick = default;
            purchAmount = default;
            thislifetimeMoney = default;
            prestigePoints = default;
            lastlifetimeMoney = default;
            totalgametime = default;
            thislifegametime = default;
            lastsavetimestamp = DateTime.Now;
            lastWindowState = default;
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
        public Color colButtonDisabled = Color.FromArgb(37, 39, 46);//BlackOlive
        public Color colButtonEnabled = Color.FromArgb(63, 210, 255);//PaleAzure
        public Color colButtonPurchased = Color.FromArgb(20, 81, 195);//SteelBlue
        public Color colBackground = Color.FromArgb(210, 180, 140);//Tan
        public Color colTextPrimary = Color.Black;
        public Color colTextSecondary = Color.White;
        public Color colBorders = Color.FromArgb(78, 213, 215);//TiffanyBlue
    }
}
