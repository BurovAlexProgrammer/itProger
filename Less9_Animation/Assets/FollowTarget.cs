using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    Transform followTarget;
    [SerializeField]
    Vector3 followOffset;
    [SerializeField]
    Transform lookAtTarget;
    [SerializeField]
    Vector3 lookAtOffset;
    [SerializeField]
    float followSpeed;

    Vector3 velocity = Vector3.zero;

    void Start()
    {
        
    }

    void Update()
    {
        if (followTarget != null)
        {
            var desiredPosition = followTarget.position + followOffset;
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, followSpeed);
        }

        if (lookAtTarget != null)
            transform.LookAt(lookAtTarget.position + lookAtOffset);

    }
}
