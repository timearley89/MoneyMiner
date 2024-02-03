namespace MoneyMiner.Windows
{
    partial class WelcomeBack
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
            btnOK = new Button();
            lblWelcomeText = new Label();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom;
            btnOK.BackColor = Color.FromArgb(128, 255, 255);
            btnOK.DialogResult = DialogResult.OK;
            btnOK.FlatStyle = FlatStyle.Popup;
            btnOK.Font = new Font("Bahnschrift SemiBold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnOK.Location = new Point(248, 140);
            btnOK.Margin = new Padding(200, 5, 200, 15);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(70, 60);
            btnOK.TabIndex = 0;
            btnOK.Text = "Great!";
            btnOK.UseVisualStyleBackColor = false;
            btnOK.Click += btnOK_Click;
            // 
            // lblWelcomeText
            // 
            lblWelcomeText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblWelcomeText.BackColor = Color.FromArgb(128, 255, 255);
            lblWelcomeText.BorderStyle = BorderStyle.Fixed3D;
            lblWelcomeText.Font = new Font("Bahnschrift SemiBold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWelcomeText.Location = new Point(12, 24);
            lblWelcomeText.Margin = new Padding(3, 15, 3, 0);
            lblWelcomeText.Name = "lblWelcomeText";
            lblWelcomeText.Size = new Size(539, 111);
            lblWelcomeText.TabIndex = 1;
            lblWelcomeText.Text = "label1";
            lblWelcomeText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // WelcomeBack
            // 
            AcceptButton = btnOK;
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Tan;
            ClientSize = new Size(563, 211);
            ControlBox = false;
            Controls.Add(lblWelcomeText);
            Controls.Add(btnOK);
            Font = new Font("Bahnschrift SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WelcomeBack";
            Opacity = 0.75D;
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Since you've been gone...";
            TransparencyKey = Color.Transparent;
            Load += WelcomeBack_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnOK;
        private Label lblWelcomeText;
    }
}