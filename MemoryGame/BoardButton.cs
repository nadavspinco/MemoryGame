using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;
using MemoryGameLogic;

namespace MemoryGameUi
{
    public class BoardButton : PictureBox
    {
        private static readonly Image sr_DeflutImage  =  global::MemoryGame.Properties.Resources.ButtonDeafultPicture; 
        public Pair<int, int> LocationInBoard { get; }
        public Label ColorLabel { get; set; }

        public BoardButton(int i_Row, int i_Col)
        {
            ColorLabel = new Label();
            ColorLabel.AutoSize = false;
            ColorLabel.Visible = true;
            LocationInBoard = new Pair<int, int>(i_Row, i_Col);
            Image = sr_DeflutImage;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public override Color BackColor
        {
            get { return this.ColorLabel.BackColor; }
            set { ColorLabel.BackColor = value; }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ColorLabel.Size = new Size(this.Size.Width + 10, this.Size.Height + 10);
        }
        


        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            Point locationToColorLabel = new Point(Location.X - 5 , Location.Y -5);
            ColorLabel.Location = locationToColorLabel;
        }

        
        public void SetToDefult()
        {
            Enabled = true;
            Image = sr_DeflutImage;
        }
    }
}
