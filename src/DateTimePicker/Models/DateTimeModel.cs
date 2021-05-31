using System;

namespace DateTimePicker.Models
{
    internal class DateTimeModel : BaseNotifier
    {
        public DateTimeModel(DateTime time)
        {
            Time = time;
            OnPropertyChanged(Value);
        }

        public string Value => Time.ToShortTimeString();

        public DateTime Time { get; private set; }
    }
}
