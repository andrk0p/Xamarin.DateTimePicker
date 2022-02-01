using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using DateTimePicker.Models;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace DateTimePicker.ViewModels
{
    internal class DateTimePickerVM : BaseNotifier
    {
        internal TaskCompletionSource<DateTime?> taskCompletion;

        private readonly int minuteStep;

        internal DateTimePickerVM(
            INavigation navigation,
            DateTimePickerOptions options)
        {
            this.minuteStep = options.MinuteStep;
            CurrentDate = options.CurrentDate;
            taskCompletion = new TaskCompletionSource<DateTime?>();

            PreviousDayCommand = new Command(()
                => CurrentDate = CurrentDate.AddDays(-1));
            NextDayCommand = new Command(()
                => CurrentDate = CurrentDate.AddDays(1));
            SelectTimeCommand = new Command<DateTimeModel>(async time
                => await SetResultAndClosePage(time, navigation));
        }

        public ICommand PreviousDayCommand { get; }

        public ICommand NextDayCommand { get; }

        public ICommand SelectTimeCommand { get; }

        public IEnumerable<DateTimeModel> Dates { get; private set; }

        public string DayString => CurrentDate.ToString("dd MMMM");

        private DateTime currentDate;

        public DateTime CurrentDate
        {
            get => currentDate;
            set
            {
                currentDate = value;
                SetListDates();
            }
        }

        public void OnDisappearing(object sender, EventArgs e)
        {
            SetTaskCompletionResult(null);
        }

        private void SetListDates()
        {
            var dates = new List<DateTimeModel>();

            for (var date = CurrentDate.Date + new TimeSpan(00, 00, 00); !IsDateNotInRange(date); date = date.AddMinutes(minuteStep))
            {
                dates.Add(new DateTimeModel(date));
            }

            Dates = dates;
            OnPropertyChanged(nameof(DayString));
            OnPropertyChanged(nameof(Dates));
        }

        private bool IsDateNotInRange(DateTime date)
        {
            return date.Day > CurrentDate.Day
                || date.Month > CurrentDate.Month
                || date.Year > CurrentDate.Year;
        }

        private async Task SetResultAndClosePage(DateTimeModel result, INavigation navigation)
        {
            SetTaskCompletionResult(result);
            await ClosePopupPageAsync(navigation);
        }

        private void SetTaskCompletionResult(DateTimeModel result)
            => taskCompletion.TrySetResult(result?.Time);

        private Task ClosePopupPageAsync(INavigation navigation)
            => navigation.PopPopupAsync();
    }
}
