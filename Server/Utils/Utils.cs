using NLog;

namespace Utils;

public static class Logger
{
    // Where to store logs on the server.
    private static string LogPath = "file.log";

    #region logger

    public static readonly NLog.Logger Log =
        NLog.LogManager.GetCurrentClassLogger();

    public static void LoggerInit()
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
