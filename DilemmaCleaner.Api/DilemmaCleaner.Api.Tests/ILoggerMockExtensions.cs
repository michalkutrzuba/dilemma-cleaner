using Microsoft.Extensions.Logging;

namespace DilemmaCleaner.Api.Tests;

internal static class ILoggerMockExtensions
{
    public static void VerifyLogError<T>(this Mock<ILogger<T>> loggerMock, string message, Times times)
    {
        loggerMock.VerifyLog(LogLevel.Error, message, times);
    }

    private static void VerifyLog<T>(this Mock<ILogger<T>> loggerMock, LogLevel level, string message, Times times)
    {
        loggerMock.Verify(logger =>
                logger.Log(
                    level,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) =>
                        string.Equals(message, o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            times);
    }
}