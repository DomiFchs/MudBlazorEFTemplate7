namespace Domain.Extensions;

public static class DateTimeExtensions {
    public static DateTime SetTimeOfDay(this DateTime time, TimeSpan timeOfDay) =>
        new(time.Year, time.Month, time.Day, timeOfDay.Hours, timeOfDay.Minutes, timeOfDay.Seconds);

    public static DateOnly ToDateOnly(this DateTime dateTime) =>
        new(dateTime.Year, dateTime.Month, dateTime.Day);
    
    public static DateTime ToDateTime(this DateOnly dateOnly) =>
        new(dateOnly.Year, dateOnly.Month, dateOnly.Day);
}