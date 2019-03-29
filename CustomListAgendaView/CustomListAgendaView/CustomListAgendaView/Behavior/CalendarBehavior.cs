using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;

namespace CustomListAgendaView
{
    public class CalendarBehavior : Behavior<ContentPage>
    {
        private SfCalendar calendar;
        ListView listView;
        private CalendarEventCollection calendarInlineEvents;
        private ObservableCollection<string> meetingSubjectCollection;
        private ObservableCollection<Color> colorCollection;
        public CalendarBehavior()
        {
            calendarInlineEvents = new CalendarEventCollection();
            this.AddAppointmentDetails();
            this.AddAppointments();
        }

        protected override void OnAttachedTo(ContentPage bindable)
        {
            var page = bindable as Page;
            calendar = page.FindByName<SfCalendar>("Calendar");
            listView = page.FindByName<ListView>("ListView");
            calendar.SelectionChanged += Calendar_SelectionChanged;
            listView.ItemsSource = GetSelectedDateAppointments(DateTime.Now.Date);
            // Setting Datasource for 3 Months.
            calendar.DataSource = calendarInlineEvents;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            calendar.SelectionChanged -= Calendar_SelectionChanged;
            base.OnDetachingFrom(bindable);
        }

        /// <summary>
        /// Calendars the selection changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void Calendar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Setting ItemSource for ListView based on appointment selection
            listView.ItemsSource = GetSelectedDateAppointments(calendar.SelectedDate);
        }

        /// <summary>
        /// Gets the selected date appointments.
        /// </summary>
        /// <returns>The selected date appointments.</returns>
        /// <param name="date">Date.</param>
        public CalendarEventCollection GetSelectedDateAppointments(DateTime? date)
        {
            CalendarEventCollection selectedDateAppointments = new CalendarEventCollection();
            for (int i = 0; i < calendarInlineEvents.Count; i++)
            {
                DateTime startDate = calendarInlineEvents[i].StartTime;

                if (date.Value.Day == startDate.Day && date.Value.Month == startDate.Month && date.Value.Year == startDate.Year)
                {
                    selectedDateAppointments.Add(calendarInlineEvents[i]);
                }
            }
            return selectedDateAppointments;
        }

        /// <summary>
        /// Adds the appointment details.
        /// </summary>
        private void AddAppointmentDetails()
        {
            meetingSubjectCollection = new ObservableCollection<string>();
            meetingSubjectCollection.Add("General Meeting");
            meetingSubjectCollection.Add("Plan Execution");
            meetingSubjectCollection.Add("Project Plan");
            meetingSubjectCollection.Add("Consulting");
            meetingSubjectCollection.Add("Support");
            meetingSubjectCollection.Add("Development Meeting");
            meetingSubjectCollection.Add("Scrum");
            meetingSubjectCollection.Add("Project Completion");
            meetingSubjectCollection.Add("Release updates");
            meetingSubjectCollection.Add("Performance Check");

            colorCollection = new ObservableCollection<Color>();
            colorCollection.Add(Color.FromHex("#FFA2C139"));
            colorCollection.Add(Color.FromHex("#FFD80073"));
            colorCollection.Add(Color.FromHex("#FF1BA1E2"));
            colorCollection.Add(Color.FromHex("#FFE671B8"));
            colorCollection.Add(Color.FromHex("#FFF09609"));
            colorCollection.Add(Color.FromHex("#FF339933"));
            colorCollection.Add(Color.FromHex("#FF00ABA9"));
            colorCollection.Add(Color.FromHex("#FFE671B8"));
            colorCollection.Add(Color.FromHex("#FF1BA1E2"));
            colorCollection.Add(Color.FromHex("#FFD80073"));
            colorCollection.Add(Color.FromHex("#FFA2C139"));
            colorCollection.Add(Color.FromHex("#FFA2C139"));
            colorCollection.Add(Color.FromHex("#FFD80073"));
            colorCollection.Add(Color.FromHex("#FF339933"));
            colorCollection.Add(Color.FromHex("#FFE671B8"));
            colorCollection.Add(Color.FromHex("#FF00ABA9"));
        }

        /// <summary>
        /// Adds the appointments.
        /// </summary>
        private void AddAppointments()
        {
            var today = DateTime.Now.Date;
            var random = new Random();
            for (int month = -1; month < 2; month++)
            {
                for (int day = -5; day < 5; day++)
                {
                    for (int count = 0; count < 2; count++)
                    {
                        var appointment = new CalendarInlineEvent();
                        appointment.Subject = meetingSubjectCollection[random.Next(7)];
                        appointment.Color = colorCollection[random.Next(10)];
                        appointment.StartTime = today.AddMonths(month).AddDays(random.Next(1, 28)).AddHours(random.Next(9, 18));
                        appointment.EndTime = appointment.StartTime.AddHours(2);
                        this.calendarInlineEvents.Add(appointment);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Date time to string converter.
    /// </summary>
    public class DateTimeToStringConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dateTime = Convert.ToDateTime(value);
            return dateTime.ToString("hh:mm tt");
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
