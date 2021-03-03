using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingList.Models
{
    public class Group
    {
        public string GroupName { get; set; }
        public List<Lists> GroupList { get; set; }
        public List<Users> GroupMembers { get; set; }
        public Users GroupOwner { get; set; }
    }
}
