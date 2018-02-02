using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public LevelData levelData;

	public GameObject[] hazards;
	public GameObject[] props;
	public GameObject[] Enemies;
	public GameObject[] player;
	public GameObject PowerUp;
	public GameObject Panel;

	public Vector3 spawnValues;

	public int maxWaves;
	public int hazardCount;

	public float spawnMaxSize;
    public float startWait;

	public Text scoreText;
	public Text gameOverText;
	public Text restartText;

	public Button ship1;
	public Button ship2;

	private int score;
	private int wave;

	private bool gameOver;
	private bool restart;

	private Quaternion spawnRotation;

	void Start(){
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		wave = 0;
		UpdateScore ();

		Panel.SetActive (true);
		ship1.onClick.AddListener (first);
		ship2.onClick.AddListener (second);
		spawnRotation = Quaternion.identity;
		StartCoroutine (SpawnHazards ());
		StartCoroutine (SpawnProps ());
	}
 
	void Update ()
    {
        if (restart)
        {
			if (Input.GetKeyDown (KeyCode.R) || Input.GetButton("Fire1"))
            {
                //Application.LoadLevel (Application.loadedLevel);
				SceneManager.LoadScene ("start", LoadSceneMode.Single);
            }
		}
	}

	IEnumerator SpawnProps ()
	{
		while (true) {
			for (int i = 0; i < props.Length; i++) {
				
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), -8.5f, spawnValues.z);
				Instantiate (props [i], spawnPosition, Quaternion.identity);
				yield return new WaitForSeconds (levelData.propWait [i]);

				if (gameOver) {
					break;
				}
			}
		}
		
	}

	IEnumerator SpawnHazards ()
	{
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				//Quaternion spawnRotation = Quaternion.identity;
				GameObject asteroid = Instantiate (hazards [Random.Range (0, hazards.Length)], spawnPosition, spawnRotation) as GameObject;
				float numRandom = (Random.value * spawnMaxSize) + 1.0f;
				asteroid.transform.localScale = Vector3.one * numRandom;
				yield return new WaitForSeconds (0.5f);
			}

				if (gameOver) {
					restartText.text = "Tap To Restart";
					restart = true;
					break;
				}
			yield return new WaitForSeconds (5);
		}
	}
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true) {

			if ((wave + 1) % 5 == 0) {
				Instantiate (PowerUp, new Vector3(0.0f,0.0f,16.0f), spawnRotation);
			}
			yield return new WaitForSeconds (5);
			
			if (wave < maxWaves) {
				/*for (int i = 0; i < hazardCount; i++) {
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					//Quaternion spawnRotation = Quaternion.identity;
					GameObject asteroid = Instantiate (hazards [Random.Range (0, hazards.Length)], spawnPosition, spawnRotation) as GameObject;
					float numRandom = (Random.value * spawnMaxSize) + 1.0f;
					asteroid.transform.localScale = Vector3.one * numRandom;
					yield return new WaitForSeconds (0.5f);
				}*/

				float spawnx = Random.Range (-spawnValues.x, spawnValues.x);

				/*for (int i = 0; i < EnemyCountWave [wave]; i++) {
					Vector3 spawnPosition = new Vector3 (spawnx, spawnValues.y, spawnValues.z);
					//Quaternion spawnRotation = Quaternion.identity;
					Instantiate (Enemies [wave], spawnPosition, spawnRotation);
					yield return new WaitForSeconds (spawnWait);
				}*/
				for (int i = 0; i < levelData.waveEnemyCount [wave]; i++) {
					Vector3 spawnPosition = new Vector3 (spawnx, spawnValues.y, spawnValues.z);
					//Quaternion spawnRotation = Quaternion.identity;
					Instantiate (Enemies [levelData.waveEnemy [wave]], spawnPosition, spawnRotation);
					yield return new WaitForSeconds (levelData.waveSpawnTime[wave]);
				}

				if (gameOver) {
					//restartText.text = "Tap To Restart";
					//restart = true;
					break;
				}
				yield return new WaitForSeconds (levelData.waveWaitTime[wave]);
				wave++;
			}
		}
	}
	
	public void AddScore(int newScoreValue){

		score += newScoreValue;
		UpdateScore ();
		
	}

	void UpdateScore(){
		scoreText.text = "Score:" + score;
	}
	
	public void GameOver ()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }

	public void first(){
		
		player [0].SetActive (true);
		player [1].SetActive (false);
		Panel.SetActive (false);
		StartCoroutine (SpawnWaves ());
		
	}
	public void second(){
		
		player [1].SetActive (true);
		player [0].SetActive (false);
		Panel.SetActive (false);
		StartCoroutine (SpawnWaves ());

	}
}
