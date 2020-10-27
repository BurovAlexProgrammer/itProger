using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalTankEffect : MonoBehaviour
{
    Rigidbody rigidbody;
    [SerializeField]
    float torForce = 100f;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();   
    }

    void FixedUpdate()
    {
        var vertical = Input.GetAxisRaw("Vertical");
        if (vertical != 0)
        {
            rigidbody.AddRelativeTorque(Vector3.right * torForce * -vertical);
        }
    }
}
