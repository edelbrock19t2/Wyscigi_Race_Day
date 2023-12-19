using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wyscigi_Dogs_Test1_Laboratorium_1
{
    internal class Bet
    {
        public static User[] users {  get; set; }

        public Bet()
        {
            users = new User[3];
        }

        public void addUser(User user, int index)
        {
            users[index] = user;
        }

        public static void WinBet(int? winningChartId)
        {
            if (winningChartId == null)
            {
                MessageBox.Show("WinningCharId is null(((");
                return;
            }
            else
            {
                foreach (User user in users)
                {
                    if (user.ChartId == winningChartId)
                    {
                        user.Cash += GetWinningAmount();
                        MessageBox.Show("Gratulacje user: " + user.Name + ". Wygrywa: +" + GetWinningAmount() + " zł");
                        return;
                    }
                }
            }
            MessageBox.Show("Niestety nikt z uczęstników nie wygrał");
            return;
        }

        private static int GetWinningAmount()
        {
            int result = 0;

            foreach(User user in users)
            {
                result += user.Amount;
            }

            return result;
        }
    }
}
