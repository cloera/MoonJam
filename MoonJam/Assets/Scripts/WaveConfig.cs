using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject {
    // Config
    [SerializeField] private GameObject enemyPrefab = null;
    [SerializeField] private GameObject lanePrefab = null;

    public GameObject getEnemyPrefab() {
        return enemyPrefab;
    }

    public GameObject getLanePrefab() {
        return lanePrefab;
    }

    public Vector2 getEnemySpawnPoint() {
        foreach (Transform child in lanePrefab.transform) {
            if (child.name == "EnemyArea") {
                return child.transform.position;
            }
        }

        Exception exception = new Exception("No EnemyArea Set on Lane Prefab");

        Debug.LogError(exception);

        throw exception;
    }
}
