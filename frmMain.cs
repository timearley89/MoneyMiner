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
        //NOTES & TODO
        //Convert purchaseamount to enum, including 1, 10, 100, Next, Max - DONE!

        //Need a 'Pause' menu!
        //-should have an 'About' window that holds credit information for resources used in the project - song/sound creators, image owners, any 3rd party libraries used, etc.
        //-add link to stats window
        //-Save/Load buttons?

        //Prestige is broken again - points accumulate fine, but first load after prestige isn't changing salaries/clickamount --FIXED!

        //Need a 'Settings' menu!
        //-master volume slider
        //-add checkboxes for backgroundmusic and soundFX toggles
        //-if backgroundmusic is toggled Off, playback should also stop - not just mute.
        //-fullscreen toggle?
        //-Number Notation setting? Would require fleshing out the Stringify method the rest of the way, could use the enum already implemented to facilitate.
        //-Delete Save/Master Reset button needed... This will overwrite the save file in the root directory with an empty one. Next time the game launches, it will initialize to defaults.
        //-Autosave Interval Setting

        //Autosave Feature

        //Money needs to 'do' something, or be used for something. Would add some depth, but what?

        //Prestige button should change color (or overlays an exclamation point or something) when prestigepoints to be earned >= current prestigepoints (or > if at 0)

        //sliderVolume not setting MusicVol and FXVol, but instead calling SetAudioVolume(int, int). Needs to set properties so saving will enable persistent state on load. -FIXED!

        //We shouldn't need to actually close the application to prestige - should be able to savegame() and then set this = new frmMain(lastlifemoney, prestigePoints); and then this.Show();

        //In addition, since we're using GameState to store and update all parameters related to prestige, we shouldn't need those constructor params either, meaning frmMain() should suffice. --DONE

        //Achievements

        //Upgrades sorted into button/per type which update to the next related upgrade after buying one?

        //New stats:
        //Money earned by clicking
        //Money earned by clicking lifetime
        //Money earned by miners
        //Money earned by miners lifetime
        //Highest clickAmount lifetime
        //Highest salary lifetime

        //Floating indicators when clicking btnMine that show total $ gained --DONE!

        //Progress bars for items? --DONE!

        //Individual item salary timing? --DONE!

        //Add "Quick-Buy" button to buy all upgrades affordable, starting with least expensive

        //Add 'Buy: Next" option to btnPurchAmount to purchase item to next unlock amount   --DONE!

        //Need a proper 'Stats' window!

        //Need Prestige Earned window!

        //Need 'Welcome Back' window!

        //Need more background music? If so, we'll need a callback method for when playback is finished that triggers next song in list. I guess List<string> would work for holding
        //track names, and the callback method would iterate to the next string in the list, open it's file, seek to 0L, and play.

        //Need more upgrades!

        //Need Prestige Upgrades as well - as in upgrades you buy with prestige points. -Maybe, Maybe not...

        //We should implement unlocks that double individual salaries at ownership thresholds, like, for example:
        //double when qty = 25, 50, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, etc      --DONE!

        public MyColors Colors;
        public double myMoney;
        public double salary;
        public double clickAmount;
        public double thislifetimeMoney;
        public double lastlifetimeMoney;
        public double prestigePoints;
        public double prestigeMultiplier;
        public int matsMined;
        public int incrperclick;
        public int toolTipDelay;
        public int toolTipVisibleTime;
        public TimeSpan thislifeGameTime;
        public TimeSpan totalGameTime;
        public int MusicVolume; //0 to 1000
        public int FXVolume; //0 to 1000
        //need to add matsMinedLifetime here and in gamestate and load/save and btnMine_Click


        public ItemView[] myItems;
        public List<UpgradeButton> upgradeButtons;
        internal List<Upgrade> MainUpgradeList;
        public System.Windows.Forms.Timer toolTipTimer = new System.Windows.Forms.Timer();
        public ToolTip? myTip;
        internal bool PrestigeNextRestart;
        //this constant is just here for my sanity, mainly. This is the largest quantity I've seen ingame so far. I need to test further to find our little 'infinity' glitch - I believe it's in prestigeMultiplier Recalculation, within upgradeClicked()...
        internal const double Octoquinquagintillion = 1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000.00d;
        internal readonly static string[] TextStrings = { "Million", "Billion", "Trillion", "Quadrillion", "Quintillion", "Sextillion", "Septillion", "Octillion", "Nonillion", "Decillion", "Undecillion", "Duodecillion", "Tredecillion", "Quattordecillion", "Quindecillion", "Sexdecillion", "Septendecillion", "Octodecillion", "Novemdecillion", "Vigintillion",
                                                "Unvigintillion", "Duovigintillion", "Tresvigintillion", "Quattorvigintillion", "Quinvigintillion", "Sexvigintillion", "Septenvigintillion", "Octovigintillion", "Novemvigintillion", "Trigintillion", "Untrigintillion", "Duotrigintillion", "Tretrigintillion", "Quattortrigintillion", "Quintrigintillion",
                                                "Sextrigintillion", "Septentrigintillion", "Octotrigintillion", "Novemtrigintillion", "Quadragintillion", "Unquadragintillion", "Duoquadragintillion", "Trequadragintillion", "Quattorquadragintillion", "Quinquadragintillion", "Sexquadragintillion", "Septenquadragintillion", "Octoquadragintillion", "Novemquadragintillion",
                                                "Quinquagintillion", "Unquinquagintillion", "Duoquinquagintillion", "Trequinquagintillion", "Quattorquinquagintillion", "Quinquinquagintillion", "Sexquinquagintillion", "Septenquinquagintillion", "Octoquinquagintillion", "Novemquinquagintillion", "Sexagintillion", "Unsexagintillion", "Duosexagintillion",
                                                "Tresexagintillion", "Quattorsexagintillion", "Quinsexagintillion", "Sexsexagintillion", "Septensexagintillion", "Octosexagintillion", "Novemsexagintillion", "Septuagintillion", "Unseptuagintillion", "Duoseptuagintillion", "Treseptuagintillion", "Quattorseptuagintillion", "Quinseptuagintillion", "Sexseptuagintillion",
                                                "Septseptuagintillion", "Octoseptuagintillion", "Novemseptuagintillion", "Octogintillion", "Unoctogintillion", "Duooctogintillion", "Treoctogintillion", "Quattoroctogintillion", "Quinoctogintillion", "Sexoctogintillion", "Septoctogintillion", "Octoctogintillion", "Novemoctogintillion", "Nonagintillion",
                                                "Unnonagintillion", "Duononagintillion", "Trenonagintillion", "Quattornonagintillion", "Quinonagintillion", "Sexnonagintillion", "Septenonagintillion", "Octononagintillion", "Novemnonagintillion", "Centillion", "Uncentillion"};  //handles full size of type 'double'
        public List<int> unlockList = new List<int> { 1, 10, 25, 50, 100, 200, 300, 400, 500, 600, 666, 700, 777, 800, 900, 1000, 1100, 1111, 1200, 1300, 1400, 1500, 1600, 1700, 1800, 1900, 2000, 2100, 2200, 2222, 2300, 2400, 2500, 2600, 2700, 2800, 2900, 3000, 3100, 3200, 3300, 3333, 3400, 3500, 3600, 3700, 3800, 3900, 4000, 4100, 4200, 4300, 4400, 4500, 4600, 4700, 4800, 4900, 5000 };
        public const double unlockMultiplier = 2.0d;
        internal Stopwatch GameClock;
        bool PlayRegisterSound1 = true;
        public PurchaseAmount myPurchaseAmount;
        public void SetAudioVolume(int musicVol, int fxVol)
        {
            if (musicVol < 0) { musicVol = 0; } else if (musicVol > 1000) { musicVol = 1000; }
            if (fxVol < 0) { fxVol = 0; } else if (fxVol > 1000) { fxVol = 1000; }
            mciSendString($"setaudio backgroundmusic01 volume to {musicVol}", null, 0, IntPtr.Zero);
            mciSendString($"setaudio registersound volume to {fxVol}", null, 0, IntPtr.Zero);
            mciSendString($"setaudio registersound2 volume to {fxVol}", null, 0, IntPtr.Zero);
            mciSendString($"setaudio clicksound volume to {fxVol}", null, 0, IntPtr.Zero);
            for (int i = 1; i <= 8; i++)
            {
                mciSendString($"setaudio pickaxe{i}sound volume to {fxVol}", null, 0, IntPtr.Zero);
            }
        }

        public void initItemsDefault()
        {
            //items should be initialized to defaults before game is loaded
            if (this.myItems == default || this.myItems.Length == 0)
            {
                myItems = [new ItemView(1, "Wood Miner", 3.738d, 1.07d, 1.0d, 600),
                    new ItemView(2, "Stone Miner", 60d, 1.15d, 60d, 3000),
                    new ItemView(3, "Iron Miner", 720d, 1.14d, 540d, 6000),
                    new ItemView(4, "Steel Miner", 8640d, 1.13d, 4320d, 12000),
                    new ItemView(5, "Diamond Miner", 103680d, 1.12d, 51840d, 24000),
                    new ItemView(6, "Uranium Miner", 1244160d, 1.11d, 622080d, 96000),
                    new ItemView(7, "Antimatter Miner", 14929920d, 1.10d, 7464960d, 384000),
                    new ItemView(8, "Black Hole Miner", 179159040d, 1.09d, 89579520d, 1536000)];
            }
            if (this.clickAmount == default)
            {
                this.clickAmount = 0.25;
            }
        }
        
        public frmMain()
        {
            InitializeComponent();
            PrestigeNextRestart = false;
            DoubleBuffered = true;
            mciSendString($@"open {Environment.CurrentDirectory}\Resources\cashregisterpurchase.wav type mpegvideo alias registersound", null, 0, IntPtr.Zero);
            mciSendString($@"open {Environment.CurrentDirectory}\Resources\clickbutton.wav type mpegvideo alias clicksound", null, 0, IntPtr.Zero);
            mciSendString($@"open {Environment.CurrentDirectory}\Resources\pickaxe-clank-01.wav type mpegvideo alias pickaxe1sound", null, 0, IntPtr.Zero);
            mciSendString($@"open {Environment.CurrentDirectory}\Resources\pickaxe-clank-02.wav type mpegvideo alias pickaxe2sound", null, 0, IntPtr.Zero);
            mciSendString($@"open {Environment.CurrentDirectory}\Resources\pickaxe-clank-03.wav type mpegvideo alias pickaxe3sound", null, 0, IntPtr.Zero);
            mciSendString($@"open {Environment.CurrentDirectory}\Resources\pickaxe-clank-04.wav type mpegvideo alias pickaxe4sound", null, 0, IntPtr.Zero);
            mciSendString($@"open {Environment.CurrentDirectory}\Resources\pickaxe-clank-05.wav type mpegvideo alias pickaxe5sound", null, 0, IntPtr.Zero);
            mciSendString($@"open {Environment.CurrentDirectory}\Resources\pickaxe-clank-06.wav type mpegvideo alias pickaxe6sound", null, 0, IntPtr.Zero);
            mciSendString($@"open {Environment.CurrentDirectory}\Resources\pickaxe-clank-07.wav type mpegvideo alias pickaxe7sound", null, 0, IntPtr.Zero);
            mciSendString($@"open {Environment.CurrentDirectory}\Resources\pickaxe-clank-08.wav type mpegvideo alias pickaxe8sound", null, 0, IntPtr.Zero);
            mciSendString($@"open {Environment.CurrentDirectory}\Resources\BackgroundMusic01.mp3 type mpegvideo alias backgroundmusic01", null, 0, IntPtr.Zero);
            mciSendString($@"open {Environment.CurrentDirectory}\Resources\cashregisterpurchase2.wav type mpegvideo alias registersound2", null, 0, IntPtr.Zero);

            //set form colors
            this.Colors = new MyColors();
            this.BackColor = Colors.colBackground;
            btnMine.BackColor = Colors.colButtonEnabled;
            btnPrestige.BackColor = Colors.colButtonEnabled;
            btnPurchAmount.BackColor = Colors.colButtonEnabled;
            btnStats.BackColor = Colors.colButtonEnabled;
            itemPanel.BackColor = Colors.colBorders;
            UpgradePanel.BackColor = Colors.colBorders;
            grpMoney.BackColor = Colors.colBorders;
            lblMatsMined.AutoSize = true;
            lblIncrPerClick.AutoSize = true;

            //initialize default item list before loading to fix prestige issue. If not prestige load, make sure to overwrite them.
            initItemsDefault();

            //frmMain constructor now initializes all fields to default IF LoadGame() didn't override them.
            this.LoadGame();
            
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
            if (this.myPurchaseAmount == default) { myPurchaseAmount = PurchaseAmount.BuyOne; }
            if (this.thislifetimeMoney == default) { this.thislifetimeMoney = 0.0d; }   //if they're default here, then we couldn't load from the last save. Set to 0.
            if (this.prestigePoints == default) { this.prestigePoints = 0.0d; }
            if (this.lastlifetimeMoney == default) { this.lastlifetimeMoney = 0.0d; }
            if (this.GameClock == default) { this.GameClock = new Stopwatch(); }
            //if (this.MusicVolume == default) { this.MusicVolume = 500; }
            //if (this.FXVolume == default) { this.FXVolume = 500; }

             
            //add upgrade button for each item and adjust salary for prestigepoints.

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
            myTip.UseFading = true; //try it and see?
            UpgradeButton btn = (UpgradeButton)sender;
            //Point mousepos = MousePosition;
            //adjust mousepos for window size and position rather than screen coords
            //a helper method already exists for this...
            Point mousepos = PointToClient(MousePosition);
            mousepos.Offset(-100, -30); //offset the tooltip up 30px and left 100px so the cursor can still click the buttons
            toolTipTimer.Start();
            if (btn.myUpgrade.itemID >= 1 && btn.myUpgrade.itemID <= myItems.Length)
            {
                //upgrade refers to an item
                myTip.Show($"{btn.myUpgrade.Description} multiplies each {myItems[btn.myUpgrade.itemID - 1].Name}'s salary per second by {btn.myUpgrade.Multiplier}!", this, mousepos);
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
                myTip.Show($"{btn.myUpgrade.Description} multiplies all miner salaries by {btn.myUpgrade.Multiplier}!", this, mousepos);
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

            sliderVolume.Value = this.MusicVolume;
            SetAudioVolume(MusicVolume, FXVolume);
            //if we're loading after a prestige, find a way to get the previous position so it doesn't restart!
            mciSendString("play backgroundmusic01 repeat", null, 0, IntPtr.Zero);

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

            foreach (ItemView item in myItems) 
            { 
                item.UpdateLabels(); 
                if (item.myQty > 0) 
                { 
                    item.progressMining.Enabled = true;
                    item.progressMining.Value = item.myprogressvalue <= item.progressMining.Maximum ? item.myprogressvalue : item.progressMining.Maximum;
                    
                    item.myTimer.Start();   //If we loaded a game, should we save and calculate interval time vs elapsed real time to get amount of fires to feed to salary?
                    //item.mystopwatch.Restart();
                } 
            }
            this.GameClock = new Stopwatch();
            this.timerPerSec.Start();
            this.timerVisualUpdate.Start();
            this.GameClock.Reset();
            this.GameClock.Start();
            if (this.myPurchaseAmount == PurchaseAmount.BuyNext)
            {
                myPurchaseAmount = PurchaseAmount.Buy100;
                btnPurchAmount_Click(this, e);
            }
            itemPanel_Resize(this, e);  //make sure items are centered
            UpgradePanel_Resize(this, e);   //make sure upgrade buttons are centered

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

                        //previously threw an exception when trying to just cast it to 'ItemView'. '.Where' returns an Enumerable of type T (inferred or specified), so being that
                        //an enumerable is just a list that meets the requirements of IEnumerable, we have to return the first item. We need to figure out how to handle btnitemID not found though...
                        ItemView tempItem = myItems.Where(x => x.myID == btnitemID).First<ItemView>();
                        tempItem.mySalary *= btnsender.myUpgrade.Multiplier;
                        
                        btnsender.myUpgrade = Upgrade.SetPurchased(btnsender.myUpgrade);

                        //find the upgrade by upgradeid in mainupgradelist that matches the button's upgradeid, and set it's Purchased property, then overwrite it's old entry in mainupgradelist.
                        Upgrade tempUpgrade = MainUpgradeList.Find(x => x.upgradeID == btnsender.myUpgrade.upgradeID);
                        MainUpgradeList[MainUpgradeList.IndexOf(tempUpgrade)] = Upgrade.SetPurchased(tempUpgrade);
                        if (PlayRegisterSound1)
                        {
                            mciSendString("seek registersound to start", null, 0, IntPtr.Zero);
                            mciSendString("play registersound", null, 0, IntPtr.Zero);
                            PlayRegisterSound1 = false;
                        }
                        else
                        {
                            mciSendString("seek registersound2 to start", null, 0, IntPtr.Zero);
                            mciSendString("play registersound2", null, 0, IntPtr.Zero);
                            PlayRegisterSound1 = true;
                        }

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
                        if (PlayRegisterSound1)
                        {
                            mciSendString("seek registersound to start", null, 0, IntPtr.Zero);
                            mciSendString("play registersound", null, 0, IntPtr.Zero);
                            PlayRegisterSound1 = false;
                        }
                        else
                        {
                            mciSendString("seek registersound2 to start", null, 0, IntPtr.Zero);
                            mciSendString("play registersound2", null, 0, IntPtr.Zero);
                            PlayRegisterSound1 = true;
                        }
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
                        if (PlayRegisterSound1)
                        {
                            mciSendString("seek registersound to start", null, 0, IntPtr.Zero);
                            mciSendString("play registersound", null, 0, IntPtr.Zero);
                            PlayRegisterSound1 = false;
                        }
                        else
                        {
                            mciSendString("seek registersound2 to start", null, 0, IntPtr.Zero);
                            mciSendString("play registersound2", null, 0, IntPtr.Zero);
                            PlayRegisterSound1 = true;
                        }
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
                        if (PlayRegisterSound1)
                        {
                            mciSendString("seek registersound to start", null, 0, IntPtr.Zero);
                            mciSendString("play registersound", null, 0, IntPtr.Zero);
                            PlayRegisterSound1 = false;
                        }
                        else
                        {
                            mciSendString("seek registersound2 to start", null, 0, IntPtr.Zero);
                            mciSendString("play registersound2", null, 0, IntPtr.Zero);
                            PlayRegisterSound1 = true;
                        }
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
        /// <summary>
        /// Returns calculated amount of prestige points if prestiged right now.
        /// </summary>
        /// <param name="lastlifeMoney">Money made in the last prestige loop</param>
        /// <param name="thislifeMoney">Money made this prestige loop</param>
        /// <param name="prestigeModifier">Modifier for prestige difficulty. Default is 4B, but can be overridden.</param>
        /// <returns>double representing calculated prestige points to gain.</returns>
        public double calcPrestige(double lastlifeMoney, double thislifeMoney, double prestigeModifier = 4000000000.0d)
        {
            double newlifeMoney = lastlifeMoney + thislifeMoney;
            double term1 = Math.Pow(newlifeMoney / (prestigeModifier / 9), 0.5d);
            double term2 = Math.Pow(lastlifeMoney / (prestigeModifier / 9), 0.5d);
            return term1 - term2 >= 0 ? double.Round(term1 - term2, MidpointRounding.ToZero) : 0;
        }

        public void frmMain_UpdateLabels()
        {
            lblMoney.Text = $"Money: ${(myMoney >= 1000000.0d ? Stringify(myMoney.ToString("R"), StringifyOptions.LongText) : double.Round(myMoney, 2).ToString("N"))}";
            lblSalary.Text = $"Salary: ${(salary >= 1000000.0d ? Stringify(salary.ToString("R"), StringifyOptions.LongText) : double.Round(salary, 2).ToString("N"))} Per Second";
            lblClickAmount.Text = $"Mining: ${(clickAmount >= 1000000.0d ? Stringify(clickAmount.ToString("R"), StringifyOptions.LongText) : double.Round(clickAmount, 2).ToString("N"))} Per Click";
            lblIncrPerClick.Text = $"Mined Per Click: {incrperclick:N0}";
            lblMatsMined.Text = $"Materials Mined: {matsMined:N0}";
            if (this.myPurchaseAmount == PurchaseAmount.BuyOne || this.myPurchaseAmount == PurchaseAmount.BuyTen || this.myPurchaseAmount == PurchaseAmount.Buy100)
            {
                btnPurchAmount.Text = $"Buy: x{(int)myPurchaseAmount}";
            }
            else if (this.myPurchaseAmount == PurchaseAmount.BuyMax)
            {
                btnPurchAmount.Text = "Buy: Max";
            }
            else if (this.myPurchaseAmount == PurchaseAmount.BuyNext)
            {
                btnPurchAmount.Text = "Buy: Next";
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
                //btn.Refresh();  //is this necessary?

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
                tempsal += ((view.mySalary / ((double)view.mySalaryTimeMS / 1000.0d)) * view.myQty);    //if qty is 0, salary increment will be 0.
            }
            salary = tempsal;
            //myMoney += salary;
            //thislifetimeMoney += salary;
        }
        public void BuyClicked(ItemView sender)     //--TEST UNLOCKS!!!--//
        {
            if (sender.CanAfford(this.myMoney, sender.purchaseAmount, sender.myCostMult))
            {

                myMoney -= sender.calculatedCost;
                if (PlayRegisterSound1)
                {
                    mciSendString("seek registersound to start", null, 0, IntPtr.Zero);
                    mciSendString("play registersound", null, 0, IntPtr.Zero);
                    PlayRegisterSound1 = false;
                }
                else
                {
                    mciSendString("seek registersound2 to start", null, 0, IntPtr.Zero);
                    mciSendString("play registersound2", null, 0, IntPtr.Zero);
                    PlayRegisterSound1 = true;
                }
                sender.myQty += sender.purchaseAmount;
                if (sender.myQty > 0)
                {
                    sender.progressMining.Enabled = true;
                    sender.myTimer.Start();
                }
                sender.myCost = sender.myCost * Math.Pow(sender.myCostMult, sender.purchaseAmount);

                int highestunlockindex = sender.latestUnlock;
                do
                {
                    highestunlockindex++;
                }
                while (sender.myQty >= unlockList[highestunlockindex]);
                highestunlockindex--;   //since we're checking condition after iteration, we need to back down by 1 to get the last valid result. If we're in this routine, we just bought at least 1. HUI is 0 now.
                if (highestunlockindex > sender.latestUnlock)     //0 > -1 == true
                {
                    //we reached a milestone!
                    do
                    {
                        sender.latestUnlock++;        //-1 + 1 = 0;
                        if (sender.myQty > 1) { sender.mySalary *= unlockMultiplier; }  //apply the bonus to this item if we bought more than 1, so that we can still have buy1 within buynext, and so we get an unlock for buying 1 of all.
                        bool allOthersHave = true;
                        for (int i = 0; i < myItems.Length; i++)
                        {
                            if (myItems[i].myID != sender.myID)   //don't need to check this one again...
                            {
                                if (myItems[i].myQty < unlockList[sender.latestUnlock])       //if qty < UL[0]==1, then we don't own at least 1 of everything else!
                                {
                                    allOthersHave = false;
                                    break;      //if any one of them fails, exit the loop - it would be computationally wasteful not to.
                                }
                            }
                        }
                        if (allOthersHave)
                        {
                            for (int i = 0; i < myItems.Length; i++)
                            {
                                myItems[i].mySalary *= unlockMultiplier;
                            }
                        }
                    }
                    while (sender.latestUnlock < highestunlockindex); //bonus will be applied, and latestunlock will be set afterwards to equal highestunlockindex.
                                                                      //0 < 0 == false, so execution falls out of loop and continues elsewhere.
                }
                else
                {
                    //they're the same, so we didn't reach a new milestone.
                }
            }
            if (this.myPurchaseAmount == PurchaseAmount.BuyNext)
            {
                //calculate next
                int temppurchamount;
                if (sender.latestUnlock + 1 < unlockList.Count && sender.latestUnlock + 1 >= 0)     //make sure the unlock we're looking for is in the list...
                {
                    temppurchamount = unlockList[sender.latestUnlock + 1] - sender.myQty;
                }
                else
                {
                    temppurchamount = 1;    //when in doubt, set it to 1. If we've bought all unlocks, we only need to calculate for 1 item purchase.
                }
                sender.calculatedCost = this.costcalcnew(sender, temppurchamount);
                sender.purchaseAmount = temppurchamount;
            }
            if (sender.mySalaryTimeMS < sender.myTimer.Interval) { ItemView.NormalizeSalary(sender, sender.myTimer.Interval); }
            sender.UpdateLabels();
            sender.ButtonColor(myMoney, sender.purchaseAmount, sender.myCostMult);
        }




        private void btnPurchAmount_Click(object sender, EventArgs e)
        {
            mciSendString("seek clicksound to start", null, 0, IntPtr.Zero);
            mciSendString("play clicksound", null, 0, IntPtr.Zero);

            //purchAmount should be made into an enum. Perfect use-case for it, and reduces possible errors from invalid values.
            if (myPurchaseAmount == PurchaseAmount.BuyOne)
            {
                myPurchaseAmount = PurchaseAmount.BuyTen;
            }
            else if (myPurchaseAmount == PurchaseAmount.BuyTen)
            {
                myPurchaseAmount = PurchaseAmount.Buy100;
            }
            else if (myPurchaseAmount == PurchaseAmount.Buy100)
            {
                myPurchaseAmount = PurchaseAmount.BuyNext;
            }
            else if (myPurchaseAmount == PurchaseAmount.BuyNext)
            {
                myPurchaseAmount = PurchaseAmount.BuyMax;
            }
            else if (myPurchaseAmount == PurchaseAmount.BuyMax)
            {
                myPurchaseAmount = PurchaseAmount.BuyOne;
            }

            if (myPurchaseAmount == PurchaseAmount.BuyOne || myPurchaseAmount == PurchaseAmount.BuyTen || myPurchaseAmount == PurchaseAmount.Buy100)
            {
                for (int i = 0; i < myItems.Length; i++)
                {
                    //we just need to update the item with the new amount, and then update the labels and colors.
                    myItems[i].purchaseAmount = (int)myPurchaseAmount;
                    myItems[i].CalcCost(myItems[i].purchaseAmount, myItems[i].myCostMult);
                    myItems[i].ButtonColor(myMoney, myItems[i].purchaseAmount, myItems[i].myCostMult);
                    myItems[i].UpdateLabels();
                }
            }
            else if (myPurchaseAmount == PurchaseAmount.BuyMax)
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
            else if (myPurchaseAmount == PurchaseAmount.BuyNext)
            {
                for (int i = 0; i < myItems.Length; i++)
                {
                    int temppurchamount;
                    if (myItems[i].latestUnlock + 1 < unlockList.Count && myItems[i].latestUnlock + 1 >= 0)     //make sure the unlock we're looking for is in the list...
                    {
                        temppurchamount = unlockList[myItems[i].latestUnlock + 1] - myItems[i].myQty;
                    }
                    else
                    {
                        temppurchamount = 1;    //when in doubt, set it to 1. If we've bought all unlocks, we only need to calculate for 1 item purchase.
                    }
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

            } //for now, when matsMined reaches another multiple of 100, amountperclick is incremented.
            //incrperclick has a max of 10 for now.
            else { matsMined += incrperclick; }

            //We're only calculating clickAmount for one incrperclick. We can add that amount for multiple clicks, but it shouldn't double clickAmount.
            //Later we can use visual and audible cues to signify multiple clicks (like floating text saying 'x2' 'x3' etc, and disappearing after a second, with little 'pop' sounds for each incrperclick in quick succession...)
            this.myMoney += clickAmount * incrperclick;
            thislifetimeMoney += clickAmount * incrperclick;

            Random randnumber = new Random();
            int i = randnumber.Next(1, 8);
            mciSendString($"seek pickaxe{i}sound to start", null, 0, IntPtr.Zero);
            mciSendString($"play pickaxe{i}sound", null, 0, IntPtr.Zero);

            //testing...
            FloatText();
        }

        private void timerVisualUpdate_Tick(object sender, EventArgs e)
        {
            foreach (ItemView item in myItems)
            {
                if (this.myPurchaseAmount == PurchaseAmount.BuyMax) { item.purchaseAmount = (int)calcMax(myMoney, item); }  //only used for max calculation updates
                item.ButtonColor(myMoney, item.purchaseAmount, item.myCostMult);
                if (item.calculatedCost == 0.00d) { item.calculatedCost = costcalcnew(item, 1); }   //prevent showing $0 in item cost field
                item.UpdateLabels();
            }
            //TODO: Add upgrade labelupdates and buttoncolor updates
            frmMain_UpdateLabels();
            foreach (Label lbl in floatlabels)
            {
                lbl.Location = new Point(lbl.Location.X, lbl.Location.Y - 40);
            }
            foreach (Label lbl in floatlabeldeletelist)
            {
                if (floatlabels.Contains(lbl))
                {
                    floatlabels.Remove(lbl);
                    lbl.Dispose();
                }
            }
            floatlabeldeletelist.Clear();
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
            mciSendString("seek clicksound to start", null, 0, IntPtr.Zero);
            mciSendString("play clicksound", null, 0, IntPtr.Zero);

            //In order for this to work, I need to refactor the main game logic into it's own gameobject that takes parameters for prestige amount, and default params(overrideable) for money, upgrades, purchased items, etc.
            //Or I can take advantage of the load/save system, and just configure the save to reset for a prestige-flagged restart, that way i can customize what parameters get changed.
            double tempprestige = this.calcPrestige(lastlifetimeMoney, thislifetimeMoney);
            foreach (ItemView item in myItems)
            {
                item.myTimer.Stop();
            }
            this.timerPerSec.Stop();
            this.timerVisualUpdate.Stop();
            this.GameClock.Stop();
            this.thislifeGameTime += GameClock.Elapsed;
            this.totalGameTime += GameClock.Elapsed;

            DialogResult dres = MessageBox.Show($"Current Prestige: {Stringify(prestigePoints.ToString("R"), StringifyOptions.LongText)}. \nPrestige to Gain: {Stringify(tempprestige.ToString("R"), StringifyOptions.LongText)}. Prestige?", "Reset to earn prestige?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dres == DialogResult.Yes)
            {
                this.timerPerSec.Stop();
                this.timerVisualUpdate.Stop();
                this.toolTipTimer.Stop();
                this.GameClock.Stop();
                foreach (ItemView item in myItems)
                {
                    item.myTimer.Stop();
                    item.myprogressvalue = item.progressMining.Value;
                }

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
                this.clickAmount = 0.25d;
                this.prestigeMultiplier = 2;
                this.matsMined = default;
                this.incrperclick = 1;
                this.thislifetimeMoney = default;
                this.thislifeGameTime = default;
                Program.RestartForPrestige = true;
                this.PrestigeNextRestart = true;
                SaveGame(tempprestige);
                
                this.Close();
            }
            else if (dres == DialogResult.No)
            {
                foreach (ItemView item in myItems)
                {
                    if (item.progressMining.Enabled)
                    {
                        item.myTimer.Start();
                    }

                }
                timerPerSec.Start();
                timerVisualUpdate.Start();
                this.GameClock.Reset();
                this.GameClock.Start();
                //this.Focus();
                //this.Refresh();
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
            mciSendString("close registersound2", null, 0, IntPtr.Zero);
            mciSendString("close backgroundmusic01", null, 0, IntPtr.Zero);
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

        public void SaveGame(double presttogain = 0.0d)
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
            save.myPurchaseAmount = this.myPurchaseAmount;
            save.thislifetimeMoney = this.thislifetimeMoney;
            save.prestigePoints = this.prestigePoints;
            save.lastlifetimeMoney = this.lastlifetimeMoney;
            save.totalgametime = this.totalGameTime;
            save.thislifegametime = this.thislifeGameTime;
            save.lastsavetimestamp = DateTime.Now;
            save.lastWindowState = this.WindowState;
            save.MusicVol = this.MusicVolume;
            save.FXVol = this.FXVolume;
            if (this.PrestigeNextRestart)
            {
                save.PrestigeSaveFlag = true;
                save.PrestigePointsGained = presttogain;
            }
            else
            {
                save.PrestigeSaveFlag = false;
                save.PrestigePointsGained = 0.0d;
            }

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
            //If there's a problem or we can't find the file, just skip loading altogether. The constructor will initialize to default, and next time
            //the game is closed it will save a new file, overwriting the corrupted one if it exists.
            catch 
            {
                this.MusicVolume = 500;
                this.FXVolume = 500;
                this.WindowState = FormWindowState.Maximized;
                return; 
            }
            List<ItemView> tempitemlist = new List<ItemView>();
            for (int i = 0; i < save.myItemDatas.Count(); i++)
            {
                tempitemlist.Add(new ItemView(save.myItemDatas[i]));
            }
            //only overwrite list if we actually loaded items from the save. Otherwise, keep defaults.
            if (tempitemlist.Count != 0)
            {
                this.myItems = tempitemlist.ToArray();
            }
            this.MainUpgradeList = save.MainUpgradeList;

            this.toolTipVisibleTime = save.toolTipVisibleTime;
            this.toolTipDelay = save.toolTipDelay;
            this.prestigeMultiplier = save.prestigeMultiplier;
            this.clickAmount = save.clickAmount;
            this.salary = save.salary;
            this.matsMined = save.matsMined;
            this.myMoney = save.myMoney;
            this.incrperclick = save.incrperclick;
            this.myPurchaseAmount = save.myPurchaseAmount;
            this.thislifetimeMoney = save.thislifetimeMoney;
            this.prestigePoints = save.prestigePoints;
            this.lastlifetimeMoney = save.lastlifetimeMoney;
            this.totalGameTime = save.totalgametime;
            this.thislifeGameTime = save.thislifegametime;
            this.WindowState = save.lastWindowState;
            this.MusicVolume = save.MusicVol;
            this.FXVolume = save.FXVol;
            TimeSpan sincelastsave = DateTime.Now.Subtract(save.lastsavetimestamp);
            this.thislifeGameTime.Add(sincelastsave);
            this.totalGameTime.Add(sincelastsave);
            if (sincelastsave.TotalSeconds > 1.0d) 
            { 
                //myMoney += salary * sincelastsave.TotalSeconds; 
                //thislifetimeMoney += salary * sincelastsave.TotalSeconds;

                double salearned = 0.0d;
                for (int i = 0; i < myItems.Length; i++)
                {
                    int thisremainingMS;
                    salearned += (progressCompletedIterations(sincelastsave, myItems[i], out thisremainingMS) * myItems[i].mySalary * myItems[i].myQty);
                    myItems[i].myprogressvalue = thisremainingMS;
                }
                myMoney += salearned;
                thislifetimeMoney += salearned;

                MessageBox.Show($"Welcome Back!\nYou were gone for {sincelastsave.TotalHours:N0} hours, {sincelastsave.Minutes:N0} minutes, and {sincelastsave.Seconds:N0} seconds.\nYou made ${((salearned) >= 1000000.0d ? Stringify((salearned).ToString("R"), StringifyOptions.LongText) : (salearned).ToString("N"))} while you were gone!", "Since you've been gone...", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, false); 
            }
            if (save.PrestigeSaveFlag || (save.clickAmount == 0.25d && this.prestigePoints > 0.0d))
            {
                //We know that clickAmount and myItems are empty right now - we need to initialize these to default before loading.
                //Find out why MusicVol and FXVol didn't save.
                //show message displaying amount of prestige earned?
                MessageBox.Show($"You gained {(save.PrestigePointsGained >= 1000000.0d ? Stringify(save.PrestigePointsGained.ToString("R"), StringifyOptions.LongText) : save.PrestigePointsGained.ToString("N"))} prestige points!", "Congratulations!");
                //after doing something, set prestigesaveflag to false, then savegame() so that the save file doesn't cause repeated messages at startup.
                if (this.prestigePoints > 0.0d)
                {
                    //we got here, but clickamount and myItems were default/null. Reevaluate save method and loading/initialization order.
                    this.clickAmount *= ((prestigePoints / (100.0d / prestigeMultiplier)) + 1);
                }
                foreach (var item in myItems)
                {
                    //upgradeButtons.Add(new UpgradeButton());
                    if (this.prestigePoints > 0.0d)
                    {
                        item.mySalary *= ((prestigePoints / (100.0d / prestigeMultiplier)) + 1);
                    }
                }
                save.PrestigeSaveFlag = false;
                SaveGame();
            }
        }

        public void ToggleItemSalaryDisplays()
        {
            //Toggle all item displays
            foreach (ItemView item in myItems)
            {
                item.displaySalPerSec = !item.displaySalPerSec;
            }
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
            mciSendString("seek clicksound to start", null, 0, IntPtr.Zero);
            mciSendString("play clicksound", null, 0, IntPtr.Zero);
            //open stats window with current stats, update times and restart gameclock

            this.thislifeGameTime += GameClock.Elapsed;
            this.totalGameTime += GameClock.Elapsed;
            this.GameClock.Reset();
            this.GameClock.Start();

            DialogResult result = MessageBox.Show(
                $"Salary: ${(salary > 1000000.0d ? Stringify(this.salary.ToString("R"), StringifyOptions.LongText) : this.salary.ToString("N"))} Per Second" +
                $"\nClickAmount: ${(clickAmount > 1000000.0d ? Stringify(this.clickAmount.ToString("R"), StringifyOptions.LongText) : this.clickAmount.ToString("N"))} Per Click" +
                $"\nMoney Earned This Lifetime: ${(thislifetimeMoney > 1000000.0d ? Stringify(this.thislifetimeMoney.ToString("R"), StringifyOptions.LongText) : this.thislifetimeMoney.ToString("N"))}" +
                $"\nMoney Earned All Lifetimes: ${(lastlifetimeMoney + thislifetimeMoney > 1000000.0d ? Stringify((this.lastlifetimeMoney + this.thislifetimeMoney).ToString("R"), StringifyOptions.LongText) : (this.thislifetimeMoney + this.lastlifetimeMoney).ToString("N"))}" +
                $"\nMoney Earned Last Lifetime: ${(lastlifetimeMoney > 1000000.0d ? Stringify(this.lastlifetimeMoney.ToString("R"), StringifyOptions.LongText) : this.lastlifetimeMoney.ToString("N"))}" +
                $"\nTime spent this lifetime: {this.thislifeGameTime.ToString(@"h\:mm\:ss")}" +
                $"\nTime spent all lifetimes: {this.totalGameTime.ToString(@"h\:mm\:ss")}" +
                $"\nPrestige Points: {Stringify(this.prestigePoints.ToString("R"), StringifyOptions.LongText)}" +
                $"\nPrestige Multiplier: {Stringify(this.prestigeMultiplier.ToString("R"), StringifyOptions.LongText)}% Per Point" +
                $"\nPrestige Percentage: {(prestigePoints * prestigeMultiplier > 1000000.0d ? Stringify((this.prestigePoints * this.prestigeMultiplier).ToString("R"), StringifyOptions.LongText) : (prestigePoints * prestigeMultiplier).ToString("N0"))} %"
                , "MoneyMiner Statistics", MessageBoxButtons.OK
                );

        }

        private void sliderVolume_Scroll(object sender, EventArgs e)
        {
            this.MusicVolume = sliderVolume.Value;
            this.FXVolume = sliderVolume.Value;
            SetAudioVolume(MusicVolume, FXVolume);
        }

        List<Label> floatlabels = new List<Label>();
        List<Label> floatlabeldeletelist = new List<Label>();
        public void FloatText()
        {
            //get center x coord of btnMine

            //create a label just above btnMine

            //add label to list of floatlabels?

            //each visualupdatetick move each label in the list up by some small amount of px

            //create eventhandler for label.locationchanged that changes forecolor to a bit lighter if not transparent

            //if it is transparent, dispose of it
            int mousex = PointToClient(MousePosition).X;
            int centerxofbtn = btnMine.Location.X + (btnMine.Size.Width / 2);
            Label lblNew = new Label();
            lblNew.Location = new Point(mousex, btnMine.Location.Y - 30);
            lblNew.LocationChanged += new EventHandler(floatlabelmoved);
            lblNew.AutoSize = true;
            lblNew.Parent = this;
            lblNew.BringToFront();
            lblNew.Name = $"lblFloat{floatlabels.Count + 1}";
            lblNew.Text = $"${(clickAmount * incrperclick >= 1000000.0d ? Stringify((clickAmount * incrperclick).ToString()) : double.Round(clickAmount * incrperclick, 2).ToString("N"))}";
            lblNew.ForeColor = Color.FromArgb(255, Colors.colTextPrimary);
            lblNew.BackColor = Color.FromArgb(0, Colors.colTextSecondary);
            lblNew.Visible = true;
            lblNew.Enabled = true;
            lblNew.Show();
            floatlabels.Add(lblNew);
        }
        private void floatlabelmoved(object? sender, EventArgs e)
        {
            int dimmingamount = 40;
            Label thislbl;
            if (sender != null)
            {
                thislbl = (Label)sender;
            }
            else { return; }
            if (thislbl.ForeColor.A > 0)
            {   //subtract 20 from alpha value

                int alphaval = thislbl.ForeColor.A - dimmingamount >= 0 ? thislbl.ForeColor.A - dimmingamount : 0;
                thislbl.ForeColor = Color.FromArgb(alphaval, Colors.colTextPrimary);
            }
            else
            {
                floatlabeldeletelist.Add(thislbl);
            }
        }

        private void UpgradePanel_Resize(object sender, EventArgs e)
        {
            //calculate and recenter each button within panel
            //do this at frmload as well after buttons are added in
            int truecenterx = UpgradePanel.Location.X + ((UpgradePanel.Width - SystemInformation.VerticalScrollBarWidth) / 2);
            for (int i = 0; i < UpgradePanel.Controls.Count; i++)
            {
                UpgradePanel.Controls[i].Location = new Point((truecenterx - (UpgradePanel.Controls[i].Width / 2)) + UpgradePanel.Margin.Left, UpgradePanel.Controls[i].Location.Y);
            }
            //refresh panel to redraw it and it's controls
            UpgradePanel.Refresh();
        }

        private void itemPanel_Resize(object sender, EventArgs e)
        {
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

        public void itemTimer_Tick(object? sender, EventArgs e)
        {
            //sender is the ItemView that contains the timer that 'ticked'. Execution is caught by itemview.salarytimer_tick, and thrown here.
            if (sender == null) { return; }
            ItemView senderview = (ItemView)sender;
            //senderview.myTimer.Stop();
            if (senderview.progressMining.Value == senderview.progressMining.Maximum)
            {
                //payout, force draw, then iterate
                myMoney += senderview.mySalary * senderview.myQty;
                thislifetimeMoney += senderview.mySalary * senderview.myQty;
                senderview.progressMining.Value = 1;
                senderview.progressMining.Value = 0;
                senderview.progressMining.Value += senderview.myTimer.Interval;
                senderview.progressMining.Value--;
            }
            else if (senderview.progressMining.Value < senderview.progressMining.Maximum)
            {
                if (senderview.progressMining.Value + senderview.myTimer.Interval >= senderview.progressMining.Maximum)
                {
                    senderview.progressMining.Value = senderview.progressMining.Maximum;
                }
                else
                {
                senderview.progressMining.Value += senderview.myTimer.Interval;
                }
                senderview.progressMining.Value--;
                senderview.progressMining.Value++;
            }
            senderview.myprogressvalue = senderview.progressMining.Value;
            /*
             * if max, payout, set to 0 then increment
             * else, increment
             */

            //senderview.myTimer.Start();
        }

        public int progressCompletedIterations(TimeSpan elapsed, ItemView item, out int remainingMS)
        {
            //calculate how many complete iterations have passed
            if (item.myprogressvalue + elapsed.TotalMilliseconds < item.mySalaryTimeMS)
            {
                remainingMS = item.mySalaryTimeMS - (item.myprogressvalue + (int)Math.Floor(elapsed.TotalMilliseconds));
                return 0;
            }
            else
            {
                //return will be (int)Math.Floor((elapsed + progressvalue) / saltime)
                //remainingMS will be (elapsed + progressvalue) % saltime
                remainingMS = ((int)elapsed.TotalMilliseconds + item.myprogressvalue) % item.mySalaryTimeMS;
                return (int)Math.Floor((double)((int)elapsed.TotalMilliseconds + item.myprogressvalue) / item.mySalaryTimeMS);
            }
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
        internal PurchaseAmount myPurchaseAmount;
        internal double thislifetimeMoney;
        internal double prestigePoints;
        internal double lastlifetimeMoney;
        internal TimeSpan totalgametime;
        internal TimeSpan thislifegametime;
        internal DateTime lastsavetimestamp;
        internal FormWindowState lastWindowState;
        internal bool PrestigeSaveFlag;
        internal double PrestigePointsGained;
        internal int MusicVol;
        internal int FXVol;
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
            myPurchaseAmount = default;
            thislifetimeMoney = default;
            prestigePoints = default;
            lastlifetimeMoney = default;
            totalgametime = default;
            thislifegametime = default;
            lastsavetimestamp = DateTime.Now;
            lastWindowState = default;
            PrestigeSaveFlag = false;
            PrestigePointsGained = 0.0d;
            MusicVol = 500;
            FXVol = 500;
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

    /// <summary>
    /// Represents available purchase amounts using button switch. Default value is BuyOne.
    /// </summary>
    [DefaultValue(PurchaseAmount.BuyOne)]
    public enum PurchaseAmount
    {
        BuyOne = 1,
        BuyTen = 10,
        Buy100 = 100,
        BuyNext = 200,
        BuyMax = -1
    }
    public enum StringifyOptions
    {
        LongText = 32,
        ShortText = 64,
        ScientificNotation = 128
    }
    
    
}
