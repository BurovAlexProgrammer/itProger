using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    float currentSpeed;
    [SerializeField]
    float speed = 15f,
        turnRightMult = 6f,
        turnLeftMult = -3f;

    public enum Directions { forward, left, right}
    [SerializeField]
    Directions direction;

    [SerializeField]
    float boostForce = 10f;
    [SerializeField]
    [Tooltip("Сколько времени будет держаться разгон")]
    float boostTimeSetter = 2f;
    float boostTime = 0f;

    [SerializeField]
    GameObject leftSignalGroup;
    [SerializeField]
    GameObject rightSignalGroup;
    Coroutine signalCoroutine;

    Rigidbody rigidbody = null;




    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        if (direction != Directions.forward)
            BlinkStart();
            
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
        if (boostTime > 0)
        {
            boostTime -= Time.fixedDeltaTime;
            currentSpeed = speed * boostForce;
        }
        else { 
            boostTime = 0f; 
            currentSpeed = speed; 
        }
        rigidbody.MovePosition(transform.position + transform.forward * currentSpeed * Time.fixedDeltaTime);
        //rigidbody.velocity = Vector3.forward * speed;
        //rigidbody.AddRelativeForce(Vector3.forward * speed);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("TurnRightBox") && direction == Directions.right)
        {
            float rotateSpeed = currentSpeed * turnRightMult;
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, rotateSpeed, 0) * Time.fixedDeltaTime);
            rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
        }
        if (other.transform.CompareTag("TurnLeftBox") && direction == Directions.left)
        {
            float rotateSpeed = currentSpeed * turnLeftMult;
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, rotateSpeed, 0) * Time.fixedDeltaTime);
            rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
        }
    }

    public void BlinkStart()
    {
        signalCoroutine = StartCoroutine(BlinkTurnLight());

        IEnumerator BlinkTurnLight()
        {
            rightSignalGroup.SetActive(false);
            leftSignalGroup.SetActive(false);
            var activeLightGroup = rightSignalGroup;
            if (direction == Directions.left)
                activeLightGroup = leftSignalGroup;

            var toggle = false;
            while (true)
            {
                yield return new WaitForSeconds(0.2f);
                toggle = !toggle;
                activeLightGroup.SetActive(toggle);
            }
        }
    }

    public void BlinkStop()
    {
        StopCoroutine(signalCoroutine);
        rightSignalGroup.SetActive(false);
        leftSignalGroup.SetActive(false);
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("TurnRightBox") && direction == Directions.right || 
            other.transform.CompareTag("TurnLeftBox") && direction == Directions.left)
        {
            //Выравнивание по направлению сетки
            var rotY = rigidbody.rotation.eulerAngles.y;
            var newRotY = Mathf.Round(rotY / 90f) * 90;
            rigidbody.rotation = Quaternion.Euler(0, newRotY, 0);

            BlinkStop();
        }
    }

    public void SetDirection(Directions direction)
    {
        this.direction = direction;
    }

    public void Boost()
    {
        boostTime = boostTimeSetter;
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
