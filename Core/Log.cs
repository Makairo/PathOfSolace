using System.IO;

namespace PathOfSolace.Core;

public static class Log
{
    public static readonly string FilePath = "Logs/events.log";

    public static void Write(string message)
    {
        File.AppendAllText(FilePath, $"[{System.DateTime.Now:HH:mm:ss}] {message}\n");
    }

    public static void Clear()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);

        File.WriteAllText(FilePath, string.Empty);
    }
}