using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Xunit.Abstractions;

namespace Microsoft.Logging.Tests
{
    public class LoggingSupport<T> : IDisposable
    {
        public LoggingSupport(ITestOutputHelper testOutputHelper)
        {
            LoggerFactory = Extensions.Logging.LoggerFactory.Create(config =>
            {
                config.AddXunit(testOutputHelper);
                config.AddTestableLogger(testableLogger);
                config.AddDebug();
                config.AddConsole();
                config.SetMinimumLevel(LogLevel.Debug);
            });

            Logger = LoggerFactory.CreateLogger<T>();
        }
        public void Dispose() => LoggerFactory.Dispose();

        public ILoggerFactory LoggerFactory { get; }
        public ILogger Logger { get; }
        public List<string> Logs => testableLogger.Logs;

        private readonly TestableLogger testableLogger = new TestableLogger();
    }
}

