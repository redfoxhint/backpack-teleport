using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsData
{
    // Gameplay Options
    public bool godMode;
    public bool superSpeed;
    public bool noStamina;

    // Graphics Options
    public bool fullScreen;
    public bool vSync;
    public float testValue;
    public Resolution[] resolutions;

    // Controls Options

    // Sound Options
    public float masterVolumeValue;
    public float soundVolumeValue;
    public float musicVolumeValue;

    public OptionsData(bool godMode, bool superSpeed, bool noStamina, bool fullScreen, bool vSync, float testValue, float masterVolumeValue, float soundVolumeValue, float musicVolumeValue)
    {
        this.godMode = godMode;
        this.superSpeed = superSpeed;
        this.noStamina = noStamina;
        this.fullScreen = fullScreen;
        this.vSync = vSync;
        this.testValue = testValue;
        this.masterVolumeValue = masterVolumeValue;
        this.soundVolumeValue = soundVolumeValue;
        this.musicVolumeValue = musicVolumeValue;
    }
}
