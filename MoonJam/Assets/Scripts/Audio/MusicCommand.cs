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
    [SerializeField] private GameObject enemyPrefab = null;

    private float wholeNoteInterval = 1.0f;
    private float halfNoteInterval = 0.5f;
    private float quarterNoteInterval = 0.25f;
    private float eighthNoteInterval = 0.125f;

    public void Execute()
    {
        GameObject randomLanePrefab = MusicTimingManager.getRandomLanePrefab();

        Lane randomLane = randomLanePrefab.GetComponent<Lane>();

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

    public Note GetNote() {
        return note;
    }
}
