using DateTimePicker.Models;
using DateTimePicker.Resx;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DateTimePicker.ViewModels
{
    internal class DateTimePickerVM : BaseViewModel
    {
        private readonly DateTime dateNow = DateTime.Now;
        private DateTime currentDate = DateTime.Now;
        public TaskCompletionSource<DateTime?> taskCompletion;
        internal DateTimePickerVM()
        {
            SetDates();
            SelectTimeCommand = new Command<DateTimeModel>(async (time) =>
            {
                taskCompletion.TrySetResult(time.Time);
                await Navigation.PopPopupAsync();
            });
            PreviousDayCommand = new Command(() => CurrentDate = CurrentDate.AddDays(-1));
            NextDayCommand = new Command(() => CurrentDate = CurrentDate.AddDays(1));
        }

        public ICommand SelectTimeCommand { get; }
        public ICommand PreviousDayCommand { get; }
        public ICommand NextDayCommand { get; }

        public List<DateTimeModel> Dates { get; private set; }
        public string DayString { get; private set; }
        public DateTime CurrentDate
        {
            get => currentDate;
            set
            {
                if (value.Day <= dateNow.Day && value.Month <= dateNow.Month && value.Year <= dateNow.Year)
                    currentDate = dateNow;
                else
                    currentDate = value;
                SetDates();
            }
        }

        public void OnDisappearing() => taskCompletion.TrySetResult(null);

        private void SetDates()
        {
            DateTime date;
            List<DateTimeModel> dates = null;

            if (CurrentDate.Day == dateNow.Day && CurrentDate.Month == dateNow.Month && CurrentDate.Year == dateNow.Year)
            {
                DayString = Localize.Today;
                if (CurrentDate.Minute <= 30)
                    date = CurrentDate.AddMinutes(30 - CurrentDate.Minute);
                else
                    date = CurrentDate.AddMinutes(60 - CurrentDate.Minute);
                dates = new List<DateTimeModel>() { new DateTimeModel(Localize.Now, CurrentDate) };
            }
            else if (CurrentDate.Day == dateNow.AddDays(1).Day && CurrentDate.Month == dateNow.Month && CurrentDate.Year == dateNow.Year)
            {
                DayString = Localize.Tomorrow;
                date = CurrentDate.Date + new TimeSpan(00, 00, 00);
            }
            else
            {
                DayString = CurrentDate.ToString("dd MMMM");
                date = CurrentDate.Date + new TimeSpan(00, 00, 00);
            }

            dates ??= new List<DateTimeModel>();
            dates.Add(new DateTimeModel(date.ToShortTimeString(), date));

            while (true)
            {
                date = date.AddMinutes(30);
                if (date.Day > CurrentDate.Day || date.Month > CurrentDate.Month || date.Year > CurrentDate.Year)
                    break;
                dates.Add(new DateTimeModel(date.ToShortTimeString(), date));
            }

            Dates = dates;
            OnPropertyChanged(nameof(DayString));
            OnPropertyChanged(nameof(Dates));
        }
    }
}
