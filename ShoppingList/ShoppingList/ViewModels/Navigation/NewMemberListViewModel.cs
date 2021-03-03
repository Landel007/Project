using ShoppingList.DataService;
using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ShoppingList.ViewModels.Navigation
{
    public class NewMemberListViewModel : BaseViewModel
    {
        IAuth auth;
        private FireBaseHelper fbHelp;
        private Users CurrentUser;

        public string Email { get; set; }
        public string GroupName { get; set; }

        private bool _hasGroup;
        public bool HasNoGroup { get; set; }
        public bool HasGroup
        {
            get
            {
                return _hasGroup;
            }
            set
            {
                _hasGroup = value;
                HasNoGroup = !value;
                NotifyPropertyChanged(nameof(HasGroup));
                NotifyPropertyChanged(nameof(HasNoGroup));
            }
        }



        public ICommand AddUserCommand => new Command(GroupInvite);
        public ICommand NewGroupCommand => new Command(NewGroup);

        public NewMemberListViewModel()
        {
            fbHelp = new FireBaseHelper();
            auth = DependencyService.Get<IAuth>();
            CurrentUser = fbHelp.GetCurrentUser();
            HasGroup = CurrentUser.GroupID != "";
            HasNoGroup = !HasGroup;
        }

        private async void GroupInvite()
        {
            // HasGroup = false;
            Group g = await fbHelp.GetGroupFromGroupID(fbHelp.GetCurrentUser().GroupID);
                    
            var e = await fbHelp.GetUIDFromEmail(Email);
            await auth.SendInviteNotification(e, g);
            //auth.SendNoti();
            
        }
        private async void NewGroup()
        {
            Group group = new Group();
            group.GroupList = new List<Lists>();
            group.GroupMembers = new List<Users>();
            group.GroupMembers.Add(CurrentUser);
            group.GroupOwner = CurrentUser;
            group.GroupName = GroupName;
            var key = await fbHelp.CreateNewGroup(group);
            CurrentUser.GroupID = key;
            await fbHelp.UpdateUser(CurrentUser);

            HasGroup = true;
        }
    }
}
