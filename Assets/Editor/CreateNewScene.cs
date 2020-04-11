using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor.SceneManagement;

public class CreateNewScene : EditorWindow
{
    private static string assetFilePath = "Assets/_Project/_Scenes/Generated";


    /* Template Paths*/
    // Default Template Path 
    private const string templateFilePaths = "Assets/_Project/_Scenes/";
    private string defaultTemplatePath = $"{templateFilePaths}template.unity";

    private string sceneName = "New Scene";

    private string folderName = "New Folder";
    private string newPath;

    const int spacerSize = 5;

    enum SceneTemplate { DEFAULT };
    private SceneTemplate currentTemplate;

    [MenuItem("Adrian's Tools/Create New Scene")]
    private static void Init()
    {
        CreateNewScene window = (CreateNewScene)GetWindow(typeof(CreateNewScene));
        window.minSize = new Vector2(300, 200);
        window.Show();
    }

    private void InitializeFolder()
    {
        folderName = sceneName + "_Scene";
        newPath = $"{assetFilePath}/{folderName}";

        if (!AssetDatabase.IsValidFolder(newPath))
        {
            AssetDatabase.CreateFolder(assetFilePath, folderName);
            EditorUtility.FocusProjectWindow();
            AssetDatabase.SaveAssets();

            LogUtils.Log("Scene folder created.");
        }
        else
        {
            LogUtils.LogWarning($"{folderName} already exists! Directory not created.");
        }
    }

    private void OnGUI()
    {
        GUI.backgroundColor = Color.grey;

        EditorGUILayout.Space(spacerSize);
        EditorGUILayout.LabelField("New Scene Configuration", EditorStyles.boldLabel);
        EditorGUILayout.Space(spacerSize);

        sceneName = EditorGUILayout.TextField("Enter New Scene Name", sceneName);
        currentTemplate = (SceneTemplate)EditorGUILayout.EnumPopup("Choose Template", currentTemplate);

        if (GUILayout.Button("Create New Scene") && CanCreateScene())
        {
            int option = EditorUtility.DisplayDialogComplex("Are you sure you want to create this scene?", 
                $"Are you sure you want to create a new scene with these settings?\n\nTemplate: {currentTemplate.ToString()}\nScene name: {sceneName}", 
                "Create", 
                "Cancel", "");

            switch(option)
            {
                // Create button
                case 0:
                    CreateScene();
                    break;
                // Cancel button
                case 1:
                    break;
                // No clue what this is. Probably the X button.
                case 2:
                    break;
                default:
                    LogUtils.LogError("Unrecognized option. Scene creation failed.");
                    break;
            }

            //LogUtils.Log($"{sceneName} has been created!");

            //CreateScene();
        }
    }

    private bool CanCreateScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            return true;
        }
        else
        {
            LogUtils.LogError("Cannot create scene!");
            EditorUtility.DisplayDialog("Scene Creation Error", "Cannot create scene. Make sure all fields are filled.", "Continue");
            return false;
        }
    }

    private void CreateScene()
    {
        InitializeFolder();

        string savePath = $"{newPath}/{sceneName + "_Scene"}{sceneName}.unity";

        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        EditorSceneManager.SaveScene(scene, savePath);

        File.Copy(defaultTemplatePath, savePath, true);
        AssetDatabase.SaveAssets();

        LogUtils.Log("Scene created!");
    }
}
