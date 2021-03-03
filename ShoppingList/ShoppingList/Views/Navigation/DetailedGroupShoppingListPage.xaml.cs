using GalaSoft.MvvmLight.Messaging;
using Plugin.FirebasePushNotification;
using ShoppingList.Data;
using ShoppingList.DataService;
using ShoppingList.Models;
using ShoppingList.ViewModels.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingList.Views.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailedGroupShoppingListPage : ContentPage
    {
        //FireBaseHelper fbHelp = new FireBaseHelper();
        //Lists lists;
        //IAuth auth;
        public DetailedGroupShoppingListPage(DetailedGroupShoppingListViewModel detailedGroupList)
        {
            InitializeComponent();
            BindingContext = detailedGroupList;
            //auth = DependencyService.Get<IAuth>();
            //lists = list;
            //listView.ItemsSource = lists.Items;
            //Items item = new Items();
           
        }

        private async void SfCheckBox_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            string myMessage = "change";
            Messenger.Default.Send(myMessage); //nem túl mvvm TOO BAD a
        }

        //private async void DeleteList_Clicked(object sender, EventArgs e)
        //{
        //    await fbHelp.DeleteGroupList(lists);
        //    await DisplayAlert("Sikeres törlés", "A listát sikeresen töröltük", "Vissza");
        //    await Navigation.PushModalAsync(new MyShoppingListPage());
        //}

        //private async void EditList_Clicked(object sender, EventArgs e)
        //{
        //    var lol = new NewShoppingListViewModel(lists);
        //    await Navigation.PushModalAsync(new NewShoppingListPage(lol));
        //}

        //private void GoingNotification_Clicked(object sender, EventArgs e)
        //{
        //    auth.SendNotification(fbHelp.GetCurrentUser());

        //}
    }
}