using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawner.asset", menuName = "Spawner")]
public class SpawnerData : ScriptableObject {
    public GameObject itemToSpawn;
    public int minSpawn;
    public int maxSpawn;
}

