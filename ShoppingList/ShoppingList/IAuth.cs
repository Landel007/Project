using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList
{
    public interface IAuth
    {
        Task<string> LoginWithEmailAndPassword(string email, string password);
        Task<string> SignUpWithEmailAndPassword(string email, string password, string nickname);

        bool SignOut();
        bool ResetPassword(string email);
        bool IsSignIn();

        void SendNotification(Users user);
        void SendItemData(string groupid);
        Task SendInviteNotification(string invitedUID, Group group);

        string GetCurrentUserID();
    }
}
