using NLog;
using System.Text;
using System;

namespace Utils;

/// <summary>
///     Logger Class
/// </summary>
public static class Logger
{
    /// <summary>
    ///     Where to store logs on the server.
    /// </summary>
    private static readonly string LogPath = "file.log";

    #region logger

    /// <summary>
    ///     Logger method
    /// </summary>
    public static readonly NLog.Logger Log =
        NLog.LogManager.GetCurrentClassLogger();

    /// <summary>
    ///     Init Logger
    /// </summary>
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

/// <summary>
///     Security Class
/// </summary>
public static class Security
{
    /// <summary>
    ///     512 bits hash from a string input to string in hex (utf8 encoded)
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string SHA512(string input)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(input);
        using var hash = System.Security.Cryptography.SHA512.Create();
        var hashedInputBytes = hash.ComputeHash(bytes);

        // Convert to text
        // StringBuilder Capacity is 128, 512 bits / 8 bits in byte * 2(hex)
        var hashedInputStringBuilder = new System.Text.StringBuilder(128);
        foreach (var b in hashedInputBytes)
            // Convert to hex.
            hashedInputStringBuilder.Append(b.ToString("x2"));
        return hashedInputStringBuilder.ToString();
    }

    /// <summary>
    ///     256 bits hash from a string input to string in hex (utf8 encoded)
    /// </summary>
    /// <param name="input"> input</param>
    /// <returns>an hash</returns>
    public static string SHA256(string input)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(input);
        using var hash = System.Security.Cryptography.SHA256.Create();
        var hashedInputBytes = hash.ComputeHash(bytes);

        // Convert to text
        // StringBuilder Capacity is 64, 256 bits / 8 bits in byte * 2(hex)
        var hashedInputStringBuilder = new System.Text.StringBuilder(64);
        foreach (var b in hashedInputBytes)
            // Convert to hex.
            hashedInputStringBuilder.Append(b.ToString("x2"));
        return hashedInputStringBuilder.ToString();
    }

    /// <summary>
    ///     128 bits hash from a string input to string in hex (utf8 encoded)
    /// </summary>
    /// <param name="input">input</param>
    /// <returns> an hash </returns>
    public static string SHA128(string input)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(input);
        using var hash = System.Security.Cryptography.SHA1.Create();
        var hashedInputBytes = hash.ComputeHash(bytes);

        // Convert to text
        // StringBuilder Capacity is 32, because 128 bits / 8 bits in byte * 2
        var hashedInputStringBuilder = new System.Text.StringBuilder(32);
        foreach (var b in hashedInputBytes)
            // Convert to hex.
            hashedInputStringBuilder.Append(b.ToString("x2"));
        return hashedInputStringBuilder.ToString();
    }
}

/// <summary>
///     File Class
/// </summary>
public static class File
{
    /// <summary>
    ///     write text to a file asynchronously.
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="text"></param>
    /// <returns>an awaitable task</returns>
    public static async Task WriteTextAsync(string filePath, string text)
    {
        byte[] encodedText = Encoding.UTF8.GetBytes(text);

        await using var sourceStream = new FileStream(
                   filePath, FileMode.Append, FileAccess.Write, FileShare.None,
                   bufferSize: 4096, useAsync: true);
        
        await sourceStream.WriteAsync(encodedText);
        
    }

    /// <summary>
    ///     Copy files asynchronously.
    /// </summary>
    /// <param name="sourceFile"> source path</param>
    /// <param name="destinationFile"> destination path</param>
    /// <returns> An awaitable Task </returns>
    public static async Task CopyFileAsync(string sourceFile,
                                           string destinationFile)
    {
        using var sourceStream = new FileStream(
                sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096,
                FileOptions.Asynchronous |
                    FileOptions
                        .SequentialScan); using var destinationStream =
                                                     new FileStream(
                                                         destinationFile,
                                                         FileMode.CreateNew,
                                                         FileAccess.Write,
                                                         FileShare.None, 4096,
                                                         FileOptions.Asynchronous |
                                                             FileOptions
                                                                 .SequentialScan);
        await sourceStream.CopyToAsync(destinationStream);
    }
}