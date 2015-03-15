using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TP
{
    class APIAccess
    {
        //fetches one item at a time
        //useful for adding individual items one by one
        //not so good for lots of items in a batch
        public static async Task<string> FetchSingleAPIDataAsync(string id)
        {
            String APIdata = "something didn't work";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://api.gw2tp.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //gets the item by item id
                HttpResponseMessage response = await client.GetAsync("/1/items?ids="+id+"&fields=name,buy,sell");

                if (response.IsSuccessStatusCode)
                {
                   
                    APIdata = await response.Content.ReadAsStringAsync();
       
                    return APIdata;
                }
                else
                {
                    return null;
                }
            }
        }

        //used for batch searching
        //takes up to 100 ids and returns the result
        public static async Task<string> FetchMultipleAPIDataAsync(string[] ids)
        {
            String APIdata = "something didn't work";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://api.gw2tp.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //build the api string
                string apiStr = "/1/items?ids=";
                foreach (var id in ids)
                {
                    apiStr += id;
                    apiStr += ",";
                }
                apiStr += "&fields=name,buy,sell";

                //gets the item by item id
                HttpResponseMessage response = await client.GetAsync(apiStr);

                if (response.IsSuccessStatusCode)
                {
                   
                    APIdata = await response.Content.ReadAsStringAsync();
                    // Console.WriteLine("string recieved");
                    //  Console.WriteLine(APIdata);
                    return APIdata;
                }
                else
                {
                    return null;
                }
            }
        }

  
    }
}
