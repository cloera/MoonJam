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

    // public GameObject musicObjectPrefab;
    // public List<MusicCommand> musicCommands = new List<MusicCommand>();

    // Cache
    private float beatsPerSecond;
    private float beatLength;
    private float barLength;
    private int barCount;
    private static MusicTimingManager mInstance = null;
    private AudioSource musicPlayer;
    private GameStatus gameStatus;
    private float oneSecond = 1f;

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
        musicPlayer = gameObject.GetComponent<AudioSource>();
        gameStatus = FindObjectOfType<GameStatus>();

        foreach(GameObject musicCommandPrefab in musicCommandPrefabs)
        {
            MusicCommand musicCommand = musicCommandPrefab.GetComponent<MusicCommand>();

            float noteInSeconds = beatLength * musicCommand.GetNoteFraction();

            float intervalTime = noteInSeconds * noteIntervalDurationMultiplier;

            StartCoroutine(ExecuteAfterTime(musicCommand, intervalTime));
        }

    }

    IEnumerator ExecuteAfterTime(MusicCommand musicCommand, float time)
    {
        while (true)
        {
            Note musicCommandNote = musicCommand.GetNote();

            bool noteIsAllowed = gameStatus.noteIsAllowed(musicCommandNote);

            bool transitioning = gameStatus.shouldTransitionBackground();

            if (gameStatus.shouldTransitionBackground())
            {
                yield return new WaitForSeconds(gameStatus.getSpawningTransitionInterval());
            } else
            {
                if (noteIsAllowed)
                {
                    musicCommand.Execute(lanePrefabs);
                }

                yield return new WaitForSeconds(time);
            }

            playMusic();
        }
    }

    private void playMusic() {
        if (!musicPlayer.isPlaying) {
            musicPlayer.PlayDelayed(musicDelay);
        }
    }
}
