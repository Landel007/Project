using Plugin.FirebasePushNotification;
using ShoppingList.DataService;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShoppingList
{
    public partial class App : Application
    {
        //public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        public static string BaseImageUrl { get; } 
        IAuth auth;
        public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzY4Nzg1QDMxMzgyZTM0MmUzMFMrZURxd2lzUGcxbUdTTnB6Z0kybytuaE5Wa2hBMm16N3hyZm40cmg2UE09");
            //hola
            //auth = DependencyService.Get<IAuth>();
            //firebase = new FireBaseHelper();
            //var a = auth.GetCurrentUserID();            
            //Task.Run(async () => { await firebase.GetCurrentUserFromFirebase(a); });
            

            //CrossFirebasePushNotification.Current.OnNotificationAction += (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine("Action");

            //    if (!string.IsNullOrEmpty(p.Identifier))
            //    {
            //        System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
            //        foreach (var data in p.Data)
            //        {
            //            System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
            //        }

            //    }

            //};
        }
        private async Task vmi()
        {
            var a = auth.GetCurrentUserID();
            await InitializeFirebase.init(a);
        }

        protected override async void OnStart()
        {
            MainPage = new Views.Forms.LoadingPage(); //TODO csere loading screenre
            try
            {
                auth = DependencyService.Get<IAuth>();
                if (auth.IsSignIn())
                {
                    await vmi();
                    MainPage = new Views.Navigation.BottomNavigationPage();

                    //CrossFirebasePushNotification.Current.Subscribe("general");

                    //CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
                    //{
                    //    System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
                    //};

                    //CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
                    //{

                    //    System.Diagnostics.Debug.WriteLine("Received");

                    //};                    
                }            
                else
                {
                    MainPage = new Views.Forms.SimpleLoginPage();
                }
            }
            catch (Exception e)
            {

                await MainPage.DisplayAlert(e.Message, "fos", "szar");
            }

            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
