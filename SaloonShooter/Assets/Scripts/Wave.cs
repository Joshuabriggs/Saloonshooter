using UnityEngine;
using System.Collections;

[System.Serializable]
public class Wave
{
    public GameObject[] enemyPrefab;
    public float spawnInterval = 2f;
    public int maxEnemies = 10;
}
