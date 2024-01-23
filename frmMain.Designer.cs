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
            statusPane = new FlowLayoutPanel();
            lblMoney = new Label();
            lblSalary = new Label();
            lblIncrPerClick = new Label();
            timerVisualUpdate = new System.Windows.Forms.Timer(components);
            btnPrestige = new Button();
            statusPane.SuspendLayout();
            SuspendLayout();
            // 
            // itemPanel
            // 
            itemPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            itemPanel.AutoScroll = true;
            itemPanel.CausesValidation = false;
            itemPanel.Location = new Point(12, 12);
            itemPanel.Name = "itemPanel";
            itemPanel.Size = new Size(353, 546);
            itemPanel.TabIndex = 0;
            // 
            // timerPerSec
            // 
            timerPerSec.Interval = 1000;
            timerPerSec.Tick += timerPerSec_Tick;
            // 
            // btnPurchAmount
            // 
            btnPurchAmount.CausesValidation = false;
            btnPurchAmount.Location = new Point(381, 503);
            btnPurchAmount.Name = "btnPurchAmount";
            btnPurchAmount.Size = new Size(65, 55);
            btnPurchAmount.TabIndex = 4;
            btnPurchAmount.Text = "Buy: x1";
            btnPurchAmount.UseVisualStyleBackColor = true;
            btnPurchAmount.Click += btnPurchAmount_Click;
            // 
            // UpgradePanel
            // 
            UpgradePanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            UpgradePanel.AutoScroll = true;
            UpgradePanel.Location = new Point(792, 12);
            UpgradePanel.Name = "UpgradePanel";
            UpgradePanel.Size = new Size(196, 546);
            UpgradePanel.TabIndex = 5;
            // 
            // btnMine
            // 
            btnMine.Font = new Font("Segoe UI", 24F);
            btnMine.Location = new Point(422, 178);
            btnMine.Name = "btnMine";
            btnMine.Size = new Size(317, 295);
            btnMine.TabIndex = 6;
            btnMine.Text = "Mine Materials";
            btnMine.UseVisualStyleBackColor = true;
            btnMine.Click += btnMine_Click;
            // 
            // lblMatsMined
            // 
            lblMatsMined.AutoSize = true;
            lblMatsMined.Font = new Font("Segoe UI", 12F);
            lblMatsMined.Location = new Point(508, 476);
            lblMatsMined.Name = "lblMatsMined";
            lblMatsMined.Size = new Size(138, 21);
            lblMatsMined.TabIndex = 7;
            lblMatsMined.Text = "Materials Mined: 0";
            // 
            // statusPane
            // 
            statusPane.CausesValidation = false;
            statusPane.Controls.Add(lblMoney);
            statusPane.Controls.Add(lblSalary);
            statusPane.Location = new Point(422, 12);
            statusPane.Name = "statusPane";
            statusPane.Size = new Size(317, 115);
            statusPane.TabIndex = 8;
            // 
            // lblMoney
            // 
            lblMoney.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblMoney.AutoSize = true;
            lblMoney.Font = new Font("Segoe UI", 18F);
            lblMoney.Location = new Point(3, 0);
            lblMoney.Name = "lblMoney";
            lblMoney.Size = new Size(158, 32);
            lblMoney.TabIndex = 0;
            lblMoney.Text = "Money: $0.00";
            // 
            // lblSalary
            // 
            lblSalary.AutoSize = true;
            lblSalary.CausesValidation = false;
            lblSalary.Font = new Font("Segoe UI", 14F);
            lblSalary.Location = new Point(3, 32);
            lblSalary.Name = "lblSalary";
            lblSalary.Size = new Size(305, 50);
            lblSalary.TabIndex = 1;
            lblSalary.Text = "Salary: $0.00 Per Second, $0.00 Per Click";
            // 
            // lblIncrPerClick
            // 
            lblIncrPerClick.AutoSize = true;
            lblIncrPerClick.Location = new Point(529, 497);
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
            btnPrestige.Location = new Point(695, 503);
            btnPrestige.Name = "btnPrestige";
            btnPrestige.Size = new Size(75, 55);
            btnPrestige.TabIndex = 10;
            btnPrestige.Text = "Prestige";
            btnPrestige.UseVisualStyleBackColor = true;
            btnPrestige.Click += btnPrestige_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CausesValidation = false;
            ClientSize = new Size(1000, 570);
            Controls.Add(btnPrestige);
            Controls.Add(lblIncrPerClick);
            Controls.Add(lblMatsMined);
            Controls.Add(btnMine);
            Controls.Add(UpgradePanel);
            Controls.Add(btnPurchAmount);
            Controls.Add(itemPanel);
            Controls.Add(statusPane);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmMain";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "First Clicker/Idle Game";
            Load += frmMain_Load;
            Click += frmMain_Click;
            Paint += frmMain_Paint;
            statusPane.ResumeLayout(false);
            statusPane.PerformLayout();
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
        private FlowLayoutPanel statusPane;
        private Label lblMoney;
        private Label lblSalary;
        private Label lblIncrPerClick;
        private System.Windows.Forms.Timer timerVisualUpdate;
        private Button btnPrestige;
    }
}