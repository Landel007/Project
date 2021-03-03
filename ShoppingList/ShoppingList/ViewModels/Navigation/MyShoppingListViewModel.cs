using ShoppingList.DataService;
using ShoppingList.Models;
using ShoppingList.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ShoppingList.ViewModels.Navigation
{
    public class MyShoppingListViewModel : INotifyPropertyChanged
    {
        FireBaseHelper fbHelp;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Lists> groupLists { get; set; }
        public string ListID { get; set; }
        public string ListName { get; set; }
        public ObservableCollection<Items> Items { get; set; }
        public ObservableCollection<Lists> userLists { get; set; }
        public Lists itemSelected { get; set; }

        public Lists groupItemSelected { get; set; }
        public Lists ItemSelected
        {
            get
            {
                return itemSelected;
            }

            set
            {
                itemSelected = value;
                OnPropertyChanged("ItemSelected");

                if (ItemSelected != null)
                {
                    var detailedViewModel = new DetailedShoppingListViewModel(ItemSelected);
                    Application.Current.MainPage.Navigation.PushModalAsync(new Views.Navigation.DetailedShoppingListPage(detailedViewModel));
                }

            }
        }

        public Lists GroupItemSelected
        {
            get
            {
                return groupItemSelected;
            }

            set
            {
                groupItemSelected = value;
                OnPropertyChanged("ItemSelected");

                if (GroupItemSelected != null)
                {
                    var detailedGroupViewModel = new DetailedGroupShoppingListViewModel(GroupItemSelected);
                    Application.Current.MainPage.Navigation.PushModalAsync(new Views.Navigation.DetailedGroupShoppingListPage(detailedGroupViewModel));
                }

            }
        }

        public MyShoppingListViewModel()
        {
            groupLists = new ObservableCollection<Lists>();
            userLists = new ObservableCollection<Lists>();
            getData();

        }

        public async void getData()
        {
            fbHelp = new FireBaseHelper();

            var groupLists2 = await fbHelp.GetGroupLists();
            foreach (var lists in groupLists2)
            {
                groupLists.Add(new Lists { ListID = lists.ListID, ListName = lists.ListName, Items = lists.Items });
            }

            var userLists2 = await fbHelp.GetUserLists();
            foreach (var ulists in userLists2)
            {
                userLists.Add(new Lists { ListID = ulists.ListID, ListName = ulists.ListName, Items = ulists.Items });
            }
        }
        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
