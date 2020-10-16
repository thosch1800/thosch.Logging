using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Extensions.Logging
{
  /// <summary>
  ///   Extends the microsoft logger.
  /// </summary>
  public static class LoggingExtensionMethods
  {
    /// <summary>
    ///   Logs the method name, arguments, file and line number after '&gt;&gt;&gt;' as debug category.
    /// </summary>
    /// <param name="logger">filled automatically (extension method)</param>
    /// <param name="args">Add parameters to this object to log them.</param>
    /// <param name="logLevel">The loglevel used for this message.</param>
    /// <param name="callerName">filled automatically</param>
    /// <param name="callerFile">filled automatically</param>
    /// <param name="callerFileLine">filled automatically</param>
    public static void LogEnter(
      this ILogger logger,
      Action<Arguments> args = null,
      LogLevel logLevel = LogLevel.Debug,
      [CallerMemberName] string callerName = "",
      [CallerFilePath] string callerFile = "",
      [CallerLineNumber] int callerFileLine = 0)
      => logger.DoLog(callerName, callerFile, callerFileLine, prefix: ">>> ", arguments: Arguments.Create(args));

    /// <summary>
    ///   Logs the method name, arguments, file and line number after '&lt;&lt;&lt;' as debug category.
    /// </summary>
    /// <param name="logger">filled automatically (extension method)</param>
    /// <param name="args">Add parameters to this object to log them.</param>
    /// <param name="logLevel">The loglevel used for this message.</param>
    /// <param name="callerName">filled automatically</param>
    /// <param name="callerFile">filled automatically</param>
    /// <param name="callerFileLine">filled automatically</param>
    public static void LogExit(
      this ILogger logger,
      Action<Arguments> args = null,
      LogLevel logLevel = LogLevel.Debug,
      [CallerMemberName] string callerName = "",
      [CallerFilePath] string callerFile = "",
      [CallerLineNumber] int callerFileLine = 0)
      => logger.DoLog(callerName, callerFile, callerFileLine, prefix: "<<< ", arguments: Arguments.Create(args));

    /// <summary>
    ///   Logs the message, method name, arguments, file and line number as debug category.
    /// </summary>
    /// <param name="logger">filled automatically (extension method)</param>
    /// <param name="message">The message that should be logged</param>
    /// <param name="args">Add parameters to this object to log them.</param>
    /// <param name="logLevel">The loglevel used for this message.</param>
    /// <param name="callerName">filled automatically</param>
    /// <param name="callerFile">filled automatically</param>
    /// <param name="callerFileLine">filled automatically</param>
    public static void LogMessage(
      this ILogger logger,
      string message,
      Action<Arguments> args = null,
      LogLevel logLevel = LogLevel.Debug,
      [CallerMemberName] string callerName = "",
      [CallerFilePath] string callerFile = "",
      [CallerLineNumber] int callerFileLine = 0)
      => logger.DoLog(callerName, callerFile, callerFileLine, arguments: Arguments.Create(args), message: message);

    /// <summary>
    ///   Logs the exception, method name, arguments, file and line number after '&gt;&gt;&gt;' with the specified log level.
    /// </summary>
    /// <param name="logger">filled automatically (extension method)</param>
    /// <param name="exception">The exception that should be logged</param>
    /// <param name="logLevel">The log level used for this entry (Default: Error).</param>
    /// <param name="args">Add parameters to this object to log them.</param>
    /// <param name="callerName">filled automatically</param>
    /// <param name="callerFile">filled automatically</param>
    /// <param name="callerFileLine">filled automatically</param>
    public static void LogException(
      this ILogger logger,
      Exception exception,
      Action<Arguments> args = null,
      LogLevel logLevel = LogLevel.Error,
      [CallerMemberName] string callerName = "",
      [CallerFilePath] string callerFile = "",
      [CallerLineNumber] int callerFileLine = 0)
      => logger.DoLog(callerName, callerFile, callerFileLine, arguments: Arguments.Create(args), logLevel: logLevel, message: exception.ToString());


    /// <summary>
    ///   Logs the method name, arguments, file and line number after '&gt;&gt;&gt;' as debug category.
    ///   Logs the method name, arguments, file after '&lt;&lt;&lt;' as debug category when going out of scope.  
    /// </summary>
    /// <param name="logger">filled automatically (extension method)</param>
    /// <param name="args">Add parameters to this object to log them.</param>
    /// <param name="logLevel">The loglevel used for this message.</param>
    /// <param name="callerName">filled automatically</param>
    /// <param name="callerFile">filled automatically</param>
    /// <param name="callerFileLine">filled automatically</param>
    public static Scope LogScope(
      this ILogger logger,
      Action<Arguments> args = null,
      LogLevel logLevel = LogLevel.Debug,
      [CallerMemberName] string callerName = "",
      [CallerFilePath] string callerFile = "",
      [CallerLineNumber] int callerFileLine = 0)
      => new Scope(
        () => LogEnter(logger, args, logLevel, callerName, callerFile, callerFileLine),
        () => LogExit(logger, args, logLevel, callerName, callerFile, callerFileLine: 0));

    private static void DoLog(
      this ILogger logger,
      string callerName,
      string callerFile,
      int callerFileLine,
      Arguments arguments,
      string prefix = "",
      string message = "",
      LogLevel logLevel = LogLevel.Debug)
    {
      if (callerFileLine > 0)
        logger.Log(logLevel, $"{prefix}{callerName}({arguments.Expand()}){message}[{callerFile}#{callerFileLine.ToString()}]");
      else
        logger.Log(logLevel, $"{prefix}{callerName}({arguments.Expand()}){message}[{callerFile}]");
    }
  }
}