using System;
using Xamarin.Forms;

namespace Sample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var options = new DateTimePicker.Models.DateTimePickerOptions(DateTime.Now, 30);
            CurrentDateTime.Text = (await DateTimePicker.DateTimePicker.Instance.PickAsync(options)).ToString();
        }
    }
}
