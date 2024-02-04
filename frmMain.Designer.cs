namespace MoneyMiner
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            itemPanel = new FlowLayoutPanel();
            timerPerSec = new System.Windows.Forms.Timer(components);
            btnPurchAmount = new Button();
            UpgradePanel = new FlowLayoutPanel();
            btnMine = new Button();
            lblMatsMined = new Label();
            lblIncrPerClick = new Label();
            timerVisualUpdate = new System.Windows.Forms.Timer(components);
            btnPrestige = new Button();
            grpMoney = new GroupBox();
            lblClickAmount = new Label();
            lblSalary = new Label();
            lblMoney = new Label();
            pctCenterBackground = new PictureBox();
            btnPause = new Button();
            pnlButtons = new Panel();
            btnUnlocks = new Button();
            btnQuickBuy = new Button();
            grpMoney.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pctCenterBackground).BeginInit();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // itemPanel
            // 
            itemPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            itemPanel.AutoScroll = true;
            itemPanel.CausesValidation = false;
            itemPanel.FlowDirection = FlowDirection.TopDown;
            itemPanel.Location = new Point(12, 12);
            itemPanel.Name = "itemPanel";
            itemPanel.Size = new Size(353, 537);
            itemPanel.TabIndex = 0;
            itemPanel.WrapContents = false;
            itemPanel.Resize += itemPanel_Resize;
            // 
            // timerPerSec
            // 
            timerPerSec.Interval = 1000;
            timerPerSec.Tick += timerPerSec_Tick;
            // 
            // btnPurchAmount
            // 
            btnPurchAmount.Anchor = AnchorStyles.Bottom;
            btnPurchAmount.CausesValidation = false;
            btnPurchAmount.Font = new Font("Bahnschrift SemiBold", 9F, FontStyle.Bold);
            btnPurchAmount.Location = new Point(3, 3);
            btnPurchAmount.Name = "btnPurchAmount";
            btnPurchAmount.Size = new Size(65, 55);
            btnPurchAmount.TabIndex = 4;
            btnPurchAmount.Text = "Buy: x1";
            btnPurchAmount.UseVisualStyleBackColor = true;
            btnPurchAmount.Click += btnPurchAmount_Click;
            // 
            // UpgradePanel
            // 
            UpgradePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            UpgradePanel.AutoScroll = true;
            UpgradePanel.FlowDirection = FlowDirection.TopDown;
            UpgradePanel.ForeColor = SystemColors.ControlLightLight;
            UpgradePanel.Location = new Point(776, 12);
            UpgradePanel.Name = "UpgradePanel";
            UpgradePanel.Size = new Size(196, 537);
            UpgradePanel.TabIndex = 5;
            UpgradePanel.WrapContents = false;
            UpgradePanel.Resize += UpgradePanel_Resize;
            // 
            // btnMine
            // 
            btnMine.Anchor = AnchorStyles.Bottom;
            btnMine.BackColor = Color.Transparent;
            btnMine.BackgroundImage = Properties.Resources.pickaxe;
            btnMine.BackgroundImageLayout = ImageLayout.Stretch;
            btnMine.FlatAppearance.BorderColor = Color.DarkSalmon;
            btnMine.FlatAppearance.BorderSize = 0;
            btnMine.FlatAppearance.MouseDownBackColor = Color.DarkSalmon;
            btnMine.FlatAppearance.MouseOverBackColor = Color.DarkSalmon;
            btnMine.FlatStyle = FlatStyle.Flat;
            btnMine.Font = new Font("Segoe UI", 24F);
            btnMine.Location = new Point(414, 169);
            btnMine.Name = "btnMine";
            btnMine.Size = new Size(317, 295);
            btnMine.TabIndex = 6;
            btnMine.Text = "Mine!";
            btnMine.UseVisualStyleBackColor = false;
            btnMine.Click += btnMine_Click;
            btnMine.MouseDown += btnMine_MouseDown;
            btnMine.MouseUp += btnMine_MouseUp;
            // 
            // lblMatsMined
            // 
            lblMatsMined.Anchor = AnchorStyles.Bottom;
            lblMatsMined.AutoSize = true;
            lblMatsMined.Font = new Font("Bahnschrift SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMatsMined.Location = new Point(500, 467);
            lblMatsMined.Name = "lblMatsMined";
            lblMatsMined.Size = new Size(141, 19);
            lblMatsMined.TabIndex = 7;
            lblMatsMined.Text = "Materials Mined: 0";
            // 
            // lblIncrPerClick
            // 
            lblIncrPerClick.Anchor = AnchorStyles.Bottom;
            lblIncrPerClick.AutoSize = true;
            lblIncrPerClick.Font = new Font("Bahnschrift SemiBold", 9F, FontStyle.Bold);
            lblIncrPerClick.Location = new Point(521, 488);
            lblIncrPerClick.Name = "lblIncrPerClick";
            lblIncrPerClick.Size = new Size(96, 14);
            lblIncrPerClick.TabIndex = 9;
            lblIncrPerClick.Text = "Mined Per Click: 1";
            // 
            // timerVisualUpdate
            // 
            timerVisualUpdate.Tick += timerVisualUpdate_Tick;
            // 
            // btnPrestige
            // 
            btnPrestige.Anchor = AnchorStyles.Bottom;
            btnPrestige.Font = new Font("Bahnschrift SemiBold", 9F, FontStyle.Bold);
            btnPrestige.Location = new Point(321, 3);
            btnPrestige.Name = "btnPrestige";
            btnPrestige.Size = new Size(75, 55);
            btnPrestige.TabIndex = 10;
            btnPrestige.Text = "Prestige";
            btnPrestige.UseVisualStyleBackColor = true;
            btnPrestige.Click += btnPrestige_Click;
            // 
            // grpMoney
            // 
            grpMoney.Anchor = AnchorStyles.Top;
            grpMoney.CausesValidation = false;
            grpMoney.Controls.Add(lblClickAmount);
            grpMoney.Controls.Add(lblSalary);
            grpMoney.Controls.Add(lblMoney);
            grpMoney.Location = new Point(414, 2);
            grpMoney.Name = "grpMoney";
            grpMoney.Size = new Size(317, 143);
            grpMoney.TabIndex = 11;
            grpMoney.TabStop = false;
            // 
            // lblClickAmount
            // 
            lblClickAmount.AutoSize = true;
            lblClickAmount.CausesValidation = false;
            lblClickAmount.Font = new Font("Bahnschrift SemiBold", 12F, FontStyle.Bold);
            lblClickAmount.Location = new Point(86, 93);
            lblClickAmount.Name = "lblClickAmount";
            lblClickAmount.Size = new Size(171, 19);
            lblClickAmount.TabIndex = 2;
            lblClickAmount.Text = "Mining: $0.25 Per Click";
            lblClickAmount.TextAlign = ContentAlignment.MiddleCenter;
            lblClickAmount.SizeChanged += lblClickAmount_SizeChanged;
            // 
            // lblSalary
            // 
            lblSalary.AutoSize = true;
            lblSalary.CausesValidation = false;
            lblSalary.Font = new Font("Bahnschrift SemiBold", 12F, FontStyle.Bold);
            lblSalary.Location = new Point(86, 59);
            lblSalary.Name = "lblSalary";
            lblSalary.Size = new Size(169, 19);
            lblSalary.TabIndex = 1;
            lblSalary.Text = "Salary: $0 Per Second";
            lblSalary.TextAlign = ContentAlignment.MiddleCenter;
            lblSalary.SizeChanged += lblSalary_SizeChanged;
            // 
            // lblMoney
            // 
            lblMoney.AutoSize = true;
            lblMoney.CausesValidation = false;
            lblMoney.Font = new Font("Bahnschrift SemiBold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMoney.Location = new Point(108, 19);
            lblMoney.Name = "lblMoney";
            lblMoney.Size = new Size(106, 25);
            lblMoney.TabIndex = 0;
            lblMoney.Text = "Money: $0";
            lblMoney.TextAlign = ContentAlignment.MiddleCenter;
            lblMoney.SizeChanged += lblMoney_SizeChanged;
            // 
            // pctCenterBackground
            // 
            pctCenterBackground.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pctCenterBackground.Image = Properties.Resources.mining_miner_cartoon;
            pctCenterBackground.Location = new Point(375, 145);
            pctCenterBackground.Name = "pctCenterBackground";
            pctCenterBackground.Size = new Size(389, 337);
            pctCenterBackground.SizeMode = PictureBoxSizeMode.StretchImage;
            pctCenterBackground.TabIndex = 12;
            pctCenterBackground.TabStop = false;
            pctCenterBackground.Resize += pctCenterBackground_Resize;
            // 
            // btnPause
            // 
            btnPause.Anchor = AnchorStyles.Bottom;
            btnPause.CausesValidation = false;
            btnPause.Font = new Font("Bahnschrift SemiBold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPause.Location = new Point(74, 28);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(75, 30);
            btnPause.TabIndex = 14;
            btnPause.Text = "Settings...";
            btnPause.UseVisualStyleBackColor = true;
            btnPause.Click += btnPause_Click;
            // 
            // pnlButtons
            // 
            pnlButtons.Anchor = AnchorStyles.Bottom;
            pnlButtons.BackColor = Color.Transparent;
            pnlButtons.CausesValidation = false;
            pnlButtons.Controls.Add(btnUnlocks);
            pnlButtons.Controls.Add(btnQuickBuy);
            pnlButtons.Controls.Add(btnPause);
            pnlButtons.Controls.Add(btnPurchAmount);
            pnlButtons.Controls.Add(btnPrestige);
            pnlButtons.Location = new Point(371, 488);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(399, 61);
            pnlButtons.TabIndex = 15;
            // 
            // btnUnlocks
            // 
            btnUnlocks.Anchor = AnchorStyles.Bottom;
            btnUnlocks.BackColor = Color.FromArgb(128, 255, 255);
            btnUnlocks.CausesValidation = false;
            btnUnlocks.Font = new Font("Bahnschrift SemiBold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUnlocks.Location = new Point(155, 28);
            btnUnlocks.Name = "btnUnlocks";
            btnUnlocks.Size = new Size(79, 30);
            btnUnlocks.TabIndex = 16;
            btnUnlocks.Text = "Unlocks...";
            btnUnlocks.UseVisualStyleBackColor = false;
            btnUnlocks.Click += btnUnlocks_Click;
            // 
            // btnQuickBuy
            // 
            btnQuickBuy.Anchor = AnchorStyles.Bottom;
            btnQuickBuy.CausesValidation = false;
            btnQuickBuy.Font = new Font("Bahnschrift SemiBold", 9F, FontStyle.Bold);
            btnQuickBuy.Location = new Point(240, 28);
            btnQuickBuy.Name = "btnQuickBuy";
            btnQuickBuy.Size = new Size(75, 30);
            btnQuickBuy.TabIndex = 15;
            btnQuickBuy.Text = "Quick-Buy";
            btnQuickBuy.UseVisualStyleBackColor = true;
            btnQuickBuy.Click += btnQuickBuy_Click;
            // 
            // frmMain
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Tan;
            CausesValidation = false;
            ClientSize = new Size(984, 561);
            Controls.Add(lblMatsMined);
            Controls.Add(lblIncrPerClick);
            Controls.Add(pnlButtons);
            Controls.Add(btnMine);
            Controls.Add(grpMoney);
            Controls.Add(UpgradePanel);
            Controls.Add(itemPanel);
            Controls.Add(pctCenterBackground);
            DoubleBuffered = true;
            Font = new Font("Bahnschrift SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            MinimumSize = new Size(1000, 600);
            Name = "frmMain";
            SizeGripStyle = SizeGripStyle.Show;
            Text = "MoneyMiner!";
            FormClosing += frmMain_FormClosing;
            Load += frmMain_Load;
            Resize += frmMain_Resize;
            grpMoney.ResumeLayout(false);
            grpMoney.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pctCenterBackground).EndInit();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel itemPanel;
        private System.Windows.Forms.Timer timerPerSec;
        private Button btnPurchAmount;
        private FlowLayoutPanel UpgradePanel;
        private Button btnMine;
        private Label lblMatsMined;
        private Label lblIncrPerClick;
        private System.Windows.Forms.Timer timerVisualUpdate;
        private Button btnPrestige;
        private GroupBox grpMoney;
        private Label lblSalary;
        private Label lblMoney;
        private Label lblClickAmount;
        private PictureBox pctCenterBackground;
        private Button btnPause;
        private Panel pnlButtons;
        private Button btnQuickBuy;
        private Button btnUnlocks;
    }
}