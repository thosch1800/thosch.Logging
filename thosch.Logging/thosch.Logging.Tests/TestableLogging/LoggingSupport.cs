using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace thosch.Logging.Tests.TestableLogging
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
    public class LoggingSupport : IDisposable
    {
        public LoggingSupport(ITestOutputHelper testOutputHelper)
        {
            LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(config =>
            {
                config.AddXunit(testOutputHelper);
                config.AddTestableLogger(testableLogger);
                config.AddDebug();
                config.AddConsole();
                config.SetMinimumLevel(LogLevel.Debug);
            });

            Logger = LoggerFactory.CreateLogger<LoggingSupport>();
        }
        public void Dispose() => LoggerFactory.Dispose();

        /// <summary>
        /// LoggerFactory for use during tests.
        /// </summary>
        public ILoggerFactory LoggerFactory { get; }

        /// <summary>
        /// Logger for use during tests.
        /// </summary>
        public ILogger Logger { get; }

        /// <summary>
        /// Contains all log entries. Can be used to assert on log entries.
        /// </summary>
        public List<string> Logs => testableLogger.Logs;

        private readonly TestableLogger testableLogger = new TestableLogger();
    }
}

