using System;
using System.Threading.Tasks;
using System.Windows;
//used for http calls
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace TP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// proof of concept
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Item> itemsCollection;
        List<Item> itemList;


        public MainWindow()
        {
            InitializeComponent();
            itemsCollection = new ObservableCollection<Item>();
            grid.ItemsSource = itemsCollection;
            itemList = new List<Item>();
        }

        private async void mainForm_Loaded(object sender, RoutedEventArgs e)
        {
            //loads the items from the itemids.txt file
            string[] items = TP.DataAccess.DataAccess.LoadItems();
            itemList = ConvertFromJSON.ConvertMultipleString(await APIAccess.FetchMultipleAPIDataAsync(items));

            foreach (var item in itemList)
            {
                itemsCollection.Add(item);
            }
        }

        private async void itemIdBTN_Click(object sender, RoutedEventArgs e)
        {
            //gets the requested item from the text box
            itemList = ConvertFromJSON.ConvertMultipleString(await APIAccess.FetchSingleAPIDataAsync(itemIdTB.Text.ToString()));

            bool contains = false;
            foreach (var item in itemsCollection)
            {
                if (item.Id == itemList.ElementAt(0).Id)
                {
                    contains = true;
                }
            }


            if (contains == false)
            {
                //can't add an item that isn't in game
                foreach (var item in itemList)
                {
                    itemsCollection.Add(item);
                }
            }
        }

        private void mainForm_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //keeps the datagrid the same horizontal size of the window
            grid.Width = this.Width - 20;
        }

        private void saveBTN_Click(object sender, RoutedEventArgs e)
        {
            List<string> ids = new List<string>();
            foreach (var item in itemsCollection)
            {
                ids.Add(item.Id);
            }
            TP.DataAccess.DataAccess.SaveItems(ids);
        }

        private async void reloadBTN_Click(object sender, RoutedEventArgs e)
        {
            //reloads the items in the itemids.txt file to keep the pricing up to date
            itemsCollection.Clear();
            string[] items = TP.DataAccess.DataAccess.LoadItems();
            itemList = ConvertFromJSON.ConvertMultipleString(await APIAccess.FetchMultipleAPIDataAsync(items));

            foreach (var item in itemList)
            {
                itemsCollection.Add(item);
            }
        }

        private void removeBTN_Click(object sender, RoutedEventArgs e)
        {
            itemsCollection.RemoveAt(grid.SelectedIndex);
        }
    }
}
