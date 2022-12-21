using NLog;
using System.Text;

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

public static class Security
{
    public static string SHA512(string input)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(input);
        using (var hash = System.Security.Cryptography.SHA512.Create())
        {
            var hashedInputBytes = hash.ComputeHash(bytes);

            // Convert to text
            // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2
            // symbols for byte
            var hashedInputStringBuilder = new System.Text.StringBuilder(128);
            foreach (var b in hashedInputBytes)
                // Convert to hex.
                hashedInputStringBuilder.Append(b.ToString("x2"));
            return hashedInputStringBuilder.ToString();
        }
    }
}

public static class Io
{
    public static async Task WriteTextAsync(string filePath, string text)
    {
        byte[] encodedText = Encoding.UTF8.GetBytes(text);

        using (FileStream sourceStream = new FileStream(
                   filePath, FileMode.Append, FileAccess.Write, FileShare.None,
                   bufferSize: 4096, useAsync: true))
        {
            await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
        };
    }

    public static async Task CopyFileAsync(string sourceFile,
                                           string destinationFile)
    {
        using (
            var sourceStream = new FileStream(
                sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096,
                FileOptions.Asynchronous |
                    FileOptions
                        .SequentialScan)) using (var destinationStream =
                                                     new FileStream(
                                                         destinationFile,
                                                         FileMode.CreateNew,
                                                         FileAccess.Write,
                                                         FileShare.None, 4096,
                                                         FileOptions.Asynchronous |
                                                             FileOptions
                                                                 .SequentialScan))
            await sourceStream.CopyToAsync(destinationStream);
    }
}
