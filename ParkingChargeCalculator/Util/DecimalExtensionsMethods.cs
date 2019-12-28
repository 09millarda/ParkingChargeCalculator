using System;

namespace ParkingChargeCalculator.Util
{
    public static class DecimalExtensionsMethods
    {
        public static string To2DpString(this decimal value)
        {
            var serializedValue = Math.Round(value, 2, MidpointRounding.AwayFromZero).ToString();
            var parts = serializedValue.Split(".");

            if (parts.Length == 1) return serializedValue += ".00";

            for (var i = parts[1].Length; i < 2; i++)
            {
                serializedValue += "0";
            }

            return serializedValue;
        }
    }
}
