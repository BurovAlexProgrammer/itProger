using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [SerializeField]
    float speed = 15f,
        turnRightMult = 6f,
        turnLeftMult = -3f;

    [SerializeField]
    bool turnRight, turnLeft;

    Rigidbody rigidbody = null;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(transform.position - transform.forward * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("TurnRightBox") && turnRight)
        {
            float rotateSpeed = speed * turnRightMult;
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, rotateSpeed, 0) * Time.fixedDeltaTime);
            rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
        }
        if (other.transform.CompareTag("TurnLeftBox") && turnLeft)
        {
            float rotateSpeed = speed * turnLeftMult;
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, rotateSpeed, 0) * Time.fixedDeltaTime);
            rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("TurnRightBox") && turnRight || 
            other.transform.CompareTag("TurnLeftBox") && turnLeft)
        {
            var rotY = rigidbody.rotation.eulerAngles.y;
            var newRotY = Mathf.Round(rotY / 90f) * 90;
            rigidbody.rotation = Quaternion.Euler(0, newRotY, 0);
        }


    }
}
