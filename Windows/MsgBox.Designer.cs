namespace MoneyMiner.Windows
{
    partial class MsgBox
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
            btnYes = new Button();
            btnNo = new Button();
            lblMsgText = new Label();
            SuspendLayout();
            // 
            // btnYes
            // 
            btnYes.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnYes.BackColor = Color.FromArgb(128, 255, 255);
            btnYes.DialogResult = DialogResult.Yes;
            btnYes.FlatStyle = FlatStyle.Popup;
            btnYes.Font = new Font("Bahnschrift SemiBold", 12F, FontStyle.Bold);
            btnYes.Location = new Point(59, 91);
            btnYes.Margin = new Padding(50, 3, 3, 3);
            btnYes.Name = "btnYes";
            btnYes.Size = new Size(70, 40);
            btnYes.TabIndex = 0;
            btnYes.Text = "Yes";
            btnYes.UseVisualStyleBackColor = false;
            // 
            // btnNo
            // 
            btnNo.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnNo.BackColor = Color.FromArgb(128, 255, 255);
            btnNo.DialogResult = DialogResult.No;
            btnNo.FlatStyle = FlatStyle.Popup;
            btnNo.Font = new Font("Bahnschrift SemiBold", 12F, FontStyle.Bold);
            btnNo.Location = new Point(234, 91);
            btnNo.Margin = new Padding(3, 3, 50, 3);
            btnNo.Name = "btnNo";
            btnNo.Size = new Size(70, 40);
            btnNo.TabIndex = 1;
            btnNo.Text = "No";
            btnNo.UseVisualStyleBackColor = false;
            // 
            // lblMsgText
            // 
            lblMsgText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblMsgText.BackColor = Color.FromArgb(128, 255, 255);
            lblMsgText.BorderStyle = BorderStyle.Fixed3D;
            lblMsgText.Font = new Font("Bahnschrift SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMsgText.Location = new Point(12, 9);
            lblMsgText.Margin = new Padding(3, 0, 3, 10);
            lblMsgText.Name = "lblMsgText";
            lblMsgText.Size = new Size(339, 69);
            lblMsgText.TabIndex = 2;
            lblMsgText.Text = "label1";
            lblMsgText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MsgBox
            // 
            AcceptButton = btnYes;
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Tan;
            CancelButton = btnNo;
            ClientSize = new Size(363, 143);
            ControlBox = false;
            Controls.Add(lblMsgText);
            Controls.Add(btnNo);
            Controls.Add(btnYes);
            Font = new Font("Bahnschrift SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MsgBox";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Reset To Earn Prestige Points?";
            TransparencyKey = Color.Transparent;
            ResumeLayout(false);
        }

        #endregion

        private Button btnYes;
        private Button btnNo;
        private Label lblMsgText;
    }
}