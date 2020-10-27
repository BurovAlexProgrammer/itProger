using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Behaviour/FollowTarget")]
public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    [SerializeField]
    Vector3 targetOffset = Vector3.zero;

    [SerializeField]
    float followSpeed = 10f;

    [SerializeField]
    float followDistance = 5f;

    [SerializeField]
    float followMinDistance = 2f;

    [SerializeField]
    float followMaxDistance = 8f;

    [SerializeField]
    float smoothTime = 0.3F;

    private void Update()
    {
        var distance = Vector3.Distance(transform.position, target.transform.position);
        var diff = distance - followDistance;
        var direction = Mathf.Sign(diff);
        transform.LookAt(target.transform);
        var newPosition = target.transform.position + targetOffset - Vector3.forward * followDistance;
        //transform.position = Vector3.Lerp(transform.position, newPosition, followSpeed);
        var velocity = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime, followSpeed);
        GetComponent<Rigidbody>().velocity = velocity;
    }
}
