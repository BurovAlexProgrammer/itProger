using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamesController : MonoBehaviour
{
    [SerializeField]
    GameObject[] carPrefabs;
    [SerializeField]
    GameObject[] respawns;
    [SerializeField]
    float timeToSpawn = 1f;

    void Start()
    {
        StartCoroutine(RespawnCar());
    }

    void Update()
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
            var randomRespawn = Random.Range(0, respawns.Length);
            var respawn = respawns[randomRespawn];
            var newCar = Instantiate(carPrefabs[Random.Range(0,2)], respawn.transform.position, respawn.transform.rotation);
            newCar.GetComponent<CarController>().SetDirection(CarController.RandomDirection());
            yield return new WaitForSeconds(timeToSpawn);
        }
    }
}
