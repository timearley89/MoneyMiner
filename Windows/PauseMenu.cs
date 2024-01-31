using FirstClicker;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoneyMiner
{
    public partial class PauseMenu : Form
    {
        private frmMain FrmMainObj;
        private int mySizeX;
        private int mySizeY;
        private Point myLocation;
        public PauseMenu(frmMain Obj, Game myGame)
        {
            InitializeComponent();
            //get a reference to the active main form
            FrmMainObj = Obj;
            mySizeX = this.Size.Width;
            mySizeY = this.Size.Height;
            int centeroffrmmainx;
            int centeroffrmmainy;
            centeroffrmmainx = Obj.Location.X + (Obj.Width / 2);
            centeroffrmmainy = Obj.Location.Y + (Obj.Height / 2);
            Point pauseLoc = new Point(centeroffrmmainx - (this.Width / 2), centeroffrmmainy - (this.Height / 2));
            myLocation = pauseLoc;
            this.Location = myLocation; //fire the move event which will set the window location to myLocation.
            this.sliderVolume.Value = myGame.MusicVolume;
            this.checkMusicEnabled.Checked = myGame.MusicEnabled;
            this.checkEffectsEnabled.Checked = myGame.FXEnabled;
        }

        private void sliderVolume_Scroll(object sender, EventArgs e)
        {
            //Set volume in frmMain object
            FrmMainObj.SetAudioVolume(sliderVolume.Value, sliderVolume.Value);
        }

        private void checkMusicEnabled_CheckedChanged(object sender, EventArgs e)
        {
            FrmMainObj.ToggleMusic(((CheckBox)sender).Checked);
            FrmMainObj.PlaySound(SoundList.ClickSound);
        }

        private void checkEffectsEnabled_CheckedChanged(object sender, EventArgs e)
        {
            FrmMainObj.ToggleFX(((CheckBox)sender).Checked);
            FrmMainObj.PlaySound(SoundList.ClickSound);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            FrmMainObj.PlaySound(SoundList.ClickSound);
            FrmMainObj.Close();
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            FrmMainObj.PlaySound(SoundList.ClickSound);
            this.Close();
        }

        private void PauseMenu_Resize(object sender, EventArgs e)
        {
            //don't need resizing happening
            Size newsize = new Size(this.mySizeX, this.mySizeY);
            this.Size = newsize;
        }

        private void PauseMenu_Move(object sender, EventArgs e)
        {
            this.Location = this.myLocation;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                FrmMainObj.PlaySound(SoundList.ClickSound);
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnSaveGame_Click(object sender, EventArgs e)
        {
            FrmMainObj.PlaySound(SoundList.ClickSound);
            //temp for testing
            saveDialog.InitialDirectory = Environment.CurrentDirectory;
            DialogResult saveresult = saveDialog.ShowDialog();
            if (saveresult == DialogResult.OK)
            {
                //user selected a valid save location and file
                FrmMainObj.SaveGame(Path.GetFullPath(saveDialog.FileName));
                //-Didn't actually write to location
            }
        }

        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            FrmMainObj.PlaySound(SoundList.ClickSound);
            //temp for testing
            loadDialog.InitialDirectory = Environment.CurrentDirectory;
            DialogResult loadresult = loadDialog.ShowDialog();
            if (loadresult == DialogResult.OK)
            {
                FrmMainObj.LoadGame(Path.GetFullPath(loadDialog.FileName));
            }
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            if (FrmMainObj != null)
            {
                FrmMainObj.btnStats_Click(sender, e);
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            FrmMainObj.PlaySound(SoundList.ClickSound);
            FrmMainObj.ShowAbout();
        }

        private void btnMasterReset_Click(object sender, EventArgs e)
        {
            FrmMainObj.PlaySound(SoundList.ClickSound);
        }
    }
}
