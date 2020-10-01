using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float rotateSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        var hAxis = Input.GetAxis("Horizontal");
        var vAxis = Input.GetAxis("Vertical");
        if (hAxis != 0)
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime * hAxis);
        if (vAxis != 0)
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * vAxis);
    }
}
