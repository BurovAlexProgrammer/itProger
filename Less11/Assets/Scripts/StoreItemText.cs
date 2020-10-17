using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[AddComponentMenu("Game/Store/StoreItemText")]
public class StoreItemText : MonoBehaviour
{
    [SerializeField]
    string storeItemName;

    StoreController.StoreItem item;

    Text text;

    enum ItemInfos { Count, Name, Title, Description, CoinsCost, UsdCost }
    [SerializeField]
    ItemInfos itemInfo;

    private void OnValidate()
    {
        if (!Application.isPlaying)
            Init();
    }

    private void Start()
    {
        Init();
        
    }

    void Init()
    {
        item = StoreController.Instance.GetItem(storeItemName);
        text = GetComponent<Text>();
        StoreController.Instance.StoreItemsChanged.AddListener(SetText);
    }

    public void SetText()
    {
        item = StoreController.Instance.GetItem(storeItemName);
        switch (itemInfo)
        {
            case ItemInfos.CoinsCost:
                text.text = item.CoinsCost.ToString();
                break;
            case ItemInfos.Count:
                text.text = item.Count.ToString();
                break;
            case ItemInfos.Description:
                text.text = item.Description;
                break;
            case ItemInfos.Name:
                text.text = item.Name;
                break;
            case ItemInfos.Title:
                text.text = item.Title;
                break;
            case ItemInfos.UsdCost:
                text.text = item.UsdCost.ToString();
                break;
        }
    }
}