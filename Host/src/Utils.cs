using NLog;

namespace Host;

internal static class Utils
{
    // Where to store logs on the server.
    private static string LogPath = "file.log";

    #region logger

    internal static readonly NLog.Logger Log =
        NLog.LogManager.GetCurrentClassLogger();

    internal static void LoggerInit()
    {
        NLog.LogManager.Setup().LoadConfiguration(builder =>
        {
            builder.ForLogger().FilterMinLevel(LogLevel.Info).WriteToConsole();
            builder.ForLogger()
                .FilterMinLevel(LogLevel.Debug)
                .WriteToFile(fileName: LogPath);
        });
    }

    #endregion
}
