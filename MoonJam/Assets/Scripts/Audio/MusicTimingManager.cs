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
    [SerializeField] private List<GameObject> midiPrefabs = new List<GameObject>();
    [SerializeField] private float noteIntervalDurationMultiplier = 1f;
    [SerializeField] private float musicDelay = 0.5f;

    // Cache
    private float beatsPerSecond;
    private float beatLength;
    private float barLength;
    private int barCount;
    private static MusicTimingManager mInstance = null;
    private AudioSource musicPlayer;
    private GameStatus gameStatus;

    private List<LockState> lockStateList = new List<LockState>();
    private static LockState mainLockState1 = new LockState();
    private static LockState mainLockState2 = new LockState();
    private static LockState mainLockState3 = new LockState();
    private static LockState mainLockState4 = new LockState();

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

        lockStateList.Add(mainLockState1);
        lockStateList.Add(mainLockState2);
        lockStateList.Add(mainLockState3);
        lockStateList.Add(mainLockState4);
    }

    void Start()
    {
        // Calculate beats
        beatsPerSecond = beatsPerMinute / 60.0f;
        beatLength = 1.0f / beatsPerSecond;
        barLength = beatLength * 4.0f;

        musicPlayer = gameObject.GetComponent<AudioSource>();
        gameStatus = FindObjectOfType<GameStatus>();

        MusicCommand.OnLockSetDelegate += setLockState;
        MusicCommand.OnLockGetDelegate += getLockState;
        MIDIPlayer.OnLockSetDelegate += setLockState;

        for(int i = 0; i < midiPrefabs.Count-1; i++)
        {
            LockState ls = lockStateList[i];
            
            MusicCommand command = musicCommandPrefabs[i].GetComponent<MusicCommand>();
            command.InitCommand(ls);

            MIDIPlayer mPlayer = midiPrefabs[i].GetComponent<MIDIPlayer>();
            mPlayer.InitPlayer(ls);

            StartCoroutine(ExecuteAfterTime(command));
        }

        playMusic();
    }

    private void OnDisable()
    {
        StopMusic();
        MIDIPlayer.OnLockSetDelegate -= setLockState;
        MusicCommand.OnLockSetDelegate -= setLockState;
        MusicCommand.OnLockGetDelegate -= getLockState;
    }

    IEnumerator ExecuteAfterTime(MusicCommand musicCommand)
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
                if (noteIsAllowed)
                {
                    musicCommand.Execute();
                }
                yield return null;
            }
            
        }
    }

    public void setLockState(LockState lockState, bool state)
    {
        lock(lockState.threadLock)
        {
            lockState.state = state;
        }
    }

    public bool getLockState(LockState lockState)
    {
        bool state;

        lock(lockState.threadLock)
        {
            state = lockState.state;
        }

        return state;
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
