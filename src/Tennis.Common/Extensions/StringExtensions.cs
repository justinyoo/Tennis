namespace Tennis.Common.Extensions
{
    /// <summary>
    /// This represents the extension entity for string values.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts the value to phone number format.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Returns the phone number formatted value.</returns>
        public static string ToPhone(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            value = value.Replace(" ", "");
            var result = $"{value.Substring(0, 2)} {value.Substring(2, 4)} {value.Substring(6, 4)}";

            return result;
        }

        /// <summary>
        /// Converts the value to mobile number format.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Returns the mobile number formatted value.</returns>
        public static string ToMobile(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            value = value.Replace(" ", "");
            var result = $"{value.Substring(0, 4)} {value.Substring(4, 3)} {value.Substring(7, 3)}";

            return result;
        }
    }
}
