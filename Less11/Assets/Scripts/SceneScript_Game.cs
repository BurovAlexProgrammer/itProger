using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneScript_Game : MonoBehaviour
{

    [SerializeField]
    GameObject
        GamePanel = null,
        SettingsPanel = null,
        RestartPanel = null,
        DialogMainMenu = null;
    [SerializeField]
    Toggle soundToggle = null, musicToggle = null, postToggle = null;
    [SerializeField]
    Image buttonTimeShift = null;
    [SerializeField]
    Text scoresLabel = null, topScores = null;

    private void Awake()
    {
        GamesController.instance.GameOverEvent += OnGameOver;
        //GamesController.instance.TimeShiftChanged += OnTimeShiftChanged;
    }

    void Start()
    {
        musicToggle.isOn = SettingKeys.IsEnabled(SettingKeys.MusicOn);
        soundToggle.isOn = SettingKeys.IsEnabled(SettingKeys.SoundOn);
        postToggle.isOn = SettingKeys.IsEnabled(SettingKeys.PostEffectOn);
    }

    private void Update()
    {
        scoresLabel.text = GamesController.instance.CurrentScores.ToString();
        topScores.text = GamesController.instance.TopScores.ToString();

        if (GamesController.instance.IsTimeShift)
        {
            buttonTimeShift.color = Color.white.SetAlpha(0.3f);
            buttonTimeShift.raycastTarget = false;
        }
        else
        {
            buttonTimeShift.color = Color.white.SetAlpha(1f);
            buttonTimeShift.raycastTarget = true;
        }
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

    public void TimeShift()
    {
        if (!GamesController.instance.IsTimeShift)
            GamesController.instance.TimeShift();
    }


    public void RestartGame()
    {
        GamesController.instance.RestartGame();
        CloseWindows();
    }
}
