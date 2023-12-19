using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wyscigi_Dogs_Test1_Laboratorium_1
{
    internal class Dog
    {
        public int dogId { get; set;  }
        private string dogName;
        readonly int constStartX;
        readonly int constStartY;
        private PictureBox dogPicture;
        private Image dog_image;
        public int? WonDog {  get; set; }

        public Dog(int dogId, string name, int startX, int startY, PictureBox dogPicture)
        {
            this.dogId = dogId;
            this.dogName = name;
            this.constStartX = startX;
            this.constStartY = startY;
            this.dogPicture = dogPicture;
            this.ResetDog();
        }

        public Dog(int dogId, string name, int startX, int startY, PictureBox dogPicture, Image dog_image)
        {
            this.dogId = dogId;
            this.dogName = name;
            this.constStartX = startX;
            this.constStartY = startY;
            this.dogPicture = dogPicture;
            this.dog_image = dog_image;
            this.ResetDog();
        }


        public void DogMoveForward(int step)
        {
            dogPicture.Image.Dispose();
            dogPicture.Image = null;
            dogPicture.Invalidate();
            dogPicture.Update();


            int currentXpos = dogPicture.Location.X;
            dogPicture.Location = new Point(currentXpos + step, constStartY);
            dogPicture.Image = new Bitmap(dog_image);

            dogPicture.Invalidate();
            dogPicture.Update();
            
        }

        public void DogMoveForward2(int step)
        {
            dogPicture.Left = dogPicture.Left + step;
        }

        public void ResetDog()
        {
            dogPicture.Location = new Point(constStartX, constStartY);
        }

        public bool IsWin()
        {
            WonDog = null;
            if (dogPicture.Location.X >= 923)
            {
                MessageBox.Show("Chart " + dogName + "  wygrał wyścig!!!");
                Bet.WinBet(dogId);
                return false;
            }
            return true;
        }
    }
}
