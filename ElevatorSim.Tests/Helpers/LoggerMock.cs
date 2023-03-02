using FluentAssertions;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace ElevatorSim.Tests.Helpers
{
    public class LoggerMock<T> : ILogger<T>
    {
        private static IReadOnlyCollection<LogMessage> Empty = new LogMessage[] { };

        public class LogMessage
        {
            public string Message { get; }
            public Exception Exception { get; }

            public LogMessage(
                string message,
                Exception exception)
            {
                Message = message;
                Exception = exception;
            }
        }

        private readonly ConcurrentDictionary<LogLevel, List<LogMessage>> _logMessages = new ConcurrentDictionary<LogLevel, List<LogMessage>>();

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);
            var list = _logMessages.GetOrAdd(
                logLevel,
                _ => new List<LogMessage>());
            list.Add(new LogMessage(message, exception));
        }

        public bool IsEnabled(LogLevel _)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return DisposableAction.Empty;
        }

        public void VerifyNoProblems()
        {
            var messages = Logs(LogLevel.Critical, LogLevel.Error)
                .Select(m => m.Message)
                .ToList();
            messages.Should().BeEmpty(string.Join(", ", messages));
        }

        public void VerifyProblemLogged(params Exception[] expectedExceptions)
        {
            var exceptions = Logs(LogLevel.Error, LogLevel.Critical)
                .Select(m => m.Exception)
                .ToList();
            exceptions.Should().AllBeEquivalentTo(expectedExceptions);
        }

        public IReadOnlyCollection<LogMessage> Logs(params LogLevel[] logLevels)
        {
            return logLevels
                .SelectMany(l => _logMessages.TryGetValue(l, out var list) ? list : Empty)
                .ToList();
        }
    }
}
