using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[System.Serializable]
public class MusicTimingManager : MonoBehaviour
{
    public int beatsPerMinute = 128;
    public List<MusicCommand> musicCommands = new List<MusicCommand>();

    static MusicTimingManager mInstance = null;

    private float beatsPerSecond;
    private float beatLength;
    private float barLength;
    private int barCount;

    private void Awake()
    {
        if (mInstance == null)
        {
            mInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        // Calculate beats
        beatsPerSecond = beatsPerMinute / 60.0f;
        beatLength = 1.0f / beatsPerSecond;
        barLength = beatLength * 4.0f;
        barCount = 0;

        foreach(MusicCommand command in musicCommands)
        {
            float noteInSeconds = beatLength * command.GetNoteFraction();
            StartCoroutine(ExecuteAfterTime(command, noteInSeconds));
        }
        
    }

    IEnumerator ExecuteAfterTime(MusicCommand command, float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);

            command.Execute();
        }
    }


}
