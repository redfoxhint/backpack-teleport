using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private string defaultTemplateDayPath = $"{templateFilePaths}templateDay.unity";
    private string defaultTemplateNightPath = $"{templateFilePaths}templateNight.unity";

    /* Custom Fields */
    private string sceneName = "New Scene";
    private SceneTemplate currentTemplate;
    private bool addToBuildIndex;

    // Level Data Config
    private string levelName = "Default Name";
    private string levelSlug = "ex: level_demo_one";
    private string levelDescription = "Default Description";
    private Sprite levelSelectPreview;
    private bool levelLockedByDefault;

    private string folderName = "New Folder";
    private string newPath;
    const int spacerSize = 5;
    enum SceneTemplate { DEFAULTDAY, DEFAULTNIGHT };
    private LevelData sceneLevelData;

    [MenuItem("Adrian's Tools/Create New Scene")]
    private static void Init()
    {
        CreateNewScene window = (CreateNewScene)GetWindow(typeof(CreateNewScene));
        window.minSize = new Vector2(300, 10);
        window.titleContent.text = "Create New Scene";
        window.Show();
    }

    private void InitializeFolder()
    {
        if (!Directory.Exists(assetFilePath))
        {
            Directory.CreateDirectory(assetFilePath);
            LogUtils.LogWarning("Asset save path folder [Generated] was not found. Folder generated.");
        }
        else
        {
            LogUtils.LogWarning("Asset save path folder [Generated] was found.");
        }

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

        sceneName = EditorGUILayout.TextField("Scene Name", sceneName);
        currentTemplate = (SceneTemplate)EditorGUILayout.EnumPopup("Choose Template", currentTemplate);

        EditorGUILayout.Space(spacerSize);

        addToBuildIndex = EditorGUILayout.BeginToggleGroup("Add to build settings?", addToBuildIndex);
        levelName = EditorGUILayout.TextField("Level Name", levelName);
        levelSlug = EditorGUILayout.TextField("Level Slug", levelSlug);
        EditorGUILayout.LabelField("Level Description");
        levelDescription = EditorGUILayout.TextArea(levelDescription, GUILayout.Height(150f));
        EditorGUILayout.Space(spacerSize);
        levelSelectPreview = (Sprite)EditorGUILayout.ObjectField(levelSelectPreview, typeof(Sprite), true);
        levelLockedByDefault = EditorGUILayout.Toggle("Level Starts Locked", levelLockedByDefault);
        EditorGUILayout.EndToggleGroup();

        EditorGUILayout.Space(spacerSize);

        //addToBuildIndex = EditorGUILayout.Toggle("Add to build settings?", addToBuildIndex);

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
        }
    }

    private void CreateScene()
    {
        InitializeFolder();

        string savePath = $"{newPath}/{sceneName + "_Scene"}{sceneName}.unity";

        // Save the currently open scene.
        Scene currentOpenScene = EditorSceneManager.GetActiveScene();
        EditorSceneManager.SaveScene(currentOpenScene);

        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        EditorSceneManager.SaveScene(scene, savePath);

        switch(currentTemplate)
        {
            case SceneTemplate.DEFAULTDAY:
                File.Copy(defaultTemplateDayPath, savePath, true);
                break;
            case SceneTemplate.DEFAULTNIGHT:
                File.Copy(defaultTemplateNightPath, savePath, true);
                break;
        }

        if(addToBuildIndex)
        {
            AddSceneToBuildIndex(savePath);
        }

        EditorSceneManager.OpenScene(savePath, OpenSceneMode.Single);
        AssetDatabase.SaveAssets();

        LogUtils.Log("Scene created!");
    }

    private void AddSceneToBuildIndex(string scenePath)
    {
        List<EditorBuildSettingsScene> scenes = EditorBuildSettings.scenes.ToList();
        scenes.Add(new EditorBuildSettingsScene(scenePath, true));

        EditorBuildSettings.scenes = scenes.ToArray();

        LevelData levelData = CreateLevelData();
        levelData.levelBuildIndex = EditorBuildSettings.scenes.Length - 1;

        SelectMainMenuPrefab();
    }

    private LevelData CreateLevelData()
    {
        sceneLevelData = ScriptableObject.CreateInstance<LevelData>();
        AssetDatabase.CreateAsset(sceneLevelData, $"{newPath}/{sceneName}_Scene.asset");

        return sceneLevelData;
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

    private void SelectMainMenuPrefab()
    {
        string assetPath = "Assets/_Project/Prefabs/UI Elements/Main Menu/Resources/Prefabs/pfbMainMenu.prefab";
        GameObject asset = (GameObject)AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject));
        EditorGUIUtility.PingObject(asset);
        Selection.activeObject = asset;
    }
}