using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsData
{
    // Gameplay Options
    public bool useGamepad;

    // Graphics Options
    public bool fullScreen;
    public bool vSync;
    public Resolution[] resolutions;

    // Controls Options

    // Sound Options
    public float masterVolumeValue;
    public float soundVolumeValue;
    public float musicVolumeValue;

    public OptionsData(bool useGamepad, bool fullScreen, bool vSync, float masterVolumeValue, float soundVolumeValue, float musicVolumeValue)
    {
        this.useGamepad = useGamepad;
        this.fullScreen = fullScreen;
        this.vSync = vSync;
        this.masterVolumeValue = masterVolumeValue;
        this.soundVolumeValue = soundVolumeValue;
        this.musicVolumeValue = musicVolumeValue;
    }
}
