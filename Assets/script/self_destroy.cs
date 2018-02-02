using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class self_destroy : MonoBehaviour {

	public DestroyByContact parent;
	public GameObject explosion;

	[Range(0,1)]
	public float deathThreshold;

	void Update(){

		if (parent.getCurrentHealth () < parent.healthPoints * deathThreshold) {


			Destroy (gameObject);

			if (explosion != null)
			{
				Instantiate (explosion, transform.position, transform.rotation);
			}
		}

	}
}
