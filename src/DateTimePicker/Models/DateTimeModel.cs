using System;

namespace DateTimePicker.Models
{
    internal class DateTimeModel
    {
        internal DateTimeModel(DateTime time)
        {
            Time = time;
        }

        public string Value => Time.ToShortTimeString();

        public DateTime Time { get; private set; }
    }
}
