using Examen.Data;
using Examen.Data.Models;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Linq;
using System.Numerics;
using Windows.UI;

namespace Examen.Pages
{
    public sealed partial class MainPage : Page
    {
        private DateTime _currentDate = DateTime.Now;
        private AppDbContext _db = new AppDbContext();
        private Doctor? _selectedDoctor = null;

        public MainPage()
        {
            this.InitializeComponent();
            _db.Database.EnsureCreated();
            LoadDoctors();
            RenderCalendar();
        }

        private void LoadDoctors()
        {
            var doctors = _db.Doctors.ToList();
            DoctorPicker.ItemsSource = doctors;
        }

        private void DoctorPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedDoctor = DoctorPicker.SelectedItem as Doctor;
            RenderCalendar();
        }

        private void RenderCalendar()
        {
            CalendarGrid.Children.Clear();
            MonthText.Text = _currentDate.ToString("MMMM yyyy");

            DateTime firstDay = new DateTime(_currentDate.Year, _currentDate.Month, 1);
            int startDay = (int)firstDay.DayOfWeek;
            startDay = (startDay == 0) ? 6 : startDay - 1;

            int daysInMonth = DateTime.DaysInMonth(_currentDate.Year, _currentDate.Month);
            int cellIndex = startDay;

            // Filter by selected doctor, or load all if none selected
            var appointments = _selectedDoctor == null
                ? _db.Appointments.ToList()
                : _db.Appointments
                      .Where(a => a.DoctorId == _selectedDoctor.Id)
                      .ToList();

            for (int day = 1; day <= daysInMonth; day++)
            {
                int row = cellIndex / 7;
                int col = cellIndex % 7;

                var border = new Border
                {
                    Background = new SolidColorBrush(Colors.White),
                    CornerRadius = new CornerRadius(10),
                    Padding = new Thickness(6),
                    Margin = new Thickness(4)
                };

                var stack = new StackPanel();

                var dayText = new TextBlock
                {
                    Text = day.ToString(),
                    HorizontalAlignment = HorizontalAlignment.Right
                };
                stack.Children.Add(dayText);

                DateTime currentDay = new DateTime(_currentDate.Year, _currentDate.Month, day);

                var dayAppointments = appointments
                    .Where(a => a.Date.Date == currentDay.Date)
                    .ToList();

                foreach (var appt in dayAppointments)
                {
                    var btn = new Button
                    {
                        Content = appt.Title,
                        Margin = new Thickness(0, 2, 0, 0),
                        HorizontalAlignment = HorizontalAlignment.Stretch
                    };

                    btn.Click += async (s, e) =>
                    {
                        var dialog = new ContentDialog
                        {
                            Title = appt.Title,
                            Content = appt.Description,
                            CloseButtonText = "Close",
                            XamlRoot = this.Content.XamlRoot
                        };
                        await dialog.ShowAsync();
                    };

                    stack.Children.Add(btn);
                }

                border.Child = stack;
                Grid.SetRow(border, row);
                Grid.SetColumn(border, col);
                CalendarGrid.Children.Add(border);

                cellIndex++;
            }
        }

        private void PrevMonth_Click(object sender, RoutedEventArgs e)
        {
            _currentDate = _currentDate.AddMonths(-1);
            RenderCalendar();
        }

        private void NextMonth_Click(object sender, RoutedEventArgs e)
        {
            _currentDate = _currentDate.AddMonths(1);
            RenderCalendar();
        }
    }
}