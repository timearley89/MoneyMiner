namespace MoneyMiner.Windows
{
    partial class PrestigeEarned
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
            lblPrestigeText = new Label();
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom;
            btnOk.BackColor = Color.FromArgb(128, 255, 255);
            btnOk.DialogResult = DialogResult.OK;
            btnOk.FlatStyle = FlatStyle.Popup;
            btnOk.Font = new Font("Bahnschrift SemiBold", 12F);
            btnOk.Location = new Point(130, 95);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 50);
            btnOk.TabIndex = 0;
            btnOk.Text = "Thanks!";
            btnOk.UseVisualStyleBackColor = false;
            // 
            // lblPrestigeText
            // 
            lblPrestigeText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblPrestigeText.BackColor = Color.FromArgb(128, 255, 255);
            lblPrestigeText.Font = new Font("Bahnschrift SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPrestigeText.Location = new Point(12, 9);
            lblPrestigeText.Name = "lblPrestigeText";
            lblPrestigeText.Size = new Size(303, 74);
            lblPrestigeText.TabIndex = 1;
            lblPrestigeText.Text = "label1";
            lblPrestigeText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PrestigeEarned
            // 
            AcceptButton = btnOk;
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Tan;
            ClientSize = new Size(327, 157);
            ControlBox = false;
            Controls.Add(lblPrestigeText);
            Controls.Add(btnOk);
            Font = new Font("Bahnschrift SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PrestigeEarned";
            Opacity = 0.75D;
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Prestige Earned";
            TransparencyKey = Color.Transparent;
            ResumeLayout(false);
        }

        #endregion

        private Button btnOk;
        private Label lblPrestigeText;
    }
}