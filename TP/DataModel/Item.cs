using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP
{
   public class Item
    {
        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private float sellPrice;
        public string SellPrice
        {
            get { return sellPrice.ToString("#0'g'#0's'#0'c'"); }
            set { sellPrice = Convert.ToSingle(value); }
        }

        private float buyPrice;
        public string BuyPrice
        {
            get { return buyPrice.ToString("#0'g'#0's'#0'c'"); }
            set { buyPrice = Convert.ToSingle(value); }
        }

        public string MinSell
        {
            get { return (buyPrice / 0.85f).ToString("#0'g'#0's'#0'c'"); }
        }

        private float minSell
        {
            get { return buyPrice / 0.85f; }
        }

        public string Flippable
        {
            get
            {
                if (minSell < sellPrice)
                {
                    return "Flippable";
                }
                else
                {
                    return "no";
                }
            }
        }

        public string Spread
        {
            get { return ((sellPrice - minSell)*0.85).ToString("#0'g'#0's'#0'c'"); }
        }
        


    }
}
