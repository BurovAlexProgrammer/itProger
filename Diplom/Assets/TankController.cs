using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField]
    float accForce = 1000;
    Rigidbody rigidbody;

    [SerializeField]
    List<WheelCollider> motorWheels;
    [SerializeField]
    List<WheelCollider> streetWheels;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            /*
            motorWheels.ForEach(w => {
                w.motorTorque = accForce * Time.deltaTime * Input.GetAxis("Vertical");
            });
            */
        }
    }
}
