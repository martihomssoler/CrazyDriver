using System.Collections;
using System.Collections.Generic;
using TWCore.Events;
using TWCore.Variables;
using UnityEngine;

public class VolumeSliderHandler : MonoBehaviour
{
    [Header("Master Volume")]
    [SerializeField] private FloatReference MasterVolume;
    [SerializeField] private GameEvent MasterVolumeChanged;

    public void ChangeMasterVolume(float value)
    {
        MasterVolume.SetValue(value);
        MasterVolumeChanged?.Raise();
    }
}
