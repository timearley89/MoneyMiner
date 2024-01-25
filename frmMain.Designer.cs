namespace FirstClicker
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
            grpMoney.SuspendLayout();
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
            btnPurchAmount.Location = new Point(373, 494);
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
            // 
            // btnMine
            // 
            btnMine.Anchor = AnchorStyles.Bottom;
            btnMine.Font = new Font("Segoe UI", 24F);
            btnMine.Location = new Point(414, 169);
            btnMine.Name = "btnMine";
            btnMine.Size = new Size(317, 295);
            btnMine.TabIndex = 6;
            btnMine.Text = "Mine Materials";
            btnMine.UseVisualStyleBackColor = true;
            btnMine.Click += btnMine_Click;
            // 
            // lblMatsMined
            // 
            lblMatsMined.Anchor = AnchorStyles.Bottom;
            lblMatsMined.AutoSize = true;
            lblMatsMined.Font = new Font("Segoe UI", 12F);
            lblMatsMined.Location = new Point(500, 467);
            lblMatsMined.Name = "lblMatsMined";
            lblMatsMined.Size = new Size(138, 21);
            lblMatsMined.TabIndex = 7;
            lblMatsMined.Text = "Materials Mined: 0";
            // 
            // lblIncrPerClick
            // 
            lblIncrPerClick.Anchor = AnchorStyles.Bottom;
            lblIncrPerClick.AutoSize = true;
            lblIncrPerClick.Location = new Point(521, 488);
            lblIncrPerClick.Name = "lblIncrPerClick";
            lblIncrPerClick.Size = new Size(102, 15);
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
            btnPrestige.Location = new Point(687, 494);
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
            lblClickAmount.Font = new Font("Segoe UI", 12F);
            lblClickAmount.Location = new Point(86, 93);
            lblClickAmount.Name = "lblClickAmount";
            lblClickAmount.Size = new Size(168, 21);
            lblClickAmount.TabIndex = 2;
            lblClickAmount.Text = "Mining: $0.25 Per Click";
            lblClickAmount.TextAlign = ContentAlignment.MiddleCenter;
            lblClickAmount.SizeChanged += lblClickAmount_SizeChanged;
            // 
            // lblSalary
            // 
            lblSalary.AutoSize = true;
            lblSalary.CausesValidation = false;
            lblSalary.Font = new Font("Segoe UI", 12F);
            lblSalary.Location = new Point(86, 59);
            lblSalary.Name = "lblSalary";
            lblSalary.Size = new Size(159, 21);
            lblSalary.TabIndex = 1;
            lblSalary.Text = "Salary: $0 Per Second";
            lblSalary.TextAlign = ContentAlignment.MiddleCenter;
            lblSalary.SizeChanged += lblSalary_SizeChanged;
            // 
            // lblMoney
            // 
            lblMoney.AutoSize = true;
            lblMoney.CausesValidation = false;
            lblMoney.Font = new Font("Segoe UI", 16F);
            lblMoney.Location = new Point(108, 19);
            lblMoney.Name = "lblMoney";
            lblMoney.Size = new Size(116, 30);
            lblMoney.TabIndex = 0;
            lblMoney.Text = "Money: $0";
            lblMoney.TextAlign = ContentAlignment.MiddleCenter;
            lblMoney.SizeChanged += lblMoney_SizeChanged;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CausesValidation = false;
            ClientSize = new Size(984, 561);
            Controls.Add(grpMoney);
            Controls.Add(btnPrestige);
            Controls.Add(lblIncrPerClick);
            Controls.Add(lblMatsMined);
            Controls.Add(btnMine);
            Controls.Add(UpgradePanel);
            Controls.Add(btnPurchAmount);
            Controls.Add(itemPanel);
            MinimumSize = new Size(1000, 600);
            Name = "frmMain";
            SizeGripStyle = SizeGripStyle.Show;
            Text = "MoneyMiner!";
            FormClosing += frmMain_FormClosing;
            Load += frmMain_Load;
            Click += frmMain_Click;
            Paint += frmMain_Paint;
            grpMoney.ResumeLayout(false);
            grpMoney.PerformLayout();
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
    }
}