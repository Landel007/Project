using Firebase.Database;
using Firebase.Database.Query;
using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.DataService
{
    class FireBaseHelper
    {
        private static Users User;  
        private static string UserID;

        FirebaseClient firebase = new FirebaseClient("https://szofttech-3ad42.firebaseio.com/");

        public async Task<List<Lists>> GetAllLists()
        {
            return (await firebase
                .Child("Lists")
                .OnceAsync<Lists>()).Select(item => new Lists
                {
                    Items = item.Object.Items,
                    ListID = item.Object.ListID,
                    ListName = item.Object.ListName
                }).ToList();
        }

        public async Task AddUserList(Lists list)
        {
            var listid = await firebase
                .Child("Lists")
                .Child(UserID)
                .PostAsync(list);
            list.ListID = listid.Key;
            await UpdateUserList(list);

        }

        public async Task UpdateUserList(Lists list)
        {
            await firebase
             .Child("Lists")
             .Child(UserID)
             .Child(list.ListID)
             .PutAsync(list);

        }
        public async Task AddGroupList(Lists list)
        {
            var listid = await firebase
              .Child("Lists")
              .Child(User.GroupID)
              .PostAsync(list);
            list.ListID = listid.Key;
            await UpdateGroupList(list);
        }

        public async Task UpdateGroupList(Lists list)
        {
            await firebase
             .Child("Lists")
             .Child(User.GroupID)
             .Child(list.ListID)
             .PutAsync(list);

        }

        public async Task<bool> SpecificGroupList(Lists list)
        {
            var query = await GetGroupLists();
            List<string> ids = new List<string>();

            foreach (var item in query)
            {
                ids.Add(item.ListID);
            }

            if (ids.Contains(list.ListID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> SpecificUserList(Lists list)
        {
            var query = await GetUserLists();
            List<string> ids = new List<string>();
            foreach (var item in query)
            {
                ids.Add(item.ListID);
            }

            if (ids.Contains(list.ListID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<ObservableCollection<Lists>> GetUserLists()
        {
            var query = await firebase
                .Child("Lists")
                .Child(UserID)
                .OnceAsync<Lists>();
            ObservableCollection<Lists> lists = new ObservableCollection<Lists>();
            foreach (var item in query)
            {
                lists.Add(item.Object);
            }
            return lists;
        }
        public async Task<ObservableCollection<Lists>> GetGroupLists()
        {
            if (User.GroupID != "")
            {
                var query = await firebase
                .Child("Lists")
                .Child(User.GroupID)
                .OnceAsync<Lists>();
                ObservableCollection<Lists> lists = new ObservableCollection<Lists>();
                foreach (var item in query)
                {
                    lists.Add(item.Object);
                }
                return lists;
            }
            else
                return new ObservableCollection<Lists>();
        }

        public async Task<Lists> GetSpecificUserList(string listID)
        {
            var query = await firebase
               .Child("Lists")
               .Child(UserID)
               .Child(listID)
               .OnceSingleAsync<Lists>();
            return query;
        }
        public async Task<Lists> GetSpecificGroupList(string listID)
        {
            if (User.GroupID != "")
            {
                var query = await firebase
               .Child("Lists")
               .Child(User.GroupID)
               .Child(listID)
               .OnceSingleAsync<Lists>();
                return query;
            }
            else
            {
                return null;
            } 
        }

        public async Task DeleteUserList(Lists list)
        {
            await firebase
                .Child("Lists")
                .Child(UserID)
                .Child(list.ListID)
                .DeleteAsync();
        }

        public async Task DeleteGroupList(Lists list)
        {
            await firebase
                .Child("Lists")
                .Child(User.GroupID)
                .Child(list.ListID)
                .DeleteAsync();
        }

        //public async Task AddUser(int groupId, string email, string nickname)
        //{
        //    await firebase
        //        .Child("Users")
        //        .PostAsync(new Users() { GroupID = groupId, Email = email, NickName = nickname });
        //}

        public async Task<string> CreateNewGroup(Group group)
        {
            var push = await firebase
              .Child("Groups")
              .PostAsync(group);
            group.GroupOwner.GroupID = push.Key;
            group.GroupMembers.FirstOrDefault().GroupID = push.Key;
            await UpdateGroup(group);
            return push.Key;
        }
        public async Task UpdateUser(Users user)
        {
            await firebase
              .Child("Users")
              .Child(UserID)
              .PutAsync(user);
        } 
        public async Task UpdateGroup(Group group)
        {
            await firebase.Child("Groups")
                .Child(User.GroupID)
                .PutAsync(group);
        }
        public async Task GroupInviteAccept(string groupid)
        {
            var group = await GetGroupFromGroupID(groupid);            
            group.GroupMembers.Add(User);
            User.GroupID = groupid;
            await UpdateUser(User);
            await UpdateGroup(group);
        }
        public async Task<string> GetUIDFromEmail(string email)
        {
            var asd = await firebase
            .Child("emailToUid")
            .OnceAsync<EmailToUid>();
            var id = asd.Where(item => item.Object.Email == email).Select(item => item.Object.UID);
            return id.FirstOrDefault();
        }

        public Users GetCurrentUser()
        {
            return User;
        }
        public async Task<Users> GetCurrentUserFromFirebase(string token)
        {
            var query = await firebase
             .Child("Users/" + token)
             .OnceSingleAsync<Users>();
            Users user = query;
            User = user;
            UserID = token;
            return user;
        }

        public async Task<Group> GetGroupFromGroupID(string GroupID)
        {
            var query = await firebase
             .Child("Groups/" + GroupID)
             .OnceSingleAsync<Group>();
            Group group = query;
            return group;
        }
         
        public async Task<List<string>> GetCategories()
        {
            var query = await firebase
                             .Child("Category")
                             .OnceAsync<List<string>>();            
            List<string> category = new List<string>();
            
            foreach (var item in query.FirstOrDefault().Object)
            {
                category.Add(item);
            }
            return category;
        }
    }
}
