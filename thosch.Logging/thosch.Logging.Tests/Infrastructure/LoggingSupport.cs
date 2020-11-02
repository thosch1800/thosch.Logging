using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace thosch.Logging.Tests.Infrastructure
{
    /// <summary>
    /// LoggingSupport enables logging for xUnit. Derive xunit test fixtures from this class to add logged output to test output.
    /// Property LoggerFactory can be used to create logger instances or passed into test code. 
    /// Property Logger that can directly be used for logging during tests.
    /// Property Logs contains all logged output and can be used to assert on logged output.
    /// <code>
    /// public class MyTestFixture : LoggingSupport
    /// {
    ///     public MyTestFixture(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }
    /// }
    /// </code>
    /// </summary>
    public class LoggingSupport<T> : IDisposable
    {
        protected LoggingSupport(ITestOutputHelper testOutputHelper)
        {
            LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(config =>
            {
                config.AddXunit(testOutputHelper);
                config.AddTestableLogger(testableLogger);
                config.AddDebug();
                config.AddConsole();
                config.SetMinimumLevel(LogLevel.Debug);
            });

            Logger = LoggerFactory.CreateLogger<LoggingSupport<T>>();
        }
        public void Dispose() => LoggerFactory.Dispose();

        /// <summary>
        /// LoggerFactory for use during tests.
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        protected ILoggerFactory LoggerFactory { get; }

        /// <summary>
        /// Logger for use during tests.
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Contains all log entries. Can be used to assert on log entries.
        /// </summary>
        // ReSharper disable once ReturnTypeCanBeEnumerable.Global
        protected List<string> Messages => testableLogger.Messages;

        private readonly TestableLogger<T> testableLogger = new TestableLogger<T>();
    }
}

