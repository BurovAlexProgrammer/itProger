using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class SettingsController : MonoBehaviour
{
    public static SettingsController Instance { get; private set; }
    [SerializeField]
    List<SettingItem> settingItems = new List<SettingItem>();

    public UnityEvent 
        SettingsChanged, SettingsLoaded;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("SettingsController.instance уже определен.");
        Instance = this;

        settingItems = new List<SettingItem>()
        {   
            new SettingItem(Names.CoinsCount, 0),
            new SettingItem(Names.MusicOn, 1),
            new SettingItem(Names.SoundOn, 1),
            new SettingItem(Names.EffectOn, 1)
        };

    }

    void Start()
    {
        SettingsLoaded?.Invoke();
        SettingsChanged?.Invoke();
    }


    public object GetValue(string name)
    {
        if (!PlayerPrefs.HasKey(name))
            throw new PlayerPrefsException($"Не найден ключ: '{name}'.");
        return settingItems.Single((i) => i.Name.Equals(name)).Value;
    }

    public int GetInt(string name)
    {
        return (int)GetValue(name);
    }

    public float GetFloat(string name)
    {
        return (float)GetValue(name);
    }

    public string GetString(string name)
    {
        return GetValue(name).ToString();
    }

    public void SetValue(string name, object value)
    {
        if (!PlayerPrefs.HasKey(name))
            throw new PlayerPrefsException($"Не найден ключ: '{name}'.");
        settingItems.Single((i) => i.Name.Equals(name)).SetValue(value);
        SettingsChanged?.Invoke();
    }

    [System.Serializable]
    class SettingItem
    {
        enum Types { Int, Float, String }
        [SerializeField]
        string name = "";
        public string Name { get { return name; } }
        [SerializeField]
        Types type = Types.Int;
        object value = null;
        [SerializeField]
        string ValueAsString;
        public object Value { get { return value; } private set { this.value = value; ValueAsString = value.ToString(); } }

        public SettingItem(string name, int defValue)
        {
            this.name = name;
            type = Types.Int;
            if (!HasPref())
            {
                PlayerPrefs.SetInt(name, defValue);
                Value = defValue;
            }
            else
                Value = PlayerPrefs.GetInt(name);
        }
        public SettingItem(string name, float defValue)
        {
            this.name = name;
            type = Types.Float;
            if (!HasPref())
            {
                PlayerPrefs.SetFloat(name, defValue);
                Value = defValue;
            }
            else
                Value = PlayerPrefs.GetFloat(name);
        }
        public SettingItem(string name, string defValue)
        {
            this.name = name;
            type = Types.String;
            if (!HasPref())
            {
                PlayerPrefs.SetString(name, defValue);
                Value = defValue;
            }
            else
                Value = PlayerPrefs.GetFloat(name);
        }

        public void SetValue(object value)
        {
            Value = value;
            if (type == Types.Float)
                PlayerPrefs.SetFloat(name, (float)Value);
            if (type == Types.Int)
                PlayerPrefs.SetInt(name, (int)Value);
            if (type == Types.String)
                PlayerPrefs.SetString(name, (string)Value);
        }

        public bool HasPref()
        {
            return PlayerPrefs.HasKey(name);
        }
    }

    public class Names
    {
        public const string BoostLowMotionCount = "BoostLowMotionCount";
        public const string CoinsCount = "CoinsCount";
        public const string MusicOn = "MusicOn";
        public const string SoundOn = "SoundOn";
        public const string EffectOn = "EffectOn";
    }
}
