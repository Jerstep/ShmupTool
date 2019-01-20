using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {
   
    public List<MobWave> Waves = new List<MobWave>();
    public Vector3 spawnValues;

    private int waveNumber = 1;
    public float startWaitTime;
    public float waveWaitTime;
    public bool canSpawn, breakLoop;

    // Use this for initialization
    void Update()
    {
        if(canSpawn)
        {
            StartCoroutine(SpawnWaves());
            Debug.Log("shit");
        }      
    }

    // In connection with WaveControllerEditor, Spawns waves that are made there.
    IEnumerator SpawnWaves()
    {
        canSpawn = false;
        yield return new WaitForSeconds(startWaitTime);
        foreach(MobWave wave in Waves)
        {
            if(breakLoop)
            {
                break;
            }
            else
            {
                spawnValues.y = wave.spawnValueYPos;                                                            // Sets Y Axis offset to given value from WaveControllerEditor  
                Vector3 spawnPosition = new Vector3(spawnValues.x, spawnValues.y, spawnValues.z);
                Quaternion SpawnRotation = wave.enemyFormation.transform.rotation;
                if (wave.Type == MobWave.WaveType.Enemy)
                {
                    wave.enemyFormation.GetComponent<ShapeRay>().enemyAmount = wave.formationEnemyCount;
                }
                Instantiate(wave.enemyFormation, spawnPosition, SpawnRotation);
                yield return new WaitForSeconds(wave.spawnWaitTime);
            }
        }
    }
}
