//rev 1
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Behaviour/ChangePrefab")]
public class ChangePrefab : MonoBehaviour
{
    enum Types { OnCollisionEnter }

    [SerializeField]
    GameObject newPrefab;

    [SerializeField]


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(newPrefab, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
    }
}
