using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class MusicCommand : MonoBehaviour
{
    public enum Note
    {
        WHOLE,
        HALF,
        QUARTER,
        EIGTH,
    }

    public AudioClip musicClip;
    public Note note;

    public void Execute()
    {
        // Randomly select lane
        // Tell lane to spawn enemy
        // Play sound
        Debug.Log(note);
    }

    public float GetNoteFraction()
    {
        float time = 0.0f;

        switch(note)
        {
            case Note.WHOLE:
                time = 1.0f;
                break;
            case Note.HALF:
                time = 0.5f;
                break;
            case Note.QUARTER:
                time = 0.25f;
                break;
            case Note.EIGTH:
                time = 0.17f;
                break;
            default:
                break;
        }

        return time;
    }
}
