using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : Singleton<GameControl>
{
    [SerializeField]
    GameObject menuPanel = null;
    [SerializeField]
    PlayerControl player = null;
    [SerializeField]
    bool isPause = false;
    [SerializeField]
    int jumpRecord = 0;


    //События
    public event Action GamePauseChanged;

    private void Start()
    {
        ReadRecordFromPrefs();
    }

    public void SetPause(bool value)
    {
        GamePauseChanged?.Invoke();
        isPause = value;
        menuPanel.gameObject.SetActive(isPause);
        player.enabled = !isPause;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetRecord(int jumpCount)
    {
        if (jumpCount > jumpRecord)
        {
            jumpRecord = jumpCount;
            PlayerPrefs.SetInt("JumpCount", jumpRecord);
        }
    }

    public int GetRecord()
    {
        return jumpRecord;
    }

    void ReadRecordFromPrefs()
    {
        jumpRecord = PlayerPrefs.GetInt("JumpCount");
    }


}

