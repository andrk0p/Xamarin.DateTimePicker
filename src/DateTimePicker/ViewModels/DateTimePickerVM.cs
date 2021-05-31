using DateTimePicker.Models;
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
        public string DayString => CurrentDate.ToString("dd MMMM");
        public DateTime CurrentDate
        {
            get => currentDate;
            set
            {
                currentDate = value;
                SetDates();
            }
        }

        public void OnDisappearing() => taskCompletion.TrySetResult(null);

        private void SetDates()
        {
            var date = CurrentDate.Date + new TimeSpan(00, 00, 00);
            var dates = new List<DateTimeModel>()
            {
                new DateTimeModel(date)
            };

            while (true)
            {
                date = date.AddMinutes(30);
                if (date.Day > CurrentDate.Day || date.Month > CurrentDate.Month || date.Year > CurrentDate.Year)
                    break;
                dates.Add(new DateTimeModel(date));
            }

            Dates = dates;
            OnPropertyChanged(nameof(DayString));
            OnPropertyChanged(nameof(Dates));
        }
    }
}
