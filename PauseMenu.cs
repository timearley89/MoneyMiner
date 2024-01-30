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
        public PauseMenu(frmMain Obj)
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
            this.Location = pauseLoc;
            myLocation = this.Location;
            this.sliderVolume.Value = FrmMainObj.MusicVolume;
            this.checkMusicEnabled.Checked = FrmMainObj.MusicEnabled;
            this.checkEffectsEnabled.Checked = FrmMainObj.FXEnabled;
        }

        private void sliderVolume_Scroll(object sender, EventArgs e)
        {
            //Set volume in frmMain object
            FrmMainObj.MusicVolume = sliderVolume.Value;
            FrmMainObj.FXVolume = sliderVolume.Value;
            FrmMainObj.SetAudioVolume(sliderVolume.Value, sliderVolume.Value);
        }

        private void checkMusicEnabled_CheckedChanged(object sender, EventArgs e)
        {
            FrmMainObj.ToggleMusic(((CheckBox)sender).Checked);
        }

        private void checkEffectsEnabled_CheckedChanged(object sender, EventArgs e)
        {
            FrmMainObj.ToggleFX(((CheckBox)sender).Checked);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            FrmMainObj.Close();
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
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
    }
}
