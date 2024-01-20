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
            lblTotalSal = new Label();
            lblSalPerSec = new Label();
            lblCost = new Label();
            lblQuantity = new Label();
            grpItem.SuspendLayout();
            SuspendLayout();
            // 
            // btnBuy
            // 
            btnBuy.Location = new Point(3, 70);
            btnBuy.Name = "btnBuy";
            btnBuy.Size = new Size(291, 45);
            btnBuy.TabIndex = 0;
            btnBuy.Text = "Purchase x1";
            btnBuy.UseVisualStyleBackColor = true;
            btnBuy.Click += btnBuy_Click;
            // 
            // grpItem
            // 
            grpItem.Controls.Add(lblTotalSal);
            grpItem.Controls.Add(lblSalPerSec);
            grpItem.Controls.Add(lblCost);
            grpItem.Controls.Add(lblQuantity);
            grpItem.Location = new Point(3, 3);
            grpItem.Name = "grpItem";
            grpItem.Size = new Size(291, 61);
            grpItem.TabIndex = 1;
            grpItem.TabStop = false;
            grpItem.Text = "groupBox1";
            // 
            // lblTotalSal
            // 
            lblTotalSal.AutoSize = true;
            lblTotalSal.Location = new Point(6, 34);
            lblTotalSal.Name = "lblTotalSal";
            lblTotalSal.Size = new Size(38, 15);
            lblTotalSal.TabIndex = 3;
            lblTotalSal.Text = "label1";
            // 
            // lblSalPerSec
            // 
            lblSalPerSec.AutoSize = true;
            lblSalPerSec.Location = new Point(144, 34);
            lblSalPerSec.Name = "lblSalPerSec";
            lblSalPerSec.Size = new Size(38, 15);
            lblSalPerSec.TabIndex = 2;
            lblSalPerSec.Text = "label1";
            // 
            // lblCost
            // 
            lblCost.AutoSize = true;
            lblCost.Location = new Point(144, 19);
            lblCost.Name = "lblCost";
            lblCost.Size = new Size(38, 15);
            lblCost.TabIndex = 1;
            lblCost.Text = "label1";
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Location = new Point(6, 19);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(38, 15);
            lblQuantity.TabIndex = 0;
            lblQuantity.Text = "label1";
            // 
            // ItemView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(grpItem);
            Controls.Add(btnBuy);
            Name = "ItemView";
            Size = new Size(297, 118);
            Load += ItemView_Load;
            Paint += ItemView_Paint;
            grpItem.ResumeLayout(false);
            grpItem.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnBuy;
        private GroupBox grpItem;
        private Label lblCost;
        private Label lblQuantity;
        private Label lblSalPerSec;
        private Label lblTotalSal;
    }
}
