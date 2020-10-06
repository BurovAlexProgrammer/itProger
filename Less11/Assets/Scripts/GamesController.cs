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
