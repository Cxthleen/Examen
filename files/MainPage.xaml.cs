using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace CrudApp
{
    public sealed partial class MainPage : Page
    {
        // Service met alle CRUD-methoden
        private readonly ProductService _service = new();

        // Bijhouden of we een nieuw product maken of een bestaand bewerken
        private bool _isBewerken = false;
        private int _bewerkId = -1;

        public MainPage()
        {
            this.InitializeComponent();

            // Welkomstbericht tonen
            WelkomText.Text = $"Ingelogd als: {SessionManager.HuidigeGebruiker}";

            // Lijst laden
            LaadProducten();
        }

        // ===== LIJST LADEN =====
        private void LaadProducten()
        {
            ProductenLijst.ItemsSource = null; // ververs
            ProductenLijst.ItemsSource = _service.AlleProducten();
        }

        // ===== SELECTIE VERANDERD =====
        private void ProductenLijst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool isGeselecteerd = ProductenLijst.SelectedItem != null;
            BewerkButton.IsEnabled   = isGeselecteerd;
            VerwijderButton.IsEnabled = isGeselecteerd;
        }

        // ===== NIEUW PRODUCT =====
        private void NieuwButton_Click(object sender, RoutedEventArgs e)
        {
            _isBewerken = false;
            _bewerkId   = -1;
            FormulierLeegmaken();
            ProductenLijst.SelectedItem = null;
        }

        // ===== BEWERKEN =====
        private void BewerkButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductenLijst.SelectedItem is not Product geselecteerd) return;

            _isBewerken = true;
            _bewerkId   = geselecteerd.Id;

            NaamBox.Text  = geselecteerd.Naam;
            PrijsBox.Text = geselecteerd.Prijs.ToString("F2");
        }

        // ===== VERWIJDEREN =====
        private async void VerwijderButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductenLijst.SelectedItem is not Product geselecteerd) return;

            // Bevestigingsdialoog
            ContentDialog dialoog = new()
            {
                Title             = "Verwijderen bevestigen",
                Content           = $"Weet je zeker dat je '{geselecteerd.Naam}' wilt verwijderen?",
                PrimaryButtonText = "Ja, verwijderen",
                CloseButtonText   = "Annuleren",
                XamlRoot          = this.XamlRoot
            };

            var resultaat = await dialoog.ShowAsync();
            if (resultaat == ContentDialogResult.Primary)
            {
                _service.Verwijderen(geselecteerd.Id);
                LaadProducten();
                FormulierLeegmaken();
            }
        }

        // ===== OPSLAAN =====
        private void OpslaanButton_Click(object sender, RoutedEventArgs e)
        {
            // Validatie
            if (string.IsNullOrWhiteSpace(NaamBox.Text))
            {
                ToonFout("Vul een naam in.");
                return;
            }

            if (!double.TryParse(PrijsBox.Text, out double prijs) || prijs < 0)
            {
                ToonFout("Vul een geldige prijs in.");
                return;
            }

            if (_isBewerken)
            {
                // UPDATE
                _service.Bijwerken(new Product
                {
                    Id    = _bewerkId,
                    Naam  = NaamBox.Text,
                    Prijs = prijs
                });
            }
            else
            {
                // CREATE
                _service.Toevoegen(new Product
                {
                    Naam  = NaamBox.Text,
                    Prijs = prijs
                });
            }

            LaadProducten();
            FormulierLeegmaken();
        }

        // ===== ANNULEREN =====
        private void AnnulerenButton_Click(object sender, RoutedEventArgs e)
        {
            FormulierLeegmaken();
            ProductenLijst.SelectedItem = null;
        }

        // ===== UITLOGGEN =====
        private void UitloggenButton_Click(object sender, RoutedEventArgs e)
        {
            SessionManager.Uitloggen();
            Frame.Navigate(typeof(LoginPage));
        }

        // ===== HULPFUNCTIES =====
        private void FormulierLeegmaken()
        {
            NaamBox.Text  = string.Empty;
            PrijsBox.Text = string.Empty;
            _isBewerken   = false;
            _bewerkId     = -1;
        }

        private async void ToonFout(string bericht)
        {
            ContentDialog fout = new()
            {
                Title           = "Fout",
                Content         = bericht,
                CloseButtonText = "OK",
                XamlRoot        = this.XamlRoot
            };
            await fout.ShowAsync();
        }
    }
}
