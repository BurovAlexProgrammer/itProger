using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    GameObject model = null;
    [SerializeField]
    GameObject cameraTarget = null;
    [SerializeField]
    GameObject platformPrefab = null;
    [SerializeField]
    GameObject killZone = null;

    bool isOnPlatform = false;
    
    float jumpForce = 0f;
    [Tooltip("Скорость набора силы прыжка")]
    [SerializeField]
    float jumpForceAcc = 1.5f;
    [SerializeField]
    float jumpForceMax = 500;
    int jumpAmount = 0;

    Rigidbody _rigidbody = null;
    AudioSource _audioSource = null;


    [SerializeField]
    AudioClip[] ohs;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        jumpAmount = 0;
    }

    public int GetJumpAmount()
    {
        
        return jumpAmount;
    }

    void Update()
    {
        var height = 1f;
        if (isOnPlatform)
        {
            if (Input.GetAxisRaw("Fire1") != 0)
            {
                jumpForce += jumpForceAcc * Time.deltaTime;
                if (jumpForce > jumpForceMax) 
                    jumpForce = jumpForceMax;
                height = (jumpForceMax - jumpForce) / (jumpForceMax*2) + 0.5f;
                model.transform.localScale = new Vector3(1f, height, 1f);
            } else
            {
                if (jumpForce != 0)
                {
                    _rigidbody.AddForce(new Vector3(1f* jumpForce, 1f* jumpForce, 0f));
                    jumpForce = 0;
                    model.transform.localScale = Vector3.one;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            cameraTarget.transform.position = collision.transform.position + new Vector3(1.8f, -0.7f);
            killZone.transform.position = collision.transform.position + new Vector3(0, -13f);
            var newPlatform = Instantiate(platformPrefab);
            newPlatform.transform.position = collision.transform.position + new Vector3(Random.Range(2.5f, 3.5f), Random.Range(-4, -5));
            newPlatform.transform.localScale = new Vector3(Random.Range(1.2f, 2f), Random.Range(0.5f, 0.7f), 1);
            _audioSource.clip = ohs[Random.Range(0, ohs.Length - 1)];
            _audioSource.Play();
            jumpAmount++;
            collision.gameObject.tag = "PlatformDisabled";
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        isOnPlatform = collision.gameObject.tag == "PlatformDisabled";
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "KillZone")
        {
            GameControl.Instance.SetRecord(jumpAmount);
            GameControl.Instance.SetPause(true);
        }
    }

}
