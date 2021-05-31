using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneDisplay : MonoBehaviour {
    // Config
    [SerializeField] private Text currentScoreText = null;

    // Cache
    private GameStatus gameStatus = null;

    // Start is called before the first frame update
    void Start() {
        gameStatus = FindObjectOfType<GameStatus>();
    }

    // Update is called once per frame
    void Update() {
        currentScoreText.text = gameStatus.getCurrentScore().ToString("D7");
    }
}
