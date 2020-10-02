using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float rotateSpeedX;
    [SerializeField]
    float rotateSpeedY;
    [SerializeField]
    Transform cameraPivot;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        var hAxis = Input.GetAxis("Horizontal");
        var vAxis = Input.GetAxis("Vertical");
        var vMouse = Input.GetAxis("Mouse Y");

        if (hAxis != 0)
            transform.Rotate(Vector3.up, rotateSpeedX * Time.deltaTime * hAxis);
        if (vAxis != 0)
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * vAxis);
        if (vMouse != 0)
            cameraPivot.Rotate(Vector3.right, rotateSpeedY * Time.deltaTime * vMouse);

        animator.SetFloat("moveSpeed", vAxis);
        animator.SetBool("isRun", vAxis != 0);
    }
}
