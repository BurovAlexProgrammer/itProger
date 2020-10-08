using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingKeys
{
    public static string
        SoundOn = "soundOn",
        MusicOn = "musicOn",
        PostEffectOn = "postEffectOn";

    public static int
        Enabled = 1,
        Disabled = 0;

    public static bool IsEnabled(string settingKey)
    {
        var pref = PlayerPrefs.GetInt(settingKey, 0);
        return pref == 1;
    }
}
