using System;
using System.Threading.Tasks;
using System.Windows;
//used for http calls
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace TP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// proof of concept
    /// </summary>
    public partial class MainWindow : Window
    {
        Item item;

        public static ObservableCollection<Item> itemsCollection;
        List<Item> itemList;
        //   string iteminfo;

        public MainWindow()
        {
            InitializeComponent();
            itemsCollection = new ObservableCollection<Item>();
            grid.ItemsSource = itemsCollection;
            itemList = new List<Item>();
        }

        private async void mainForm_Loaded(object sender, RoutedEventArgs e)
        {
            #region Sample Data

            //default items for testing
            string[] items = { "12229", "12261" };

            itemList = ConvertFromJSON.ConvertMultipleString(await APIAccess.FetchMultipleAPIDataAsync(items));
            
            foreach (var item in itemList)
            {
                itemsCollection.Add(item);
            }



            #endregion
        }

        private async void itemIdBTN_Click(object sender, RoutedEventArgs e)
        {
            //gets the requested item from the text box
              itemList = ConvertFromJSON.ConvertMultipleString(await APIAccess.FetchSingleAPIDataAsync(itemIdTB.Text.ToString()));

            //can't add an item that isn't in game
              foreach (var item in itemList)
              {
                  itemsCollection.Add(item);
              }

        }

        private void mainForm_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //keeps the datagrid the same horizontal size of the window
            grid.Width = this.Width - 20;
        }
    }
}
