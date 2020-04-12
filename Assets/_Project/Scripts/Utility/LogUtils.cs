using UnityEngine;

public static class LogUtils
{
    /// <summary>
    /// Logs a message to the console.
    /// </summary>
    /// <param name="message"></param>
    public static void Log(string message)
    {
        string log = $"<color=green>[MESSAGE]: {message}</color>";
        Debug.Log(log);
    }

    /// <summary>
    /// Logs a not implemented message to the console. Useful for remebering non implemented features.
    /// </summary>
    /// <param name="message"></param>
    public static void LogNotImp(string message)
    {
        string log = $"<color=purple>[NOT IMPLEMENTED]: {message}</color>";
        Debug.Log(log);
    }

    /// <summary>
    /// Logs a warning to the console.
    /// </summary>
    /// <param name="message"></param>
    public static void LogWarning(string message)
    {
        string log = $"<color=orange>[WARNING]: </color><color=yellow>{message}</color>";
        Debug.Log(log);
    }

    /// <summary>
    /// Logs an error to the console.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="pause">Set to true to pause the editor when an error is logged.</param>
    public static void LogError(string message, bool pause = false)
    {
        string log = $"<color=red>[ERROR]: {message}</color>";
        Debug.Log(log);

        #if UNITY_EDITOR
        if(pause && Application.isPlaying)
        {
            Debug.Break();
        }
        #endif
    }
}
