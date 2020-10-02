using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    Transform followTarget = null;
    [SerializeField]
    Vector3 followOffset = Vector3.zero;
    [SerializeField]
    Transform lookAtTarget = null;
    [SerializeField]
    Vector3 lookAtOffset = Vector3.zero;

    [SerializeField]
    [Range(1,100)]
    float _followSpeed = 10;
    float followSpeed = 1;

    Vector3 velocity = Vector3.zero;

    private void OnValidate()
    {
        followSpeed = 10 / _followSpeed;
    }

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
