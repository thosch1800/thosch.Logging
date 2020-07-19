﻿using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Extensions.Logging
{
    public static class LoggingExtensionMethods
    {
        public static void LogEnter(
            this ILogger logger,
            Action<Arguments> args = null,
            [CallerMemberName] string callerName = "",
            [CallerFilePath] string callerFile = "",
            [CallerLineNumber] int callerFileLine = 0)
            => logger.DoLog(callerName, callerFile, callerFileLine, prefix: ">>> ", arguments: Arguments.Create(args));

        public static void LogExit(
            this ILogger logger,
            Action<Arguments> args = null,
            [CallerMemberName] string callerName = "",
            [CallerFilePath] string callerFile = "",
            [CallerLineNumber] int callerFileLine = 0)
            => logger.DoLog(callerName, callerFile, callerFileLine, prefix: "<<< ", arguments: Arguments.Create(args));

        public static void LogMessage(
            this ILogger logger,
            string message,
            Action<Arguments> args = null,
            [CallerMemberName] string callerName = "",
            [CallerFilePath] string callerFile = "",
            [CallerLineNumber] int callerFileLine = 0)
            => logger.DoLog(callerName, callerFile, callerFileLine, prefix: "### ", arguments: Arguments.Create(args), message: message);

        public static void LogException(
            this ILogger logger,
            Exception exception,
            Action<Arguments> args = null,
            LogLevel logLevel = LogLevel.Error,
            [CallerMemberName] string callerName = "",
            [CallerFilePath] string callerFile = "",
            [CallerLineNumber] int callerFileLine = 0)
            => logger.DoLog(callerName, callerFile, callerFileLine, arguments: Arguments.Create(args), logLevel: logLevel, message: exception.ToString());


        private static void DoLog(
            this ILogger logger,
            string callerName,
            string callerFile,
            int callerFileLine,
            string prefix = "",
            string message = "",
            Arguments arguments = null,
            LogLevel logLevel = LogLevel.Debug) => logger.Log(logLevel, $"{prefix}{callerName}({arguments.Expand()}){message}[{callerFile}#{callerFileLine}]");
    }
}