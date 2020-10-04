using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript_MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenuPanel = null;
    [SerializeField]
    GameObject sureExitPanel = null;

    string webplayerQuitURL = "https://portai.ru/Burovav";

    void Start()
    {
        var temp = webplayerQuitURL; //Использование переменной для скрытие уведомления компилятора в других режимах Unity
    }

    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    void HideAllPanels()
    {
        mainMenuPanel.SetActive(false);
        sureExitPanel.SetActive(false);
    }

    public void ShowMainMenu()
    {
        HideAllPanels();
        mainMenuPanel.SetActive(true);
    }

    public void ShowSureExit()
    {
        HideAllPanels();
        sureExitPanel.SetActive(true);
    }

    public void GoToStore()
    {

    }

    public void MuteMusic()
    {

    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
#else
        Application.Quit();
#endif
    }
}
