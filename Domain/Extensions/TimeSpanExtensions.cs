namespace Domain.Extensions;

public static class TimeSpanExtensions {
    public static string ToTimeFormat(this TimeSpan timeSpan) =>
        $"{timeSpan.Hours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
}