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

    public UnityEvent SettingsChanged;

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
        SetValue(Names.CoinsCount, 999);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GetInt(Names.CoinsCount));
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
        [SerializeField]
        object value = null;
        public object Value { get { return value; } }

        public SettingItem(string name, int defValue)
        {
            this.name = name;
            type = Types.Int;
            if (!HasPref())
            {
                PlayerPrefs.SetInt(name, defValue);
                value = defValue;
            }
            else
                value = PlayerPrefs.GetInt(name);
        }
        public SettingItem(string name, float defValue)
        {
            this.name = name;
            type = Types.Float;
            if (!HasPref())
            {
                PlayerPrefs.SetFloat(name, defValue);
                value = defValue;
            }
            else
                value = PlayerPrefs.GetFloat(name);
        }
        public SettingItem(string name, string defValue)
        {
            this.name = name;
            type = Types.String;
            if (!HasPref())
            {
                PlayerPrefs.SetString(name, defValue);
                value = defValue;
            }
            else
                value = PlayerPrefs.GetFloat(name);
        }

        public void SetValue(object value)
        {
            this.value = value;
            if (type == Types.Float)
                PlayerPrefs.SetFloat(name, (float)value);
            if (type == Types.Int)
                PlayerPrefs.SetInt(name, (int)value);
            if (type == Types.String)
                PlayerPrefs.SetString(name, (string)value);
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
