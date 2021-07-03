using DateTimePicker.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DateTimePicker
{
    public class DateTimePicker
    {
        private static readonly Lazy<DateTimePicker> lazy = new Lazy<DateTimePicker>(() => new DateTimePicker());
        private DateTimePicker()
        {
            IPlatformHelper platform = DependencyService.Get<IPlatformHelper>();
            if (!(platform?.IsInitialized ?? false))
                throw new Exception("Xamarin.DateTimePicker must be initialized on the platform");
        }

        public static DateTimePicker Instance { get => lazy.Value; }

        public async ValueTask<DateTime?> PickAsync(int minuteStep = 30)
        {
            var dateTimePickerPopupPage = new DateTimePickerPopupPage(minuteStep);
            return await dateTimePickerPopupPage.OpenPageAndWaitResult();
        }
    }
}
