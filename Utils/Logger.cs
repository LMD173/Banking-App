namespace BankingApp.Utils;

/// <summary>
/// A simple utility class to log messages to the console.
/// Imported from a different project.
/// </summary>
public class Logger
{

    /// <summary>
    /// Logs an input message.
    /// </summary>
    /// <param name="message">the input message to output.</param>
    /// <param name="newLine">whether to add a new line after the message.</param>
    public static void Input(string message, bool newLine = false)
    {
        Log($"{message} >>> ", ConsoleColor.Cyan, newLine);
    }

    /// <summary>
    /// Logs an informational message.
    /// </summary>
    /// <param name="message">the message to output.</param>
    /// <param name="newLine">whether to add a new line after the message.</param>
    public static void Info(string message, bool newLine = true)
    {
        Log($"[INFO] {message}", ConsoleColor.White, newLine);
    }

    /// <summary>
    /// Logs an error message.
    /// </summary>
    /// <param name="message">the error message to output.</param>
    /// <param name="newLine">whether to add a new line after the message.</param>
    public static void Error(string message, bool newLine = true)
    {
        Log($"[ERROR] {message}", ConsoleColor.Red, newLine);
    }

    /// <summary>
    /// Logs a success message.
    /// </summary>
    /// <param name="message">the success message to output.</param>
    /// <param name="newLine">whether to add a new line after the message.</param>
    public static void Success(string message, bool newLine = true)
    {
        Log($"[SUCCESS] {message}", ConsoleColor.Green, newLine);
    }

    /// <summary>
    /// Logs a warning message.
    /// </summary>
    /// <param name="message">the warning message to output.</param>
    /// <param name="newLine">whether to add a new line after the message.</param>
    public static void Warn(string message, bool newLine = true)
    {
        Log($"[WARNING] {message}", ConsoleColor.Yellow, newLine);
    }

    /// <summary>
    /// Logs a message to the console.
    /// </summary>
    /// <param name="message">the message to log to the console.</param>
    /// <param name="color">the console colour to use.</param>
    /// <param name="newLine">whether to add a new line after the message.</param>
    private static void Log(string message, ConsoleColor color = ConsoleColor.White, bool newLine = true)
    {
        Console.ForegroundColor = color;
        if (newLine)
        {
            Console.WriteLine(message);
        }
        else
        {
            Console.Write(message);
        }
        Console.ResetColor();
    }
}