using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{   
    public AudioMixer audioMixer;
    public bool isFullScreen;

    public void FullScreenToggle()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    public void AudioVolume(float sliderValue)
    {
        audioMixer.SetFloat("masterVolume", sliderValue);
    }

}
