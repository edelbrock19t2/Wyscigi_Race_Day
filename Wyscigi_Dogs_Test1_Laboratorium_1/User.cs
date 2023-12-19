using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyscigi_Dogs_Test1_Laboratorium_1
{
    internal class User
    {
        public string Name { get; set; }
        public int Cash { get; set; }
        public int ChartId { get; set; }
        public string ChartInfo { get; set; }
        public bool IsBetted { get; set; }
        public int Amount { get; set; }

        public User() { }
        public User(string name, int cash)
        {
            Name = name;
            Cash = cash;
        }


        public string CreateDogBet(int chartId, int amount)
        {
            if(Cash >= amount)
            {
                Cash -= amount;
                this.Amount = amount;
                IsBetted = true;
                this.ChartId = chartId;
                return $"{Name} stawia {amount} na charta numer {chartId}";
            }

            return $"{Name} nie ma wolnych środków na koncie";
        }

        public void ResetUserProperties()
        {
            ChartId = 0;
            IsBetted = false;
            Amount = 0;
        }
    }
}
