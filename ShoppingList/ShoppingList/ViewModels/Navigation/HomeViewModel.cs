using Plugin.FirebasePushNotification;
using ShoppingList.DataService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ShoppingList.ViewModels.Navigation
{
    public class HomeViewModel : BaseViewModel
    {
        string groupid;

        IAuth auth;
        FireBaseHelper fbHelper;
        public ICommand SignOutCommand => new Command(signOut);
        public ICommand AcceptCommand => new Command(accept);
        public ICommand DeclineCommand => new Command(decline);
        private bool _isInvited;
        public bool IsInvited 
        {
            get { return _isInvited; }
            set
            {
                _isInvited = value;
                NotifyPropertyChanged(nameof(IsInvited));
            }
        }

        public string GroupName { get; set; }
        public string UserName { get; set; }
        public HomeViewModel()
        {
            Application.Current.PageAppearing += (s, p) => { };
            IsInvited = false;
            auth = DependencyService.Get<IAuth>();
            fbHelper = new FireBaseHelper();
            UserName = "Hello " + fbHelper.GetCurrentUser().NickName;
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                foreach (var data in p.Data)
                {
                    if (data.Key == "Type")
                        if (data.Value.ToString() == "GroupInvite")
                        {
                            IsInvited = true;
                        }
                        else
                        {
                            break;
                        }
                    if (data.Key == "GroupID")
                    {
                        groupid = data.Value.ToString();
                    }


                    if (data.Key == "GroupName")
                        GroupName = data.Value.ToString();
                    if (data.Key == "GroupOwnerName")
                        GroupName = data.Value.ToString();
                }
            };

        }
        
        public void signOut()
        {
            var signOut = auth.SignOut();
            if (signOut)
            {
                Application.Current.MainPage = new Views.Forms.SimpleLoginPage();
            }
        }
        public async void accept()
        {

            await fbHelper.GroupInviteAccept(groupid);
            IsInvited = false;
            //await fbHelp.GroupInviteAccept(groupid);
            //Application.Current.MainPage.DisplayAlert("Accepted " + GroupName, "Nice", "no");
        }
        public async void decline()
        {            
            var a = await Application.Current.MainPage.DisplayAlert("Declined", "Nice", "yes", "no");
            if (a)
            {
                IsInvited = false;
            }
        }
    }
}

