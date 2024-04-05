namespace Rihal.ReelRise.Application.Common.Utilities;

public static class DateTimeExtensions
{
    public static DateTime? ToUtc(this DateTime? dateTime)
    {
        if (!dateTime.HasValue)
        {
            return null;
        }

        // If the DateTime kind is already UTC, no conversion is necessary
        if (dateTime.Value.Kind == DateTimeKind.Utc)
        {
            return dateTime;
        }

        // If the DateTime kind is Unspecified or Local, convert to UTC
        return DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Utc).ToUniversalTime();
    }
}
