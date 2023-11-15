using System;

namespace _Tools.Extensions
{
    public static class TimeExtension
    {
        public static string GetFloatToClockFormat(this float time)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            return $"{(int)timeSpan.TotalHours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }
    }
}