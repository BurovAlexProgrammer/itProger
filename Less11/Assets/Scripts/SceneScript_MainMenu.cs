using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneScript_MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject 
        mainMenuPanel = null,
        exitDialog = null,
        storePanel = null,
        settingsPanel = null;
    [SerializeField]
    Text coinsCountLabel = null;

    string webplayerQuitURL = "https://portai.ru/Burovav";

    private void Awake()
    {
    }

    void Start()
    {
        var temp = webplayerQuitURL; //Использование переменной для скрытие уведомления компилятора в других режимах Unity
        
    }

    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(LoadScene("Game"));
    }

    IEnumerator LoadScene(string sceneName)
    {
        var sceneFading = Camera.main.GetComponent<SceneFading>();
        sceneFading.FadeOut();
        float fadeTime = sceneFading.GetFadeTime();
        Debug.Log(fadeTime);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(sceneName);
    }

    void HideAllPanels()
    {
        mainMenuPanel.SetActive(false);
        //exitDialog.SetActive(false);
        storePanel.SetActive(false);
    }

    public void ShowMainMenu()
    {
        HideAllPanels();
        mainMenuPanel.SetActive(true);
    }

    public void ShowExitDialog()
    {
        exitDialog.SetActive(true);
    }

    public void ShowStorePanel()
    {
        HideAllPanels();
        storePanel.SetActive(true);
    }

    public void ShowSettingsPanel()
    {
        HideAllPanels();
        settingsPanel.SetActive(true);
    }

    public void OnSettingsChanged()
    {
        coinsCountLabel.text = SettingsController.Instance.GetInt(SettingsController.Names.CoinsCount).ToString();
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
