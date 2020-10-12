using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    public static SettingsController Instance { get; private set; }

    public event System.Action SettingsChanged;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("SettingsController.instance уже определен.");
        Instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
