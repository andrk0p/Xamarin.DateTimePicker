using DateTimePicker.Models;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DateTimePicker.ViewModels
{
    internal class DateTimePickerVM : BaseNotifier
    {
        private readonly int minuteStep;
        internal TaskCompletionSource<DateTime?> taskCompletion;
        private DateTime currentDate;

        internal DateTimePickerVM(INavigation navigation, int minuteStep)
        {
            ThrowExIfMinuteStepInValid(minuteStep);
            this.minuteStep = minuteStep;
            CurrentDate = DateTime.Now;
            taskCompletion = new TaskCompletionSource<DateTime?>();

            PreviousDayCommand = new Command(() => CurrentDate = CurrentDate.AddDays(-1));
            NextDayCommand = new Command(() => CurrentDate = CurrentDate.AddDays(1));
            SelectTimeCommand = new Command<DateTimeModel>(async time => await SetResultAndClosePage(time, navigation));
        }

        public ICommand PreviousDayCommand { get; }
        public ICommand NextDayCommand { get; }
        public ICommand SelectTimeCommand { get; }

        public List<DateTimeModel> Dates { get; private set; }
        public string DayString => CurrentDate.ToString("dd MMMM");
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
            return date.Day > CurrentDate.Day || date.Month > CurrentDate.Month || date.Year > CurrentDate.Year;
        }

        private async Task SetResultAndClosePage(DateTimeModel result, INavigation navigation)
        {
            SetTaskCompletionResult(result);
            await ClosePopupPage(navigation);
        }

        private void SetTaskCompletionResult(DateTimeModel result)
        {
            taskCompletion.TrySetResult(result?.Time);
        }

        private async Task ClosePopupPage(INavigation navigation)
        {
            await navigation.PopPopupAsync();
        }

        private void ThrowExIfMinuteStepInValid(int minuteStep)
        {
            if (minuteStep < 1 || minuteStep > 59)
            {
                throw new Exception("The minute step should have values from 1 to 59");
            }
        }
    }
}
