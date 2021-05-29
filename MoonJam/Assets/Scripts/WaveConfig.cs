using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject {
    // Config
    [SerializeField] private GameObject enemyPrefab = null;
    [SerializeField] private GameObject lanePrefab = null;
    [SerializeField] private GameObject spawnPoint = null;

    public GameObject getEnemyPrefab() {
        return enemyPrefab;
    }

    public GameObject getLanePrefab() {
        return lanePrefab;
    }

    public Vector3 getEnemySpawnPoint() {
        return spawnPoint.transform.position;
    }
}
