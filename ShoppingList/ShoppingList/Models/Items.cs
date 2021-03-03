using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ShoppingList.Models
{
    public class Items
    {
        public string ItemName { get; set; }
        public string Category { get; set; }

        private bool isDone;

        public bool IsDone
        {
            get { return isDone; }
            set
            {
                if (isDone != value)
                {
                    isDone = value;
                    OnPropertyChanged("IsDone");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
