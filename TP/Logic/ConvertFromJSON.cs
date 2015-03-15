using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;


namespace TP
{
    public class ConvertFromJSON
    {

        //gets the newly converted string as an Item object
        //the api always returns id, name, buy, sell
        public static List<Item> GetConverted(string[] item)
        {
            List<Item> itemList = new List<Item>();
            Item newItem;

            if (!(item[0].Length < 1))
            {
                for (int i = 0; i < item.Length; i++)
                {
                    if (i % 4 == 0)
                    {
                        newItem = new Item();

                        newItem.Id = item[i];
                        newItem.Name = item[i + 1];
                        newItem.BuyPrice = item[i + 2].ToString();
                        newItem.SellPrice = item[i + 3].ToString();

                        itemList.Add(newItem);
                    }
                }
            }
            return itemList;
        }

        //takes a longer JSON string and turns it into a string array 
        //used for batch api calls
        //{"results":[[12229,"Chocolate Bar",262,317],[12261,"Bowl of Cherry Pie Filling",153,206]]}
        public static List<Item> ConvertMultipleString(string APIResponse)
        {

            string RawData;
            string[] splitString;

            //remove useless JSON data
            RawData = APIResponse.Substring(12, APIResponse.Length - (12 + 2));

            //remove all extra brackets
            RawData = RawData.Replace("[", "");
            RawData = RawData.Replace("]", "");

            //split up the entire string by commas, every 4 values make up 1 item
            splitString = RawData.Split(',');

            return GetConverted(splitString);
        }
    }
}
