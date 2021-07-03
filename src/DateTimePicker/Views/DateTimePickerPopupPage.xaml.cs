using DateTimePicker.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace DateTimePicker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DateTimePickerPopupPage : PopupPage
    {
        private readonly DateTimePickerVM dateTimePickerVM;
        public DateTimePickerPopupPage(int minuteStep)
        {
            InitializeComponent();

            dateTimePickerVM = new DateTimePickerVM(Navigation, minuteStep);
            BindingContext = dateTimePickerVM;
            Disappearing += dateTimePickerVM.OnDisappearing;
        }

        public async ValueTask<DateTime?> OpenPageAndWaitResult()
        {
            await Navigation.PushPopupAsync(this);
            return await dateTimePickerVM.taskCompletion.Task;
        }
    }
}