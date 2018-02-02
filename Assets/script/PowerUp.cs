using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
	
	public int type;
	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Player")) {
			switch (type) {

			case 0: 
				DestroyByContact otherdbc = other.GetComponent<DestroyByContact> ();
				otherdbc.heal ();
				Destroy (gameObject);
				break;

			default:
				Debug.Log("error");
				break;
			
			}
		}
	}
}
