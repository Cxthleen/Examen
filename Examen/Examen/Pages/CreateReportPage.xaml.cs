using Examen.Data;
using Examen.Data.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;
using System.Numerics;

namespace Examen.Pages
{
    // Wrapper class used to show a readable label in the appointment ComboBox
    // instead of just showing the raw Appointment object
    public class AppointmentOption
    {
        public int Id { get; set; }
        public string DisplayText { get; set; }
    }

    public sealed partial class CreateReportPage : Page
    {
        // Database context used to load and save data
        private readonly AppDbContext _db = new AppDbContext();

        // Track which patient and doctor are currently selected
        // so we can filter appointments when both are chosen
        private Patient? _selectedPatient;
        private Doctor? _selectedDoctor;

        public CreateReportPage()
        {
            this.InitializeComponent();

            // Load all patients and doctors into their dropdowns on page load
            PatientPicker.ItemsSource = _db.Patients.ToList();
            DoctorPicker.ItemsSource = _db.Doctors.ToList();
        }

        // Fires when the user picks a patient
        // Updates the tracked patient and refreshes the appointment dropdown
        private void PatientPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedPatient = PatientPicker.SelectedItem as Patient;
            RefreshAppointments();
        }

        // Fires when the user picks a doctor
        // Updates the tracked doctor and refreshes the appointment dropdown
        private void DoctorPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedDoctor = DoctorPicker.SelectedItem as Doctor;
            RefreshAppointments();
        }

        // Filters the appointment dropdown based on the selected patient and doctor.
        // The appointment picker stays disabled until both are chosen.
        private void RefreshAppointments()
        {
            // Don't load appointments until we have both a patient and a doctor
            if (_selectedPatient == null || _selectedDoctor == null)
            {
                AppointmentPicker.ItemsSource = null;
                AppointmentPicker.IsEnabled = false;
                return;
            }

            // Query only appointments that belong to this patient AND this doctor
            var list = _db.Appointments
                .Where(a => a.PatientId == _selectedPatient.Id && a.DoctorId == _selectedDoctor.Id)
                .ToList()
                // Wrap each result in AppointmentOption so the ComboBox shows a readable label
                .Select(a => new AppointmentOption
                {
                    Id = a.Id,
                    DisplayText = $"{a.Date:dd MMM yyyy} — {a.Title}"
                })
                .ToList();

            AppointmentPicker.ItemsSource = list;

            // Only enable the dropdown if there are actual results
            AppointmentPicker.IsEnabled = list.Count > 0;
            AppointmentPicker.PlaceholderText = list.Count > 0
                ? "Select an appointment"
                : "No appointments found";
        }

        // Validates the form, builds the Report object, and saves it to the database
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Check all required fields before saving
            if (_selectedPatient == null || _selectedDoctor == null
                || string.IsNullOrWhiteSpace(SymptomsBox.Text)
                || string.IsNullOrWhiteSpace(DiagnosisBox.Text))
            {
                ErrorText.Text = "Patient, doctor, symptoms and diagnosis are required.";
                ErrorText.Visibility = Visibility.Visible;
                return;
            }

            // Build the report from the form values
            var report = new Report
            {
                CreatedAt = DateTime.Now,
                PatientId = _selectedPatient.Id,
                DoctorId = _selectedDoctor.Id,
                // AppointmentId is optional — null if nothing was selected
                AppointmentId = (AppointmentPicker.SelectedItem as AppointmentOption)?.Id,
                Symptoms = SymptomsBox.Text.Trim(),
                Diagnosis = DiagnosisBox.Text.Trim(),
                Treatment = TreatmentBox.Text.Trim(),
                Medication = MedicationBox.Text.Trim(),
                Notes = NotesBox.Text.Trim()
            };

            // Save to the database
            _db.Reports.Add(report);
            await _db.SaveChangesAsync();

            // Show a confirmation dialog then go back to the previous page
            var dialog = new ContentDialog
            {
                Title = "Saved",
                Content = $"Report for {_selectedPatient.Name} saved.",
                CloseButtonText = "OK",
                XamlRoot = this.Content.XamlRoot
            };
            await dialog.ShowAsync();

            if (Frame.CanGoBack) Frame.GoBack();
        }

        // Navigates back to the previous page without saving
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack) Frame.GoBack();
        }
    }
}