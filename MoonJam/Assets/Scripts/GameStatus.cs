using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NoteType;

public class GameStatus : MonoBehaviour {
    [Header("Game Configs")]
    [SerializeField] private int initialScore = 0;

    [Header("Note Types")]
    [SerializeField] private List<Note> noteTypes = new List<Note>();

    [Header("Background Transition Configs")]
    [SerializeField] private float transitionSpeed = 0.2f;
    [SerializeField] private float spawningTransitionInterval = 2f;
    [SerializeField] private List<float> transitionTimesInSeconds = new List<float>();


    // State
    private int currentScore;
    private float currentTime;
    private bool transitionBackground = false;
    private int currentTransitionIndex;
    private float nextTransitionTime;
    private HashSet<Note> allowedNotesForTransition = new HashSet<Note>();

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
        allowedNotesForTransition = getAllowedNotesForTransition();
    }

    // Update is called once per frame
    void Update() {
        currentTime += Time.deltaTime;

        transitionBackground = shouldStartBackgroundTransition();
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

    public void setupForNextTransitionBackground() {
        currentTransitionIndex += 1;
        nextTransitionTime = getNextTransitionTime(currentTransitionIndex);
        allowedNotesForTransition = getAllowedNotesForTransition();
    }

    public bool noteIsAllowed(Note note) {
        return allowedNotesForTransition.Contains(note);
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

        for (int index = 0; index <= (currentTransitionIndex + 1); index++) {
            allowedNotes.Add(noteTypes[index]);
        }

        return allowedNotes;
    }
}
