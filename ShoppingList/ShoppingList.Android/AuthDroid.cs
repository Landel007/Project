using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Firebase.Auth;
using ShoppingList.Droid;
using FirebaseNet.Messaging;
using Message = FirebaseNet.Messaging.Message;
using Firebase.Database;
using ShoppingList.Models;
using ShoppingList.Data;
using Firebase.Database.Query;

[assembly: Dependency(typeof(AuthDroid))]
namespace ShoppingList.Droid
{
    public class AuthDroid : IAuth
    {
        FirebaseClient firebase = new FirebaseClient("https://szofttech-3ad42.firebaseio.com/");
        public AuthDroid()
        {
            
        }

        public string GetCurrentUserID()
        {
            return FirebaseAuth.Instance.CurrentUser.Uid;
        }

        public bool IsSignIn()
        {
            var user = FirebaseAuth.Instance.CurrentUser;
            return user != null;
        }

        public async Task<string> LoginWithEmailAndPassword(string email, string password)
        {
            try
            {
                var newUser = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var token = newUser.User.Uid;
                return token;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            }
        }

        public async Task SendInviteNotification(string invitedUID, Group group)
        {
            FCMClient client = new FCMClient("AAAA6ZtqyyU:APA91bF3lWZr1noMVFxqQPOH-47SjwxPGdp_8Jpav__8G-2INVN820ufMO43opgTmkf_QuIcd1hr5PFes7ktL3iQjmcb7nrP4lkNppOBhJysACaKd_M6J29pl01heyq5TzHSIOzlBHDI"); //as derived from https://console.firebase.google.com/project/
            var Notification = new Message()
            {
                To = $"/topics/{invitedUID}",
                Notification = new AndroidNotification()
                {
                    Body = "You got invited to " + group.GroupName,
                    Title = "Invited by " + group.GroupOwner.NickName,
                    Icon = "myIcon"
                }
                
            };
            var Data = new Message()
            {
                To = $"/topics/{invitedUID}",
                Data = new Dictionary<string, string>
                {
                    { "Type", Constants.GroupInviteString },
                    { "GroupID", group.GroupOwner.GroupID },
                    { "GroupName", group.GroupName },
                    { "GroupOwnerName", group.GroupOwner.NickName }
                }
            };
            await client.SendMessageAsync(Notification);
            await client.SendMessageAsync(Data);
        }

        public void SendNotification(Users user)
        {
            Task.Run(async () => {

                FCMClient client = new FCMClient("AAAA6ZtqyyU:APA91bF3lWZr1noMVFxqQPOH-47SjwxPGdp_8Jpav__8G-2INVN820ufMO43opgTmkf_QuIcd1hr5PFes7ktL3iQjmcb7nrP4lkNppOBhJysACaKd_M6J29pl01heyq5TzHSIOzlBHDI"); //as derived from https://console.firebase.google.com/project/


                var message = new Message()
                {
                    Priority = MessagePriority.high,                    
                    To = $"/topics/{user.GroupID}",
                    Notification = new AndroidNotification()
                    {
                        Title = $"{user.NickName} boltba ment!",
                        Body = "Gyors írj fel mindent a bevásárló listára"
                    },
                    Data = new Dictionary<string, string>
                    {                        
                        { "Type", Constants.ShopNotificationString },
                        { "Message", $"{user.NickName} boltba ment!" }
                    }
                };
                var result = await client.SendMessageAsync(message);                  
                return result;
            });
        }

        public void SendItemData(string groupid)
        {
            Task.Run(async () => {

                FCMClient client = new FCMClient("AAAA6ZtqyyU:APA91bF3lWZr1noMVFxqQPOH-47SjwxPGdp_8Jpav__8G-2INVN820ufMO43opgTmkf_QuIcd1hr5PFes7ktL3iQjmcb7nrP4lkNppOBhJysACaKd_M6J29pl01heyq5TzHSIOzlBHDI"); //as derived from https://console.firebase.google.com/project/


                var message = new Message()
                {
                    To = $"/topics/{groupid}",
                    Data = new Dictionary<string, string>
                    {
                        { "Type", Constants.ItemDataString },
                    }
                };
                var result = await client.SendMessageAsync(message);
                return result;
            });
        }
        public bool ResetPassword(string email)
        {
            try
            {
                FirebaseAuth.Instance.SendPasswordResetEmail(email);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool SignOut()
        {
            try
            {
                FirebaseAuth.Instance.SignOut();   
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> SignUpWithEmailAndPassword(string email, string password, string nickname)
        {
            try
            {
                var newUser = await Firebase.Auth.FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                var token = newUser.User.Uid;
                var users = new Users();
                users.Email = email;
                users.NickName = nickname;
                users.GroupID = "";

                await firebase
                .Child("Users/"+token)
                .PutAsync(users);

                EmailToUid emailToUser = new EmailToUid();
                emailToUser.Email = email;
                emailToUser.UID = token;
                await firebase
                .Child("emailToUid")
                .PostAsync(emailToUser);

                return token;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            }
            catch(FirebaseAuthInvalidCredentialsException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            }

        }
    }
}