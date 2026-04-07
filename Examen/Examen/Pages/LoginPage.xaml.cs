using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Examen.Services;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Examen.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ErrorText.Text = "Please fill in all fields.";
                ErrorText.Visibility = Visibility.Visible;
                return;
            }

            bool success = AuthService.Login(username, password);

            if (success)
            {
                MainWindow.Instance?.NavigateTo(typeof(MainPage));
            }
            else
            {
                ErrorText.Text = "Wrong username or password.";
                ErrorText.Visibility = Visibility.Visible;
            }
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance?.NavigateTo(typeof(RegisterPage));
        }
    }
}
