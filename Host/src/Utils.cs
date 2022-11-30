using NLog;

namespace Host;

public static class Utils
{
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
                .WriteToFile(fileName: "file.txt");
        });
    }

    #endregion
}
