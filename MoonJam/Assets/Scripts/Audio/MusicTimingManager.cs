using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using NoteType;

public class MusicTimingManager : MonoBehaviour
{
    // config
    [SerializeField] public int beatsPerMinute = 128;
    [SerializeField] private List<GameObject> lanePrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> musicCommandPrefabs = new List<GameObject>();
    [SerializeField] private float noteIntervalDurationMultiplier = 1f;
    [SerializeField] private float musicDelay = 0.5f;

    // Cache
    private float beatsPerSecond;
    private float beatLength;
    private float barLength;
    //private int barCount;
    private static MusicTimingManager mInstance = null;
    private AudioSource musicPlayer;
    private GameStatus gameStatus;
    private MIDIPlayer midiPlayer;
    //private float oneSecond = 1f;
    public bool hasNoteEvent = false;
    private Object noteEventLock = new Object();

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
        //barCount = 0;
        musicPlayer = gameObject.GetComponent<AudioSource>();
        gameStatus = FindObjectOfType<GameStatus>();

        foreach (GameObject musicCommandPrefab in musicCommandPrefabs)
        {
            MusicCommand musicCommand = musicCommandPrefab.GetComponent<MusicCommand>();

            float noteInSeconds = beatLength * musicCommand.GetNoteFraction();

            float intervalTime = noteInSeconds * noteIntervalDurationMultiplier;

            StartCoroutine(ExecuteAfterTime(musicCommand, intervalTime));
        }

        midiPlayer = this.gameObject.AddComponent<MIDIPlayer>();
        midiPlayer.InitPlayer("Kick_2_Irrupt.mid");
        MIDIPlayer.notePlaybackDelegate += noteEventOn;

        playMusic();
    }

    private void OnDisable()
    {
        StopMusic();
        MIDIPlayer.notePlaybackDelegate -= noteEventOn;
    }

    IEnumerator ExecuteAfterTime(MusicCommand musicCommand, float time)
    {
        while (true)
        {
            Note musicCommandNote = musicCommand.GetNote();

            bool noteIsAllowed = gameStatus.noteIsAllowed(musicCommandNote);

            if (gameStatus.shouldTransitionBackground())
            {
                yield return new WaitForSeconds(gameStatus.getSpawningTransitionInterval());
            }
            else
            {
                lock(noteEventLock)
                {
                    if (noteIsAllowed && hasNoteEvent)
                    {
                        musicCommand.Execute();
                        noteEventOff();
                    }
                }
                

                yield return new WaitForSeconds(time);
            }
            
        }
    }

    public void noteEventOn()
    {
        lock(noteEventLock)
        {
            this.hasNoteEvent = true;
        }
    }

    public void noteEventOff()
    {
        this.hasNoteEvent = false;
    }

    private void playMusic() 
    {
        if (!musicPlayer.isPlaying)
        {
            musicPlayer.PlayDelayed(musicDelay);
        }
    }

    private void StopMusic()
    {
        if(musicPlayer.isPlaying)
        {
            musicPlayer.Stop();
        }
    }

    public static GameObject getRandomLanePrefab()
    {
        int randomIndex = Random.Range(0, mInstance.lanePrefabs.Count);

        return mInstance.lanePrefabs[randomIndex];
    }
}
