using System.Collections.Generic;
using System.IO;
 

namespace TP.DataAccess
{
    class DataAccess
    {
        private const string dir = @".";
        private const string path = dir + "itemids.txt";

        public static string[] LoadItems()
        {
            string[] itemIds = {};
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            StreamReader textIn = new StreamReader(
                new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read));


            while (textIn.Peek() != -1)
            {
                string row = textIn.ReadLine();
                itemIds = row.Split(',');
            }


            textIn.Close(); //always close

            return itemIds;
        }


        public static void SaveItems(List<string> ids)
        {
            StreamWriter textOut = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write));

            foreach (var id in ids)
            {
                textOut.Write(id + ",");
            }
            textOut.Close();
        }
    }
}
