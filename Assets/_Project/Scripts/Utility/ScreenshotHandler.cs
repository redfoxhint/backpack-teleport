using System.Collections;
using System.IO;
using UnityEngine;

public class ScreenshotHandler : Singleton<ScreenshotHandler>
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Delete))
        {
            TakeScreenshot();
        }
    }

    public void TakeScreenshot()
    {
        StartCoroutine(captureScreenshot());
    }

    IEnumerator captureScreenshot()
    {
        yield return new WaitForEndOfFrame();

        string folderPath = $"{Application.dataPath}/Screenshots/";

        if (!Directory.Exists(folderPath))
        {

            Directory.CreateDirectory(folderPath);
        }

        string path = $"{folderPath}_{Random.Range(0, 999)}_{Screen.width}X{Screen.height}.png";

        Texture2D screenImage = new Texture2D(Screen.width, Screen.height);
        //Get Image from screen
        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImage.Apply();
        //Convert to png
        byte[] imageBytes = screenImage.EncodeToPNG();

        //Save image to file
        System.IO.File.WriteAllBytes(path, imageBytes);

        Debug.Log("screen captured");
        yield break;
    }
}
