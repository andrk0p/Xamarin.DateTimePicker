using Android.OS;
using Xamarin.Forms.Platform.Android;

namespace DateTimePicker.Droid
{
    public static class Platform
    {
        internal static bool IsInitialized { get; set; }
        internal static FormsAppCompatActivity CurrentActivity { get; private set; }
        internal static Bundle CurrentBundle { get; private set; }

        public static void Init(FormsAppCompatActivity activity, Bundle bundle)
        {
            CurrentActivity = activity;
            CurrentBundle = bundle;
            Rg.Plugins.Popup.Popup.Init(activity);
            IsInitialized = true;
            LinkAssemblies();
        }

        private static void LinkAssemblies()
        {

        }
    }
}
