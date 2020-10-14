using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class GamesController : MonoBehaviour
{
    public static GamesController instance;

    [SerializeField]
    AudioSource musicController;
    [SerializeField]
    AudioListener audioListener;
    [SerializeField]
    UnityEngine.Rendering.PostProcessing.PostProcessLayer postProcessing;

    [Header("Respawn")]
    [SerializeField]
    GameObject[] carPrefabs;
    [SerializeField]
    GameObject[] respawns;
    [SerializeField]
    float initTimeSpan = 1.1f;
    [SerializeField]
    float timeToSpawn = 1f;

    [Header("TimeShift")]
    [SerializeField]
    float timeShiftValue = 0.6f;
    [SerializeField]
    float timeShiftDuration = 2f;
    bool isTimeShift = false;
    public bool IsTimeShift { get { return isTimeShift; } }

    [Header("Scores")]
    [SerializeField]
    int currentScores = 0;
    public int CurrentScores { get { return currentScores; } }
    int topScores = 0;
    public int TopScores { get { return topScores; } }

    bool isGameOver = false;
    public bool IsGameOver { get { return isGameOver; } }
    bool isGamePaused = false;
    Coroutine respawnCoroutine = null;

    public UnityEvent
        SettingsChanged,
        GameOverEvent,
        TimeShiftChanged;

    private void Awake()
    {
        if (instance != null)
            Debug.LogError("GamesController.instance уже определен.");
        instance = this;

        topScores = PlayerPrefs.GetInt("TopScores");
    }

    public void OnSettingsChanged()
    {
        var musicOn = SettingKeys.IsEnabled(SettingKeys.MusicOn);
        musicController.enabled = musicOn;

        var soundOn = SettingKeys.IsEnabled(SettingKeys.SoundOn);
        audioListener.enabled = soundOn;

        var effectsOn = SettingKeys.IsEnabled(SettingKeys.PostEffectOn);
        postProcessing.enabled = effectsOn;
    }

    public void GameOver()
    {
        isGameOver = true;
        GameOverEvent?.Invoke();
        //Debug.Log("GameOver");
    }

    void Start()
    {
        respawnCoroutine = StartCoroutine(RespawnCar());
    }

    void Update()
    {
        if (!isGameOver && !isGamePaused)
            Play();
    }

    void Play()
    {
        bool tap1;
        Vector2 tapPosition1 = new Vector2();
#if UNITY_EDITOR
        tap1 = Input.GetMouseButtonDown(0);
        tapPosition1 = Input.mousePosition;
#else
        tap1 = Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began;
        if (tap1)
            tapPosition1 = Input.touches[0].position
#endif
        if (tap1)
        {
            Ray ray = Camera.main.ScreenPointToRay(tapPosition1);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    //Color newColor = Random.ColorHSV();
                    hit.collider.gameObject.GetComponent<CarController>()?.Boost();
                }
            }
        }
    }

    IEnumerator RespawnCar()
    {
        while (true)
        {
            if (!isGameOver && !isGamePaused)
            {
                var randomRespawn = Random.Range(0, respawns.Length);
                var respawn = respawns[randomRespawn];
                var newCar = Instantiate(carPrefabs[Random.Range(0, 2)], respawn.transform.position, respawn.transform.rotation);
                if (newCar.GetComponent<CarController>() == null)
                    throw new System.Exception("Error");
                newCar.GetComponent<CarController>().SetDirection(CarController.RandomDirection());
            }
            yield return new WaitForSeconds(timeToSpawn);
        }
    }

    public void PauseGame()
    {
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        isGamePaused = false;
    }

    public void RestartGame()
    {
        isGameOver = false;
        GameObject.FindGameObjectsWithTag("Car").ToList().ForEach((car) => GameObject.Destroy(car));
        timeToSpawn = initTimeSpan;
        currentScores = 0;
    }

    public void TimeShift()
    {
        StartCoroutine(TimeShiftCoroutine());
    }

    IEnumerator TimeShiftCoroutine()
    {
        isTimeShift = true;
        Time.timeScale = timeShiftValue;
        //TimeShiftChanged?.Invoke();
        yield return new WaitForSeconds(timeShiftDuration);
        isTimeShift = false;
        Time.timeScale = 1f;
        //TimeShiftChanged?.Invoke();  TODO - разобраться почему в корутине не работает подписка
    }

    public void SetRecord()
    {
        if (currentScores > topScores)
            topScores = currentScores;
        PlayerPrefs.SetInt("TopScores", currentScores);
    }

    public void AddScore(int scoreValue)
    {
        if (IsGameOver) return;
        currentScores += scoreValue;
    }
}
