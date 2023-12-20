using System.ComponentModel;
using System.Reflection;

namespace Workers.Services {
    internal static class EnumExtension {
        public static string GetEnumDescription(Enum value) {
            FieldInfo? fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[]? attributes
                = fi?.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any()) {
                return attributes.First().Description;
            }

            return string.Empty;
        }
    }
}
