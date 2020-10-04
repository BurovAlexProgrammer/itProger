using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneControl : MonoBehaviour
{
    [SerializeField]
    PlayerControl playerControl = null;
    [SerializeField]
    Text jumpAmountLabel = null;
    [SerializeField]
    Text jumpRecordLabel = null;

    private void Awake()
    {
        Debug.Log("Awake");
        GameControl.Instance.GamePauseChanged += OnGamePauseChanged;
    }

    private void FixedUpdate()
    {
        jumpAmountLabel.text = playerControl.GetJumpAmount().ToString();
    }

    public void StartGame()
    {
        GameControl.Instance.SetPause(false);
    }

    public void SetRecordLabel(string label)
    {
        jumpRecordLabel.text = label;
    }

    void OnGamePauseChanged()
    {
        SetRecordLabel("Рекорд: "+GameControl.Instance.GetRecord());
    }

    
}
