using System;
using System.Reflection;

namespace ParkingChargeCalculator.Util
{
    public static class PropertyInfoExtensionMethods
    {
        public static void SetProperty<TEntity>(this PropertyInfo property, string serializedValue, TEntity entity) where TEntity : class
        {
            if (property.PropertyType == typeof(DateTime))
            {
                property.SetValue(entity, DateTime.Parse(serializedValue));
            }
            else if (property.PropertyType.BaseType == typeof(Enum))
            {
                property.SetValue(entity, Enum.Parse(property.PropertyType, serializedValue));
            }
        }
    }
}
