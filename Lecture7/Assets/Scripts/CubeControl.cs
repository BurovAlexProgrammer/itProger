using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeControl : MonoBehaviour
{

    [SerializeField]
    float accelerationForce = 20000;
    [SerializeField]
    float rotateForce = 100;
    Rigidbody thisRigidbody;
    AudioSource audioSource;
    [SerializeField]
    float pitch = 1;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var vAxis = Input.GetAxis("Vertical");
        var hAxis = Input.GetAxis("Horizontal");

        if (vAxis != 0)
            thisRigidbody.AddForce(new Vector3(0, 0, vAxis * accelerationForce));
        

        if (hAxis != 0)
            thisRigidbody.AddTorque(transform.up * hAxis * rotateForce);

        if (hAxis + vAxis != 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.pitch = Mathf.Abs(hAxis) + Mathf.Abs(vAxis);
                audioSource.Play();
            }
        }
    }
}
