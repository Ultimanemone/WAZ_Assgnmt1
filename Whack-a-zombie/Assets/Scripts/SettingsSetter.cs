using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSetter : MonoBehaviour
{
    [SerializeField] private Slider music;
    [SerializeField] private Slider sfx;
    [SerializeField] private TMP_InputField duration;

    private void Start()
    {
        music.value = SettingsData.instance.musicVol;
        sfx.value = SettingsData.instance.sfxVol;
        if (duration != null) duration.text = SettingsData.instance.runDuration.ToString();

        music.onValueChanged.AddListener(SetMusicVol);
        sfx.onValueChanged.AddListener(SetSFXVol);
        duration?.onValueChanged.AddListener(SetDuration);
    }

    private void SetMusicVol(float value)
    {
        SettingsData.instance.musicVol = (int)value;
    }

    private void SetSFXVol(float value)
    {
        SettingsData.instance.sfxVol = (int)value;
    }

    private void SetDuration(string value)
    {
        if (int.TryParse(value, out int result))
        {
            if (result >= 38 && result <= 100)
            {
                SettingsData.instance.runDuration = result;
            }
            else
            {
                duration.text = Mathf.Clamp(result, 38, 100).ToString();
            }
        }
    }
}
