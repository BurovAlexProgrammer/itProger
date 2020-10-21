using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using UnityEditor;
using UnityEngine.UI;

[ExecuteAlways]
public class StoreController : MonoBehaviour
{
    public static StoreController Instance;

    [SerializeField]
    List<StoreItem> storeItems;

    public UnityEvent BuySuccessful, BuyFailedNotEnoughMoney, StoreItemsChanged;

    private void OnValidate()
    {
        Init();
    }

    private void Awake()
    {
        Init();
        SettingsController.Instance.SetValue(SettingsController.Names.CoinsCount, 999);  //Temp remove it
    }

    void Init()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            throw new System.Exception("Обнаружен дубликат");
    }

    void Start()
    {
        storeItems = new List<StoreItem>() {
            new StoreItem("Boost Slowmotion","Slow motion", 100),
            new StoreItem("Donnut1","Donnut for programmer", 1f),
            new StoreItem("Donnut2","Donnut with coffee for programmer", 2f)
        };
        StoreItemsChanged?.Invoke();
    }

    public void Buy(string itemName)
    {
        var item = storeItems.Single((i) => i.Name.Equals(itemName));
        if (item.IsDonnut) //Донат
        {
            //TODO написать покупку доната
        }
        else //Внитриигровая волюта
        {
            var coinsCount = SettingsController.Instance.GetInt(SettingsController.Names.CoinsCount);
            if (coinsCount >= item.CoinsCost)
            {
                coinsCount -= item.CoinsCost;
                item.AddCount(1);
                BuySuccessful?.Invoke();
                SettingsController.Instance.SetValue(SettingsController.Names.CoinsCount, coinsCount);
                StoreItemsChanged?.Invoke();
            } else
            {
                BuyFailedNotEnoughMoney?.Invoke();
            }
        }
    }

    public StoreItem GetItem(string itemName)
    {
        try
        {
           return storeItems.Single((i) => i.Name.Equals(itemName));
        }
        catch {
            throw new System.Exception($"Не найден ключ: '{itemName}'");
        }
    }

    [System.Serializable]
    public class StoreItem {
        [SerializeField]
        string name, title, description;
        public string Name { get { return name; } }
        public string Title { get { return title; } }
        public string Description { get { return description; } }

        [SerializeField]
        bool isDonnut;
        public bool IsDonnut { get { return isDonnut; } }
        [SerializeField]
        int coinsCost;
        public int CoinsCost { get { return coinsCost; } }
        [SerializeField]
        float usdCost;
        public float UsdCost { get { return usdCost; } }
        [SerializeField]
        int count;
        public int Count { get { return count; } }

        public StoreItem(string name, string title, int coinsCost, string description = "")
        {
            isDonnut = false;
            this.name = name;
            this.title = title;
            this.coinsCost = coinsCost;
            this.usdCost = 0f;
            this.description = description;
            count = GetCountPref();
        }

        public StoreItem(string name, string title, float usdCost, string description = "")
        {
            isDonnut = true;
            this.name = name;
            this.title = title;
            this.coinsCost = 0;
            this.usdCost = usdCost;
            this.description = description;
            count = GetCountPref();
        }

        /// <summary>
        /// Get count value from PlayerPref 
        /// </summary>
        /// <returns></returns>
        int GetCountPref()
        {
            return PlayerPrefs.GetInt($"StoreItem<{name}>");
        }

        /// <summary>
        /// Update value of playerPref in current StoreItem
        /// </summary>
        void SetCountPref()
        {
            PlayerPrefs.SetInt($"StoreItem<{name}>", count);
        }

        /// <summary>
        /// Add item count (you can use -1)
        /// </summary>
        /// <param name="count"></param>
        public void AddCount(int count)
        {
            this.count += count;
            SetCountPref();
        }
    }
}