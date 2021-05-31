using DateTimePicker.Models;
using Xamarin.Forms;

namespace DateTimePicker.ViewModels
{
    internal class BaseViewModel : BaseNotifier
    {
        public INavigation Navigation { get; set; }
    }
}
