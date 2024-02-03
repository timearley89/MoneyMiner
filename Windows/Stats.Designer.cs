namespace MoneyMiner.Windows
{
    partial class Stats
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
            btnOk = new Button();
            lblStats = new Label();
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom;
            btnOk.BackColor = Color.FromArgb(128, 255, 255);
            btnOk.DialogResult = DialogResult.OK;
            btnOk.FlatStyle = FlatStyle.Popup;
            btnOk.Font = new Font("Bahnschrift SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnOk.Location = new Point(137, 297);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(92, 40);
            btnOk.TabIndex = 0;
            btnOk.Text = "Close";
            btnOk.UseVisualStyleBackColor = false;
            btnOk.Click += btnOk_Click;
            // 
            // lblStats
            // 
            lblStats.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblStats.BackColor = Color.FromArgb(128, 255, 255);
            lblStats.BorderStyle = BorderStyle.Fixed3D;
            lblStats.Font = new Font("Bahnschrift SemiBold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStats.Location = new Point(12, 9);
            lblStats.Margin = new Padding(3, 0, 3, 5);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(343, 280);
            lblStats.TabIndex = 1;
            lblStats.Text = "label1";
            lblStats.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Stats
            // 
            AcceptButton = btnOk;
            AutoScaleMode = AutoScaleMode.None;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.Tan;
            ClientSize = new Size(367, 349);
            ControlBox = false;
            Controls.Add(lblStats);
            Controls.Add(btnOk);
            Font = new Font("Bahnschrift SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Stats";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            Text = "MoneyMiner Stats";
            ResumeLayout(false);
        }

        #endregion

        private Button btnOk;
        private Label lblStats;
    }
}