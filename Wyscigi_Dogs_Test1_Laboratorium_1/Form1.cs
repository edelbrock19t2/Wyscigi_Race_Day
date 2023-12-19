using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Wyscigi_Dogs_Test1_Laboratorium_1
{
    public partial class Form1 : Form
    {
        private Dog dog1;
        private Dog dog2;
        private Dog dog3;

        private User user1;
        private User user2;
        private User user3;

        private Bet bet;
        private Random random;

        public Form1()
        {
            InitializeComponent();

            Image dog1_image = Image.FromFile("C:\\Users\\Xopero\\Documents\\Praktyki\\Wyscigi_Dogs_Test1_Laboratorium_1\\Wyscigi_Dogs_Test1_Laboratorium_1\\bin\\Debug\\dog_1_64.png");
            Image dog2_image = Image.FromFile("C:\\Users\\Xopero\\Documents\\Praktyki\\Wyscigi_Dogs_Test1_Laboratorium_1\\Wyscigi_Dogs_Test1_Laboratorium_1\\bin\\Debug\\dog_2_64.png");
            Image dog3_image = Image.FromFile("C:\\Users\\Xopero\\Documents\\Praktyki\\Wyscigi_Dogs_Test1_Laboratorium_1\\Wyscigi_Dogs_Test1_Laboratorium_1\\bin\\Debug\\dog_3_64.png");

            dog1 = new Dog(1, "Dog number 1", 186, 62, pictureDog1, dog1_image);
            dog2 = new Dog(2, "Dog number 2", 186, 168, pictureDog2, dog2_image);
            dog3 = new Dog(3, "Dog number 3", 186, 272, pictureDog3, dog3_image);

            user1 = new User("AlekSASS", 61);
            user2 = new User("DimASS", 62);
            user3 = new User("MarceLINGUS", 36);

            bet = new Bet();
            bet.addUser(user1, 0);
            bet.addUser(user2, 1);
            bet.addUser(user3, 2);

            random = new Random();

            firstUserRadioBtn.Checked = false;
            secondUserRadioBtn.Checked = false;
            thirdUserRadioBtn.Checked = false;

            firstUserInfo.Text = writeInfoAboutBet(user1, null, null);
            secondUserInfo.Text = writeInfoAboutBet(user2, null, null);
            thirdUserInfo.Text = writeInfoAboutBet(user3, null, null);

            UpdateForm();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {

        } // 234 strona w książce z laboratotrium

        private void button1_Click(object sender, EventArgs e)
        {
            dogRun();
            user1.ResetUserProperties();
            user2.ResetUserProperties();
            user3.ResetUserProperties();
        }

        private void dogRun()
        {
            allButtonsEnableControl(false);

            while (dog1.IsWin() && dog2.IsWin() && dog3.IsWin())
            {
                int randomIndexDog = random.Next(1, 4);
                int randomForwardStep = random.Next(1, 15);

                if (randomIndexDog == 1)
                {
                    dog1.DogMoveForward(randomForwardStep);
                }
                else if (randomIndexDog == 2)
                {
                    dog2.DogMoveForward(randomForwardStep);
                }
                else
                {
                    dog3.DogMoveForward(randomForwardStep);
                }

                Thread.Sleep(100);
            }

            dog1.ResetDog();
            dog2.ResetDog();
            dog3.ResetDog();

            firstUserInfo.Text = writeInfoAboutBet(user1, null, null);
            secondUserInfo.Text = writeInfoAboutBet(user2, null, null);
            thirdUserInfo.Text = writeInfoAboutBet(user3, null, null);

            allButtonsEnableControl(true);
            UpdateForm();
        }

        private void allButtonsEnableControl(bool control)
        {
            startButton.Enabled = control;
            firstUserRadioBtn.Enabled = control;
            secondUserRadioBtn.Enabled = control;
            thirdUserRadioBtn.Enabled = control;
            submitChart.Enabled = control;

            groupBox1.Enabled = control;

            if (!control)
            {
                firstUserRadioBtn.Checked = control;
                secondUserRadioBtn.Checked = control;
                thirdUserRadioBtn.Checked = control;
            }

            nameLabel.Text = "None";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            nameLabel.Text = user2.Name;
            userAmount.Value = userAmount.Minimum;
            chartNumber.Value = chartNumber.Minimum;

            if (user2.IsBetted) submitChart.Enabled = false;
            else submitChart.Enabled = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            userAmount.Value = userAmount.Minimum;
            chartNumber.Value = chartNumber.Minimum;

            // First User Radio Button
            nameLabel.Text = user1.Name;
            if (user1.IsBetted) submitChart.Enabled = false;
            else submitChart.Enabled = true;
        }

        private void submitChart_Click(object sender, EventArgs e)
        {
            if (firstUserRadioBtn.Checked)
            {
                acceptBet(user1);

                firstUserInfo.Text = writeInfoAboutBet(user1, user1.Amount, user1.ChartId);
            }
            else if (secondUserRadioBtn.Checked)
            {
                acceptBet(user2);

                secondUserInfo.Text = writeInfoAboutBet(user2, user2.Amount, user2.ChartId);
            }
            else
            {
                acceptBet(user3);

                thirdUserInfo.Text = writeInfoAboutBet(user3, user3.Amount, user3.ChartId);
            }

            UpdateForm();
        }

        private string writeInfoAboutBet(User user, int? amount, int? chartId)
        {
            if (amount != null && chartId != null)
            {
                return $"{user.Name} stawia {amount} zł na charta numer {chartId}";
            }

            return $"{user.Name} nie zawarł zakładu";
        }

        private void acceptBet(User user)
        {
            if (Convert.ToInt32(userAmount.Value) > user.Cash)
            {
                userAmount.Value = userAmount.Minimum;
                MessageBox.Show(user.Name + " nie ma wolnych środków");
                user.IsBetted = false;
            }
            else
            {
                user.CreateDogBet(Convert.ToInt32(chartNumber.Value), Convert.ToInt32(userAmount.Value));
                user.IsBetted = true;
                submitChart.Enabled = false;
            }
        }

        private void thirdUserRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            nameLabel.Text = user3.Name;
            userAmount.Value = userAmount.Minimum;
            chartNumber.Value = chartNumber.Minimum;

            if (user3.IsBetted) submitChart.Enabled = false;
            else submitChart.Enabled = true;
        }

        public void UpdateForm()
        {
            firstUserRadioBtn.Text = user1.Name + " ma " + user1.Cash + " zł";
            secondUserRadioBtn.Text = user2.Name + " ma " + user2.Cash + " zł";
            thirdUserRadioBtn.Text = user3.Name + " ma " + user3.Cash + " zł";
        }
    }
}
