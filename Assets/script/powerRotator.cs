using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerRotator : MonoBehaviour {

	private Rigidbody rb;
	public float tumble;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		rb.angularVelocity = Random.insideUnitSphere * tumble; 
	}

	void FixedUpdate(){
		rb.position = transform.parent.position;
	}
}
