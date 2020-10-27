using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField]
    GameObject target = null;

    [SerializeField]
    Vector3 offset = Vector3.zero;

    [SerializeField]
    float rotateSpeed = 1.5f;

    enum LockAxises { None, Horizontal, Vertical, }
    [SerializeField]
    LockAxises lockAxis = LockAxises.None;

    [Header("Angles")]
    [SerializeField ,Range(-180f, 180f)]
    float verticalMinAngle = -45;
    [SerializeField, Range(-180f, 180f)]
    float verticalMaxAngle = 45;
    [SerializeField, Range(-180f, 180f)]
    float horizontalMinAngle = -45;
    [SerializeField, Range(-180f, 180f)]
    float horizontalMaxAngle = 45;

    //Previous values to track changes
    float 
        verticalMinAnglePrev,
        verticalMaxAnglePrev,
        horizontalMinAnglePrev,
        horizontalMaxAnglePrev;
    Quaternion original;

    private void OnValidate()
    {
        if (verticalMinAnglePrev != verticalMinAngle)
        {
            if (verticalMinAngle > verticalMaxAngle)
                verticalMaxAngle = verticalMinAngle;
        }
        if (verticalMaxAnglePrev != verticalMaxAngle)
        {
            if (verticalMinAngle > verticalMaxAngle)
                verticalMinAngle = verticalMaxAngle;
        }
        if (horizontalMinAnglePrev != horizontalMinAngle)
        {
            if (horizontalMinAngle > horizontalMaxAngle)
                horizontalMaxAngle = horizontalMinAngle;
        }
        if (horizontalMaxAnglePrev != horizontalMaxAngle)
        {
            if (horizontalMinAngle > horizontalMaxAngle)
                horizontalMinAngle = horizontalMaxAngle;
        }
        verticalMinAnglePrev = verticalMinAngle;
        verticalMaxAnglePrev = verticalMaxAngle;
        horizontalMinAnglePrev = horizontalMinAngle;
        horizontalMaxAnglePrev = horizontalMaxAngle;
    }

    private void Start()
    {
        original = transform.localRotation;
    }

    void Update()
    {
        var localRotateVector =  transform.parent.InverseTransformDirection(target.transform.position - transform.position + offset);
        var rotation = Quaternion.LookRotation(localRotateVector);
        rotation.z = original.z;
        if (lockAxis == LockAxises.Horizontal)
            rotation.y = original.y;
        if (lockAxis == LockAxises.Vertical)
            rotation.x = original.x;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotation, Time.deltaTime * rotateSpeed);

        var angles = transform.localEulerAngles;
        
        if (lockAxis != LockAxises.Vertical)
        {
            if (angles.x > 180) angles.x -= 360;
            angles.x = Mathf.Clamp(angles.x, verticalMinAngle, verticalMaxAngle);
        }
        if (lockAxis != LockAxises.Horizontal)
        {
            if (angles.y > 180) angles.y -= 360;
            angles.y = Mathf.Clamp(angles.y, horizontalMinAngle, horizontalMaxAngle);
        }

        transform.localEulerAngles = angles;
    }
}
