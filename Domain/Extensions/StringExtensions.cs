namespace Domain.Extensions;

public static class StringExtensions {
    
    public static bool IsNullEmptyOrWhiteSpace(this string? str) {
        return string.IsNullOrWhiteSpace(str) || string.IsNullOrEmpty(str);
    }
}