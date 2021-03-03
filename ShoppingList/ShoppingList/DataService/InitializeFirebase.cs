using Plugin.FirebasePushNotification;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.DataService
{
    class InitializeFirebase
    {
        public static async Task init(string token)
        {
            FireBaseHelper fbHelper = new FireBaseHelper();
            CrossFirebasePushNotification.Current.Subscribe(token);
            var user = await fbHelper.GetCurrentUserFromFirebase(token);
            if (user.GroupID != "")
            {
                CrossFirebasePushNotification.Current.Subscribe(user.GroupID);
            }
        }
    }
}
