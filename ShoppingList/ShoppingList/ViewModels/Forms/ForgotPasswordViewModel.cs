using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShoppingList.ViewModels.Forms
{
    /// <summary>
    /// ViewModel for forgot password page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ForgotPasswordViewModel : LoginViewModel
    {
        IAuth auth;
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ForgotPasswordViewModel" /> class.
        /// </summary>
        public ForgotPasswordViewModel()
        {
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.SendCommand = new Command(this.SendClicked);
            this.BackToSignInCommand = new Command(this.BackToSignInClicked);
            auth = DependencyService.Get<IAuth>();
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Send button is clicked.
        /// </summary>
        public Command SendCommand { get; set; }

        public Command BackToSignInCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Send button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SendClicked(object obj)
        {
            if (!IsInvalidEmail)
            {
                if (auth.ResetPassword(Email))
                    await Application.Current.MainPage.DisplayAlert("Sikeres jelszó visszaállítás", "A jelszó visszaállításához szükséges információkat elküldtük az email címére", "OK");                
                else
                    await Application.Current.MainPage.DisplayAlert("Sikertelen jelszó visszaállítás", "Valami hiba történt", "OK");
            }
            else
                await Application.Current.MainPage.DisplayAlert("Hiba", "Nem megfelelő email", "OK");
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            Application.Current.MainPage = new Views.Forms.SimpleLoginPage();
        }

        private void BackToSignInClicked(object obj)
        {
            Application.Current.MainPage = new Views.Forms.SimpleLoginPage();
        }

        #endregion
    }
}