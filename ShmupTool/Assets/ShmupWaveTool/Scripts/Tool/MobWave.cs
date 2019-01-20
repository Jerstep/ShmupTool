using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public struct MobWave
{
    [System.Serializable]
    public enum WaveType
    {
        Enemy,
        Boss
    }

    public WaveType Type;

    public GameObject enemyFormation;

    public int formationEnemyCount;

    public float spawnValueYPos;

    public float spawnWaitTime;
}