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
    public partial class MyShoppingListPage : ContentPage
    {
        public MyShoppingListPage()
        {
            InitializeComponent();
            BindingContext = new MyShoppingListViewModel();

        }
        protected override void OnAppearing()
        {
            //InitializeComponent();
            this.grouplistView.SelectedItem = null;
            this.privatelistView.SelectedItem = null;
        }
        //protected async override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    var allUserLists = await firebaseHelper.GetUserLists();
        //    var allGroupLists = await firebaseHelper.GetGroupLists();
        //    //allLists = allUserLists.Concat(allGroupLists).ToList();
        //    if (allGroupLists != null)
        //    {
        //        grouplistView.ItemsSource = allGroupLists;
        //    }
        //    if (allUserLists != null)
        //    {
        //        privatelistView.ItemsSource = allUserLists;
        //    }


        //}

        //async void ListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var l = e.SelectedItem as Lists;

        //    if (e.SelectedItem != null)
        //    {
        //        await Navigation.PushModalAsync(new DetailedShoppingListPage(l));
        //    }
        //}

        //async void ListViewGroupItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var lg = e.SelectedItem as Lists;

        //    if (e.SelectedItem != null)
        //    {
        //        await Navigation.PushModalAsync(new DetailedGroupShoppingListPage(lg));
        //    }
        //}
    }
}