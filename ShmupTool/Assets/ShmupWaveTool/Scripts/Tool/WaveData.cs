using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class WaveData
{

    //public GameController gameController;
    public int waveSize;

    public MobWave.WaveType [] type;

    public string[] waveTypes;

    public int [] formationEnemyCount;
    public float [] spawnValueYPos;
    public float [] spawnWaitTime;


    public WaveData(WaveController controller)
    {
        // Turns all seporate variables into lists of variables (the vars you use in the game controller)
        
        waveSize = controller.Waves.Count;

        type = new MobWave.WaveType [waveSize];
        waveTypes = new string [waveSize];
        formationEnemyCount = new int [waveSize];
        spawnValueYPos = new float [waveSize];
        spawnWaitTime = new float [waveSize];

        for(int i = 0; i < waveSize; i++)
        {
            type[i] = controller.Waves[i].Type;
            waveTypes[i] = AssetDatabase.GetAssetPath(controller.Waves[i].enemyFormation).ToString();
            Debug.Log("Save Path: " + waveTypes[i]);
            formationEnemyCount[i] = controller.Waves[i].formationEnemyCount;
            spawnValueYPos[i] = controller.Waves[i].spawnValueYPos;
            spawnWaitTime[i] = controller.Waves[i].spawnWaitTime;
        }
    }
}
