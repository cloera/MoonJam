using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour {
    [SerializeField] private GameObject spawner = null;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    public void spawnEnemy(GameObject enemyToSpawn) {
        if (enemyToSpawn != null) {
            Instantiate(
                enemyToSpawn,
                spawner.transform.position,
                Quaternion.identity
            );
        }
    }
}
