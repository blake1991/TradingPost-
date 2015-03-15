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
            itemIdTB.Focus();
        }

        private void mainForm_Loaded(object sender, RoutedEventArgs e)
        {
            //loads the items from the itemids.txt file
            Load();
        }

        private async void submitBTN_Click(object sender, RoutedEventArgs e)
        {
            //gets the requested item from the text box
            itemList = ConvertFromJSON.ConvertMultipleString(await APIAccess.FetchSingleAPIDataAsync(itemIdTB.Text.ToString()));

            Console.WriteLine(itemList.Count);

            if (itemList.Count > 0)
            {
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
                    MessageBox.Show("Added!");
                    itemIdTB.Clear();
                    Save();
                }
                else
                {
                    MessageBox.Show("This item already exists");
                    itemIdTB.Clear();
                }
            }
        }

        private void mainForm_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //keeps the datagrid the same horizontal size of the window
            grid.Width = this.Width - 20;
        }


        private void reloadBTN_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void removeBTN_Click(object sender, RoutedEventArgs e)
        {
            itemsCollection.RemoveAt(grid.SelectedIndex);
            Save();
        }

        private async void Load()
        {
            itemsCollection.Clear();
            string[] items = TP.DataAccess.DataAccess.LoadItems();

            if (items.Length > 0)
            {
                itemList = ConvertFromJSON.ConvertMultipleString(await APIAccess.FetchMultipleAPIDataAsync(items));

                foreach (var item in itemList)
                {
                    itemsCollection.Add(item);
                }
            }
        }

        private void Save()
        {
            List<string> ids = new List<string>();
            foreach (var item in itemsCollection)
            {
                ids.Add(item.Id);
            }
            TP.DataAccess.DataAccess.SaveItems(ids);
        }


    }
}
