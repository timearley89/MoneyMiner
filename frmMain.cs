using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Numerics;
using System.Reflection;
using System.Resources;
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
using MoneyMiner;
using MoneyMiner.Properties;
using static FirstClicker.Upgrade;

namespace FirstClicker
{

    public partial class frmMain : Form
    {
        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        static extern Int32 mciSendString(string command, StringBuilder? buffer, int bufferSize, IntPtr hwndCallback);


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
        public System.Windows.Forms.Timer toolTipTimer = new System.Windows.Forms.Timer();
        public ToolTip? myTip;
        internal bool PrestigeNextRestart;
        internal List<SoundPlayer> pickSounds;
        internal SoundPlayer registerSound;
        internal SoundPlayer clickSound;
        internal const double Octoquinquagintillion = 1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000.00d;
        internal readonly static string[] TextStrings = { "Million", "Billion", "Trillion", "Quadrillion", "Quintillion", "Sextillion", "Septillion", "Octillion", "Nonillion", "Decillion", "Undecillion", "Duodecillion", "Tredecillion", "Quattordecillion", "Quindecillion", "Sexdecillion", "Septendecillion", "Octodecillion", "Novemdecillion", "Vigintillion",
                                                "Unvigintillion", "Duovigintillion", "Tresvigintillion", "Quattorvigintillion", "Quinvigintillion", "Sexvigintillion", "Septenvigintillion", "Octovigintillion", "Novemvigintillion", "Trigintillion", "Untrigintillion", "Duotrigintillion", "Tretrigintillion", "Quattortrigintillion", "Quintrigintillion",
                                                "Sextrigintillion", "Septentrigintillion", "Octotrigintillion", "Novemtrigintillion", "Quadragintillion", "Unquadragintillion", "Duoquadragintillion", "Trequadragintillion", "Quattorquadragintillion", "Quinquadragintillion", "Sexquadragintillion", "Septenquadragintillion", "Octoquadragintillion", "Novemquadragintillion",
                                                "Quinquagintillion", "Unquinquagintillion", "Duoquinquagintillion", "Trequinquagintillion", "Quattorquinquagintillion", "Quinquinquagintillion", "Sexquinquagintillion", "Septenquinquagintillion", "Octoquinquagintillion", "Novemquinquagintillion", "Sexagintillion", "Unsexagintillion", "Duosexagintillion",
                                                "Tresexagintillion", "Quattorsexagintillion", "Quinsexagintillion", "Sexsexagintillion", "Septensexagintillion", "Octosexagintillion", "Novemsexagintillion", "Septuagintillion", "Unseptuagintillion", "Duoseptuagintillion", "Treseptuagintillion", "Quattorseptuagintillion", "Quinseptuagintillion", "Sexseptuagintillion",
                                                "Septseptuagintillion", "Octoseptuagintillion", "Novemseptuagintillion", "Octogintillion", "Unoctogintillion", "Duooctogintillion", "Treoctogintillion", "Quattoroctogintillion", "Quinoctogintillion", "Sexoctogintillion", "Septoctogintillion", "Octoctogintillion", "Novemoctogintillion", "Nonagintillion",
                                                "Unnonagintillion", "Duononagintillion", "Trenonagintillion", "Quattornonagintillion", "Quinonagintillion", "Sexnonagintillion", "Septenonagintillion", "Octononagintillion", "Novemnonagintillion", "Centillion", "Uncentillion"};  //handles full size of type 'double'

        internal Stopwatch GameClock;

        public frmMain(double lastlifeMoney = 0.0d, double prestPoints = 0.0d)
        {
            InitializeComponent();
            PrestigeNextRestart = false;
            DoubleBuffered = true;
            mciSendString($@"open {Environment.CurrentDirectory}\Resources\cashregisterpurchase.wav type waveaudio alias registersound", null, 0, IntPtr.Zero);
            mciSendString($@"open {Environment.CurrentDirectory}\Resources\clickbutton.wav type waveaudio alias clicksound", null, 0, IntPtr.Zero);
            mciSendString($@"open {Environment.CurrentDirectory}\Resources\pickaxe-clank-01.wav type waveaudio alias pickaxe1sound", null, 0, IntPtr.Zero);
            mciSendString($@"open {Environment.CurrentDirectory}\Resources\pickaxe-clank-02.wav type waveaudio alias pickaxe2sound", null, 0, IntPtr.Zero);
            mciSendString($@"open {Environment.CurrentDirectory}\Resources\pickaxe-clank-03.wav type waveaudio alias pickaxe3sound", null, 0, IntPtr.Zero);
            mciSendString($@"open {Environment.CurrentDirectory}\Resources\pickaxe-clank-04.wav type waveaudio alias pickaxe4sound", null, 0, IntPtr.Zero);
            mciSendString($@"open {Environment.CurrentDirectory}\Resources\pickaxe-clank-05.wav type waveaudio alias pickaxe5sound", null, 0, IntPtr.Zero);
            mciSendString($@"open {Environment.CurrentDirectory}\Resources\pickaxe-clank-06.wav type waveaudio alias pickaxe6sound", null, 0, IntPtr.Zero);
            mciSendString($@"open {Environment.CurrentDirectory}\Resources\pickaxe-clank-07.wav type waveaudio alias pickaxe7sound", null, 0, IntPtr.Zero);
            mciSendString($@"open {Environment.CurrentDirectory}\Resources\pickaxe-clank-08.wav type waveaudio alias pickaxe8sound", null, 0, IntPtr.Zero);
            
            //set form colors
            this.Colors = new MyColors();
            this.BackColor = Colors.colBackground;
            btnMine.BackColor = Colors.colButtonEnabled;
            btnStats.BackColor = Colors.colButtonEnabled;
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

            if (this.myItems == default || this.myItems.Length == 0)
            {
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

            //Upgrades are declared as: (string Name, double Cost, int ItemID, double Multiplier, int upgradeID). They can only be created or altered through their constructors.
            //itemID==0 - ClickAmount
            //itemID==1-8(14) - Items
            //itemID==15 - All Items
            //itemID==20 - Prestige Multiplier
            if (this.MainUpgradeList == default)
            {
                MainUpgradeList =
            [
                new Upgrade("Double Tap", 1000.0d, 0, 3, 1), //Click Upgrades
                new Upgrade("Click Amplifier", 25000.0d, 0, 3, 2),
                new Upgrade("Mega-Clicking", 75000.0d, 0, 3, 3),
                new Upgrade("Click Physics", 150000.0d, 0, 3, 4),
                new Upgrade("Parallel Clicking", 500000.0d, 0, 3, 5),
                new Upgrade("Birch Wood", 250000.0d, 1, 3, 6), //Item1 Upgrades
                new Upgrade("Pine Wood", 20000000000000.0d, 1, 3, 7),
                new Upgrade("Oak Wood", 2000000000000000000.0d, 1, 3, 8),
                new Upgrade("Cherry Wood", 25000000000000000000000.0d, 1, 3, 9),
                new Upgrade("Sequoia Wood", 1000000000000000000000000000.0d, 1, 7, 10),
                new Upgrade("Sandstone", 500000.0d, 2, 3, 11), //Item2 Upgrades
                new Upgrade("Granite", 50000000000000.0d, 2, 3, 12),
                new Upgrade("Limestone", 5000000000000000000.0d, 2, 3, 13),
                new Upgrade("Marble", 50000000000000000000000.0d, 2, 3, 14),
                new Upgrade("Slate", 5000000000000000000000000000.0d, 2, 7, 15),
                new Upgrade("Picked Iron", 1000000.0d, 3, 3, 16), //Item3 Upgrades
                new Upgrade("Cast Iron", 100000000000000.0d, 3, 3, 17),
                new Upgrade("Molded Iron", 7000000000000000000.0d, 3, 3, 18),
                new Upgrade("Forged Iron", 100000000000000000000000.0d, 3, 3, 19),
                new Upgrade("Cold-Fused Iron", 25000000000000000000000000000.0d, 3, 7, 20),
                new Upgrade("Basic Steel", 5000000.0d, 4, 3, 21), //Item4 Upgrades
                new Upgrade("Tempered Steel", 500000000000000.0d, 4, 3, 22),
                new Upgrade("Rolled Steel", 10000000000000000000.0d, 4, 3, 23),
                new Upgrade("Steel Alloy", 200000000000000000000000.0d, 4, 3, 24),
                new Upgrade("Venutian Steel", 100000000000000000000000000000.0d, 4, 7, 25),
                new Upgrade("Flawed Diamond", 10000000.0d, 5, 3, 26), //Item5 Upgrades
                new Upgrade("Improved Diamond", 1000000000000000.0d, 5, 3, 27),
                new Upgrade("Flawless Diamond", 20000000000000000000.0d, 5, 3, 28),
                new Upgrade("Synthetic Diamond", 300000000000000000000000.0d, 5, 3, 29),
                new Upgrade("Quantum Diamond", 250000000000000000000000000000.0d, 5, 7, 30),
                new Upgrade("Waste Uranium", 25000000.0d, 6, 3, 31), //Item6 Upgrades
                new Upgrade("Mined Uranium", 2000000000000000.0d, 6, 3, 32),
                new Upgrade("Refined Uranium", 35000000000000000000.0d, 6, 3, 33),
                new Upgrade("Synthetic Uranium", 400000000000000000000000.0d, 6, 3, 34),
                new Upgrade("Quantum Uranium", 500000000000000000000000000000.0d, 6, 7, 35),
                new Upgrade("Low Yield Antimatter", 500000000.0d, 7, 3, 36), //Item7 Upgrades
                new Upgrade("Mid Yield Antimatter", 5000000000000000.0d, 7, 3, 37),
                new Upgrade("High Yield Antimatter", 50000000000000000000.0d, 7, 3, 38),
                new Upgrade("Perfect Antimatter", 400000000000000000000000.0d, 7, 3, 39),
                new Upgrade("CPT Reversed Antimatter", 1000000000000000000000000000000.0d, 7, 7, 40),
                new Upgrade("Plank Black Hole", 10000000000.0d, 8, 3, 41), //Item8 Upgrades
                new Upgrade("Primordial Black Hole", 7000000000000000.0d, 8, 3, 42),
                new Upgrade("Rogue Black Hole", 75000000000000000000.0d, 8, 3, 43),
                new Upgrade("Supermassive Black Hole", 600000000000000000000000.0d, 8, 7, 44),
                new Upgrade("Universal Black Hole", 5000000000000000000000000000000.0d, 8, 7, 45),
                new Upgrade("Tax Adjustment", 1000000000000.0d, 15, 3, 46), //All-Item Upgrades
                new Upgrade("Ledger Spoofing", 50000000000000000.0d, 15, 3, 47),
                new Upgrade("Illegal Workers", 500000000000000000000.0d, 15, 3, 48),
                new Upgrade("Off-Shore Mining", 900000000000000000000000.0d, 15, 3, 49),
                new Upgrade("Cult Following", 1000000000000000000000000000000000.0d, 15, 7, 50),
                new Upgrade("Material Lobbying", 100000000000000000.0d, 20, 1.01, 51), //Prestige Upgrades
                new Upgrade("Investor Fraud", 1000000000000000000000.0d, 20, 1.01, 52),
                new Upgrade("Controlled Striking", 10000000000000000000000000.0d, 20, 1.02, 53),
                new Upgrade("Space Investing", 1000000000000000000000000000000000000.0d, 20, 1.05, 54),
                new Upgrade("Planetary Ransom", 1000000000000000000000000000000000000000.0d, 20, 1.06, 55)
                ];
                //Upgrades are declared as: (string Name, double Cost, int ItemID, double Multiplier, int upgradeID)
                MainUpgradeList = (List<Upgrade>)MainUpgradeList.OrderBy(x => x.Cost).ToList();
            }

            if (this.toolTipVisibleTime == default) { toolTipVisibleTime = 2500; }
            if (this.toolTipDelay == default) { toolTipDelay = 500; }
            if (this.prestigeMultiplier == default) { prestigeMultiplier = 2.0d; }
            if (this.clickAmount == default) { clickAmount = 0.25; }
            if (this.salary == default) { salary = 0.0d; }
            if (this.matsMined == default) { matsMined = 0; }
            if (this.myMoney == default) { myMoney = 0.0d; }
            if (this.incrperclick == default) { incrperclick = 1; }
            if (this.purchAmount == default) { purchAmount = 1; }
            if (this.thislifetimeMoney == default) { this.thislifetimeMoney = lastlifeMoney; }
            if (this.prestigePoints == default) { this.prestigePoints = prestPoints; }
            if (this.lastlifetimeMoney == default) { this.lastlifetimeMoney = lastlifeMoney; }
            if (this.GameClock == default) { this.GameClock = new Stopwatch(); }
            if (this.registerSound == default) { this.registerSound = new(); }
            if (this.pickSounds == default) { this.pickSounds = new(); }
            if (this.clickSound == default) { this.clickSound = new(); }
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
                btn.Text = upgrade.Description + $"\n${(upgrade.Cost >= 1000000.0d ? Stringify(upgrade.Cost.ToString("R"), StringifyOptions.LongText) : double.Round(upgrade.Cost, 2).ToString("N"))}";

                btn.MouseHover += Btn_Hover;
                btn.Paint += Btn_Paint;
                btn.myUpgrade = upgrade;
                btn.CausesValidation = false;
                if (btn.myUpgrade.Purchased == false)
                {
                    btn.Enabled = true;
                    btn.BackColor = Colors.colButtonDisabled;
                    btn.ForeColor = Colors.colTextSecondary;
                    btn.Enabled = false;
                }

                else if (btn.myUpgrade.Purchased == true)
                {   //if we're loading the game and an upgrade is purchased, configure button accordingly.
                    btn.Enabled = true;
                    btn.BackColor = Colors.colButtonPurchased;
                    btn.ForeColor = Colors.colTextPrimary;
                    btn.Text = upgrade.Description + $"\nPurchased!";
                    btn.Enabled = false;
                }
                upgradeButtons.Add(btn);
            }

        }


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
            this.btnMine.BackgroundImageLayout = ImageLayout.Stretch;

            this.registerSound = new SoundPlayer(Resources.cashregisterpurchase);
            this.clickSound = new SoundPlayer(Resources.clickbutton);
            this.pickSounds = new List<SoundPlayer>();
            
            //fill list with new soundplayers pointing to each 'clank' sound

            //for now we'll do it manually because I'm not smart enough to figure out how to iterate through assembly resources programmatically!
            this.pickSounds.Add(new SoundPlayer(Resources.pickaxe_clank_01));
            this.pickSounds.Add(new SoundPlayer(Resources.pickaxe_clank_02));
            this.pickSounds.Add(new SoundPlayer(Resources.pickaxe_clank_03));
            this.pickSounds.Add(new SoundPlayer(Resources.pickaxe_clank_04));
            this.pickSounds.Add(new SoundPlayer(Resources.pickaxe_clank_05));
            this.pickSounds.Add(new SoundPlayer(Resources.pickaxe_clank_06));
            this.pickSounds.Add(new SoundPlayer(Resources.pickaxe_clank_07));
            this.pickSounds.Add(new SoundPlayer(Resources.pickaxe_clank_08));


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
            this.GameClock = new Stopwatch();
            this.timerPerSec.Start();
            this.timerVisualUpdate.Start();
            this.GameClock.Reset();
            this.GameClock.Start();
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

                        //find the upgrade by upgradeid in mainupgradelist that matches the button's upgradeid, and set it's Purchased property, then overwrite it's old entry in mainupgradelist.
                        Upgrade tempUpgrade = MainUpgradeList.Find(x => x.upgradeID == btnsender.myUpgrade.upgradeID);
                        MainUpgradeList[MainUpgradeList.IndexOf(tempUpgrade)] = Upgrade.SetPurchased(tempUpgrade);
                        //registerSound.Play();
                        mciSendString("seek registersound to start", null, 0, IntPtr.Zero);
                        mciSendString("play registersound", null, 0, IntPtr.Zero);
                        //mciSendString("stop registersound", null, 0, IntPtr.Zero);
                        //mciSendString("seek to start registersound", null, 0, IntPtr.Zero);
                        
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
                        Upgrade tempUpgrade = MainUpgradeList.Find(x => x.upgradeID == btnsender.myUpgrade.upgradeID);
                        MainUpgradeList[MainUpgradeList.IndexOf(tempUpgrade)] = Upgrade.SetPurchased(tempUpgrade);
                        //registerSound.Play();
                        mciSendString("seek registersound to start", null, 0, IntPtr.Zero);
                        mciSendString("play registersound", null, 0, IntPtr.Zero);
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
                        Upgrade tempUpgrade = MainUpgradeList.Find(x => x.upgradeID == btnsender.myUpgrade.upgradeID);
                        MainUpgradeList[MainUpgradeList.IndexOf(tempUpgrade)] = Upgrade.SetPurchased(tempUpgrade);
                        //registerSound.Play();
                        mciSendString("seek registersound to start", null, 0, IntPtr.Zero);
                        mciSendString("play registersound", null, 0, IntPtr.Zero);
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
                        double oldPrestigeMult = prestigeMultiplier;
                        prestigeMultiplier += newmult;
                        /*recalculate: 
                         * {
                                each item.salary for new prestigeMultiplier
                                clickAmount for new prestigeMultiplier
                         * }
                         * without invalidating upgrades already purchased, unlocks obtained(not yet implemented), or any other increases/multipliers obtained.
                         * Above, it's calculated by:
                         * this.clickAmount *= ((prestigePoints / (100.0d / prestigeMultiplier)) + 1);
                         * for clickAmount, and by:
                         * item.mySalary *= ((prestigePoints / (100.0d / prestigeMultiplier)) + 1);
                         * for each item salary.
                         * 
                         * 100/3=33.333, 50/33.333 = 1.333, +1 = 2.333, meaning a 133% bonus. Not right, because 50*0.02=100%, so 50*0.03=150%. what about
                         * (((prestmult / 100)*prestpoints)+1)*salary=salary? 3/100=0.03, *prestpoints=1.5, +1=2.5, so 50 points @ 3% boost gives 150% gain (or 250% of salary)?
                         * what if it's 100 points? then boost should be 100*3%=300%, or 400% of salary.
                         * 3/100=0.03, *prestpoints=3, +1=4, which is correct. Lets recalc this.
                         * 
                         * first we do need to determine what 100% of salary would be:
                         * ((oldprestmult / 100)*prestpoints) gives boost percentage.(100%)
                         * 100/(boost+100) => 100/200=0.5, or 100/250=0.4, or 100/724(624% boost!)=0.138125, so on. multiply that by current salary to get initial, then
                         * recalc prestigemult with that value. 1/totalmultiplier (1/2.5) would be the same, or even oldsalary = salary * (1 / ((prestigePoints / (100.0d / oldPrestigeMult)) + 1))
                         * Therefore, 
                         * 
                         * item.mySalary = (item.mySalary * (1 / ((prestigePoints / (100.0d / oldPrestigeMult)) + 1)) * ((prestigePoints / (100.0d / prestigeMultiplier)) + 1));
                         * Can we refactor that equation to reduce it? I might need to dust off some old algebra...
                         * or just use an online factoring tool.
                         * item.mySalary = (item.mySalary * ((prestigeMultiplier * prestigePoints) + 100)) / ((oldPrestigeMult * prestigePoints) + 100);
                         * 
                         * 
                         */
                        foreach (ItemView item in myItems)
                        {
                            item.mySalary = (item.mySalary * ((prestigeMultiplier * prestigePoints) + 100)) / ((oldPrestigeMult * prestigePoints) + 100);
                        }
                        this.clickAmount = (this.clickAmount * ((prestigeMultiplier * prestigePoints) + 100)) / ((oldPrestigeMult * prestigePoints) + 100);

                        btnsender.myUpgrade = Upgrade.SetPurchased(btnsender.myUpgrade);
                        Upgrade tempUpgrade = MainUpgradeList.Find(x => x.upgradeID == btnsender.myUpgrade.upgradeID);
                        MainUpgradeList[MainUpgradeList.IndexOf(tempUpgrade)] = Upgrade.SetPurchased(tempUpgrade);
                        //registerSound.Play();
                        mciSendString("seek registersound to start", null, 0, IntPtr.Zero);
                        mciSendString("play registersound", null, 0, IntPtr.Zero);
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
            lblMoney.Text = $"Money: ${(myMoney >= 1000000.0d ? Stringify(myMoney.ToString("R"), StringifyOptions.LongText) : double.Round(myMoney, 2).ToString("N"))}";
            lblSalary.Text = $"Salary: ${(salary >= 1000000.0d ? Stringify(salary.ToString("R"), StringifyOptions.LongText) : double.Round(salary, 2).ToString("N"))} Per Second";
            lblClickAmount.Text = $"Mining: ${(clickAmount >= 1000000.0d ? Stringify(clickAmount.ToString("R"), StringifyOptions.LongText) : double.Round(clickAmount, 2).ToString("N"))} Per Click";
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
                btn.Refresh();  //is this necessary?

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
                //registerSound.Play();
                mciSendString("seek registersound to start", null, 0, IntPtr.Zero);
                mciSendString("play registersound", null, 0, IntPtr.Zero);
                sender.myQty += sender.purchaseAmount;
                sender.myCost = sender.myCost * Math.Pow(sender.myCostMult, sender.purchaseAmount);
            }
            sender.UpdateLabels();
            //frmMain_UpdateLabels();
            sender.ButtonColor(myMoney, sender.purchaseAmount, sender.myCostMult);
        }

        private void btnPurchAmount_Click(object sender, EventArgs e)
        {
            //clickSound.Play();
            mciSendString("seek clicksound to start", null, 0, IntPtr.Zero);
            mciSendString("play clicksound", null, 0, IntPtr.Zero);

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
                //clickAmount = (clickAmount / (incrperclick - 1)) * incrperclick;

            } //for now, when matsMined reaches another multiple of 100, amountperclick is incremented. clickAmount reflects this.
            //incrperclick has a max of 10 for now.
            else { matsMined += incrperclick; }

            //We're only calculating clickAmount for one incrperclick. We can add that amount for multiple clicks, but it shouldn't double clickAmount.
            //Later we can use visual and audible cues to signify multiple clicks (like floating text saying 'x2' 'x3' etc, and disappearing after a second, with little 'pop' sounds for each incrperclick in quick succession...)
            this.myMoney += clickAmount * incrperclick;
            thislifetimeMoney += clickAmount * incrperclick;
            /*
            if (pickSounds != null && pickSounds.Count >= 1)
            {
                Random randnumber = new Random();
                int soundindex = randnumber.Next(0, pickSounds.Count - 1);
                pickSounds[soundindex].Play();
            }*/
            Random randnumber = new Random();
            int i = randnumber.Next(1, 8);
            mciSendString($"seek pickaxe{i}sound to start", null, 0, IntPtr.Zero);
            mciSendString($"play pickaxe{i}sound", null, 0, IntPtr.Zero);
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
            //clickSound.Play();
            mciSendString("seek clicksound to start", null, 0, IntPtr.Zero);
            mciSendString("play clicksound", null, 0, IntPtr.Zero);

            //In order for this to work, I need to refactor the main game logic into it's own gameobject that takes parameters for prestige amount, and default params(overrideable) for money, upgrades, purchased items, etc.
            //Or I can take advantage of the load/save system, and just configure the save to reset for a prestige-flagged restart, that way i can customize what parameters get changed.
            double tempprestige = this.calcPrestige(lastlifetimeMoney, thislifetimeMoney);
            this.timerPerSec.Stop();
            this.timerVisualUpdate.Stop();
            this.GameClock.Stop();
            this.thislifeGameTime += GameClock.Elapsed;
            this.totalGameTime += GameClock.Elapsed;

            DialogResult dres = MessageBox.Show($"Current Prestige: {Stringify(prestigePoints.ToString("R"), StringifyOptions.LongText)}. \nPrestige to Gain: {Stringify(tempprestige.ToString("R"), StringifyOptions.LongText)}. Prestige?", "Reset to earn prestige?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, false);
            if (dres == DialogResult.Yes)
            {
                this.timerPerSec.Stop();
                this.timerVisualUpdate.Stop();
                this.toolTipTimer.Stop();
                this.GameClock.Stop();
                

                this.lastlifetimeMoney = thislifetimeMoney + lastlifetimeMoney;
                this.prestigePoints += tempprestige;
                this.myItems = new ItemView[0];
                this.upgradeButtons = new List<UpgradeButton>();
                for (int i = 0; i < MainUpgradeList.Count; i++)
                {
                    MainUpgradeList[i] = Upgrade.UnsetPurchased(MainUpgradeList[i]);
                }
                this.salary = default;
                this.myMoney = default;
                this.clickAmount = default;
                this.prestigeMultiplier = default;
                this.matsMined = default;
                this.incrperclick = default;
                this.thislifetimeMoney = default;
                this.thislifeGameTime = default;
                SaveGame();
                this.PrestigeNextRestart = true;
                Program.RestartForPrestige = true;
                this.Close();
            }
            else if (dres == DialogResult.No)
            {
                timerPerSec.Start();
                timerVisualUpdate.Start();
                this.GameClock.Reset();
                this.GameClock.Start();
                this.Focus();
                this.Refresh();
            }
        }
        internal static string Stringify(string input, StringifyOptions option = StringifyOptions.LongText)
        {
            if (input == double.PositiveInfinity.ToString())
            {
                throw new ArgumentOutOfRangeException(nameof(input), input, "Positive infinity reached!");
            }
            else if (input == double.NegativeInfinity.ToString())
            {
                throw new ArgumentOutOfRangeException(nameof(input), input, "Negative Infinity reached!");
            }

            //IMPORTANT - Currently will not handle non-numerical input. Intended for 'lblMoney.Text = Stringify(myMoney.ToString(), StringifyOptions.LongText);' or similar!
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
            mciSendString("close clicksound", null, 0, IntPtr.Zero);
            mciSendString("close registersound", null, 0, IntPtr.Zero);
            for (int i = 1; i < 8; i++)
            {
                mciSendString($"close pickaxe{i}sound", null, 0, IntPtr.Zero);
            }

            if (!PrestigeNextRestart)
            {
                this.SaveGame();
                Program.RestartForPrestige = false;
            }



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

        public void SaveGame()
        {
            GameState save = new GameState();

            List<ItemData> tempitemdatas = new List<ItemData>();
            for (int i = 0; i < this.myItems.Count(); i++)
            {
                tempitemdatas.Add(new ItemData(this.myItems[i]));
            }
            save.myItemDatas = tempitemdatas.ToArray();
            save.MainUpgradeList = this.MainUpgradeList;
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


            FileStream fstream = new FileStream(Environment.CurrentDirectory + @"\GameState.mmf", FileMode.Create);

            //serialize and write to disk
#pragma warning disable SYSLIB0011 // Type or member is obsolete
            BinaryFormatter bformatter = new BinaryFormatter();
#pragma warning restore SYSLIB0011 // Type or member is obsolete
            bformatter.Serialize(fstream, save);
            fstream.Close();
        }
        public void LoadGame()
        {
            FileStreamOptions fsOptions = new FileStreamOptions();
            GameState save = new GameState();
            try
            {
                fsOptions.Mode = FileMode.Open;
                using FileStream fstream = new FileStream(Environment.CurrentDirectory + @"\GameState.mmf", fsOptions);
                //read from disk and deserialize
#pragma warning disable SYSLIB0011 // Type or member is obsolete
                BinaryFormatter bformatter = new BinaryFormatter();
#pragma warning restore SYSLIB0011 // Type or member is obsolete
                save = (GameState)bformatter.Deserialize(fstream);
                fstream.Close();
            }
            catch { return; }
            List<ItemView> tempitemlist = new List<ItemView>();
            for (int i = 0; i < save.myItemDatas.Count(); i++)
            {
                tempitemlist.Add(new ItemView(save.myItemDatas[i]));
            }
            this.myItems = tempitemlist.ToArray();
            this.MainUpgradeList = save.MainUpgradeList;

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
            if (sincelastsave.TotalSeconds > 1.0d) { myMoney += salary * sincelastsave.TotalSeconds; thislifetimeMoney += salary * sincelastsave.TotalSeconds; MessageBox.Show($"Welcome Back!\nYou were gone for {sincelastsave.TotalHours:N0} hours, {sincelastsave.Minutes:N0} minutes, and {sincelastsave.Seconds:N0} seconds.\nYou made ${Stringify((salary * sincelastsave.TotalSeconds).ToString("R"), StringifyOptions.LongText)} while you were gone!", "Since you've been gone...", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, false); }
        }

        private void btnMine_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender == null) { return; }
            Button thisbutton = (Button)sender;
            if (thisbutton != null && thisbutton.BackgroundImage != null)
            {
                thisbutton.BackgroundImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                //play mine sound
                SoundPlayer sound = new SoundPlayer();
                //
            }
        }

        private void btnMine_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender == null) { return; }
            Button thisbutton = (Button)sender;
            if (thisbutton != null && thisbutton.BackgroundImage != null)
            {
                thisbutton.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                thisbutton.Refresh();
            }
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            //clickSound.Play();
            mciSendString("seek clicksound to start", null, 0, IntPtr.Zero);
            mciSendString("play clicksound", null, 0, IntPtr.Zero);
            //open stats window with current stats, pause timers
            timerPerSec.Stop();
            timerVisualUpdate.Stop();
            this.GameClock.Stop();
            this.thislifeGameTime += GameClock.Elapsed;
            this.totalGameTime += GameClock.Elapsed;
            DialogResult result = MessageBox.Show(
                $"Salary: ${Stringify(this.salary.ToString("R"), StringifyOptions.LongText)} Per Second" +
                $"\nClickAmount: ${Stringify(this.clickAmount.ToString("R"), StringifyOptions.LongText)} Per Click" +
                $"\nMoney Earned This Lifetime: ${Stringify(this.thislifetimeMoney.ToString("R"), StringifyOptions.LongText)}" +
                $"\nMoney Earned All Lifetimes: ${Stringify((this.lastlifetimeMoney + this.thislifetimeMoney).ToString("R"), StringifyOptions.LongText)}" +
                $"\nTime spent this lifetime: ${this.thislifeGameTime.ToString()}" +
                $"\nTime spent all lifetimes: ${this.totalGameTime.ToString()}" +
                $"\nPrestige Points: {Stringify(this.prestigePoints.ToString("R"), StringifyOptions.LongText)}" +
                $"\nPrestige Multiplier: {Stringify(this.prestigeMultiplier.ToString("R"), StringifyOptions.LongText)}% Per Point" +
                $"\nPrestige Percentage: {Stringify((this.prestigePoints * this.prestigeMultiplier).ToString("R"), StringifyOptions.LongText)}%"
                ,"MoneyMiner Statistics",MessageBoxButtons.OK
                );
            timerPerSec.Start();
            timerVisualUpdate.Start();
            this.GameClock.Reset();
            this.GameClock.Start();
        }
    }
    [Serializable]
    public class GameState
    {
        //ItemData shuffles and stores data for the ItemView object.

        internal ItemData[] myItemDatas;
        internal List<Upgrade> MainUpgradeList;
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
            myItemDatas = new ItemData[0];
            MainUpgradeList = new List<Upgrade>();
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
    [Serializable]
    internal struct Upgrade(string description, double cost, int ID, double multiplier, int upgradeID)
    {
        //public getters and struct declaration means we can read the struct and it's properties from anywhere, even a different namespace.
        //private fields and internal constructor mean that only the struct can access it's fields, and only this assembly can create new upgrades.
        public string Description { get { return _description; } }
        public double Cost { get { return _cost; } }
        public int itemID { get { return _itemID; } }
        public int upgradeID { get { return _upgradeID; } }
        public double Multiplier { get { return _multiplier; } }
        public bool Purchased { get { return _purchased; } }

        private string _description = description;
        private double _cost = cost;
        private int _itemID = ID;
        private int _upgradeID = upgradeID;
        private double _multiplier = multiplier;
        public bool _purchased;

        internal static Upgrade SetPurchased(Upgrade myUpgrade)
        {
            myUpgrade._purchased = true;
            return myUpgrade;
        }
        internal static Upgrade UnsetPurchased(Upgrade myUpgrade)
        {
            myUpgrade._purchased = false;
            return myUpgrade;
        }
        

        
    }
    public class UpgradeButton : Button
    {
        internal Upgrade myUpgrade { get; set; }
        
    }
    [Serializable]
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
