using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class MusicCommand : MonoBehaviour
{
    private enum Note
    {
        WHOLE,
        HALF,
        QUARTER,
        EIGTH,
    }

    // Config
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private Note note;
    [SerializeField] private GameObject enemyPrefab = null;

    private float wholeNoteInterval = 1.0f;
    private float halfNoteInterval = 0.5f;
    private float quarterNoteInterval = 0.25f;
    private float eighthNoteInterval = 0.125f;

    public void Execute(List<GameObject> lanePrefabs)
    {
        GameObject randomLanePrefab = getRandomLanePrefab(lanePrefabs);

        Lane randomLane = randomLanePrefab.GetComponent<Lane>();

        // Debug.Log("Got random lane " + randomLanePrefab.name);
        randomLane.spawnEnemy(enemyPrefab);
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

    private GameObject getRandomLanePrefab(List<GameObject> lanePrefabs)
    {
        int randomIndex = Random.Range(0, lanePrefabs.Count);

        return lanePrefabs[randomIndex];
    }
}
