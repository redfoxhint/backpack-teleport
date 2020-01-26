using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";
    private const string SAVE_EXTENSION = "dat";

    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Save(string saveString)
    {
        int saveNumber = 1;

        while (File.Exists($"{SAVE_FOLDER}save_{saveNumber}.{SAVE_EXTENSION}"))
        {
            saveNumber++;
        }

        File.WriteAllText($"{SAVE_FOLDER}save_{saveNumber}.{SAVE_EXTENSION}", saveString);
    }

    public static string Load(FileInfo saveFileToLoad)
    {
        FileInfo[] saveFiles = GetSaveFiles();

        foreach (FileInfo fileInfo in saveFiles)
        {
            if(saveFileToLoad != null)
            {
                if (saveFileToLoad == fileInfo)
                {
                    string saveString = File.ReadAllText(saveFileToLoad.FullName);
                    return saveString;
                }
            }
        }

        return null;
    }

    public static string Load()
    {
        FileInfo[] saveFiles = GetSaveFiles();

        FileInfo mostRecentFile = null;

        foreach (FileInfo fileInfo in saveFiles)
        {
            if (mostRecentFile == null)
            {
                mostRecentFile = fileInfo;
            }
            else
            {
                if (fileInfo.LastWriteTime > mostRecentFile.LastWriteTime)
                {
                    mostRecentFile = fileInfo;
                }
            }
        }

        if (mostRecentFile != null)
        {
            string saveString = File.ReadAllText(mostRecentFile.FullName);
            return saveString;
        }
        else
        {
            return null;
        }
    }

    public static FileInfo[] GetSaveFiles()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);

        if(directoryInfo == null)
        {
            Init();
        }

        FileInfo[] saveFiles = directoryInfo.GetFiles($"*.{SAVE_EXTENSION}");
        return saveFiles;
    }
}
