using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace ShoppingList.Models
{
    public class Lists
    {
        public string ListID { get; set; }
        public string ListName { get; set; }
        public ObservableCollection<Items> Items { get; set; }

    }
}
