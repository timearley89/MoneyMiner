using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
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
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FirstClicker.Controls;
using Microsoft.Win32;
using MoneyMiner;
using MoneyMiner.Properties;
using MoneyMiner.Windows;
using static FirstClicker.Upgrade;

namespace FirstClicker
{

    public partial class frmMain : Form
    {
        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        static extern Int32 mciSendString(string command, StringBuilder? buffer, int bufferSize, IntPtr hwndCallback);
        /*NOTES & TODO

        //Settings menu:
        //-Number Notation setting? Would require fleshing out the Stringify method the rest of the way, could use the enum already implemented to facilitate.
            -Long Text Notation --DONE
            -Short Text Notation
            -Scientific Notation

        //Money needs to 'do' something, or be used for something. Would add some depth, but what? In 'other games', money is used to buy 'Mega-bucks', which can be
            used to purchase global multipliers, time warps, etc.

        //Should have bonuses like time warps, profit multipliers, autoclickers, etc. We have the framework now to implement them pretty easily.

        //Prestige button should change color (or overlays an exclamation point or something) when prestigepoints to be earned >= current prestigepoints (or > 0 if at 0)

        //Achievements

        //Upgrades sorted into button/per type which update to the next related upgrade after buying one? Or is the list approach better?

        //Add new stats:
        //Money earned by clicking - matsMinedEarnedThisLifetime
        //Money earned by clicking lifetime - matsMinedEarnedTotal
        //Money earned by miners - autoMinerEarnedThisLifetime
        //Money earned by miners lifetime - autoMinerEarnedTotal

        //Need a proper 'Stats' window!

        //Need a proper 'About' window!

        //Need proper Prestige Earned window!

        //Need proper 'Welcome Back' window!

        //Need more background music? If so, we'll need a callback method for when playback is finished that triggers next song in list. A new BGMusic enum would work for holding
        //track names, and the callback method would iterate to the next item in BGMusic, open it's file, seek to 0L, and play.

        //Need more upgrades! & Rebalance upgrades. Add more clickAmount upgrades and change multipliers to keep items relevant.

        //Need Prestige Upgrades as well - as in upgrades you buy with prestige points. -Maybe, Maybe not...

        //Maybe we should add an upgradeID for item.mySalaryTimeMS / upgrade.Multiplier... "Speed X4", etc. If already normalized, multiply salary instead.

        //Move default items to external (xml?) file with permissions (create from internal default if it doesn't exist or is outdated - compare application.settings.builddate with file date?)

        //Move default upgrades to external (xml?) file with permissions (create from internal default if it doesn't exist or is outdated - compare application.settings.builddate with file date?)

        //Store instance of unlockList in each item as well as the global one, each item will reference their 
            own first, then the global one, to determine global unlocks. This allows for different unlock levels across the board,
            and also, keeping things dynamic this way allows for things like custom events later.

        //UpgradeButton tooltips are appearing within upgradePanel, thus limiting where they appear(I think). Need to reference them to frmMain instead. -DONE, not fixed

        */

        //----Properties/Fields----//
        public Game myGame;

        //----Initialization----//
        public frmMain()
        {
            InitializeComponent();

            //attempt autoload of $"{Environment.CurrentDirectory}\GameState.mmf"
            GameState? tempSave = this.LoadGame();
            bool wasSaveNull = false;
            if (tempSave != null)
            {
                //load data from tempsave and use it to initialize.
                myGame = new Game(tempSave);
                if (myGame.PrestigeNextRestart && myGame.prestigeGainedNextRestart > 0.0d && tempSave.saveType == SaveType.Prestigesave)
                {
                    this.myGame = ApplyNewPrestige(myGame);
                }
            }
            else
            {
                //load default data and initialize.
                wasSaveNull = true;
                myGame = new Game();
            }
            CreateLog();
            LogMessage(wasSaveNull ? "New Game Initialized" : "Game Save Loaded");
            InitAudio();
            InitControls();
            LoadItemControlsToForm();
            LoadUpgradeControlsToForm();

        }
        public void InitAudio()
        {
            LogMessage("Initializing Audio...");
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
            mciSendString($@"open {Environment.CurrentDirectory}\Resources\ping.wav type mpegvideo alias pingsound", null, 0, IntPtr.Zero);
            LogMessage("Audio Initialized");
        }   //before frmMain_load
        public void InitControls()
        {
            LogMessage("Initializing Controls...");
            //Set default form colors
            this.BackColor = Colors.colBackground;
            this.btnMine.BackColor = Colors.colButtonEnabled;
            this.btnPrestige.BackColor = Colors.colButtonEnabled;
            this.btnPurchAmount.BackColor = Colors.colButtonEnabled;
            this.btnQuickBuy.BackColor = Colors.colButtonDisabled;
            this.btnQuickBuy.ForeColor = Colors.colUpgradeTextDisabled;
            this.btnQuickBuy.Enabled = false;   //only enable if we have upgrade(s) available to buy, and we can afford them.
            this.itemPanel.BackColor = Colors.colBorders;
            this.UpgradePanel.BackColor = Colors.colBorders;
            this.grpMoney.BackColor = Colors.colBorders;
            this.btnPause.BackColor = Colors.colButtonEnabled;
            this.btnMine.BackgroundImageLayout = ImageLayout.Stretch;
            LogMessage("Controls Initialized");
        }   //after ctor, before frmMain_load
        public void LoadItemControlsToForm()
        {
            LogMessage("Loading Items To Form...");
            //Populate form with items from array, then center the items.
            for (int i = 1; i <= myGame.myItems.Length; i++)
            {
                itemPanel.Controls.Add(myGame.myItems[i - 1]);  //add all items to itempanel
            }
            itemPanel_Resize(this, EventArgs.Empty);  //make sure items are centered
            LogMessage("Items Loaded");
        }   //after ctor, before frmMain_load
        public void LoadUpgradeControlsToForm()
        {
            LogMessage("Loading Upgrades to Form...");
            //add upgrade button for each upgrade, configure it, and add to form's upgradePanel, then center it.
            foreach (Upgrade upgrade in myGame.MainUpgradeList)
            {
                UpgradeButton btn = new UpgradeButton();
                btn.Text = upgrade.Description + $"\n${(upgrade.Cost >= 1000000.0d ? Stringify(upgrade.Cost.ToString("R"), StringifyOptions.LongText) : double.Round(upgrade.Cost, 2).ToString("N"))}";
                btn.Font = new Font("Ebrima", 9, FontStyle.Bold);
                btn.MouseHover += Btn_Hover;
                btn.myUpgrade = upgrade;
                btn.CausesValidation = false;
                UpgradeButtonEnable(btn, false);
                myGame.upgradeButtons.Add(btn);
            }
            //Configure each button and add to form's UpgradePanel
            for (int i = 0; i < myGame.upgradeButtons.Count; i++)      //SET UP UPGRADE BUTTONS
            {
                //itemID==0 is clickAmount upgrade button, itemID==1-8 is item upgrade button.
                //itemID==15 is allitem upgrade, itemID==20 is prestigemultiplier upgrade.

                myGame.upgradeButtons[i].Click += new EventHandler(upgradeClicked);
                myGame.upgradeButtons[i].Width = (int)(UpgradePanel.Width * 0.85);  //button width is 85% of panel width
                myGame.upgradeButtons[i].Height = (int)(myGame.upgradeButtons[i].Width * 0.40); //button height is 40% of button width
                UpgradePanel.Controls.Add(myGame.upgradeButtons[i]);   //add all upgrade buttons to upgradepanel
            }


            UpgradePanel_Resize(this, EventArgs.Empty);   //make sure upgrade buttons are centered
            LogMessage("Upgrades Loaded");
        }   //after ctor, before frmMain_load
        private void frmMain_Load(object sender, EventArgs e)
        {
            LogMessage("Form Loaded");
            //set window state according to game object, and thus by the last save, if it exists
            this.WindowState = myGame.myWindowState;
            //initaudio
            SetAudioVolume(myGame.MusicVolume, myGame.FXVolume);
            this.Refresh();
            LogMessage("Calculating Idle Earnings...");
            this.myGame = SinceYouveBeenGone(myGame);
            this.Activate();
            LogMessage("Earnings Calculated. Starting Game...");
            GameStart();
        }
        public void GameStart()
        {
            //start backgroundmusic if enabled or default
            if (myGame.MusicEnabled) { mciSendString("play backgroundmusic01 repeat", null, 0, IntPtr.Zero); }

            //put in gamestart()
            foreach (ItemView item in myGame.myItems)
            {
                item.UpdateLabels();
                if (item.myQty > 0)
                {
                    item.progressMining.Enabled = true;
                    item.progressMining.Value = item.myprogressvalue <= item.progressMining.Maximum ? item.myprogressvalue : item.progressMining.Maximum;

                    item.myTimer.Start();   //If we loaded a game, should we save and calculate interval time vs elapsed real time to get amount of fires to feed to salary?

                }
            }

            this.timerPerSec.Start();
            this.timerVisualUpdate.Start();
            myGame.GameClock.Reset();
            myGame.GameClock.Start();
            if (myGame.myPurchaseAmount == PurchaseAmount.BuyNext)
            {
                myGame.myPurchaseAmount = PurchaseAmount.Buy100;
                btnPurchAmount_Click(this, EventArgs.Empty);
            }
            if (myGame.AutosaveEnabled && myGame.AutosaveInterval > 0) { myGame.AutosaveTimer.Interval = myGame.AutosaveInterval * 60000; myGame.AutosaveTimer.Start(); }
            LogMessage("Game Started");
        }   //after frmMain_load
        public void ClearFormItems()
        {
            LogMessage("Clearing Form Items and Upgrades...");
            this.itemPanel.Controls.Clear();
            this.UpgradePanel.Controls.Clear();
            LogMessage("Form Cleared");
        }

        //----Event Handlers----//
        private void toolTipTick(object? sender, EventArgs e)
        {
            if (myGame.myTip != null) { myGame.myTip.Hide(this); }
            myGame.toolTipTimer.Stop();
            //hide the tooltip and stop the timer. If needed, it will be started again via the hover event.
        }
        private void Btn_Hover(object? sender, EventArgs e)
        {

            myGame.toolTipTimer.Interval = myGame.toolTipVisibleTime;
            myGame.toolTipTimer.Tick += new EventHandler(toolTipTick);

            //get mouse location
            //display some magical floating box that says {Description} multiplies {get-name-from-itemID()}'s salary by {multiplier}
            if (sender == null) { return; }
            if (myGame.myTip != null) { myGame.myTip.Hide(this); }
            myGame.myTip = new ToolTip();

            myGame.myTip.InitialDelay = myGame.toolTipDelay;
            myGame.myTip.IsBalloon = true;
            myGame.myTip.AutoPopDelay = myGame.toolTipVisibleTime;
            myGame.myTip.UseAnimation = true;
            myGame.myTip.UseFading = true; //try it and see?
            UpgradeButton btn = (UpgradeButton)sender;
            //Point mousepos = MousePosition;
            //adjust mousepos for window size and position rather than screen coords
            //a helper method already exists for this...
            Point mousepos = PointToClient(MousePosition);
            mousepos.Offset(-200, -30); //offset the tooltip up 30px and left 100px so the cursor can still click the buttons
            myGame.toolTipTimer.Start();
            if (btn.myUpgrade.itemID >= 1 && btn.myUpgrade.itemID <= myGame.myItems.Length)
            {
                //upgrade refers to an item
                myGame.myTip.Show($"{btn.myUpgrade.Description} multiplies each {myGame.myItems[btn.myUpgrade.itemID - 1].Name}'s salary per second by {btn.myUpgrade.Multiplier}!", Application.OpenForms.OfType<frmMain>().First(), mousepos);
                //"Double-Tap multiplies Wood's salary per second by 3!"
            }
            else if (btn.myUpgrade.itemID == 0)
            {
                //upgrade is a clickamount upgrade
                myGame.myTip.Show($"{btn.myUpgrade.Description} multiplies 'Click-Mining' earnings by {btn.myUpgrade.Multiplier}!", Application.OpenForms.OfType<frmMain>().First(), mousepos);
            }
            else if (btn.myUpgrade.itemID == 15)
            {
                //upgrade is for all items
                myGame.myTip.Show($"{btn.myUpgrade.Description} multiplies all miner salaries by {btn.myUpgrade.Multiplier}!", Application.OpenForms.OfType<frmMain>().First(), mousepos);
            }
            else if (btn.myUpgrade.itemID == 20)
            {
                //upgrade is a prestige point upgrade
                myGame.myTip.Show($"{btn.myUpgrade.Description} adds {((btn.myUpgrade.Multiplier * 100) - 100):N0}% gain per prestige point!", Application.OpenForms.OfType<frmMain>().First(), mousepos);
            }
            else
            {
                //we have no idea what this button does. ItemID not found.
                Debug.WriteLine($"{DateTime.Now}: Invalid UpgradeButton.myUpgrade.itemID encountered in Btn_Hover! Sender: '{btn.Text}', Upgrade: '{btn.myUpgrade.Description}', itemID: {btn.myUpgrade.itemID}");
                return;
            }

        }
        private void timerPerSec_Tick(object sender, EventArgs e)
        {
            //calculate total salary per second
            double tempsal = 0.00d;    //iterate through items owned and calculate total salary per cycle.
            foreach (ItemView view in myGame.myItems)
            {
                tempsal += ((view.mySalary / ((double)view.mySalaryTimeMS / 1000.0d)) * view.myQty);    //if qty is 0, salary increment will be 0.
            }
            myGame.salary = tempsal;

        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            LogMessage("Game closing. Shutting down audio...");
            mciSendString("close clicksound", null, 0, IntPtr.Zero);
            mciSendString("close registersound", null, 0, IntPtr.Zero);
            mciSendString("close registersound2", null, 0, IntPtr.Zero);
            mciSendString("close backgroundmusic01", null, 0, IntPtr.Zero);
            for (int i = 1; i < 8; i++)
            {
                mciSendString($"close pickaxe{i}sound", null, 0, IntPtr.Zero);
            }

            if (!myGame.PrestigeNextRestart)
            {
                LogMessage("Audio shut down. Saving...");
                this.SaveGame();
                LogMessage("Save complete. Closing Game...");
                Program.RestartForPrestige = false;
            }



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
                myGame.floatlabeldeletelist.Add(thislbl);
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
                myGame.myMoney += senderview.mySalary * senderview.myQty;
                myGame.thislifetimeMoney += senderview.mySalary * senderview.myQty;
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
        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized) { myGame.myWindowState = this.WindowState; }
        }

        //----Control Interactions----//
        public void upgradeClicked(Object? sender, EventArgs e)
        {
            if (sender == null) { LogMessage("upgradeClicked(sender) was null"); return; } //basic protection
            else
            {
                UpgradeButton btnsender = (UpgradeButton)sender;
                if (!btnsender.IsEnabled) { return; }
                int btnitemID = btnsender.myUpgrade.itemID;
                if (btnitemID >= 1 && btnitemID <= myGame.myItems.Length)
                {
                    //btnvars itemID is between 1 and the last itemID in myItems, so ItemUpgrade
                    if (myGame.myMoney >= btnsender.myUpgrade.Cost)
                    {
                        myGame.myMoney -= btnsender.myUpgrade.Cost;
                        ItemView tempItem = myGame.myItems.Where(x => x.myID == btnitemID).First<ItemView>();
                        tempItem.mySalary *= btnsender.myUpgrade.Multiplier;

                        btnsender.myUpgrade = Upgrade.SetPurchased(btnsender.myUpgrade);
                        //find the upgrade by upgradeid in mainupgradelist that matches the button's upgradeid, and set it's Purchased property, then overwrite it's old entry in mainupgradelist.
                        Upgrade tempUpgrade = myGame.MainUpgradeList.Find(x => x.upgradeID == btnsender.myUpgrade.upgradeID);
                        myGame.MainUpgradeList[myGame.MainUpgradeList.IndexOf(tempUpgrade)] = Upgrade.SetPurchased(tempUpgrade);
                        PlaySound(SoundList.Register);
                        LogMessage($"Upgrade '{btnsender.myUpgrade.Description}' Purchased");
                    }
                }
                else if (btnitemID == 0)
                {
                    //clickAmount upgrade, may be removed entirely, not sure yet
                    if (myGame.myMoney >= btnsender.myUpgrade.Cost)
                    {
                        myGame.myMoney -= btnsender.myUpgrade.Cost;
                        myGame.clickAmount *= btnsender.myUpgrade.Multiplier;
                        //had to make 'SetPurchased' a static method that returned an object reference. For some reason it wasn't updating the object passed to it before...
                        btnsender.myUpgrade = Upgrade.SetPurchased(btnsender.myUpgrade);
                        Upgrade tempUpgrade = myGame.MainUpgradeList.Find(x => x.upgradeID == btnsender.myUpgrade.upgradeID);
                        myGame.MainUpgradeList[myGame.MainUpgradeList.IndexOf(tempUpgrade)] = Upgrade.SetPurchased(tempUpgrade);
                        PlaySound(SoundList.Register);
                        LogMessage($"Upgrade {btnsender.myUpgrade.Description} Purchased");
                    }
                }
                else if (btnitemID == 15)
                {
                    //All Items Upgrade
                    if (myGame.myMoney >= btnsender.myUpgrade.Cost)
                    {
                        myGame.myMoney -= btnsender.myUpgrade.Cost;
                        foreach (ItemView item in myGame.myItems)
                        {
                            item.mySalary *= btnsender.myUpgrade.Multiplier;
                        }
                        btnsender.myUpgrade = Upgrade.SetPurchased(btnsender.myUpgrade);
                        Upgrade tempUpgrade = myGame.MainUpgradeList.Find(x => x.upgradeID == btnsender.myUpgrade.upgradeID);
                        myGame.MainUpgradeList[myGame.MainUpgradeList.IndexOf(tempUpgrade)] = Upgrade.SetPurchased(tempUpgrade);
                        PlaySound(SoundList.Register);
                        LogMessage($"Upgrade {btnsender.myUpgrade.Description} Purchased");
                    }
                }
                else if (btnitemID == 20)
                {
                    //prestige points upgrade
                    if (myGame.myMoney >= btnsender.myUpgrade.Cost)
                    {
                        double newmult = (btnsender.myUpgrade.Multiplier * 100) - 100;
                        double oldPrestigeMult = myGame.prestigeMultiplier;
                        myGame.prestigeMultiplier += newmult;
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
                        foreach (ItemView item in myGame.myItems)
                        {
                            item.mySalary = (item.mySalary * ((myGame.prestigeMultiplier * myGame.prestigePoints) + 100)) / ((oldPrestigeMult * myGame.prestigePoints) + 100);
                        }
                        myGame.clickAmount = (myGame.clickAmount * ((myGame.prestigeMultiplier * myGame.prestigePoints) + 100)) / ((oldPrestigeMult * myGame.prestigePoints) + 100);

                        btnsender.myUpgrade = Upgrade.SetPurchased(btnsender.myUpgrade);
                        Upgrade tempUpgrade = myGame.MainUpgradeList.Find(x => x.upgradeID == btnsender.myUpgrade.upgradeID);
                        myGame.MainUpgradeList[myGame.MainUpgradeList.IndexOf(tempUpgrade)] = Upgrade.SetPurchased(tempUpgrade);
                        PlaySound(SoundList.Register);
                        LogMessage($"Upgrade {btnsender.myUpgrade.Description} Purchased");
                    }
                }
                UpgradeButtonEnable(btnsender, false);
            }
        }
        public void BuyClicked(ItemView sender)
        {
            if (sender.CanAfford(myGame.myMoney, sender.purchaseAmount, sender.myCostMult))
            {

                myGame.myMoney -= sender.calculatedCost;
                PlaySound(SoundList.Register);
                LogMessage($"{sender.purchaseAmount} of Item {sender.Name} Purchased");
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
                while (sender.myQty >= Game.unlockList[highestunlockindex]);
                highestunlockindex--;   //since we're checking condition after iteration, we need to back down by 1 to get the last valid result. If we're in this routine, we just bought at least 1. HUI is 0 now.
                if (highestunlockindex > sender.latestUnlock)     //0 > -1 == true
                {
                    //we reached a milestone!
                    do
                    {
                        sender.latestUnlock++;        //-1 + 1 = 0;
                        LogMessage($"{sender.Name} Unlock Reached - X{Game.unlockList[sender.latestUnlock]}");
                        if (sender.myQty > 1) { sender.mySalary *= Game.unlockMultiplier; }  //apply the bonus to this item if we bought more than 1, so that we can still have buy1 within buynext, and so we get an unlock for buying 1 of all.
                        bool allOthersHave = true;
                        for (int i = 0; i < myGame.myItems.Length; i++)
                        {
                            if (myGame.myItems[i].myID != sender.myID)   //don't need to check this one again...
                            {
                                if (myGame.myItems[i].myQty < Game.unlockList[sender.latestUnlock])       //if qty < UL[0]==1, then we don't own at least 1 of everything else!
                                {
                                    allOthersHave = false;
                                    break;      //if any one of them fails, exit the loop - it would be computationally wasteful not to.
                                }
                            }
                        }
                        if (allOthersHave)
                        {
                            LogMessage($"Global Unlock Reached - X{Game.unlockList[sender.latestUnlock]}");
                            //double all salaries - global unlock
                            for (int i = 0; i < myGame.myItems.Length; i++)
                            {
                                myGame.myItems[i].mySalary *= Game.unlockMultiplier;
                            }
                            //double clickAmount as well
                            myGame.clickAmount *= 2;
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
            if (myGame.myPurchaseAmount == PurchaseAmount.BuyNext)
            {
                //calculate next
                int temppurchamount;
                if (sender.latestUnlock + 1 < Game.unlockList.Length && sender.latestUnlock + 1 >= 0)     //make sure the unlock we're looking for is in the list...
                {
                    temppurchamount = Game.unlockList[sender.latestUnlock + 1] - sender.myQty;
                }
                else
                {
                    temppurchamount = 1;    //when in doubt, set it to 1. If we've bought all unlocks, we only need to calculate for 1 item purchase.
                }
                sender.calculatedCost = costcalcnew(sender, temppurchamount);
                sender.purchaseAmount = temppurchamount;
            }
            if (sender.mySalaryTimeMS < sender.myTimer.Interval) { ItemView.NormalizeSalary(sender, sender.myTimer.Interval); }
            sender.UpdateLabels();
            sender.ButtonColor(myGame.myMoney, sender.purchaseAmount, sender.myCostMult);
        }
        private void btnPurchAmount_Click(object sender, EventArgs e)
        {
            PlaySound(SoundList.ClickSound);
            
            //purchAmount should be made into an enum. Perfect use-case for it, and reduces possible errors from invalid values.
            if (myGame.myPurchaseAmount == PurchaseAmount.BuyOne)
            {
                myGame.myPurchaseAmount = PurchaseAmount.BuyTen;
                LogMessage($"Purchase Amount Set to 10");
            }
            else if (myGame.myPurchaseAmount == PurchaseAmount.BuyTen)
            {
                myGame.myPurchaseAmount = PurchaseAmount.Buy100;
                LogMessage($"Purchase Amount Set to 100");
            }
            else if (myGame.myPurchaseAmount == PurchaseAmount.Buy100)
            {
                myGame.myPurchaseAmount = PurchaseAmount.BuyNext;
                LogMessage($"Purchase Amount Set to Next");
            }
            else if (myGame.myPurchaseAmount == PurchaseAmount.BuyNext)
            {
                myGame.myPurchaseAmount = PurchaseAmount.BuyMax;
                LogMessage($"Purchase Amount Set to Max");
            }
            else if (myGame.myPurchaseAmount == PurchaseAmount.BuyMax)
            {
                myGame.myPurchaseAmount = PurchaseAmount.BuyOne;
                LogMessage($"Purchase Amount Set to 1");
            }

            if (myGame.myPurchaseAmount == PurchaseAmount.BuyOne || myGame.myPurchaseAmount == PurchaseAmount.BuyTen || myGame.myPurchaseAmount == PurchaseAmount.Buy100)
            {
                for (int i = 0; i < myGame.myItems.Length; i++)
                {
                    //we just need to update the item with the new amount, and then update the labels and colors.
                    myGame.myItems[i].purchaseAmount = (int)myGame.myPurchaseAmount;
                    myGame.myItems[i].CalcCost(myGame.myItems[i].purchaseAmount, myGame.myItems[i].myCostMult);
                    myGame.myItems[i].ButtonColor(myGame.myMoney, myGame.myItems[i].purchaseAmount, myGame.myItems[i].myCostMult);
                    myGame.myItems[i].UpdateLabels();
                }
            }
            else if (myGame.myPurchaseAmount == PurchaseAmount.BuyMax)
            {
                for (int i = 0; i < myGame.myItems.Length; i++)
                {
                    int temppurchamount = (int)calcMax(myGame.myMoney, myGame.myItems[i]);
                    //myItems[i].CalcCost(temppurchamount, myItems[i].myCostMult);
                    myGame.myItems[i].calculatedCost = costcalcnew(myGame.myItems[i], temppurchamount);
                    myGame.myItems[i].purchaseAmount = temppurchamount;
                    myGame.myItems[i].ButtonColor(myGame.myMoney, myGame.myItems[i].purchaseAmount, myGame.myItems[i].myCostMult);
                    myGame.myItems[i].UpdateLabels();
                }
            }
            else if (myGame.myPurchaseAmount == PurchaseAmount.BuyNext)
            {
                for (int i = 0; i < myGame.myItems.Length; i++)
                {
                    int temppurchamount;
                    if (myGame.myItems[i].latestUnlock + 1 < Game.unlockList.Length && myGame.myItems[i].latestUnlock + 1 >= 0)     //make sure the unlock we're looking for is in the list...
                    {
                        temppurchamount = Game.unlockList[myGame.myItems[i].latestUnlock + 1] - myGame.myItems[i].myQty;
                    }
                    else
                    {
                        temppurchamount = 1;    //when in doubt, set it to 1. If we've bought all unlocks, we only need to calculate for 1 item purchase.
                    }
                    myGame.myItems[i].calculatedCost = costcalcnew(myGame.myItems[i], temppurchamount);
                    myGame.myItems[i].purchaseAmount = temppurchamount;
                    myGame.myItems[i].ButtonColor(myGame.myMoney, myGame.myItems[i].purchaseAmount, myGame.myItems[i].myCostMult);
                    myGame.myItems[i].UpdateLabels();
                }
            }
        }
        private void btnMine_Click(object sender, EventArgs e)
        {
            const double matThreshval = 50.0d;  //allows incrperclick to change at different values.
            if (myGame.matsMined != 0 && myGame.matsMined + myGame.incrperclick >= (Math.Round((double)myGame.matsMined / matThreshval, MidpointRounding.ToPositiveInfinity) * matThreshval) && myGame.incrperclick < 10 && myGame.matsMined != (Math.Round((double)myGame.matsMined / matThreshval, MidpointRounding.ToPositiveInfinity) * matThreshval))
            {
                myGame.matsMined += myGame.incrperclick;
                myGame.matsMinedLifetime += myGame.incrperclick;
                myGame.incrperclick++;
                //clickAmount = (clickAmount / (incrperclick - 1)) * incrperclick;

            } //for now, when matsMined reaches another multiple of 100, amountperclick is incremented.
            //incrperclick has a max of 10 for now.
            else { myGame.matsMined += myGame.incrperclick; myGame.matsMinedLifetime += myGame.incrperclick; }

            //We're only calculating clickAmount for one incrperclick. We can add that amount for multiple clicks, but it shouldn't double clickAmount.
            //Later we can use visual and audible cues to signify multiple clicks (like floating text saying 'x2' 'x3' etc, and disappearing after a second, with little 'pop' sounds for each incrperclick in quick succession...)
            myGame.myMoney += myGame.clickAmount * myGame.incrperclick;
            myGame.thislifetimeMoney += myGame.clickAmount * myGame.incrperclick;

            Random randnumber = new Random();
            int i = randnumber.Next(1, 8);
            PlaySound(SoundList.Pickaxe);

            //testing...
            FloatText();
        }
        private void btnPrestige_Click(object sender, EventArgs e)
        {
            PlaySound(SoundList.ClickSound);

            //In order for this to work, I need to refactor the main game logic into it's own gameobject that takes parameters for prestige amount, and default params(overrideable) for money, upgrades, purchased items, etc.
            //Or I can take advantage of the load/save system, and just configure the save to reset for a prestige-flagged restart, that way i can customize what parameters get changed.   Edit: Why not both?
            double tempprestige = calcPrestige(myGame.lastlifetimeMoney, myGame.thislifetimeMoney);
            foreach (ItemView item in myGame.myItems)
            {
                item.myTimer.Stop();
            }
            this.timerPerSec.Stop();
            this.timerVisualUpdate.Stop();
            myGame.GameClock.Stop();
            myGame.thislifeGameTime += myGame.GameClock.Elapsed;
            myGame.totalGameTime += myGame.GameClock.Elapsed;

            DialogResult dres = MessageBox.Show($"Current Prestige: {Stringify(myGame.prestigePoints.ToString("R"), StringifyOptions.LongText)}. \nPrestige to Gain: {Stringify(tempprestige.ToString("R"), StringifyOptions.LongText)}. Prestige?", "Reset to earn prestige?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dres == DialogResult.Yes)
            {
                this.timerPerSec.Stop();
                this.timerVisualUpdate.Stop();
                myGame.toolTipTimer.Stop();
                myGame.GameClock.Stop();
                foreach (ItemView item in myGame.myItems)
                {
                    item.myTimer.Stop();
                    item.myprogressvalue = item.progressMining.Value;
                }

                Program.RestartForPrestige = true;
                myGame.PrestigeNextRestart = true;

                myGame.prestigeGainedNextRestart = tempprestige;
                LogMessage($"{tempprestige:N0} Prestige Points Added. Saving...");
                SaveGame(myGame.lastSaveLocation);
                LogMessage($"Save Complete. Loading...");
                LoadGame(myGame.lastSaveLocation);
            }
            else if (dres == DialogResult.No)
            {
                foreach (ItemView item in myGame.myItems)
                {
                    if (item.progressMining.Enabled)
                    {
                        item.myTimer.Start();
                    }

                }
                timerPerSec.Start();
                timerVisualUpdate.Start();
                myGame.GameClock.Reset();
                myGame.GameClock.Start();
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
        public void btnStats_Click(object sender, EventArgs e)
        {
            PlaySound(SoundList.ClickSound);
            //open stats window with current stats, update times and restart gameclock

            myGame.thislifeGameTime += myGame.GameClock.Elapsed;
            myGame.totalGameTime += myGame.GameClock.Elapsed;
            myGame.GameClock.Reset();
            myGame.GameClock.Start();

            DialogResult result = MessageBox.Show(
                $"Salary: ${(myGame.salary > 1000000.0d ? Stringify(myGame.salary.ToString("R"), StringifyOptions.LongText) : myGame.salary.ToString("N"))} Per Second" +
                $"\nClickAmount: ${(myGame.clickAmount > 1000000.0d ? Stringify(myGame.clickAmount.ToString("R"), StringifyOptions.LongText) : myGame.clickAmount.ToString("N"))} Per Click" +
                $"\nMoney Earned This Lifetime: ${(myGame.thislifetimeMoney > 1000000.0d ? Stringify(myGame.thislifetimeMoney.ToString("R"), StringifyOptions.LongText) : myGame.thislifetimeMoney.ToString("N"))}" +
                $"\nMoney Earned Last Lifetime: ${(myGame.lastlifetimeMoney > 1000000.0d ? Stringify(myGame.lastlifetimeMoney.ToString("R"), StringifyOptions.LongText) : myGame.lastlifetimeMoney.ToString("N"))}" +
                $"\nMoney Earned All Lifetimes: ${(myGame.lastlifetimeMoney + myGame.thislifetimeMoney > 1000000.0d ? Stringify((myGame.lastlifetimeMoney + myGame.thislifetimeMoney).ToString("R"), StringifyOptions.LongText) : (myGame.thislifetimeMoney + myGame.lastlifetimeMoney).ToString("N"))}" +
                $"\nTime spent this lifetime: {myGame.thislifeGameTime.ToString(@"h\:mm\:ss")}" +
                $"\nTime spent all lifetimes: {myGame.totalGameTime.ToString(@"h\:mm\:ss")}" +
                $"\nPrestige Points: {Stringify(myGame.prestigePoints.ToString("R"), StringifyOptions.LongText)}" +
                $"\nPrestige Multiplier: {Stringify(myGame.prestigeMultiplier.ToString("R"), StringifyOptions.LongText)}% Per Point" +
                $"\nPrestige Percentage: {(myGame.prestigePoints * myGame.prestigeMultiplier > 1000000.0d ? Stringify((myGame.prestigePoints * myGame.prestigeMultiplier).ToString("R"), StringifyOptions.LongText) : (myGame.prestigePoints * myGame.prestigeMultiplier).ToString("N0"))} %" +
                $"\nMaterials Mined this lifetime: {(myGame.matsMined >= 1000000.0d ? Stringify(myGame.matsMined.ToString("R"), StringifyOptions.LongText) : myGame.matsMined.ToString("N0"))}" +
                $"\nMaterials Mined all lifetimes: {(myGame.matsMinedLifetime >= 1000000.0d ? Stringify(myGame.matsMinedLifetime.ToString("R"), StringifyOptions.LongText) : myGame.matsMinedLifetime.ToString("N0"))}"
                , "MoneyMiner Statistics", MessageBoxButtons.OK
                );

        }
        private void btnMine_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender == null) { return; }
            Button thisbutton = (Button)sender;
            if (thisbutton != null && thisbutton.BackgroundImage != null)
            {
                thisbutton.BackgroundImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                // Handle key at form level.
                btnPause_Click(this, EventArgs.Empty);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
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
            lblNew.Name = $"lblFloat{myGame.floatlabels.Count + 1}";
            lblNew.Text = $"${(myGame.clickAmount * myGame.incrperclick >= 1000000.0d ? Stringify((myGame.clickAmount * myGame.incrperclick).ToString()) : double.Round(myGame.clickAmount * myGame.incrperclick, 2).ToString("N"))}";
            lblNew.ForeColor = Color.FromArgb(255, Colors.colTextPrimary);
            lblNew.BackColor = Color.FromArgb(0, Colors.colTextSecondary);
            lblNew.Visible = true;
            lblNew.Enabled = true;
            lblNew.Show();
            myGame.floatlabels.Add(lblNew);
        }
        private void btnPause_Click(object sender, EventArgs e)
        {
            PlaySound(SoundList.ClickSound);
            PauseMenu pauseMenu = new PauseMenu(this as frmMain, myGame);

            pauseMenu.ShowDialog();
        }
        public void ShowAbout()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"MoneyMiner {Application.ProductVersion.Split("+")[0]}");
            sb.AppendLine($"\nCreated By Tim Earley");
            sb.AppendLine($"\nLocation: '{Environment.CurrentDirectory}'\n");
            TextReader treader = File.OpenText($@"{Environment.CurrentDirectory}\Resources\ResourceAttributions.txt");
            string filecontents = treader.ReadToEnd();
            string[] textlines = filecontents.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            sb.AppendLine($"\nResource Attributions:\n");
            foreach (string line in textlines)
            {
                if (line != "")
                {
                    if (!line.Contains("http"))
                    {
                        sb.AppendLine($"{line}");
                    }
                }
            }
            sb.AppendLine($"\nThanks for playing MoneyMiner!");
            sb.AppendLine($"\n© 2024 - All Rights Reserved");
            MessageBox.Show(sb.ToString(), "About MoneyMiner", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //for testing only...
            About aboutme = new About();
            aboutme.Show();
        }
        public static void btnBuy_Hover(object sender, EventArgs e)
        {
            Button btnbuy;
            ItemView? btnItem;
            try
            {
                btnbuy = (Button)sender;
                btnItem = btnbuy.Parent == null ? null : btnbuy.Parent as ItemView;
            }
            catch (Exception ex)
            {
                if (ex is NullReferenceException) { MessageBox.Show(ex.Message, "NullReferenceException in btnBuy_Hover"); }
                else { MessageBox.Show(ex.Message, "Unknown Exception in btnBuy_Hover"); }
                return;
            }
            if (btnItem == null) { return; }
            ToolTip salaryGainTip = new ToolTip();
            double saltogain = btnItem.displaySalPerSec ? (btnItem.mySalary * btnItem.purchaseAmount) / (btnItem.mySalaryTimeMS / 1000.0d) : btnItem.mySalary * btnItem.purchaseAmount;
            string strGain = btnItem.displaySalPerSec ? $"Gain ${(saltogain >= 1000000.0d ? Stringify(saltogain.ToString("R")) : double.Round(saltogain, 2).ToString("N"))} per second for {btnItem.Name}!" :
                $"Gain ${(saltogain >= 1000000.0d ? Stringify(saltogain.ToString("R")) : double.Round(saltogain, 2).ToString("N"))} per cycle for {btnItem.Name}!";
            salaryGainTip.SetToolTip(btnbuy, strGain);
            //salaryGainTip.Show(strGain, Application.OpenForms.OfType<frmMain>().First());
        }
        public void DeleteForever()
        {
            File.Delete(myGame.lastSaveLocation);
            LogMessage($"Save Deleted! Starting New Game...");
            myGame = new Game();
            ClearFormItems();
            InitControls();
            LoadItemControlsToForm();
            LoadUpgradeControlsToForm();
            frmMain_Load(new object(), EventArgs.Empty);
        }
        public void EnableAutosave(bool autosave)
        {
            LogMessage($"Autosave {(autosave ? "Enabled" : "Disabled")}");
            myGame.AutosaveEnabled = autosave;

            if (myGame.AutosaveEnabled && myGame.AutosaveInterval != 0)
            {
                myGame.AutosaveTimer.Tick -= Autosave;
                myGame.AutosaveTimer.Tick += Autosave;
                myGame.AutosaveTimer.Interval = myGame.AutosaveInterval * 60000;
                myGame.AutosaveTimer.Start();
            }
            else
            {
                myGame.AutosaveTimer.Stop();
            }
        }
        public void SetAutosaveInterval(int Interval)
        {
            myGame.AutosaveInterval = Interval;
            if (myGame.AutosaveEnabled && myGame.AutosaveInterval != 0) { myGame.AutosaveTimer.Interval = myGame.AutosaveInterval * 60000; myGame.AutosaveTimer.Start(); }
            else { myGame.AutosaveTimer.Stop(); }
        }
        private void btnQuickBuy_Click(object sender, EventArgs e)
        {
            //when this is enabled and clicked, starting with the lowest cost, buy all upgrades we can afford.
            double currentBalance = myGame.myMoney;
            int upgradeIndex = 0;
            while (currentBalance >= 0.0d && upgradeIndex < myGame.MainUpgradeList.Count)
            {
                //if balance dips below 0 or we've gone through all upgrades, do not enter this loop.
                currentBalance -= myGame.MainUpgradeList[upgradeIndex].Cost;
                upgradeIndex++;
            }
            if (currentBalance < 0) { upgradeIndex--; }
            if (upgradeIndex < 0)
            {
                //we can't buy any upgrades - we shouldn't enter here at all, but you never know...
            }
            for (int i = 0; i <= upgradeIndex; i++)
            {
                //buy upgradelist[i] using existing handler
                upgradeClicked(UpgradePanel.Controls[i], EventArgs.Empty);
            }
            frmMain_UpdateLabels(); //just to ensure the view is updated before user can click something again
        }

        //----Calculations----//
        /// <summary>
        /// Returns calculated amount of prestige points if prestiged right now.
        /// </summary>
        /// <param name="lastlifeMoney">Money made in the last prestige loop</param>
        /// <param name="thislifeMoney">Money made this prestige loop</param>
        /// <param name="prestigeModifier">Modifier for prestige difficulty. Default is 4B, but can be overridden.</param>
        /// <returns>double representing calculated prestige points to gain.</returns>
        public static double calcPrestige(double lastlifeMoney, double thislifeMoney, double prestigeModifier = 4000000000.0d)
        {
            double newlifeMoney = lastlifeMoney + thislifeMoney;
            double term1 = Math.Pow(newlifeMoney / (prestigeModifier / 9), 0.5d);
            double term2 = Math.Pow(lastlifeMoney / (prestigeModifier / 9), 0.5d);
            return term1 - term2 >= 0 ? double.Round(term1 - term2, MidpointRounding.ToZero) : 0;
        }
        public static long calcMax(double balance, ItemView item)
        {
            long maxbuy = (long)Math.Floor((Math.Log(((balance * (item.myCostMult - 1)) / (item.baseCost * Math.Pow(item.myCostMult, item.myQty))) + 1) / Math.Log(item.myCostMult)));
            return maxbuy;
        }
        public static double costcalcnew(ItemView item, int purchqty)
        {
            return ((item.baseCost) * (((Math.Pow(item.myCostMult, item.myQty) * (Math.Pow(item.myCostMult, purchqty) - 1)) / (item.myCostMult - 1))));

        }
        public static int progressCompletedIterations(TimeSpan elapsed, ItemView item, out int remainingMS)
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
        public static Game SinceYouveBeenGone(Game mygame)
        {
            if (mygame.sinceLastSave.TotalSeconds > 1.0d)
            {
                //myMoney += salary * sincelastsave.TotalSeconds; 
                //thislifetimeMoney += salary * sincelastsave.TotalSeconds;

                double salearned = 0.0d;
                for (int i = 0; i < mygame.myItems.Length; i++)
                {
                    int thisremainingMS;
                    salearned += (progressCompletedIterations(mygame.sinceLastSave, mygame.myItems[i], out thisremainingMS) * mygame.myItems[i].mySalary * mygame.myItems[i].myQty);
                    mygame.myItems[i].myprogressvalue = thisremainingMS;
                }
                mygame.myMoney += salearned;
                mygame.thislifetimeMoney += salearned;

                if (mygame.MySaveType != SaveType.Prestigesave && mygame.MySaveType != SaveType.NewGame)
                {
                    MessageBox.Show($"Welcome Back!\nYou were gone for {mygame.sinceLastSave.TotalHours:N0} hours, {mygame.sinceLastSave.Minutes:N0} minutes, and {mygame.sinceLastSave.Seconds:N0} seconds.\nYou made ${((salearned) >= 1000000.0d ? Stringify((salearned).ToString("R"), StringifyOptions.LongText) : (salearned).ToString("N"))} while you were gone!", "Since you've been gone...", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, false);
                }
            }
            return mygame;
        }
        internal static Game ApplyNewPrestige(Game mygame)
        {
            //items and upgrades need to be reset to default
            //clickamount reset to default
            //calculate and apply clickamount and item salaries per new prestigePoints (old + gained)
            //update 'lifetime' variables
            //copy over other persistent variables
            //show popup window/msgbox as modal window

            //*Apply changes to current mygame object, then allow game to continue initialization. Next save will store new info from myGame.

            //Create default game object to initialize items, upgrades, clickamount, etc
            Game tempGame = new Game();
            //copy over relevant vars
            tempGame.FXEnabled = mygame.FXEnabled;
            tempGame.FXVolume = mygame.FXVolume;
            tempGame.MusicEnabled = mygame.MusicEnabled;
            tempGame.MusicVolume = mygame.MusicVolume;
            tempGame.lastlifetimeMoney = mygame.thislifetimeMoney + mygame.lastlifetimeMoney;
            tempGame.matsMinedLifetime = mygame.matsMinedLifetime + mygame.matsMined;
            tempGame.myPurchaseAmount = mygame.myPurchaseAmount;
            tempGame.myWindowState = mygame.myWindowState;
            tempGame.prestigePoints = mygame.prestigePoints + mygame.prestigeGainedNextRestart;
            tempGame.sinceLastSave = mygame.sinceLastSave;  //not sure if we need this yet...
            tempGame.toolTipDelay = mygame.toolTipDelay;
            tempGame.toolTipVisibleTime = mygame.toolTipVisibleTime;
            tempGame.totalGameTime = mygame.totalGameTime;

            //calculate and update clickamount and item salaries
            tempGame.clickAmount *= ((tempGame.prestigePoints / (100.0d / tempGame.prestigeMultiplier)) + 1);
            foreach (var item in tempGame.myItems)
            {
                item.mySalary *= ((tempGame.prestigePoints / (100.0d / tempGame.prestigeMultiplier)) + 1);
            }
            MessageBox.Show($"You gained {(mygame.prestigeGainedNextRestart >= 1000000.0d ? Stringify(mygame.prestigeGainedNextRestart.ToString("R"), StringifyOptions.LongText) : mygame.prestigeGainedNextRestart.ToString("N"))} prestige points!", "Congratulations!");
            return tempGame;
        }

        //----Visual Updates----//
        public void frmMain_UpdateLabels()
        {
            //set main form's labels as needed
            lblMoney.Text = $"Money: ${(myGame.myMoney >= 1000000.0d ? Stringify(myGame.myMoney.ToString("R"), StringifyOptions.LongText) : double.Round(myGame.myMoney, 2).ToString("N"))}";
            lblSalary.Text = $"Salary: ${(myGame.salary >= 1000000.0d ? Stringify(myGame.salary.ToString("R"), StringifyOptions.LongText) : double.Round(myGame.salary, 2).ToString("N"))} Per Second";
            lblClickAmount.Text = $"Mining: ${(myGame.clickAmount >= 1000000.0d ? Stringify(myGame.clickAmount.ToString("R"), StringifyOptions.LongText) : double.Round(myGame.clickAmount, 2).ToString("N"))} Per Click";
            lblIncrPerClick.Text = $"Mined Per Click: {myGame.incrperclick:N0}";
            lblMatsMined.Text = $"Materials Mined: {myGame.matsMined:N0}";
            if (myGame.myPurchaseAmount == PurchaseAmount.BuyOne || myGame.myPurchaseAmount == PurchaseAmount.BuyTen || myGame.myPurchaseAmount == PurchaseAmount.Buy100)
            {
                btnPurchAmount.Text = $"Buy: x{(int)myGame.myPurchaseAmount}";
            }
            else if (myGame.myPurchaseAmount == PurchaseAmount.BuyMax)
            {
                btnPurchAmount.Text = "Buy: Max";
            }
            else if (myGame.myPurchaseAmount == PurchaseAmount.BuyNext)
            {
                btnPurchAmount.Text = "Buy: Next";
            }
            bool CanBuyUpgrade = false;
            foreach (UpgradeButton btn in myGame.upgradeButtons)
            {
                if (btn.myUpgrade.Purchased)
                {
                    UpgradeButtonEnable(btn, false);
                }
                //if we can afford it and haven't bought it, enable it and turn it green, if not, disable it and turn it gray. real simple.
                else if (!(btn.myUpgrade.Purchased))
                {
                    //if not purchased
                    if (myGame.myMoney >= btn.myUpgrade.Cost)
                    {
                        //can afford
                        UpgradeButtonEnable(btn, true);
                        CanBuyUpgrade = true;
                    }
                    else
                    {
                        //can't afford
                        UpgradeButtonEnable(btn, false);
                    }
                }
            }
            //if any upgrades flagged CanBuyUpgrade, enable btnQuickBuy; otherwise, disable.
            if (CanBuyUpgrade) 
            {
                btnQuickBuy.BackColor = Colors.colButtonEnabled;
                btnQuickBuy.ForeColor = Colors.colUpgradeTextEnabled;
                btnQuickBuy.Enabled = true; 
            }
            else
            {
                btnQuickBuy.BackColor = Colors.colButtonDisabled;
                btnQuickBuy.ForeColor = Colors.colUpgradeTextDisabled;
                btnQuickBuy.Enabled = false;
            }
        }
        private void timerVisualUpdate_Tick(object sender, EventArgs e)
        {
            foreach (ItemView item in myGame.myItems)
            {
                if (myGame.myPurchaseAmount == PurchaseAmount.BuyMax) { item.purchaseAmount = (int)calcMax(myGame.myMoney, item); }  //only used for max calculation updates
                item.ButtonColor(myGame.myMoney, item.purchaseAmount, item.myCostMult);
                if (item.calculatedCost == 0.00d) { item.calculatedCost = costcalcnew(item, 1); }   //prevent showing $0 in item cost field
                item.UpdateLabels();
            }
            //TODO: Add upgrade labelupdates and buttoncolor updates
            frmMain_UpdateLabels();
            foreach (Label lbl in myGame.floatlabels)
            {
                lbl.Location = new Point(lbl.Location.X, lbl.Location.Y - 40);
            }
            foreach (Label lbl in myGame.floatlabeldeletelist)
            {
                if (myGame.floatlabels.Contains(lbl))
                {
                    myGame.floatlabels.Remove(lbl);
                    lbl.Dispose();
                }
            }
            myGame.floatlabeldeletelist.Clear();
        }
        public void ToggleItemSalaryDisplays()
        {
            //Play click sound
            if (myGame.FXEnabled)
            {
                mciSendString("seek clicksound to start", null, 0, IntPtr.Zero);
                mciSendString("play clicksound", null, 0, IntPtr.Zero);
            }
            //Toggle all item displays
            foreach (ItemView item in myGame.myItems)
            {
                item.displaySalPerSec = !item.displaySalPerSec;
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
        internal static void UpgradeButtonEnable(UpgradeButton btn, bool enabled)
        {
            if (enabled)    //enabled, we can purchase it
            {
                btn.BackColor = Colors.colButtonEnabled;
                btn.ForeColor = Colors.colUpgradeTextEnabled;
                btn.FlatStyle = FlatStyle.Popup;
                btn.IsEnabled = true;
            }
            else
            {
                if (!btn.myUpgrade.Purchased)   //disabled and we don't own it
                {
                    btn.BackColor = Colors.colButtonDisabled;
                    btn.ForeColor = Colors.colUpgradeTextDisabled;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.IsEnabled = false;
                }
                else    //disabled and we do own it
                {
                    btn.BackColor = Colors.colButtonPurchased;
                    btn.ForeColor = Colors.colUpgradeTextPurchased;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.Text = $"{btn.myUpgrade.Description}\nPurchased!";
                    btn.IsEnabled = false;
                }
            }
        }

        //----String Methods----//
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
                        myOutput = trimmedinput + Game.TextStrings[wordindex];
                        return myOutput;
                    }
                case (StringifyOptions.SecondsToHourMinSec):
                    {
                        //input should be a number representing an amount of seconds, as a double. eg.'13528.6'. 
                        //calibrated for at least 999999 seconds.
                        //Note: Exception handling not in place yet!
                        int houraccum = 0;
                        int minaccum = 0;
                        double parsedinput = double.Parse(input);
                        while (parsedinput >= 3600.0d)
                        {
                            parsedinput -= 3600.0d;
                            houraccum++;
                        }
                        while (parsedinput >= 60.0d)
                        {
                            parsedinput -= 60.0d;
                            minaccum++;
                        }
                        return $"{houraccum.ToString("#00")}:{minaccum.ToString("00")}:{parsedinput.ToString("00.0")}";
                    }
                case (StringifyOptions.SecondsToMinSec):
                    {
                        //Calibrated for at least 999999 seconds.
                        int minaccum = 0;
                        double parsedinput = double.Parse(input);
                        while (parsedinput >= 60.0d)
                        {
                            parsedinput -= 60.0d;
                            minaccum++;
                        }
                        return $"{minaccum.ToString("###00")}:{parsedinput.ToString("00.0")}";
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

        //----Save/Load----//
        public void SaveGame()
        {
            GameState save = new GameState(myGame);

            //if prestige was earned, set flags to ensure recalculation of salaries.
            if (myGame.PrestigeNextRestart)
            {
                save.PrestigeSaveFlag = true;
                save.saveType = SaveType.Prestigesave;
            }
            else
            {
                save.PrestigeSaveFlag = false;
                save.saveType = SaveType.Exitsave;
            }

            save.lastsavetimestamp = DateTime.Now;
            save.SaveLocation = save.SaveLocation == "" ? Environment.CurrentDirectory + @"\GameState.mmf" : save.SaveLocation;
            FileStream fstream = new FileStream(save.SaveLocation, FileMode.Create);

            //serialize and write to disk
#pragma warning disable SYSLIB0011 // Type or member is obsolete
            BinaryFormatter bformatter = new BinaryFormatter();
#pragma warning restore SYSLIB0011 // Type or member is obsolete
            bformatter.Serialize(fstream, save);
            fstream.Close();
        }
        public void SaveGame(string saveLocation, bool IsAutosave = false)
        {
            GameState save = new GameState(myGame);
            save.saveType = myGame.PrestigeNextRestart ? SaveType.Prestigesave : SaveType.Manualsave;
            if (IsAutosave) { save.saveType = SaveType.Autosave; }
            save.lastsavetimestamp = DateTime.Now;
            save.SaveLocation = saveLocation;
            myGame.lastSaveLocation = saveLocation;
            FileStream fstream = new FileStream(saveLocation, FileMode.Create);

            //serialize and write to disk
#pragma warning disable SYSLIB0011 // Type or member is obsolete
            BinaryFormatter bformatter = new BinaryFormatter();
#pragma warning restore SYSLIB0011 // Type or member is obsolete
            bformatter.Serialize(fstream, save);
            fstream.Close();
        }
        public GameState? LoadGame()
        {
            FileStreamOptions fsOptions = new FileStreamOptions();
            GameState save;
            fsOptions.Mode = FileMode.Open;
            try
            {
                //create self-disposing stream using default file (can we have this look for any *.mmf file in currentdir? What if there are multiple?)
                using FileStream fstream = new FileStream(Environment.CurrentDirectory + @"\GameState.mmf", fsOptions);
                //create formatter
#pragma warning disable SYSLIB0011 // Type or member is obsolete
                BinaryFormatter bformatter = new BinaryFormatter();
#pragma warning restore SYSLIB0011 // Type or member is obsolete

                save = (GameState)bformatter.Deserialize(fstream);

                fstream.Close();

                TimeSpan sincelastsave = DateTime.Now.Subtract(save.lastsavetimestamp);
                save.thislifegametime = save.thislifegametime.Add(sincelastsave);
                save.totalgametime = save.totalgametime.Add(sincelastsave);
                save.SaveLocation = Environment.CurrentDirectory + @"\GameState.mmf";
                return save;
            }
            //If there's a problem or we can't find the file, return null, and the constructor will initialize to default. Next time
            //the game is closed it will save a new file, overwriting the corrupted one if it exists.
            catch (FileNotFoundException noFileEx)
            {
                LogMessage($"Save file not found at {Environment.CurrentDirectory + @"\GameState.mmf"}, initializing default new game...");
                LogMessage($"Exception message: {noFileEx.Message}");
                return null;
            }
            catch (SerializationException binaryEx)
            {
                LogMessage($"Incompatible save - {binaryEx.Message}, initializing defaults");
                return null;
            }
            catch (Exception ex)
            {
                LogMessage($"Unhandled exception occurred during LoadGame() - {ex.Message}, initializing defaults");
                return null;
            }
        }
        public void LoadGame(string loadLocation)
        {
            FileStreamOptions fsOptions = new FileStreamOptions();
            GameState? save;
            try
            {
                fsOptions.Mode = FileMode.Open;
                using FileStream fstream = new FileStream(loadLocation, fsOptions);
                //read from disk and deserialize
#pragma warning disable SYSLIB0011 // Type or member is obsolete
                BinaryFormatter bformatter = new BinaryFormatter();
#pragma warning restore SYSLIB0011 // Type or member is obsolete
                save = (GameState)bformatter.Deserialize(fstream);
                fstream.Close();

                if (save != null && (save.saveType == SaveType.Manualsave || save.saveType == SaveType.Exitsave || save.saveType == SaveType.Autosave))
                {
                    TimeSpan sincelastsave = DateTime.Now.Subtract(save.lastsavetimestamp);
                    save.thislifegametime.Add(sincelastsave);
                    save.totalgametime.Add(sincelastsave);
                    myGame = new Game(save);
                    myGame.lastSaveLocation = loadLocation;
                    ClearFormItems();
                    InitAudio();
                    InitControls();
                    LoadItemControlsToForm();
                    LoadUpgradeControlsToForm();
                    frmMain_Load(new object(), EventArgs.Empty);
                }
                else if (save != null && save.saveType == SaveType.Prestigesave)
                {
                    myGame = ApplyNewPrestige(new Game(save));
                    myGame.lastSaveLocation = loadLocation;
                    ClearFormItems();
                    InitAudio();
                    InitControls();
                    LoadItemControlsToForm();
                    LoadUpgradeControlsToForm();
                    frmMain_Load(new object(), EventArgs.Empty);
                }
                else
                {
                    //save==null
                    throw new NotSupportedException();
                }
            }
            catch (NotSupportedException ex)
            {
                LogMessage($"Invalid file loaded from {loadLocation} - {ex.Message}");
                this.WindowState = FormWindowState.Maximized;
                return;
            }
            catch (SerializationException ex)
            {
                LogMessage($"Serialization error in LoadGame({loadLocation}) - {ex.Message} (Save from pre-1101 build?)");
                this.WindowState = FormWindowState.Maximized;
                return;
            }
            catch (Exception ex)
            {
                LogMessage($"Unhandled exception in LoadGame({loadLocation}) - {ex.Message}");
                this.WindowState = FormWindowState.Maximized;
                return;
            }
        }
        public string GetSaveLocation()
        {
            return myGame.lastSaveLocation;
        }
        public void Autosave(object? sender, EventArgs e)
        {
            LogMessage($"Autosaving...");
            SaveGame(myGame.lastSaveLocation, true);
            LogMessage($"Autosave complete!");
            //need some way to show it autosaved...
            PlaySound(SoundList.Ping);
        }
        /// <summary>
        /// Creates a new log file using subsequent naming, and stores the file name in myGame.CurrentLogFile. Call this as early as you can.
        /// </summary>
        public void CreateLog()
        {
            //check currentdir/logs for *.txt files containing 'GameLog' and get a count of them.
            //create new text file (filestream) called GameLog{existinglogcount + 1}.txt
            //store name of created file in myGame.CurrentLogFile
            if (!Path.Exists(Environment.CurrentDirectory + @"\Logs\"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\Logs\");
            }
            string[] allfilenames = Directory.EnumerateFiles(Environment.CurrentDirectory + @"\Logs\").ToArray<string>();
            int logfilecount = allfilenames.Where(x => (x.Contains("GameLog") && x.Contains(".txt"))).Count();
            StreamWriter logstream = File.CreateText(Environment.CurrentDirectory + $@"\Logs\GameLog{logfilecount + 1}.txt");
            myGame.CurrentLogFile = Environment.CurrentDirectory + $@"\Logs\GameLog{logfilecount + 1}.txt";
            logstream.Close();
        }
        /// <summary>
        /// Add a line to the currently referenced myGame.CurrentLogFile. Automatically includes DateTime.Now - no need to include that in 'message' parameter.
        /// </summary>
        /// <param name="message">The message to add</param>
        public void LogMessage(string message)
        {
            //open filestream to myGame.CurrentLogFile
            //set filestream position to end of file
            //writeline(DateTime.Now.ToString() + ": " + message)
            //close filestream
            File.AppendAllLines(myGame.CurrentLogFile, new string[]{ DateTime.Now + ": " + message});
            //would love to use Async, but need a way to keep them in order and avoid race conditions. Use sync for now.
        }

        //----Audio Methods----//
        public void SetAudioVolume(int musicVol, int fxVol)
        {
            myGame.MusicVolume = musicVol;
            myGame.FXVolume = fxVol;
            if (musicVol < 0) { musicVol = 0; } else if (musicVol > 1000) { musicVol = 1000; }
            if (fxVol < 0) { fxVol = 0; } else if (fxVol > 1000) { fxVol = 1000; }
            mciSendString($"setaudio backgroundmusic01 volume to {musicVol}", null, 0, IntPtr.Zero);
            mciSendString($"setaudio registersound volume to {fxVol}", null, 0, IntPtr.Zero);
            mciSendString($"setaudio registersound2 volume to {fxVol}", null, 0, IntPtr.Zero);
            mciSendString($"setaudio clicksound volume to {fxVol}", null, 0, IntPtr.Zero);
            mciSendString($"setaudio pingsound volume to {fxVol}", null, 0, IntPtr.Zero);
            for (int i = 1; i <= 8; i++)
            {
                mciSendString($"setaudio pickaxe{i}sound volume to {fxVol}", null, 0, IntPtr.Zero);
            }
        }
        public void ToggleMusic(bool enabled)
        {
            if (enabled)
            {
                if (!myGame.MusicEnabled)
                {
                    //if it wasn't already enabled, then seek to start and play.
                    mciSendString($"seek backgroundmusic01 to start", null, 0, IntPtr.Zero);
                    mciSendString($"play backgroundmusic01 repeat", null, 0, IntPtr.Zero);
                    myGame.MusicEnabled = true;
                }
            }
            else
            {
                if (myGame.MusicEnabled)
                {
                    //if it was enabled, then stop it
                    mciSendString($"stop backgroundmusic01", null, 0, IntPtr.Zero);
                    myGame.MusicEnabled = false;
                }
            }
            LogMessage($"Background Music {(enabled ? "Enabled" : "Disabled")}");
        }
        public void ToggleFX(bool enabled)
        {
            if (enabled)
            {
                if (!myGame.FXEnabled)
                {
                    //if it wasn't already enabled, then set each to on
                    //pickaxe{i}sound   1-8
                    //registersound
                    //registersound2
                    //clicksound
                    mciSendString($"setaudio clicksound on", null, 0, IntPtr.Zero);
                    mciSendString($"setaudio registersound on", null, 0, IntPtr.Zero);
                    mciSendString($"setaudio registersound2 on", null, 0, IntPtr.Zero);
                    mciSendString("setaudio pingsound on", null, 0, IntPtr.Zero);
                    for (int i = 1; i <= 8; i++)
                    {
                        mciSendString($"setaudio pickaxe{i}sound on", null, 0, IntPtr.Zero);
                    }
                    myGame.FXEnabled = true;
                }
            }
            else
            {
                if (myGame.FXEnabled)
                {
                    //if it was enabled, then stop it
                    mciSendString($"setaudio clicksound off", null, 0, IntPtr.Zero);
                    mciSendString($"setaudio registersound off", null, 0, IntPtr.Zero);
                    mciSendString($"setaudio registersound2 off", null, 0, IntPtr.Zero);
                    mciSendString("setaudio pingsound off", null, 0, IntPtr.Zero);
                    for (int i = 1; i <= 8; i++)
                    {
                        mciSendString($"setaudio pickaxe{i}sound off", null, 0, IntPtr.Zero);
                    }
                    myGame.FXEnabled = false;
                }
            }
            LogMessage($"Sound Effects {(enabled ? "Enabled" : "Disabled")}");
        }
        public void PlaySound(SoundList sound)
        {
            int soundCommandResponse = 0;
            switch (sound)
            {
                case (SoundList.ClickSound):
                    {
                        if (myGame.FXEnabled)
                        {
                            mciSendString("seek clicksound to start", null, 0, IntPtr.Zero);
                            soundCommandResponse = mciSendString("play clicksound", null, 0, IntPtr.Zero);
                        }
                        return;
                    }
                case (SoundList.Register):
                    {
                        if (myGame.FXEnabled)
                        {
                            if (myGame.PlayRegisterSound1)
                            {
                                mciSendString("seek registersound to start", null, 0, IntPtr.Zero);
                                soundCommandResponse = mciSendString("play registersound", null, 0, IntPtr.Zero);
                                myGame.PlayRegisterSound1 = false;
                            }
                            else
                            {
                                mciSendString("seek registersound2 to start", null, 0, IntPtr.Zero);
                                soundCommandResponse = mciSendString("play registersound2", null, 0, IntPtr.Zero);
                                myGame.PlayRegisterSound1 = true;
                            }
                        }
                        return;
                    }
                case (SoundList.Pickaxe):
                    {
                        Random randnumber = new Random();
                        int i = randnumber.Next(1, 8);
                        if (myGame.FXEnabled)
                        {
                            mciSendString($"seek pickaxe{i}sound to start", null, 0, IntPtr.Zero);
                            soundCommandResponse = mciSendString($"play pickaxe{i}sound", null, 0, IntPtr.Zero);
                        }
                        return;
                    }
                case (SoundList.Ping):
                    {
                        if (myGame.FXEnabled)
                        {
                            mciSendString("seek pingsound to start", null, 0, IntPtr.Zero);
                            soundCommandResponse = mciSendString("play pingsound", null, 0, IntPtr.Zero);
                        }
                        return;
                    }
            }
            if (soundCommandResponse != 0)
            {
                LogMessage($"PlaySound({sound.ToString()}) Error - MCI Code {soundCommandResponse}");
            }
        }


    }

    //------Classes------//
    public struct Game
    {
        public bool MusicEnabled;
        public bool FXEnabled;
        public bool PlayRegisterSound1;
        internal bool PrestigeNextRestart;
        public int matsMined;
        public int incrperclick;
        public int toolTipDelay;
        public int toolTipVisibleTime;
        public int matsMinedLifetime;
        public int MusicVolume; //0 to 1000
        public int FXVolume; //0 to 1000
        public double myMoney;
        public double salary;
        public double clickAmount;
        public double thislifetimeMoney;
        public double lastlifetimeMoney;
        public double prestigePoints;
        public double prestigeMultiplier;
        public double prestigeGainedNextRestart;
        public TimeSpan thislifeGameTime;
        public TimeSpan totalGameTime;
        public TimeSpan sinceLastSave;
        internal List<Label> floatlabels;
        internal List<Label> floatlabeldeletelist;
        internal List<Upgrade> MainUpgradeList;
        public List<UpgradeButton> upgradeButtons;
        public ItemView[] myItems;
        public PurchaseAmount myPurchaseAmount;
        public System.Windows.Forms.Timer toolTipTimer;
        public Stopwatch GameClock;
        public ToolTip? myTip;
        public FormWindowState myWindowState;
        public string lastSaveLocation;
        public SaveType MySaveType;
        public string CurrentLogFile;
        public bool AutosaveEnabled { get; set; }
        public int AutosaveInterval { get; set; }
        public System.Windows.Forms.Timer AutosaveTimer;

        public const double unlockMultiplier = 2.0d;
        public readonly static int[] unlockList =
            { 1, 10, 25, 50, 100, 200, 300, 400, 500, 600, 666, 700, 777, 800, 900, 1000, 1100, 1111, 1200, 1300, 1400, 1500, 1600, 1700, 1800, 1900, 2000, 2100, 2200, 2222, 2300, 2400, 2500, 2600, 2700, 2800, 2900, 3000, 3100, 3200, 3300, 3333, 3400, 3500, 3600, 3700, 3800, 3900, 4000, 4100, 4200, 4300, 4400, 4500, 4600, 4700, 4800, 4900, 5000 };
        internal readonly static string[] TextStrings = { "Million", "Billion", "Trillion", "Quadrillion", "Quintillion", "Sextillion", "Septillion", "Octillion", "Nonillion", "Decillion", "Undecillion", "Duodecillion", "Tredecillion", "Quattordecillion", "Quindecillion", "Sexdecillion", "Septendecillion", "Octodecillion", "Novemdecillion", "Vigintillion",
                                                "Unvigintillion", "Duovigintillion", "Tresvigintillion", "Quattorvigintillion", "Quinvigintillion", "Sexvigintillion", "Septenvigintillion", "Octovigintillion", "Novemvigintillion", "Trigintillion", "Untrigintillion", "Duotrigintillion", "Tretrigintillion", "Quattortrigintillion", "Quintrigintillion",
                                                "Sextrigintillion", "Septentrigintillion", "Octotrigintillion", "Novemtrigintillion", "Quadragintillion", "Unquadragintillion", "Duoquadragintillion", "Trequadragintillion", "Quattorquadragintillion", "Quinquadragintillion", "Sexquadragintillion", "Septenquadragintillion", "Octoquadragintillion", "Novemquadragintillion",
                                                "Quinquagintillion", "Unquinquagintillion", "Duoquinquagintillion", "Trequinquagintillion", "Quattorquinquagintillion", "Quinquinquagintillion", "Sexquinquagintillion", "Septenquinquagintillion", "Octoquinquagintillion", "Novemquinquagintillion", "Sexagintillion", "Unsexagintillion", "Duosexagintillion",
                                                "Tresexagintillion", "Quattorsexagintillion", "Quinsexagintillion", "Sexsexagintillion", "Septensexagintillion", "Octosexagintillion", "Novemsexagintillion", "Septuagintillion", "Unseptuagintillion", "Duoseptuagintillion", "Treseptuagintillion", "Quattorseptuagintillion", "Quinseptuagintillion", "Sexseptuagintillion",
                                                "Septseptuagintillion", "Octoseptuagintillion", "Novemseptuagintillion", "Octogintillion", "Unoctogintillion", "Duooctogintillion", "Treoctogintillion", "Quattoroctogintillion", "Quinoctogintillion", "Sexoctogintillion", "Septoctogintillion", "Octoctogintillion", "Novemoctogintillion", "Nonagintillion",
                                                "Unnonagintillion", "Duononagintillion", "Trenonagintillion", "Quattornonagintillion", "Quinonagintillion", "Sexnonagintillion", "Septenonagintillion", "Octononagintillion", "Novemnonagintillion", "Centillion", "Uncentillion"};  //handles full size of type 'double'
        internal const double Octoquinquagintillion = 1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000.00d;

        /// <summary>
        /// Default constructor - returns a Game object filled with default values, for a new game or new prestige upgrade.
        /// </summary>
        public Game()
        {
            //init myItems Default
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

            //init Upgrades Default
            //Upgrades are declared as: (string Name, double Cost, int ItemID, double Multiplier, int upgradeID). They can only be created or altered through their constructors.
            //itemID==0 - ClickAmount
            //itemID==1-8(14) - Items
            //itemID==15 - All Items
            //itemID==20 - Prestige Multiplier
            if (this.MainUpgradeList == default)
            {
                MainUpgradeList =
            [
                new Upgrade("Double Tap", 1000.0d, 0, 10, 1), //Click Upgrades
                new Upgrade("Click Amplifier", 25000.0d, 0, 10, 2),
                new Upgrade("Mega-Clicking", 150000.0d, 0, 10, 3),
                new Upgrade("Click Physics", 500000.0d, 0, 10, 4),
                new Upgrade("Parallel Clicking", 1500000.0d, 0, 10, 5),
                new Upgrade("Birch Wood", 250000.0d, 1, 3, 6), //Item1 Upgrades
                new Upgrade("Pine Wood", 20000000000000.0d, 1, 10, 7),   //----This should be more like 10, reevaluate subsequent upgrades
                new Upgrade("Oak Wood", 2000000000000000000.0d, 1, 7, 8),
                new Upgrade("Cherry Wood", 25000000000000000000000.0d, 1, 7, 9),
                new Upgrade("Sequoia Wood", 1000000000000000000000000000.0d, 1, 10, 10),
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
            if (this.upgradeButtons == default) { upgradeButtons = new List<UpgradeButton>(); }
            if (this.toolTipVisibleTime == default) { toolTipVisibleTime = 2500; }  //How long will tooltips stay visible?
            if (this.toolTipDelay == default) { toolTipDelay = 500; }   //How long do we have to hover before tooltips pop up?
            if (this.prestigeMultiplier == default) { prestigeMultiplier = 2.0d; }
            if (this.clickAmount == default) { clickAmount = 0.25; }
            if (this.salary == default) { salary = 0.0d; }  //Deprecated - Phasing out
            if (this.matsMined == default) { matsMined = 0; }
            if (this.myMoney == default) { myMoney = 0.0d; }
            if (this.incrperclick == default) { incrperclick = 1; }
            if (this.myPurchaseAmount == default) { myPurchaseAmount = PurchaseAmount.BuyOne; }
            if (this.thislifetimeMoney == default) { this.thislifetimeMoney = 0.0d; }   //if they're default here, then we couldn't load from the last save. Set to 0.
            if (this.prestigePoints == default) { this.prestigePoints = 0.0d; }
            if (this.lastlifetimeMoney == default) { this.lastlifetimeMoney = 0.0d; }
            if (this.floatlabels == default) { floatlabels = new List<Label>(); }
            if (this.floatlabeldeletelist == default) { floatlabeldeletelist = new List<Label>(); }
            if (this.toolTipTimer == default) { toolTipTimer = new System.Windows.Forms.Timer(); }
            if (this.GameClock == default) { GameClock = new Stopwatch(); }
            if (this.matsMinedLifetime == default) { matsMinedLifetime = 0; }
            if (this.thislifeGameTime == default) { thislifeGameTime = TimeSpan.Zero; }
            if (this.totalGameTime == default) { totalGameTime = TimeSpan.Zero; }
            if (this.sinceLastSave == default) { sinceLastSave = TimeSpan.Zero; }
            this.PlayRegisterSound1 = true;
            this.MusicEnabled = true;
            this.FXEnabled = true;
            if (this.MusicVolume == default) { this.MusicVolume = 500; }
            if (this.FXVolume == default) { this.FXVolume = 500; }
            this.PrestigeNextRestart = false;
            this.lastSaveLocation = Environment.CurrentDirectory + @"\GameState.mmf";
            this.MySaveType = SaveType.NewGame;
            this.AutosaveEnabled = false;
            this.AutosaveInterval = 0;
            this.AutosaveTimer = new();
            this.CurrentLogFile = "";
        }
        /// <summary>
        /// Game Load constructor - creates a new Game object from GameState (save) data.
        /// </summary>
        /// <param name="save"></param>
        public Game(GameState save)
        {
            MusicEnabled = save.MusicEn;
            FXEnabled = save.FXEn;
            PlayRegisterSound1 = false;  //simply an audio toggle, setting it here for explicit declaration purposes
            PrestigeNextRestart = save.PrestigeSaveFlag;
            matsMined = save.matsMined;
            incrperclick = save.incrperclick;
            toolTipDelay = save.toolTipDelay;
            toolTipVisibleTime = save.toolTipVisibleTime;
            matsMinedLifetime = save.matsMinedLifetime;
            MusicVolume = save.MusicVol;
            FXVolume = save.FXVol;
            myMoney = save.myMoney;
            salary = save.salary;
            clickAmount = save.clickAmount;
            thislifetimeMoney = save.thislifetimeMoney;
            lastlifetimeMoney = save.lastlifetimeMoney;
            prestigePoints = save.prestigePoints;
            prestigeMultiplier = save.prestigeMultiplier;
            prestigeGainedNextRestart = save.PrestigePointsGained;
            thislifeGameTime = save.thislifegametime;
            totalGameTime = save.totalgametime;
            floatlabels = new List<Label>();
            floatlabeldeletelist = new List<Label>();
            MainUpgradeList = save.MainUpgradeList;
            upgradeButtons = new List<UpgradeButton>();
            myItems = new ItemView[save.myItemDatas.Length];
            for (int i = 0; i < myItems.Length; i++)
            {
                myItems[i] = new ItemView(save.myItemDatas[i]);
            }
            myPurchaseAmount = save.myPurchaseAmount;
            toolTipTimer = new();
            GameClock = new();
            myTip = new ToolTip();
            myWindowState = save.lastWindowState;
            sinceLastSave = DateTime.Now - save.lastsavetimestamp;
            lastSaveLocation = save.SaveLocation;
            MySaveType = save.saveType;
            AutosaveEnabled = save.AutosaveEnabled;
            AutosaveInterval = save.AutosaveInterval;
            AutosaveTimer = new();
            CurrentLogFile = "";
        }
    }
    [Serializable]
    public class GameState
    {
        internal ItemData[] myItemDatas;    //Serializable object for ItemView
        internal List<Upgrade> MainUpgradeList;//
        internal int toolTipVisibleTime;//
        internal int toolTipDelay;//
        internal double prestigeMultiplier;//
        internal double clickAmount;//
        internal double salary;//
        internal int matsMined;//
        internal double myMoney;//
        internal int incrperclick;//
        internal PurchaseAmount myPurchaseAmount;//
        internal double thislifetimeMoney;//
        internal double prestigePoints;//
        internal double lastlifetimeMoney;//
        internal TimeSpan totalgametime;//
        internal TimeSpan thislifegametime;//
        internal DateTime lastsavetimestamp;//--
        internal FormWindowState lastWindowState;//--
        internal bool PrestigeSaveFlag;//nextrestart//--
        internal double PrestigePointsGained;//--
        internal int MusicVol;//
        internal int FXVol;//
        [OptionalField(VersionAdded = 2)]
        internal bool MusicEn;//
        [OptionalField(VersionAdded = 2)]
        internal bool FXEn;//
        [OptionalField(VersionAdded = 2)]
        internal int matsMinedLifetime;//
        [OptionalField(VersionAdded = 3)]
        internal SaveType saveType;
        [OptionalField(VersionAdded = 3)]
        internal string SaveLocation;
        [OptionalField(VersionAdded = 3)]
        internal bool AutosaveEnabled;
        [OptionalField(VersionAdded = 3)]
        internal int AutosaveInterval;
        
        /// <summary>
        /// Creates a GameState (a save structure) for Game object. 
        /// </summary>
        /// <param name="game">Required - the Game object from which to get data to save.</param>
        public GameState(Game game)
        {
            myItemDatas = new ItemData[game.myItems.Length];
            for (int i = 0; i < myItemDatas.Length; i++)
            {
                myItemDatas[i] = new ItemData(game.myItems[i]);
            }
            MainUpgradeList = game.MainUpgradeList;
            toolTipVisibleTime = game.toolTipVisibleTime;
            toolTipDelay = game.toolTipDelay;
            prestigeMultiplier = game.prestigeMultiplier;
            clickAmount = game.clickAmount;
            salary = game.salary;
            matsMined = game.matsMined;
            matsMinedLifetime = game.matsMinedLifetime;
            myMoney = game.myMoney;
            incrperclick = game.incrperclick;
            myPurchaseAmount = game.myPurchaseAmount;
            thislifetimeMoney = game.thislifetimeMoney;
            prestigePoints = game.prestigePoints;
            lastlifetimeMoney = game.lastlifetimeMoney;
            totalgametime = game.totalGameTime;
            thislifegametime = game.thislifeGameTime;
            lastsavetimestamp = DateTime.Now;
            lastWindowState = game.myWindowState;
            PrestigeSaveFlag = game.PrestigeNextRestart;
            PrestigePointsGained = game.prestigeGainedNextRestart;
            MusicVol = game.MusicVolume;
            FXVol = game.FXVolume;
            MusicEn = game.MusicEnabled;
            FXEn = game.FXEnabled;
            saveType = game.PrestigeNextRestart ? SaveType.Prestigesave : game.MySaveType;
            SaveLocation = game.lastSaveLocation;
            AutosaveEnabled = game.AutosaveEnabled;
            AutosaveInterval = game.AutosaveInterval;
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
        /// <summary>
        /// Custom property used instead of default enable
        /// </summary>
        internal bool IsEnabled { get; set; }
    }
    [Serializable]
    public static class Colors
    {
        public static Color colPrimary = Color.Green;
        public static Color colSecondary = Color.SandyBrown;
        public static Color colTertiary = Color.Tan;
        public static Color colDisable = Color.Gray;
        public static Color colButtonDisabled = Color.FromArgb(51, 46, 60);//RaisinBlack
        public static Color colButtonEnabled = Color.FromArgb(63, 210, 255);//PaleAzure
        public static Color colButtonPurchased = Color.ForestGreen;
        public static Color colBackground = Color.FromArgb(210, 180, 140);//Tan
        public static Color colTextPrimary = Color.Black;
        public static Color colTextSecondary = Color.White;
        public static Color colBorders = Color.FromArgb(78, 213, 215);//TiffanyBlue
        public static Color colUpgradeTextEnabled = Color.FromArgb(0, 29, 107);
        public static Color colUpgradeTextDisabled = Color.FromArgb(188, 194, 174);
        public static Color colUpgradeTextPurchased = Color.FromArgb(255, 255, 255);
    }
    /// <summary>
    /// Deprecated - DO NOT USE. Here for compatibility with v1.1.0.1-alpha saves! Reference static class Colors instead.
    /// </summary>
    [Serializable]
    public class MyColors   //Deprecated - Here for compatibility
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
        [OptionalField(VersionAdded = 3)]
        public Color colUpgradeTextEnabled = Color.FromArgb(0, 29, 107);
        [OptionalField(VersionAdded = 3)]
        public Color colUpgradeTextDisabled = Color.FromArgb(188, 194, 174);
        [OptionalField(VersionAdded = 3)]
        public Color colUpgradeTextPurchased = Color.FromArgb(255, 255, 255);
    }

    //------Enums------//
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
        ScientificNotation = 128,
        SecondsToMinSec = 256,
        SecondsToHourMinSec = 512
    }
    public enum SoundList
    {
        ClickSound = 1,
        Pickaxe = 2,
        Register = 4,
        Ping = 8
    }
    [DefaultValue(SaveType.Exitsave)]
    public enum SaveType
    {
        Exitsave = 10,
        Manualsave = 20,
        Prestigesave = 40,
        NewGame = 80,
        Autosave = 160
    }
    
    
}
