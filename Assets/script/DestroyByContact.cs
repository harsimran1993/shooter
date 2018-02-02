using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	//public GameObject playerExplosion;
	public int scoreValue;
	public float healthPoints;
	public int Damage;
	public Slider healthBarSlider;


	private GameController gameController;
	private float currentHealthPoints;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}

		currentHealthPoints = healthPoints;

		if (healthBarSlider != null) {
			healthBarSlider.maxValue = healthPoints;
			healthBarSlider.value = currentHealthPoints;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Boundary") || other.CompareTag ("powerup")) 
		{
			return;
		}

		if (CompareTag ("Ebolt") && other.CompareTag("Ebolt")) {
			return;
		}
		if (CompareTag ("Ebolt") && other.CompareTag("Enemy") || CompareTag ("Enemy") && other.CompareTag("Ebolt")) {
			return;
		}
		if (CompareTag ("bolt") && other.CompareTag("Player") || CompareTag ("Player") && other.CompareTag("bolt")) {
			return;
		}

		DestroyByContact otherContact = other.GetComponent<DestroyByContact> ();

		hurt (otherContact.Damage);
		otherContact.hurt (Damage);
		//Debug.Log (other.name);

		/*if (explosion != null)
		{
			Instantiate (explosion, transform.position, transform.rotation);
		}

		gameController.AddScore (scoreValue);
		
		if (other.tag == "Player")
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		}*/

		//Destroy (other.gameObject);
		//Destroy (gameObject);
	}

	public void hurt(int damage){
		
		currentHealthPoints -= damage;

		if (healthBarSlider != null) {
			healthBarSlider.value = currentHealthPoints;
		}

		if (currentHealthPoints < 1) {

			Destroy (gameObject);
			if (explosion != null)
			{
				Instantiate (explosion, transform.position, transform.rotation);
			}
			if(gameController != null)
				gameController.AddScore (scoreValue);

			if (CompareTag ("Player")) {
				gameController.GameOver ();
				
			}
		}
	}

	public float getCurrentHealth(){
		return currentHealthPoints;
	}

	public void heal(){
		currentHealthPoints += healthPoints * 0.5f;

		if (currentHealthPoints > healthPoints)
			currentHealthPoints = healthPoints;


		if (healthBarSlider != null) {
			healthBarSlider.value = currentHealthPoints;
		}
	}
}
