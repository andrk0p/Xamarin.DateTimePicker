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
        public DateTimePickerPopupPage()
        {
            InitializeComponent();

            dateTimePickerVM = new DateTimePickerVM()
            {
                Navigation = Navigation
            };
            BindingContext = dateTimePickerVM;
        }

        protected override void OnDisappearing()
        {
            dateTimePickerVM.OnDisappearing();
            base.OnDisappearing();
        }

        public async Task<DateTime?> OpenPopup()
        {
            dateTimePickerVM.taskCompletion = new TaskCompletionSource<DateTime?>();
            await Navigation.PushPopupAsync(this);
            return await dateTimePickerVM.taskCompletion.Task;
        }
    }
}