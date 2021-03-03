using ShoppingList.DataService;
using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ShoppingList.ViewModels.Navigation
{
    public class DetailedShoppingListViewModel : INotifyPropertyChanged
    {
        private Command<object> markDoneCommand;
        public ObservableCollection<Items> Items { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand DeleteListCommand => new Command(deleteList);
        public ICommand EditListCommand => new Command(editList);

        FireBaseHelper fbHelp = new FireBaseHelper();


        Lists list = new Lists();
        public DetailedShoppingListViewModel(Lists detailedlist)
        {

            MarkDoneCommand = new Command<object>(MarkItemAsDone);
            Items = detailedlist.Items;
            list = detailedlist;
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
            await fbHelp.DeleteUserList(list);
            await Application.Current.MainPage.Navigation.PushModalAsync(new Views.Navigation.BottomNavigationPage());
        }

        public async void editList()
        {
            var edit = new NewShoppingListViewModel(list);
            await Application.Current.MainPage.Navigation.PushModalAsync(new Views.Navigation.NewShoppingListPage(edit));
        }
    }
}
