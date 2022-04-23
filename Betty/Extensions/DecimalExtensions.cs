using System;

namespace Betty.Extensions
{
    internal static class DecimalExtensions
    {
        public static decimal ToCentsValue(this decimal dollarValue)
            => Math.Round(dollarValue, 2) * 100;
    }
}
