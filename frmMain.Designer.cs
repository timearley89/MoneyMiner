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
            flowLayoutPanel1 = new FlowLayoutPanel();
            lblMoney = new Label();
            lblMoneyAmt = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            lblSalary = new Label();
            btnPurchAmount = new Button();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Location = new Point(12, 12);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(363, 546);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // lblMoney
            // 
            lblMoney.AutoSize = true;
            lblMoney.Font = new Font("Segoe UI", 16F);
            lblMoney.Location = new Point(512, 12);
            lblMoney.Name = "lblMoney";
            lblMoney.Size = new Size(92, 30);
            lblMoney.TabIndex = 1;
            lblMoney.Text = "Money: ";
            // 
            // lblMoneyAmt
            // 
            lblMoneyAmt.AutoSize = true;
            lblMoneyAmt.Font = new Font("Segoe UI", 16F);
            lblMoneyAmt.Location = new Point(610, 12);
            lblMoneyAmt.Name = "lblMoneyAmt";
            lblMoneyAmt.Size = new Size(25, 30);
            lblMoneyAmt.TabIndex = 2;
            lblMoneyAmt.Text = "0";
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // lblSalary
            // 
            lblSalary.AutoSize = true;
            lblSalary.Location = new Point(566, 42);
            lblSalary.Name = "lblSalary";
            lblSalary.Size = new Size(38, 15);
            lblSalary.TabIndex = 3;
            lblSalary.Text = "label1";
            // 
            // btnPurchAmount
            // 
            btnPurchAmount.Location = new Point(381, 503);
            btnPurchAmount.Name = "btnPurchAmount";
            btnPurchAmount.Size = new Size(75, 55);
            btnPurchAmount.TabIndex = 4;
            btnPurchAmount.Text = "Buy: x1";
            btnPurchAmount.UseVisualStyleBackColor = true;
            btnPurchAmount.Click += btnPurchAmount_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 570);
            Controls.Add(btnPurchAmount);
            Controls.Add(lblSalary);
            Controls.Add(lblMoneyAmt);
            Controls.Add(lblMoney);
            Controls.Add(flowLayoutPanel1);
            Name = "frmMain";
            Text = "First Clicker/Idle Game";
            Load += frmMain_Load;
            Click += frmMain_Click;
            Paint += frmMain_Paint;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Label lblMoney;
        private Label lblMoneyAmt;
        private System.Windows.Forms.Timer timer1;
        private Label lblSalary;
        private Button btnPurchAmount;
    }
}