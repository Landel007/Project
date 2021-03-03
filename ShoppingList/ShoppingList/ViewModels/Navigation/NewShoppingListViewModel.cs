using ShoppingList.DataService;
using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ShoppingList.ViewModels.Navigation
{
    public class NewShoppingListViewModel : BaseViewModel
    {
        FireBaseHelper fbHelp = new FireBaseHelper();
        public ObservableCollection<Items> Items { get; set; }
        public List<string> Category { get; set; }
        public Lists list;
        public string ItemName { get; set; }
        public string ListName { get; set; }
        public string ListID { get; set; }
        public string SelectedCategory { get; set; }
        public bool IsGroup { get; set; }
        public bool NewUserList { get; set; }
        public bool NewGroupList { get; set; }
        public string GroupID { get; set; }
        public string UserID { get; set; }
        public ICommand AddItemCommand => new Command(addItem);
        public ICommand SaveListCommand => new Command(saveList);

        public NewShoppingListViewModel()
        {
            Items = new ObservableCollection<Items>();
            GetCategory();
            list = new Lists();
            SelectedCategory = "";                                                      
        }

        public NewShoppingListViewModel(Lists editlist)
        {
            Items = new ObservableCollection<Items>();
            list = new Lists();
            Items = editlist.Items;
            ListID = editlist.ListID;
            GetCategory();
            SelectedCategory = "";
            ListName = editlist.ListName;
        }
        public async void GetCategory()
        {
            Category = await fbHelp.GetCategories();
            NotifyPropertyChanged(nameof(Category));
        }
        public void addItem()
        {
            Items.Add(new Items { ItemName = this.ItemName, Category=SelectedCategory});
        }

        private async void saveList()
        {
            list.Items = Items;
            list.ListName = ListName;
            list.ListID = ListID;

            if (IsGroup)
            {
                NewGroupList = await fbHelp.SpecificGroupList(list);
                if (NewGroupList)
                {
                    
                    await fbHelp.UpdateGroupList(list);
                    await Application.Current.MainPage.Navigation.PushModalAsync(new Views.Navigation.BottomNavigationPage());
                }
                else
                {
                    await fbHelp.AddGroupList(list);
                    await Application.Current.MainPage.Navigation.PushModalAsync(new Views.Navigation.BottomNavigationPage());
                }
                    
            }
            else
            {
                NewUserList = await fbHelp.SpecificUserList(list);
                if (NewUserList)
                {
                    
                    await fbHelp.UpdateUserList(list);
                    await Application.Current.MainPage.Navigation.PushModalAsync(new Views.Navigation.BottomNavigationPage());
                }
                else
                {
                    await fbHelp.AddUserList(list);
                    await Application.Current.MainPage.Navigation.PushModalAsync(new Views.Navigation.BottomNavigationPage());
                }
                    
            }
            list = new Lists();
            Items = new ObservableCollection<Items>();
            ListName = string.Empty;
            NotifyPropertyChanged(nameof(Items));
            NotifyPropertyChanged(nameof(ListName));
            NotifyPropertyChanged(nameof(ItemName));
        }
    }
}
