using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    float mouseSense = 30f;

    [Header("Angles")]
    [SerializeField, Range(-180f, 180f)]
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
        if (Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0)
        {
            var angles = Vector3.zero;
            angles = transform.localEulerAngles;
            angles += 
                Vector3.up * mouseSense * Input.GetAxis("Mouse X") * Time.deltaTime + 
                Vector3.right * mouseSense * Input.GetAxis("Mouse Y") * Time.deltaTime;
            if (angles.x > 180) angles.x -= 360;
            angles.x = Mathf.Clamp(angles.x, verticalMinAngle, verticalMaxAngle);
            if (angles.y > 180) angles.y -= 360;
            angles.y = Mathf.Clamp(angles.y, horizontalMinAngle, horizontalMaxAngle);
            transform.localEulerAngles = angles;

        }
    }
}
