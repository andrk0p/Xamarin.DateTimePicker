using System;

namespace DateTimePicker.Models
{
	public struct DateTimePickerOptions
	{
		public DateTimePickerOptions(DateTime currentDate, int minuteStep)
		{
			if (minuteStep < 1 || minuteStep > 59)
			{
				throw new ArgumentException($"{nameof(minuteStep)} should have values from 1 to 59");
			}

			CurrentDate = currentDate;
			MinuteStep = minuteStep;
		}

		public DateTime CurrentDate { get; set; }

		public int MinuteStep { get; set; }
	}
}

