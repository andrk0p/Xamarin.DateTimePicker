namespace DateTimePicker.IOS
{
    public static class Platform
    {
        internal static bool IsInitialized { get; set; }

        public static void Init()
        {
            Rg.Plugins.Popup.Popup.Init();
            IsInitialized = true;
            LinkAssemblies();
        }

        private static void LinkAssemblies()
        {

        }
    }
}