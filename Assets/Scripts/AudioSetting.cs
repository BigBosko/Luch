using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider SFXslider;
    private const string parameter = "MasterAudioMixer";

    void Start()
    {

        if (PlayerPrefs.HasKey("masterVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMasterVolume();
            SetSFXVolume();
        }
    }

    public void SetMasterVolume()
    {
        float volume = volumeSlider.value;
        mixer.SetFloat("master", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = SFXslider.value;
        mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        volumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        SetMasterVolume();
        SetSFXVolume();
    }
}
