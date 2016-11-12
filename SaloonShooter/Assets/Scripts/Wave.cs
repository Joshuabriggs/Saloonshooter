using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Wave
{

    public enum enemy
    {
        Bandit,
        Cowboy,
        Clive
    }

    public List<enemy> enemiesInWave = new List<enemy>();
    public float spawnInterval = 2f;
}
