using Microsoft.UI.Xaml;

namespace CrudApp
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            // Navigeer direct naar de LoginPage
            RootFrame.Navigate(typeof(LoginPage));
        }
    }
}
