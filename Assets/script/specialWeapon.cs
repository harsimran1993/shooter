using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialWeapon : MonoBehaviour {

	public GameObject shot;
	public Transform[] shotSpawn;
	public float fireRate;
	public float delay;
	public int shotsperburst;

	private int count;

	private AudioSource audioSource;

	void Start ()
	{
		audioSource = GetComponent<AudioSource> ();
		count = 0;
		InvokeRepeating ("Fire", delay, fireRate);
	}
	void Fire ()
	{
		count++;
		if (count > shotsperburst) {
			if (count > shotsperburst * 2)
				count = 0;
			return;
		}
		for(int i = 0; i < shotSpawn.Length; i ++)
			if(shotSpawn[i] !=null)
				Instantiate (shot, shotSpawn[i].position, shotSpawn[i].rotation);
		audioSource.Play ();
	}
}
