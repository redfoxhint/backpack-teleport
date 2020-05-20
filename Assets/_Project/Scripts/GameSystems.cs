using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script will be responsible for setting up all the required the
 * game needs in order to function as intended.
 * 
 * Required systems:
 * - Notification Manager
 * - Quest Manager
 * - Game Manager
 * = Scene Loader
 * - Audio Manager
 * 
 */

public class GameSystems : PersistentSingleton<GameSystems>
{
    [Header("Prefabs")] 
    [SerializeField] private GameObject notificationManagerPrefab;
    [SerializeField] private GameObject questManagerPrefab;
    [SerializeField] private GameObject gameManagerPrefab;
    [SerializeField] private GameObject sceneLoaderPrefab;
    [SerializeField] private GameObject audioManagerPrefab;
    [SerializeField] private GameObject cameraFunctionsPrefab;
    [SerializeField] private GameObject deathScreenPrefab;
    [SerializeField] private GameObject screenshotHandlerPrefab;

    private NotificationManager notificationManager;
    private QuestManager questManager;
    private GameManager gameManager;
    private SceneLoader sceneLoader;
    private AudioManager audioManager;
    private CameraFunctions cameraFunctions;
    private RespawnScreenController deathScreen;
    private ScreenshotHandler screenshotHandler;

    public override void Awake()
    {
        base.Awake();

        notificationManager = Instantiate(notificationManagerPrefab, transform).GetComponentInChildren<NotificationManager>();
        notificationManager = NotificationManager.Instance;

        questManager = Instantiate(questManagerPrefab, transform).GetComponentInChildren<QuestManager>();
        questManager = QuestManager.Instance;

        sceneLoader = Instantiate(sceneLoaderPrefab, transform).GetComponent<SceneLoader>();
        sceneLoader = SceneLoader.Instance;

        gameManager = Instantiate(gameManagerPrefab, transform).GetComponent<GameManager>();
        gameManager = GameManager.Instance;

        audioManager = Instantiate(audioManagerPrefab, transform).GetComponent<AudioManager>();
        audioManager = AudioManager.Instance;

        //cameraFunctions = CameraFunctions.Instance;
        //cameraFunctions.transform.SetParent(transform);

        deathScreen = Instantiate(deathScreenPrefab, transform).GetComponent<RespawnScreenController>();
        deathScreen = RespawnScreenController.Instance;

        screenshotHandler = Instantiate(screenshotHandlerPrefab, transform).GetComponent<ScreenshotHandler>();
        screenshotHandler = ScreenshotHandler.Instance;

    }
}
