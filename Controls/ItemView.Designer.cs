namespace FirstClicker.Controls
{
    partial class ItemView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnBuy = new Button();
            grpItem = new GroupBox();
            lblTimeLeft = new Label();
            lblTotalSal = new Label();
            lblSalPerSec = new Label();
            lblCost = new Label();
            lblQuantity = new Label();
            progressMining = new ProgressBar();
            grpItem.SuspendLayout();
            SuspendLayout();
            // 
            // btnBuy
            // 
            btnBuy.Font = new Font("Segoe UI", 12F);
            btnBuy.Location = new Point(3, 127);
            btnBuy.Name = "btnBuy";
            btnBuy.Size = new Size(291, 32);
            btnBuy.TabIndex = 0;
            btnBuy.Text = "Purchase x1";
            btnBuy.UseVisualStyleBackColor = true;
            btnBuy.Click += btnBuy_Click;
            // 
            // grpItem
            // 
            grpItem.Controls.Add(lblTimeLeft);
            grpItem.Controls.Add(lblTotalSal);
            grpItem.Controls.Add(lblSalPerSec);
            grpItem.Controls.Add(lblCost);
            grpItem.Controls.Add(lblQuantity);
            grpItem.Font = new Font("Segoe UI", 12F);
            grpItem.Location = new Point(3, 3);
            grpItem.Name = "grpItem";
            grpItem.Size = new Size(291, 122);
            grpItem.TabIndex = 1;
            grpItem.TabStop = false;
            grpItem.Text = "groupBox1";
            // 
            // lblTimeLeft
            // 
            lblTimeLeft.Anchor = AnchorStyles.Left;
            lblTimeLeft.BackColor = SystemColors.ActiveCaption;
            lblTimeLeft.Font = new Font("Segoe UI", 11F);
            lblTimeLeft.Location = new Point(6, 58);
            lblTimeLeft.Name = "lblTimeLeft";
            lblTimeLeft.Size = new Size(132, 24);
            lblTimeLeft.TabIndex = 4;
            lblTimeLeft.Text = "Time: 0.0s";
            // 
            // lblTotalSal
            // 
            lblTotalSal.BackColor = SystemColors.ActiveCaption;
            lblTotalSal.Font = new Font("Segoe UI", 12F);
            lblTotalSal.Location = new Point(6, 88);
            lblTotalSal.Name = "lblTotalSal";
            lblTotalSal.Size = new Size(279, 31);
            lblTotalSal.TabIndex = 3;
            lblTotalSal.Text = "label1";
            lblTotalSal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSalPerSec
            // 
            lblSalPerSec.BackColor = SystemColors.ActiveCaption;
            lblSalPerSec.Font = new Font("Segoe UI", 9F);
            lblSalPerSec.Location = new Point(144, 54);
            lblSalPerSec.Name = "lblSalPerSec";
            lblSalPerSec.Size = new Size(141, 28);
            lblSalPerSec.TabIndex = 2;
            lblSalPerSec.Text = "label1";
            lblSalPerSec.TextAlign = ContentAlignment.MiddleLeft;
            lblSalPerSec.Click += lblSalPerSec_Click;
            // 
            // lblCost
            // 
            lblCost.BackColor = SystemColors.ActiveCaption;
            lblCost.Font = new Font("Segoe UI", 9F);
            lblCost.Location = new Point(144, 16);
            lblCost.Name = "lblCost";
            lblCost.Size = new Size(141, 32);
            lblCost.TabIndex = 1;
            lblCost.Text = "label1";
            lblCost.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblQuantity
            // 
            lblQuantity.BackColor = SystemColors.ActiveCaption;
            lblQuantity.Font = new Font("Segoe UI", 11F);
            lblQuantity.Location = new Point(6, 24);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(132, 24);
            lblQuantity.TabIndex = 0;
            lblQuantity.Text = "label1";
            lblQuantity.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // progressMining
            // 
            progressMining.Anchor = AnchorStyles.Bottom;
            progressMining.Location = new Point(3, 160);
            progressMining.MarqueeAnimationSpeed = 10;
            progressMining.Name = "progressMining";
            progressMining.Size = new Size(291, 18);
            progressMining.Step = 1;
            progressMining.Style = ProgressBarStyle.Continuous;
            progressMining.TabIndex = 4;
            // 
            // ItemView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.Disable;
            BorderStyle = BorderStyle.Fixed3D;
            CausesValidation = false;
            Controls.Add(progressMining);
            Controls.Add(grpItem);
            Controls.Add(btnBuy);
            Name = "ItemView";
            Size = new Size(301, 181);
            Load += ItemView_Load;
            Paint += ItemView_Paint;
            grpItem.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnBuy;
        private GroupBox grpItem;
        private Label lblCost;
        private Label lblQuantity;
        private Label lblSalPerSec;
        private Label lblTotalSal;
        public ProgressBar progressMining;
        private Label lblTimeLeft;
    }
}
