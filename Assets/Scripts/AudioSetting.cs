using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;  // Reference to your AudioMixer
    private Slider volumeSlider;
    private const string parameter = "MasterAudioMixer";  // This should match the exposed parameter name in your AudioMixer

    void Start()
    {
        volumeSlider = GetComponent<Slider>();  // Get the slider component

        if (PlayerPrefs.HasKey("masterVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMasterVolume();
        }
    }

    // This method sets the volume of the AudioMixer based on the slider value
    public void SetMasterVolume()
    {
        float volume = volumeSlider.value;
        mixer.SetFloat("master", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    private void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");

        SetMasterVolume();
    }
}
