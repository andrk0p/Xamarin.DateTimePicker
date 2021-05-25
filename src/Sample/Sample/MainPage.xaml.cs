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
            CurrentDateTime.Text = (await DateTimePicker.DateTimePicker.Instance.PickAsync()).ToString();
        }
    }
}
