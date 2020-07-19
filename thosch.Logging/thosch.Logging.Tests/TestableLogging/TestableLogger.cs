using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Microsoft.Logging.Tests
{
    public static class ILoggingBuilderExtensionMethods
    {
        public static void AddTestableLogger(this ILoggingBuilder loggingBuilder, TestableLogger testableLogger = null) => loggingBuilder.AddProvider(new TestableLoggerProvider(testableLogger ?? new TestableLogger()));
    }

    public class TestableLoggerProvider : ILoggerProvider
    {
        public TestableLoggerProvider(TestableLogger testableLogger) => this.testableLogger = testableLogger;
        public void Dispose() { }

        public ILogger CreateLogger(string categoryName) => testableLogger;

        private readonly TestableLogger testableLogger;
    }

    public class TestableLogger : ILogger
    {
        public bool IsEnabled(LogLevel logLevel) => true;
        public List<string> Logs { get; } = new List<string>();

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) => Logs.Add(formatter(state, exception));
        public IDisposable BeginScope<TState>(TState state) => null;
    }
}
