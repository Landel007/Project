using GalaSoft.MvvmLight.Messaging;
using Plugin.FirebasePushNotification;
using ShoppingList.Data;
using ShoppingList.DataService;
using ShoppingList.Models;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;


namespace ShoppingList.ViewModels.Navigation
{
    public class DetailedGroupShoppingListViewModel: BaseViewModel
    {
        private Command<object> markDoneCommand;
        public ObservableCollection<Items> Items { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand DeleteListCommand => new Command(deleteList);
        public ICommand EditListCommand => new Command(editList);
        public ICommand ShopCommand => new Command(shopNotification);
        Lists list;
        FireBaseHelper fbHelp = new FireBaseHelper();
        IAuth auth;
        public DetailedGroupShoppingListViewModel(Lists detailedGroupList)
        {
            MarkDoneCommand = new Command<object>(MarkItemAsDone);
            Items = detailedGroupList.Items;
            list = detailedGroupList;
            auth = DependencyService.Get<IAuth>();
            DataNotificationReceived(detailedGroupList.ListID);
            Messenger.Default.Register<string>(this, CheckBoxStateChanged);
        }
        private void DataNotificationReceived(string listid)
        {
            CrossFirebasePushNotification.Current.OnNotificationReceived += async (s, p) =>
            {
                foreach (var data in p.Data)
                {
                    if (data.Key == "Type")
                        if (data.Value.ToString() == Constants.ItemDataString)
                        {
                            Items = (await fbHelp.GetSpecificGroupList(listid)).Items;
                            NotifyPropertyChanged(nameof(Items));
                        }
                        else
                        {
                            break;
                        }
                }
            };
        }

        private void MarkItemAsDone(object obj)
        {
            var item = obj as Items;
            item.IsDone = !item.IsDone;
        }

        public Command<object> MarkDoneCommand
        {
            get
            {
                return markDoneCommand;
            }
            set
            {
                if (markDoneCommand != value)
                {
                    markDoneCommand = value;
                    OnPropertyChanged("MarkDoneCommand");
                }
            }
        }

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public async void deleteList()
        {
            await fbHelp.DeleteGroupList(list);
            await Application.Current.MainPage.Navigation.PushModalAsync(new Views.Navigation.BottomNavigationPage());
        }

        public async void editList()
        {
            var edit = new NewShoppingListViewModel(list);
            await Application.Current.MainPage.Navigation.PushModalAsync(new Views.Navigation.NewShoppingListPage(edit));
        }
        public void shopNotification()
        {
            auth.SendNotification(fbHelp.GetCurrentUser());
        }
        public async void CheckBoxStateChanged(string message)
        {
            if (message == "change")
            {
                auth.SendItemData(fbHelp.GetCurrentUser().GroupID);
                list.Items = Items;
                await fbHelp.UpdateGroupList(list);
            }            
        }
    }
}
