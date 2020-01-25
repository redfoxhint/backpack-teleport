using UnityEngine;

using System;
using System.Collections.Generic;
using System.Text;

// Delegates
public delegate void CommandHandler(string[] args);

public class DeveloperConsole
{
    private const string repeatCommandName = "!!";

    private List<string> commandHistory = new List<string>();
    private Dictionary<string, ConsoleCommand> commands = new Dictionary<string, ConsoleCommand>();

    // Events
    public delegate void LogChangedHandler(string log);
    public event LogChangedHandler logChanged;

    public delegate void VisibilityChangedHandler(bool visible);
    public event VisibilityChangedHandler visibilityChanged;

    class ConsoleCommand
    {
        public string Command { get; private set; }
        public CommandHandler Handler { get; private set; }
        public string Help { get; private set; }

        public ConsoleCommand(string command, CommandHandler handler, string help)
        {
            Command = command;
            Handler = handler;
            Help = help;
        }
    }

    public DeveloperConsole()
    {
        RegisterCommand("hide", HideConsole, "Hides the console.");
        RegisterCommand("reload", ReloadScene, "Reloads the current scene.");
        RegisterCommand("exit", ExitGame, "Exits the game.");
        RegisterCommand("echo", Echo, "Echos the input back to the console.");
    }

    private void RegisterCommand(string command, CommandHandler handler, string help)
    {
        commands.Add(command, new ConsoleCommand(command, handler, help));
    }

    public void AppendLogLine(string line)
    {
        logChanged?.Invoke(line);
    }

    public void RunCommandString(string commandString)
    {
        AppendLogLine($"> {commandString}");

        string[] commandSplit = ParseArguements(commandString);
        string[] args = new string[0];

        if (commandSplit.Length == 0 || commandSplit == null)
            return;

        if (commandSplit.Length >= 2)
        {
            int numArgs = commandSplit.Length - 1;
            args = new string[numArgs];
            Array.Copy(commandSplit, 1, args, 0, numArgs);
        }

        RunCommand(commandSplit[0].ToLower(), args);
        commandHistory.Add(commandString);
    }

    public void RunCommand(string command, string[] args)
    {
        ConsoleCommand reg = null;

        if(!commands.TryGetValue(command, out reg))
        {
            AppendLogLine(string.Format("Unknown command '{0}', type 'help' for list.", command));
        }
        else
        {
            if(reg.Handler == null)
            {
                AppendLogLine(string.Format("Unable to process command '{0}', handler was null."));
            }
            else
            {
                reg.Handler(args);
            }
        }
    }

    private static string[] ParseArguements(string commandString)
    {
        LinkedList<char> paramChars = new LinkedList<char>(commandString.ToCharArray());
        bool inQuote = false;
        var node = paramChars.First;

        while(node != null)
        {
            var next = node.Next;
            if(node.Value == '"')
            {
                inQuote = !inQuote;
                paramChars.Remove(node);
            }

            if(!inQuote && node.Value == ' ')
            {
                node.Value = '\n';
            }
            node = next;
        }

        char[] paramCharsArray = new char[paramChars.Count];
        paramChars.CopyTo(paramCharsArray, 0);

        return (new string(paramCharsArray)).Split(new char[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
    }

    #region Command Handlers

    private void HideConsole(string[] args)
    {
        GameObject.FindObjectOfType<DeveloperConsoleUI>().ToggleVisibility();
    }

    private void ReloadScene(string[] args)
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    private void ExitGame(string[] args)
    {
        if(Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }

    private void Echo(string[] args)
    {
        
    }

    #endregion
}
