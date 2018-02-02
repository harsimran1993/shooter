using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public GameObject shot;
	public Transform[] shotSpawn;
	public float fireRate;
	public float delay;
	public bool isboss;

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
		if (isboss && count > 5) {
			if (count > 10)
				count = 0;
			return;
		}
		for(int i = 0; i < shotSpawn.Length; i ++)
			if(shotSpawn[i] !=null)
				Instantiate (shot, shotSpawn[i].position, shotSpawn[i].rotation);
		audioSource.Play ();
	}
}
