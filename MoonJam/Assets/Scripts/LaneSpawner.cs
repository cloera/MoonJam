using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneSpawner : MonoBehaviour {
    [SerializeField] private List<GameObject> lanePrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>();
    [SerializeField] private float totalSeconds = 30f;
    [SerializeField] private float delayBetweenEnemySpawns = 1f;
    [SerializeField] private bool run = true;

    private float currentTime = 0;
    private float zeroSeconds = 0;
    private Vector3 defaultPosition = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    IEnumerator Start() {
        foreach (GameObject lanePrefab in lanePrefabs) {
            Instantiate(
                lanePrefab,
                lanePrefab.transform.position,
                Quaternion.identity
            );
        }

        while (run && (currentTime != totalSeconds)) {
            yield return StartCoroutine(spawnAllLaneWaves());
            currentTime++;
        }
    }

    // Update is called once per frame
    void Update() {
    }

    private IEnumerator spawnAllLaneWaves() {
        foreach (GameObject lanePrefab in lanePrefabs) {
            yield return StartCoroutine(spawnWave(lanePrefab));
        }
    }

    private IEnumerator spawnWave(GameObject lanePrefab) {
        Lane lane = lanePrefab.GetComponent<Lane>();

        if (lane != null) {
            lane.spawnEnemy(getRandomEnemy());

            yield return new WaitForSeconds(delayBetweenEnemySpawns);
        }

        yield return new WaitForSeconds(zeroSeconds);
    }

    private GameObject getRandomEnemy() {
        int randomIndex = Random.Range(0, enemyPrefabs.Count);

        return enemyPrefabs[randomIndex];
    }
}
