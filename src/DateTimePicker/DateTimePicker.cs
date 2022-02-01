using System;
using System.Threading.Tasks;
using DateTimePicker.Models;
using DateTimePicker.Views;
using Xamarin.Forms;

namespace DateTimePicker
{
    public class DateTimePicker
    {
        private static readonly Lazy<DateTimePicker> lazy
            = new Lazy<DateTimePicker>(() => new DateTimePicker());

        private DateTimePicker()
        {
            var platform = DependencyService.Get<IPlatformHelper>();
            if (!(platform?.IsInitialized ?? false))
                throw new Exception("Xamarin.DateTimePicker must be initialized on the platform");
        }

        public static DateTimePicker Instance { get => lazy.Value; }

        public async ValueTask<DateTime?> PickAsync(DateTimePickerOptions options)
        {
            var dateTimePickerPopupPage = new DateTimePickerPopupPage(options);
            return await dateTimePickerPopupPage.OpenPageAndWaitResult();
        }
    }
}
