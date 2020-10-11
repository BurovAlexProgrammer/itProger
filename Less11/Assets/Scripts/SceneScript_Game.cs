using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneScript_Game : MonoBehaviour
{

    [SerializeField]
    GameObject 
        GamePanel,
        SettingsPanel,
        RestartPanel,
        DialogMainMenu;
    [SerializeField]
    Toggle soundToggle, musicToggle, postToggle;

    private void Awake()
    {
        GamesController.instance.GameOverEvent += OnGameOver;
    }

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

    public void CloseWindows()
    {
        SettingsPanel.SetActive(false);
        RestartPanel.SetActive(false);
    }

    public void ShowSettingsMenu()
    {
        CloseWindows();
        SettingsPanel.SetActive(true);
    }

    public void ShowRestartMenu()
    {
        CloseWindows();
        RestartPanel.SetActive(true);
    }

    public void ShowDialogMainMenu()
    {
        DialogMainMenu.SetActive(true);
    }

    public void CloseDialogMainMenu()
    {
        DialogMainMenu.SetActive(false);
    }

    public void TryGoToMainMenu()
    {
        if (!GamesController.instance.IsGameOver)
        {
            ShowDialogMainMenu();
        } else
        {
            GoToMainMenu();
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void OnGameOver()
    {
        RestartPanel.SetActive(true);
    }
}
