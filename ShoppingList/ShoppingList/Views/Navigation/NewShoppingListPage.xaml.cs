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
    public partial class NewShoppingListPage : ContentPage
    {
        public NewShoppingListPage()
        {
            InitializeComponent();
            BindingContext = new NewShoppingListViewModel();
        }

        public NewShoppingListPage(NewShoppingListViewModel editList)
        {
            InitializeComponent();
            BindingContext = editList;
        }
    }
}