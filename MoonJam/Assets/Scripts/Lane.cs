using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Lane : MonoBehaviour {
    [SerializeField] private GameObject spawner = null;

    // Cache
    private SortingLayer sortingLayer;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    public void spawnEnemy(GameObject enemyToSpawn) {
        if (enemyToSpawn != null) {
            GameObject enemyClone = Instantiate(
                enemyToSpawn,
                spawner.transform.position,
                Quaternion.identity
            );

            Enemy enemy = enemyClone.GetComponent<Enemy>();

            enemy.setSortingGroup(getSortingGroup());
        }
    }

    private SortingGroup getSortingGroup() {
        return gameObject.GetComponent<SortingGroup>();
    }
}
