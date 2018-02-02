using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class LevelData : ScriptableObject {

	public string objectName = "level_data";


	public int[] waveEnemy;
	public int[] waveSpawnTime;
	public int[] waveWaitTime;
	public int[] waveEnemyCount;

	public float[] propWait;


}
