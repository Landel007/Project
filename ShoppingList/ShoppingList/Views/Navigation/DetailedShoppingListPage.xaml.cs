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
    public partial class DetailedShoppingListPage : ContentPage
    {
        FireBaseHelper fbHelp = new FireBaseHelper();
        public DetailedShoppingListPage(DetailedShoppingListViewModel list)
        {
            InitializeComponent();
            BindingContext = list;
            //lists = list;
            //listView.ItemsSource = lists.Items;
        }

        //private async void DeleteList_Clicked(object sender, EventArgs e)
        //{
        //    await fbHelp.DeleteUserList(lists);
        //    await DisplayAlert("Sikeres törlés", "A listát sikeresen töröltük", "Vissza");
        //    await Navigation.PushModalAsync(new MyShoppingListPage());
        //}
        //private async void EditList_Clicked(object sender, EventArgs e)
        //{
        //    var lol = new NewShoppingListViewModel(lists);
        //    await Navigation.PushModalAsync(new NewShoppingListPage(lol));
        //}
    }
}