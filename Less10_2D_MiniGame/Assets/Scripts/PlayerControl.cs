using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    GameObject cameraTarget = null;
    [SerializeField]
    GameObject platformPrefab = null;

    bool isOnPlatform = false;
    float jumpForce = 0f;
    [Tooltip("Скорость набора силы прыжка")]
    [SerializeField]
    float jumpForceAcc = 1f;

    Rigidbody rigidbody = null;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isOnPlatform)
        {
            if (Input.GetAxisRaw("Fire1") != 0)
            {
                jumpForce += jumpForceAcc * Time.deltaTime;
            } else
            {
                if (jumpForce != 0)
                {
                    rigidbody.AddRelativeForce(new Vector3(1f* jumpForce, 1f* jumpForce, 0f));
                    jumpForce = 0;
                }
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            cameraTarget.transform.position = collision.transform.position + new Vector3(1.8f, -0.7f);
            var newPlatform = GameObject.Instantiate(platformPrefab);
            newPlatform.transform.position = collision.transform.position + new Vector3(Random.Range(2.5f, 3.5f), Random.Range(-4, -5));
            newPlatform.transform.localScale = new Vector3(Random.Range(1.2f, 2f), Random.Range(0.5f, 0.7f), 1);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        isOnPlatform = collision.gameObject.tag == "Platform";
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("d");
    }
}
