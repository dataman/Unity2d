using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingIndex = 0;
    [SerializeField] bool looping = false;

    int startingWave = 0;
    
	// Use this for initialization
	IEnumerator Start () {
        do
        {
            yield return StartCoroutine(Spawn());
        } while (looping);

    }

    private IEnumerator Spawn()
    {
        WaveConfig wave = waveConfigs[Random.Range(0, waveConfigs.Count)];
        wave.SetMoveSpeed(Random.Range(4, 10));
        var newEnemy = Instantiate(wave.GetEnemyPrefab(), wave.GetWaypoints()[0].transform.position, Quaternion.identity);
        newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(wave);
        yield return new WaitForSeconds(wave.GetTimeBetweenSpawns());
    }
	
}
