using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour {
    // Config
    [SerializeField] private int initialScore = 0;

    // State
    private int currentScore;

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
    }

    // Update is called once per frame
    void Update() {
    }

    public int getCurrentScore() {
        return currentScore;
    }

    public void addToScore(int scoreValue) {
        currentScore += scoreValue;
    }

    private bool isOnlyGameStatusInstance() {
        int numberOfGameStatusInstances = FindObjectsOfType<GameStatus>().Length;

        return numberOfGameStatusInstances <= 1;
    }
}
