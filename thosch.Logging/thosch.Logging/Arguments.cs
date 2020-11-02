using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Logging
{
    /// <summary>
    ///   Provides support to log parameters.
    /// </summary>
    public class Arguments
  {
    private static readonly Arguments NullObject = new Arguments();

    internal static Arguments Create(Action<Arguments> args)
    {
      if (args == null) return NullObject;

      var instance = new Arguments();
      args(instance);
      return instance;
    }

    private Arguments() { }

    /// <summary>
    ///   Adds the specified parameter to the log output.
    /// </summary>
    /// <param name="name">Parameter name, e.g. nameof(variable).</param>
    /// <param name="value">Parameter value.</param>
    /// <returns></returns>
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

    /// <summary>
    ///   Extensions to Arguments
    /// </summary>
    public static class ArgumentsExtensions
  {
      /// <summary>
      ///   Adds the specified number in invariant culture formatting to the log.
      /// </summary>
      /// <param name="instance"></param>
      /// <param name="name">Parameter name, e.g. nameof(variable).</param>
      /// <param name="value">Parameter value.</param>
      /// <returns></returns>
      public static Arguments Add(this Arguments instance, string name, double value)
    {
      instance.Add(name, value.ToString(CultureInfo.InvariantCulture));
      return instance;
    }

      /// <summary>
      ///   Adds the specified byte array in hex to log output.
      /// </summary>
      /// <param name="instance"></param>
      /// <param name="name">Parameter name, e.g. nameof(variable).</param>
      /// <param name="value">Parameter value.</param>
      /// <returns></returns>
      public static Arguments Add(this Arguments instance, string name, byte[] value)
    {
      instance.Add(name, BitConverter.ToString(value));
      return instance;
    }

      /// <summary>
      ///   Adds the specified objects ToString output to the log.
      /// </summary>
      /// <param name="instance"></param>
      /// <param name="name">Parameter name, e.g. nameof(variable).</param>
      /// <param name="value">Parameter value.</param>
      /// <returns></returns>
      public static Arguments Add(this Arguments instance, string name, object value)
    {
      instance.Add(name, value.ToString());
      return instance;
    }
  }
}