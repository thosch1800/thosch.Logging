using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.Extensions.Logging
{
    public class Arguments
    {
        private static readonly Arguments nullObject = new Arguments();

        internal static Arguments Create(Action<Arguments> args)
        {
            if (args == null) return nullObject;

            var instance = new Arguments();
            args(instance);
            return instance;
        }

        private Arguments() { }

        public Arguments Add(string name, string value)
        {
            args.Add(new KeyValuePair<string, string>(name, value));
            return this;
        }

        internal string Expand()
        {
            var sb = new StringBuilder();
            foreach (var arg in args)
                sb.AppendFormat("{0}:{1}", arg.Key, arg.Value);
            return sb.ToString();
        }

        private readonly List<KeyValuePair<string, string>> args = new List<KeyValuePair<string, string>>();
    }

    public static class ArgumentsExtensions
    {
        public static Arguments Add(this Arguments instance, string name, double value)
        {
            instance.Add(name, value.ToString(CultureInfo.InvariantCulture));
            return instance;
        }

        public static Arguments Add(this Arguments instance, string name, byte[] value)
        {
            instance.Add(name, BitConverter.ToString(value));
            return instance;
        }

        public static Arguments Add(this Arguments instance, string name, object value)
        {
            instance.Add(name, value.ToString());
            return instance;
        }
    }
}
