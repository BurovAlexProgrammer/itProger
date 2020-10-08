using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneScript_Game : MonoBehaviour
{
    [SerializeField]
    Toggle soundToggle, musicToggle, postToggle;
    void Start()
    {
        musicToggle.isOn = SettingKeys.IsEnabled(SettingKeys.MusicOn);
        soundToggle.isOn = SettingKeys.IsEnabled(SettingKeys.SoundOn);
        postToggle.isOn = SettingKeys.IsEnabled(SettingKeys.PostEffectOn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleSound()
    {
        PlayerPrefs.SetInt(SettingKeys.SoundOn, soundToggle.isOn ? 1 : 0);
    }

    public void ToggleMusic()
    {
        PlayerPrefs.SetInt(SettingKeys.MusicOn, musicToggle.isOn ? 1 : 0);
    }

    public void TogglePostEffects()
    {
        PlayerPrefs.SetInt(SettingKeys.PostEffectOn, postToggle.isOn ? 1 : 0);
    }
}
