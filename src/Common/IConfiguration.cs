using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Common
{
    public interface IConfiguration
    {
        IEnumerable<(string Name, string Value)> Section(string section);
    }

    public static class Configuration
    {
        public static string Value(this IConfiguration configuration,
            string section,
            string property) =>
            configuration.Section(section)
                .Where(p => p.Name == property)
                .Select(p => p.Value)
                .DefaultIfEmpty()
                .Single();

        public static T Value<T>(this IConfiguration configuration,
            string section,
            string property,
            T defaultValue = default(T)) =>
            configuration.Value(section, property) == null
            ? defaultValue
            : configuration.Value(section, property).To<T>();

        public static bool Contains(this IConfiguration configuration,
            string section) =>
            configuration.Section(section).Any();

        public static bool Contains(this IConfiguration configuration,
            string section,
            string property) =>
            configuration.Value(section, property) != null;

        static T To<T>(this object source) =>
            (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(source);
    }
}