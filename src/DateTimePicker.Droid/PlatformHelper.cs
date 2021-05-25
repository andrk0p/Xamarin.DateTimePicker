using DateTimePicker.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(PlatformHelper))]
namespace DateTimePicker.Droid
{
    internal class PlatformHelper : IPlatformHelper
    {
        public bool IsInitialized => Platform.IsInitialized;
    }
}