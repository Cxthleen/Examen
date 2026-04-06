using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CrudApp
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string gebruikersnaam = UsernameBox.Text;
            string wachtwoord = PasswordBox.Password;

            // ⚠️ VOORBEELD: In een echte app gebruik je een database of hashing!
            // Hardcoded testgebruiker: admin / admin123
            if (gebruikersnaam == "admin" && wachtwoord == "admin123")
            {
                // Sla de ingelogde gebruiker op
                SessionManager.HuidigeGebruiker = gebruikersnaam;

                // Navigeer naar de hoofd (CRUD) pagina
                Frame.Navigate(typeof(MainPage));
            }
            else
            {
                ErrorText.Visibility = Visibility.Visible;
            }
        }
    }
}
