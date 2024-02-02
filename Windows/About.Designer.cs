namespace MoneyMiner.Windows
{
    partial class About
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
            ellipseButton2 = new Controls.EllipseButton();
            SuspendLayout();
            // 
            // ellipseButton2
            // 
            ellipseButton2.Location = new Point(124, 195);
            ellipseButton2.Name = "ellipseButton2";
            ellipseButton2.Size = new Size(215, 100);
            ellipseButton2.TabIndex = 0;
            ellipseButton2.Text = "ellipseButton2";
            ellipseButton2.UseVisualStyleBackColor = true;
            ellipseButton2.Paint += ellipseButton2_Paint;
            // 
            // About
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ellipseButton2);
            Name = "About";
            Text = "About";
            Load += About_Load;
            ResumeLayout(false);
        }

        #endregion

        private Controls.EllipseButton ellipseButton1;
        private Controls.EllipseButton ellipseButton2;
    }
}