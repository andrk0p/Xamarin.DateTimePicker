using DateTimePicker.IOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(PlatformHelper))]
namespace DateTimePicker.IOS
{
    internal class PlatformHelper : IPlatformHelper
    {
        public bool IsInitialized => Platform.IsInitialized;
    }
}
