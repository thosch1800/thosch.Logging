using FluentAssertions;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Logging.Tests
{
    public class LoggingExtensionMethodsTests : LoggingSupport
    {
        public LoggingExtensionMethodsTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact(DisplayName = nameof(LogEnter))]
        public void LogEnter()
        {
            var arg1 = 42.42;
            var arg2 = new { Name = "foo" };
            var arg3 = new byte[] { 0x42, 0x08, 0x15, 0x47, 0x11, 0x42 };

            Logger.LogEnter(args => args
                .Add(nameof(arg1), arg1)
                .Add(nameof(arg2), arg2)
                .Add(nameof(arg3), arg3));

            Logs.FirstOrDefault()
                .Should().NotBeNull()
                .And.Contain(">>>")
                .And.Contain("arg1:42.42")
                .And.Contain("arg2:{ Name = foo }")
                .And.Contain("arg3:42-08-15-47-11-42");
        }

        [Fact(DisplayName = nameof(LogExit))]
        public void LogExit()
        {
            var arg1 = 42.42;
            var arg2 = new { Name = "foo" };
            var arg3 = new byte[] { 0x42, 0x08, 0x15, 0x47, 0x11, 0x42 };

            Logger.LogExit(args => args
                .Add(nameof(arg1), arg1)
                .Add(nameof(arg2), arg2)
                .Add(nameof(arg3), arg3));

            Logs.FirstOrDefault()
                .Should().NotBeNull()
                .And.Contain("<<<")
                .And.Contain("arg1:42.42")
                .And.Contain("arg2:{ Name = foo }")
                .And.Contain("arg3:42-08-15-47-11-42");
        }

        [Fact(DisplayName = nameof(LogMessage))]
        public void LogMessage()
        {
            var arg1 = 42.42;
            var arg2 = new { Name = "foo" };
            var arg3 = new byte[] { 0x42, 0x08, 0x15, 0x47, 0x11, 0x42 };

            Logger.LogMessage("this is the message",
                args => args
                .Add(nameof(arg1), arg1)
                .Add(nameof(arg2), arg2)
                .Add(nameof(arg3), arg3));

            Logs.FirstOrDefault()
                .Should().NotBeNull()
                .And.Contain("###")
                .And.Contain("this is the message")
                .And.Contain("arg1:42.42")
                .And.Contain("arg2:{ Name = foo }")
                .And.Contain("arg3:42-08-15-47-11-42");
        }

        [Fact(DisplayName = nameof(LogException))]
        public void LogException()
        {
            var arg1 = 42.42;
            var arg2 = new { Name = "foo" };
            var arg3 = new byte[] { 0x42, 0x08, 0x15, 0x47, 0x11, 0x42 };

            Logger.LogException(new ApplicationException("outerEx", new NullReferenceException("innerEx")),
                args => args
                .Add(nameof(arg1), arg1)
                .Add(nameof(arg2), arg2)
                .Add(nameof(arg3), arg3));

            Logs.FirstOrDefault()
                .Should().NotBeNull()
                .And.Contain("outerEx")
                .And.Contain("ApplicationException")
                .And.Contain("innerEx")
                .And.Contain("NullReferenceException")
                .And.Contain("arg1:42.42")
                .And.Contain("arg2:{ Name = foo }")
                .And.Contain("arg3:42-08-15-47-11-42");
        }
        
        [Fact(DisplayName = nameof(LogScope))]
        public void LogScope()
        {
            var arg1 = 42.42;
            var arg2 = new { Name = "foo" };
            var arg3 = new byte[] { 0x42, 0x08, 0x15, 0x47, 0x11, 0x42 };

            {
                using var _ = Logger.LogScope(args => args
                    .Add(nameof(arg1), arg1)
                    .Add(nameof(arg2), arg2)
                    .Add(nameof(arg3), arg3));
            }

            var firstLog = Logs.FirstOrDefault();
            var lastLog = Logs.LastOrDefault();
            
            firstLog
                .Should().NotBeNull()
                .And.Contain(">>>")
                .And.Contain("arg1:42.42")
                .And.Contain("arg2:{ Name = foo }")
                .And.Contain("arg3:42-08-15-47-11-42");
            lastLog
                .Should().NotBeNull()
                .And.Contain("<<<")
                .And.Contain("arg1:42.42")
                .And.Contain("arg2:{ Name = foo }")
                .And.Contain("arg3:42-08-15-47-11-42");
        }
    }
}
