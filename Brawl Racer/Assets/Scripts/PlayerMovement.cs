using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour 
{
	[SerializeField]private float speed = 90f;
	[SerializeField]private float turnSpeed = 5f;
	[SerializeField]private float hoverForce = 65f;
	[SerializeField]private float hoverHeight = 3.5f;
	private float powerInput;
	private float turnInput;
	private Rigidbody carRigidbody;

	// Use this for initialization
	void Start () 
	{
		carRigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		powerInput = Input.GetAxis("Vertical");
		turnSpeed = Input.GetAxis("Horizontal");
	}

	void FixedUpdate()
    {
        Ray ray = new Ray (transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
            carRigidbody.AddForce(appliedHoverForce, ForceMode.Acceleration);
        }

        carRigidbody.AddRelativeForce(0f, 0f, powerInput * speed);
        carRigidbody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);

    }

}
