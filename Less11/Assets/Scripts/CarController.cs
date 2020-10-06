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

    public enum Directions { forward, left, right}
    [SerializeField]
    Directions direction;
    Rigidbody rigidbody = null;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("DeleteTrigger"))
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
        //rigidbody.velocity = Vector3.forward * speed;
        //rigidbody.AddRelativeForce(Vector3.forward * speed);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("TurnRightBox") && direction == Directions.right)
        {
            float rotateSpeed = speed * turnRightMult;
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, rotateSpeed, 0) * Time.fixedDeltaTime);
            rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
        }
        if (other.transform.CompareTag("TurnLeftBox") && direction == Directions.left)
        {
            float rotateSpeed = speed * turnLeftMult;
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, rotateSpeed, 0) * Time.fixedDeltaTime);
            rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("TurnRightBox") && direction == Directions.right || 
            other.transform.CompareTag("TurnLeftBox") && direction == Directions.left)
        {
            var rotY = rigidbody.rotation.eulerAngles.y;
            var newRotY = Mathf.Round(rotY / 90f) * 90;
            rigidbody.rotation = Quaternion.Euler(0, newRotY, 0);
        }
    }

    public void SetDirection(Directions direction)
    {
        this.direction = direction;
    }

    public static Directions RandomDirection()
    {
        int random = Random.Range(1, 4);
        if (random == 2)
            return Directions.right;
        if (random == 3)
            return Directions.left;
        return Directions.forward;
    }
}
