using Examen.Data.Models;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;

namespace Examen.Pages
{
    public sealed partial class AppointmentDetailPage : Page
    {
        public AppointmentDetailPage()
        {
            this.InitializeComponent();
        }

        // Call this right after navigation to populate the page
        public void LoadAppointment(Appointment appt)
        {
            TitleText.Text = appt.Title;
            DateText.Text = appt.Date.ToString("dddd, MMMM d yyyy");
            TimeText.Text = appt.Date.ToString("HH:mm");
            DescriptionText.Text = string.IsNullOrWhiteSpace(appt.Description)
                                    ? "No notes added."
                                    : appt.Description;
            BadgeText.Text = "Appointment";
            IdText.Text = $"ID #{appt.Id}";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }
    }
}