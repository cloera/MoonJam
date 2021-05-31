using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using NoteType;

public class MusicCommand : MonoBehaviour
{
    // Config
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private Note note;
    [SerializeField] private List<GameObject> enemyPrefabs = null;
    [SerializeField] private List<GameObject> bossProjectilePrefabs = null;

    private float wholeNoteInterval = 1.0f;
    private float halfNoteInterval = 0.5f;
    private float quarterNoteInterval = 0.25f;
    private float eighthNoteInterval = 0.125f;

    public void Execute(List<GameObject> lanePrefabs)
    {
        GameObject randomLanePrefab = getRandomLanePrefab(lanePrefabs);

        Lane randomLane = randomLanePrefab.GetComponent<Lane>();

        if (FindObjectOfType<SceneLoader>().isOnBossScene())
        {
            randomLane.spawnEnemy(getRandomProjectilePrefab());
        }
        else
        {
            randomLane.spawnEnemy(getRandomEnemyPrefab());
        }
    }

    public float GetNoteFraction()
    {
        float time = 0.0f;

        switch(note)
        {
            case Note.WHOLE:
                time = wholeNoteInterval;
                break;
            case Note.HALF:
                time = halfNoteInterval;
                break;
            case Note.QUARTER:
                time = quarterNoteInterval;
                break;
            case Note.EIGTH:
                time = eighthNoteInterval;
                break;
            default:
                break;
        }

        return time;
    }

    public Note GetNote() {
        return note;
    }

    private GameObject getRandomLanePrefab(List<GameObject> lanePrefabs)
    {
        int randomIndex = Random.Range(0, lanePrefabs.Count);

        return lanePrefabs[randomIndex];
    }

    private GameObject getRandomEnemyPrefab()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Count);

        return enemyPrefabs[randomIndex];
    }

    private GameObject getRandomProjectilePrefab()
    {
        int randomIndex = Random.Range(0, bossProjectilePrefabs.Count);

        return bossProjectilePrefabs[randomIndex];
    }
}
