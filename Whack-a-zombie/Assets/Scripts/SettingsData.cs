using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsData : MonoBehaviour
{
    public int musicVol = 4;
    public int sfxVol = 4;
    public int runDuration = 69; // 38 - 100
    [SerializeField] private Slider music;
    [SerializeField] private Slider sfx;
    [SerializeField] private TMP_InputField duration;


    private void Start()
    {
        music.onValueChanged.AddListener(SetMusicVol);
        sfx.onValueChanged.AddListener(SetSFXVol);
        duration?.onValueChanged.AddListener(SetDuration);
    }

    private void SetMusicVol(float value)
    {
        musicVol = (int)value;
    }

    private void SetSFXVol(float value)
    {
        sfxVol = (int)value;
    }

    private void SetDuration(string value)
    {
        if (int.TryParse(value, out int result))
        {
            if (result >= 38 && result <= 100)
            {
                runDuration = result;
            }
            else
            {
                duration.text = Mathf.Clamp(result, 38, 100).ToString();
            }
        }
    }
}
