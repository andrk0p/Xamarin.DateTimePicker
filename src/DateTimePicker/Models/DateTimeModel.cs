using System;

namespace DateTimePicker.Models
{
    public class DateTimeModel
    {
        public DateTimeModel(string name, DateTime time)
        {
            Name = name;
            Time = time;
        }

        public string Name { get; private set; }

        public DateTime Time { get; private set; }
    }
}
