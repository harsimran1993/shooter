using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {
	public float speed;
	public bool isboss;

	private Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		rb.velocity = transform.forward * speed;
	}

	void FixedUpdate(){


		if (transform.position.z < -10) {
			Destroy (gameObject);
		}

		if (!isboss)
			return;

		if (transform.position.z < 10) {
			rb.velocity = Vector3.zero;
			rb.constraints = RigidbodyConstraints.FreezePositionZ;
			isboss = false;
		}
		
	}
}
