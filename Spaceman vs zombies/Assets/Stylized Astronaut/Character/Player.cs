using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	Animator animator;
	Rigidbody _rigidbody;

	[SerializeField]
	float speed = 600.0f;
	[SerializeField]
	float turnSpeed = 400.0f;
	[SerializeField]
	float jumpForce = 20f;
	
	private float moveDirection = 0f;



	void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
		animator = gameObject.GetComponentInChildren<Animator>();
	}

	void Update()
	{
		animator.SetInteger("AnimationPar", (int)Input.GetAxisRaw("Vertical"));



		if (Input.GetKeyDown(KeyCode.Space) && _rigidbody.velocity.y < 0.01f)
        {
			_rigidbody.AddForce(Vector3.up * jumpForce);
			animator.SetTrigger("Jump");
			animator.SetBool("OnGround", false);
        }
		//if (controller.isGrounded)
		moveDirection = Input.GetAxis("Vertical") * speed * Time.deltaTime;

		float turn = Input.GetAxis("Horizontal");
		transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
		_rigidbody.MovePosition(transform.position + transform.forward * moveDirection);
	}

    private void FixedUpdate()
    {
		if (_rigidbody.velocity.y == 0)
			animator.SetBool("OnGround", true);
    }
}