using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        volumeSlider.value = AudioListener.volume;

        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void OnVolumeChanged(float newVolume)
    {
        AudioManager.instance.SetMasterVolume(newVolume);

        PlayerPrefs.SetFloat("masterVolume", newVolume);
        PlayerPrefs.Save();
    }

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("MasterVolume");
            volumeSlider.value = savedVolume;
            AudioManager.instance.SetMasterVolume(savedVolume);
        }
    }
}
