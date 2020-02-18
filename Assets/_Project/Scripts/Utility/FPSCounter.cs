using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public int averageFrameRate;
    private float hudRefreshRate = 1;
    public TextMeshProUGUI display_Text;

    private float timer;

    private void Update()
    {
        if(Time.unscaledTime > timer)
        {
            int fps = (int)(1 / Time.unscaledDeltaTime);
            display_Text.SetText($"FPS: {fps}");
            timer = Time.unscaledTime + hudRefreshRate;
        }
    }

    //private void Update()
    //{
    //    float current = 0;
    //    current = Time.frameCount / Time.time;
    //    averageFrameRate = (int)current;
    //    display_Text.SetText($"{averageFrameRate} FPS");
    //}
}
