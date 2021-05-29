using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneSpawner : MonoBehaviour {
    [Header("Top Lane WaveConfigs")]
    [SerializeField] private List<WaveConfig> topLaneWaveConfigs = new List<WaveConfig>();

    [Header("Second From Top Lane WaveConfigs")]
    [SerializeField] private List<WaveConfig> secondFromTopLaneWaveConfigs = new List<WaveConfig>();

    [Header("Third From Top Lane WaveConfigs")]
    [SerializeField] private List<WaveConfig> thirdFromTopLaneWaveConfigs = new List<WaveConfig>();

    [Header("Bottom Lane WaveConfigs")]
    [SerializeField] private List<WaveConfig> bottomLaneWaveConfigs = new List<WaveConfig>();

    [Header("General Configs")]
    [SerializeField] private float totalSeconds = 30f;
    [SerializeField] private float delayBetweenEnemySpawns = 1f;
    [SerializeField] private bool loopWaves = false;

    private float currentTime = 0;

    // Start is called before the first frame update
    IEnumerator Start() {
        do {
            yield return StartCoroutine(spawnAllLaneWaves());
            currentTime++;
        } while (currentTime != totalSeconds);
    }

    // Update is called once per frame
    void Update() {
    }

    private IEnumerator spawnAllLaneWaves() {
        // Randomly sets the top, second from top, third from top, and bottom lane configs.
        List<WaveConfig> allLaneWaveConfigs = new List<WaveConfig> {
            getRandomLaneConfig(topLaneWaveConfigs),
            getRandomLaneConfig(secondFromTopLaneWaveConfigs),
            getRandomLaneConfig(thirdFromTopLaneWaveConfigs),
            getRandomLaneConfig(bottomLaneWaveConfigs)
        };

        foreach (WaveConfig waveConfig in allLaneWaveConfigs) {
            yield return StartCoroutine(spawnWave(waveConfig));
        }
    }

    private IEnumerator spawnWave(WaveConfig waveConfig) {
        GameObject tempEnemy = Instantiate(
            waveConfig.getEnemyPrefab(),
            waveConfig.getEnemySpawnPoint(),
            Quaternion.identity
        );

        yield return new WaitForSeconds(delayBetweenEnemySpawns);
    }

    private WaveConfig getRandomLaneConfig(List<WaveConfig> laneWaveConfigs) {
        int randomIndex = Random.Range(0, laneWaveConfigs.Count);

        return laneWaveConfigs[randomIndex];
    }
}
