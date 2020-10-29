//rev 2
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[AddComponentMenu("Behaviour/FollowTarget")]
public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    [SerializeField]
    Vector3 targetOffset = Vector3.zero;

    //[SerializeField]
    //float followSpeed = 10f;

    //[SerializeField]
    //float followMaxDeviation = 3f;

    //[SerializeField]
    //float smoothTime = 5;

    private void FixedUpdate()
    {
        //Надо разбираться какие-то лаги, пока ограничусь простым вариантом жесткой привязки
        var newPosition = target.transform.position + targetOffset;
        //var distance = Vector3.Distance(transform.position, newPosition);
        var velocity = Vector3.zero;
        //var smoothTimeCorr = Time.fixedDeltaTime * smoothTime;
        //if (distance > followMaxDeviation)
        //    smoothTimeCorr = Time.fixedDeltaTime;
        //transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTimeCorr, followSpeed);
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, Time.fixedDeltaTime, 1000);
        GetComponent<Rigidbody>().velocity = velocity;
    }
}
