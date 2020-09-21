using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CarControl : MonoBehaviour
{
    [SerializeField]
    float accelerationForce = 20000;
    [SerializeField]
    float rotateForce = 100;

    Rigidbody rigidbody;

    [SerializeField]
    Light[] frontLights;

    [SerializeField]
    Light[] rearLights;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
            frontLights.ToList().ForEach(l => {
                l.enabled = Input.GetAxisRaw("Vertical") > 0;
            });
            rearLights.ToList().ForEach(l => {
                l.enabled = Input.GetAxisRaw("Vertical") < 0;
            });
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            var vAxis = Input.GetAxis("Vertical");
            rigidbody.AddForce(new Vector3(0, 0, Input.GetAxis("Vertical") * accelerationForce));
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            var hAxis = Input.GetAxis("Horizontal");
            rigidbody.AddTorque(transform.up * Input.GetAxis("Horizontal") * rotateForce);
        }
    }


}
