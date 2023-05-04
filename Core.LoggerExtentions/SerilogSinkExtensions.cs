using System.Runtime.CompilerServices;
using Serilog;

namespace Core.LoggerExtensions;

public static class SerilogSinkExtensions
{
    public static ILogger AddContext(this ILogger logger, [CallerFilePath] string sourceFilePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        return logger
              .ForContext("FilePath",   sourceFilePath)
              .ForContext("MemberName", memberName)
              .ForContext("LineNumber", sourceLineNumber);
    }
}