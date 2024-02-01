namespace MoneyMiner
{
    partial class PauseMenu
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
            lblHeader = new Label();
            sliderVolume = new TrackBar();
            grpVolume = new GroupBox();
            panel1 = new Panel();
            checkEffectsEnabled = new CheckBox();
            checkMusicEnabled = new CheckBox();
            grpLoadSave = new GroupBox();
            panel2 = new Panel();
            btnLoadGame = new Button();
            btnSaveGame = new Button();
            btnMasterReset = new Button();
            btnResume = new Button();
            btnExit = new Button();
            btnStats = new Button();
            btnAbout = new Button();
            loadDialog = new OpenFileDialog();
            saveDialog = new SaveFileDialog();
            grpAutosave = new GroupBox();
            pnlAutosave = new Panel();
            lblIntervalText = new Label();
            numAutosaveInterval = new NumericUpDown();
            checkAutosaveEnabled = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)sliderVolume).BeginInit();
            grpVolume.SuspendLayout();
            panel1.SuspendLayout();
            grpLoadSave.SuspendLayout();
            panel2.SuspendLayout();
            grpAutosave.SuspendLayout();
            pnlAutosave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numAutosaveInterval).BeginInit();
            SuspendLayout();
            // 
            // lblHeader
            // 
            lblHeader.Anchor = AnchorStyles.Top;
            lblHeader.BackColor = Color.FromArgb(128, 255, 255);
            lblHeader.FlatStyle = FlatStyle.Popup;
            lblHeader.Font = new Font("Impact", 16F);
            lblHeader.Location = new Point(144, 20);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(100, 36);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "Settings";
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // sliderVolume
            // 
            sliderVolume.Anchor = AnchorStyles.Top;
            sliderVolume.BackColor = Color.PaleTurquoise;
            sliderVolume.LargeChange = 100;
            sliderVolume.Location = new Point(32, 3);
            sliderVolume.Maximum = 1000;
            sliderVolume.Minimum = 1;
            sliderVolume.Name = "sliderVolume";
            sliderVolume.RightToLeft = RightToLeft.No;
            sliderVolume.Size = new Size(246, 45);
            sliderVolume.SmallChange = 25;
            sliderVolume.TabIndex = 1;
            sliderVolume.TickFrequency = 50;
            sliderVolume.Value = 1;
            sliderVolume.Scroll += sliderVolume_Scroll;
            // 
            // grpVolume
            // 
            grpVolume.Anchor = AnchorStyles.Top;
            grpVolume.BackColor = Color.FromArgb(128, 255, 255);
            grpVolume.Controls.Add(panel1);
            grpVolume.Font = new Font("Bahnschrift SemiBold", 12F, FontStyle.Underline);
            grpVolume.Location = new Point(12, 112);
            grpVolume.Name = "grpVolume";
            grpVolume.Size = new Size(366, 156);
            grpVolume.TabIndex = 2;
            grpVolume.TabStop = false;
            grpVolume.Text = "Audio Settings";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.PaleTurquoise;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(checkEffectsEnabled);
            panel1.Controls.Add(checkMusicEnabled);
            panel1.Controls.Add(sliderVolume);
            panel1.Location = new Point(26, 28);
            panel1.Name = "panel1";
            panel1.Size = new Size(315, 104);
            panel1.TabIndex = 3;
            // 
            // checkEffectsEnabled
            // 
            checkEffectsEnabled.Anchor = AnchorStyles.Bottom;
            checkEffectsEnabled.BackColor = Color.PaleTurquoise;
            checkEffectsEnabled.CheckAlign = ContentAlignment.BottomCenter;
            checkEffectsEnabled.Checked = true;
            checkEffectsEnabled.CheckState = CheckState.Checked;
            checkEffectsEnabled.Font = new Font("Impact", 13F);
            checkEffectsEnabled.ImageAlign = ContentAlignment.BottomCenter;
            checkEffectsEnabled.Location = new Point(158, 49);
            checkEffectsEnabled.Name = "checkEffectsEnabled";
            checkEffectsEnabled.Size = new Size(120, 39);
            checkEffectsEnabled.TabIndex = 5;
            checkEffectsEnabled.Text = "Enable Effects";
            checkEffectsEnabled.TextAlign = ContentAlignment.TopCenter;
            checkEffectsEnabled.TextImageRelation = TextImageRelation.TextAboveImage;
            checkEffectsEnabled.UseVisualStyleBackColor = false;
            checkEffectsEnabled.CheckedChanged += checkEffectsEnabled_CheckedChanged;
            // 
            // checkMusicEnabled
            // 
            checkMusicEnabled.Anchor = AnchorStyles.Top;
            checkMusicEnabled.BackColor = Color.PaleTurquoise;
            checkMusicEnabled.CheckAlign = ContentAlignment.BottomCenter;
            checkMusicEnabled.Checked = true;
            checkMusicEnabled.CheckState = CheckState.Checked;
            checkMusicEnabled.Font = new Font("Impact", 13F);
            checkMusicEnabled.ImageAlign = ContentAlignment.BottomCenter;
            checkMusicEnabled.Location = new Point(32, 49);
            checkMusicEnabled.Name = "checkMusicEnabled";
            checkMusicEnabled.Size = new Size(120, 39);
            checkMusicEnabled.TabIndex = 4;
            checkMusicEnabled.Text = "Enable Music";
            checkMusicEnabled.TextAlign = ContentAlignment.TopCenter;
            checkMusicEnabled.TextImageRelation = TextImageRelation.TextAboveImage;
            checkMusicEnabled.UseVisualStyleBackColor = false;
            checkMusicEnabled.CheckedChanged += checkMusicEnabled_CheckedChanged;
            // 
            // grpLoadSave
            // 
            grpLoadSave.Anchor = AnchorStyles.Top;
            grpLoadSave.BackColor = Color.FromArgb(128, 255, 255);
            grpLoadSave.Controls.Add(panel2);
            grpLoadSave.Font = new Font("Bahnschrift SemiBold", 12F, FontStyle.Underline);
            grpLoadSave.Location = new Point(12, 274);
            grpLoadSave.Name = "grpLoadSave";
            grpLoadSave.Size = new Size(366, 107);
            grpLoadSave.TabIndex = 3;
            grpLoadSave.TabStop = false;
            grpLoadSave.Text = "Load/Save";
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            panel2.BackColor = Color.PaleTurquoise;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(btnLoadGame);
            panel2.Controls.Add(btnSaveGame);
            panel2.Location = new Point(26, 28);
            panel2.Name = "panel2";
            panel2.Size = new Size(315, 55);
            panel2.TabIndex = 0;
            // 
            // btnLoadGame
            // 
            btnLoadGame.Anchor = AnchorStyles.Right;
            btnLoadGame.BackColor = Color.Cyan;
            btnLoadGame.FlatStyle = FlatStyle.Popup;
            btnLoadGame.Font = new Font("Impact", 14F);
            btnLoadGame.Location = new Point(193, 3);
            btnLoadGame.Name = "btnLoadGame";
            btnLoadGame.Size = new Size(115, 45);
            btnLoadGame.TabIndex = 1;
            btnLoadGame.Text = "Load Game...";
            btnLoadGame.UseVisualStyleBackColor = false;
            btnLoadGame.Click += btnLoadGame_Click;
            // 
            // btnSaveGame
            // 
            btnSaveGame.Anchor = AnchorStyles.Left;
            btnSaveGame.BackColor = Color.Cyan;
            btnSaveGame.FlatStyle = FlatStyle.Popup;
            btnSaveGame.Font = new Font("Impact", 14F);
            btnSaveGame.Location = new Point(3, 3);
            btnSaveGame.Name = "btnSaveGame";
            btnSaveGame.Size = new Size(115, 45);
            btnSaveGame.TabIndex = 0;
            btnSaveGame.Text = "Save Game...";
            btnSaveGame.UseVisualStyleBackColor = false;
            btnSaveGame.Click += btnSaveGame_Click;
            // 
            // btnMasterReset
            // 
            btnMasterReset.Anchor = AnchorStyles.Bottom;
            btnMasterReset.BackColor = Color.DarkRed;
            btnMasterReset.Font = new Font("Impact", 12F);
            btnMasterReset.ForeColor = SystemColors.ControlLightLight;
            btnMasterReset.Location = new Point(107, 614);
            btnMasterReset.Name = "btnMasterReset";
            btnMasterReset.Size = new Size(174, 36);
            btnMasterReset.TabIndex = 4;
            btnMasterReset.Text = "Reset Game";
            btnMasterReset.UseVisualStyleBackColor = false;
            btnMasterReset.Click += btnMasterReset_Click;
            // 
            // btnResume
            // 
            btnResume.Anchor = AnchorStyles.Top;
            btnResume.BackColor = Color.FromArgb(128, 255, 255);
            btnResume.FlatStyle = FlatStyle.Popup;
            btnResume.Font = new Font("Impact", 14F);
            btnResume.Location = new Point(107, 67);
            btnResume.Name = "btnResume";
            btnResume.Size = new Size(174, 36);
            btnResume.TabIndex = 5;
            btnResume.Text = "Return";
            btnResume.UseVisualStyleBackColor = false;
            btnResume.Click += btnResume_Click;
            // 
            // btnExit
            // 
            btnExit.Anchor = AnchorStyles.Bottom;
            btnExit.BackColor = Color.FromArgb(128, 255, 255);
            btnExit.FlatStyle = FlatStyle.Popup;
            btnExit.Font = new Font("Impact", 14F);
            btnExit.Location = new Point(107, 481);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(174, 36);
            btnExit.TabIndex = 6;
            btnExit.Text = "Exit Game";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // btnStats
            // 
            btnStats.Anchor = AnchorStyles.Bottom;
            btnStats.BackColor = Color.FromArgb(128, 255, 255);
            btnStats.FlatStyle = FlatStyle.Popup;
            btnStats.Font = new Font("Impact", 14F);
            btnStats.Location = new Point(107, 523);
            btnStats.Name = "btnStats";
            btnStats.Size = new Size(174, 30);
            btnStats.TabIndex = 7;
            btnStats.Text = "Stats...";
            btnStats.UseVisualStyleBackColor = false;
            btnStats.Click += btnStats_Click;
            // 
            // btnAbout
            // 
            btnAbout.Anchor = AnchorStyles.Bottom;
            btnAbout.BackColor = Color.FromArgb(128, 255, 255);
            btnAbout.FlatStyle = FlatStyle.Popup;
            btnAbout.Font = new Font("Impact", 14F);
            btnAbout.Location = new Point(107, 559);
            btnAbout.Name = "btnAbout";
            btnAbout.Size = new Size(174, 29);
            btnAbout.TabIndex = 8;
            btnAbout.Text = "About...";
            btnAbout.UseVisualStyleBackColor = false;
            btnAbout.Click += btnAbout_Click;
            // 
            // loadDialog
            // 
            loadDialog.DefaultExt = "mmf";
            loadDialog.FileName = "GameState.mmf";
            loadDialog.Filter = "MoneyMiner Saves|*.mmf|All Files|*.*";
            loadDialog.RestoreDirectory = true;
            loadDialog.Title = "Load MoneyMiner From:";
            // 
            // saveDialog
            // 
            saveDialog.DefaultExt = "mmf";
            saveDialog.FileName = "GameState.mmf";
            saveDialog.Filter = "MoneyMiner Saves|*.mmf|All Files|*.*";
            saveDialog.Title = "Save MoneyMiner To:";
            // 
            // grpAutosave
            // 
            grpAutosave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            grpAutosave.BackColor = Color.FromArgb(128, 255, 255);
            grpAutosave.CausesValidation = false;
            grpAutosave.Controls.Add(pnlAutosave);
            grpAutosave.Font = new Font("Bahnschrift SemiBold", 12F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            grpAutosave.Location = new Point(12, 387);
            grpAutosave.Name = "grpAutosave";
            grpAutosave.Size = new Size(180, 88);
            grpAutosave.TabIndex = 9;
            grpAutosave.TabStop = false;
            grpAutosave.Text = "Auto-Save";
            // 
            // pnlAutosave
            // 
            pnlAutosave.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlAutosave.BackColor = Color.PaleTurquoise;
            pnlAutosave.BorderStyle = BorderStyle.Fixed3D;
            pnlAutosave.CausesValidation = false;
            pnlAutosave.Controls.Add(lblIntervalText);
            pnlAutosave.Controls.Add(numAutosaveInterval);
            pnlAutosave.Controls.Add(checkAutosaveEnabled);
            pnlAutosave.Location = new Point(6, 19);
            pnlAutosave.Name = "pnlAutosave";
            pnlAutosave.Size = new Size(168, 63);
            pnlAutosave.TabIndex = 0;
            // 
            // lblIntervalText
            // 
            lblIntervalText.Anchor = AnchorStyles.Bottom;
            lblIntervalText.AutoSize = true;
            lblIntervalText.Location = new Point(67, 31);
            lblIntervalText.Name = "lblIntervalText";
            lblIntervalText.Size = new Size(65, 19);
            lblIntervalText.TabIndex = 2;
            lblIntervalText.Text = "Minutes";
            // 
            // numAutosaveInterval
            // 
            numAutosaveInterval.Anchor = AnchorStyles.Bottom;
            numAutosaveInterval.BackColor = Color.Cyan;
            numAutosaveInterval.BorderStyle = BorderStyle.FixedSingle;
            numAutosaveInterval.Enabled = false;
            numAutosaveInterval.Font = new Font("Bahnschrift SemiBold", 10F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            numAutosaveInterval.Location = new Point(28, 29);
            numAutosaveInterval.Maximum = new decimal(new int[] { 15, 0, 0, 0 });
            numAutosaveInterval.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numAutosaveInterval.Name = "numAutosaveInterval";
            numAutosaveInterval.Size = new Size(37, 24);
            numAutosaveInterval.TabIndex = 1;
            numAutosaveInterval.TextAlign = HorizontalAlignment.Center;
            numAutosaveInterval.Value = new decimal(new int[] { 5, 0, 0, 0 });
            numAutosaveInterval.ValueChanged += this.numAutosaveInterval_ValueChanged;
            // 
            // checkAutosaveEnabled
            // 
            checkAutosaveEnabled.Anchor = AnchorStyles.Top;
            checkAutosaveEnabled.AutoSize = true;
            checkAutosaveEnabled.CausesValidation = false;
            checkAutosaveEnabled.Font = new Font("Impact", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkAutosaveEnabled.Location = new Point(9, 3);
            checkAutosaveEnabled.Name = "checkAutosaveEnabled";
            checkAutosaveEnabled.Size = new Size(145, 25);
            checkAutosaveEnabled.TabIndex = 0;
            checkAutosaveEnabled.Text = "Enable Autosave";
            checkAutosaveEnabled.UseVisualStyleBackColor = true;
            checkAutosaveEnabled.CheckedChanged += checkAutosaveEnabled_CheckedChanged;
            // 
            // PauseMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Tan;
            ClientSize = new Size(390, 662);
            ControlBox = false;
            Controls.Add(grpAutosave);
            Controls.Add(btnAbout);
            Controls.Add(btnStats);
            Controls.Add(btnExit);
            Controls.Add(btnResume);
            Controls.Add(btnMasterReset);
            Controls.Add(grpLoadSave);
            Controls.Add(grpVolume);
            Controls.Add(lblHeader);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PauseMenu";
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "MoneyMiner";
            Move += PauseMenu_Move;
            Resize += PauseMenu_Resize;
            ((System.ComponentModel.ISupportInitialize)sliderVolume).EndInit();
            grpVolume.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            grpLoadSave.ResumeLayout(false);
            panel2.ResumeLayout(false);
            grpAutosave.ResumeLayout(false);
            pnlAutosave.ResumeLayout(false);
            pnlAutosave.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numAutosaveInterval).EndInit();
            ResumeLayout(false);
        }

        #endregion
        public TrackBar sliderVolume;
        private CheckBox checkMusicEnabled;
        private CheckBox checkEffectsEnabled;
        private Panel panel1;
        private Panel panel2;
        private Button btnSaveGame;
        private Button btnLoadGame;
        public Label lblHeader;
        public GroupBox grpVolume;
        public GroupBox grpLoadSave;
        public Button btnMasterReset;
        private Button btnResume;
        private Button btnExit;
        private Button btnStats;
        private Button btnAbout;
        private OpenFileDialog loadDialog;
        private SaveFileDialog saveDialog;
        private GroupBox grpAutosave;
        private Panel pnlAutosave;
        private CheckBox checkAutosaveEnabled;
        private Label lblIntervalText;
        private NumericUpDown numAutosaveInterval;
    }
}