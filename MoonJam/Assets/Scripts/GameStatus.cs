using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NoteType;

public class GameStatus : MonoBehaviour {
    [Header("Game Configs")]
    [SerializeField] private int initialScore = 0;
    [SerializeField] private float startDelayInSeconds = 2f;
    [SerializeField] private float noteIntervalDurationMultiplier = 4f;
    [SerializeField] private float noteIncreasePerTransition = 4f;

    [Header("Note Types")]
    [SerializeField] private int newNodesPerTransition = 2;
    [SerializeField] private List<Note> noteTypes = new List<Note>();

    [Header("Background Transition Configs")]
    [SerializeField] private float transitionSpeed = 0.2f;
    [SerializeField] private float spawningTransitionInterval = 3f;
    [SerializeField] private List<float> transitionTimesInSeconds = new List<float>();

    // Cache
    private AudioSource audioSource = null;

    // State
    private int currentScore;
    private float currentTime;
    private bool transitionBackground = false;
    private int currentTransitionIndex;
    private float nextTransitionTime;
    private HashSet<Note> allowedNotesForTransition = new HashSet<Note>();
    private bool isDelayed = true;
    private int currentAllowedNodes;
    private float currentNoteIntervalDurationMultiplier;

    private void Awake() {
        if (!this.isOnlyGameStatusInstance()) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start() {
        currentScore = initialScore;

        currentTime = 0;
        currentTransitionIndex = 0;
        nextTransitionTime = getNextTransitionTime(currentTransitionIndex);
        currentAllowedNodes = newNodesPerTransition;
        allowedNotesForTransition = getAllowedNotesForTransition();
        currentNoteIntervalDurationMultiplier = noteIntervalDurationMultiplier;

        audioSource = getAudioSource();

        StartCoroutine(DelayStart());
    }

    IEnumerator DelayStart() {
        yield return new WaitForSeconds(startDelayInSeconds);

        isDelayed = false;
    }

    // Update is called once per frame
    void Update() {
        if (FindObjectOfType<SceneLoader>().isOnBossScene()) {
            currentNoteIntervalDurationMultiplier = noteIntervalDurationMultiplier;
        }

        if (FindObjectOfType<SceneLoader>().isOnGameScene()) {
            currentTime += Time.deltaTime;

            nextTransitionTime = getNextTransitionTime(currentTransitionIndex);

            transitionBackground = shouldStartBackgroundTransition();

            if (!audioSource.isPlaying) {
                audioSource.UnPause();
            }
        } else {
            if (audioSource.isPlaying) {
                audioSource.Pause();
            }
        }
    }

    public int getCurrentScore() {
        return currentScore;
    }

    public void addToScore(int scoreValue) {
        currentScore += scoreValue;
    }

    public bool shouldTransitionBackground() {
        return transitionBackground;
    }

    public float getTransitionSpeed() {
        return transitionSpeed;
    }

    public float getSpawningTransitionInterval() {
        return spawningTransitionInterval;
    }

    public float getCurrentNoteIntervalDurationMultiplier() {
        return currentNoteIntervalDurationMultiplier;
    }

    public bool gameIsDelayed() {
        return isDelayed;
    }

    public bool noteIsAllowed(Note note) {
        return allowedNotesForTransition.Contains(note);
    }

    public void resetGame() {
        gameObject.SetActive(false);

        Destroy(gameObject);
    }

    public void setupForNextTransitionBackground() {
        currentTime = 0;
        currentTransitionIndex += 1;

        if (currentTransitionIndex == transitionTimesInSeconds.Count) {
            FindObjectOfType<SceneLoader>().loadNextScene();
        }

        nextTransitionTime = getNextTransitionTime(currentTransitionIndex);
        currentAllowedNodes += newNodesPerTransition;
        allowedNotesForTransition = getAllowedNotesForTransition();
        currentNoteIntervalDurationMultiplier += noteIncreasePerTransition;
    }

    private bool isOnlyGameStatusInstance() {
        int numberOfGameStatusInstances = FindObjectsOfType<GameStatus>().Length;

        return numberOfGameStatusInstances <= 1;
    }

    private bool shouldStartBackgroundTransition() {
        return currentTime >= nextTransitionTime;
    }

    // Default to max float if index is out of bounds.
    private float getNextTransitionTime(int index) {
        if (index >= transitionTimesInSeconds.Count) {
            return float.MaxValue;
        }

        return transitionTimesInSeconds[index];
    }

    private HashSet<Note> getAllowedNotesForTransition() {
        HashSet<Note> allowedNotes = new HashSet<Note>();

        for (int index = 0; index < currentAllowedNodes; index++) {
            if (index < noteTypes.Count) {
                allowedNotes.Add(noteTypes[index]);
            }
        }

        return allowedNotes;
    }

    private AudioSource getAudioSource() {
        return gameObject.GetComponent<AudioSource>();
    }
}
