using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {
    // Config
    [SerializeField] private float scrollSpeed = 0.1f;

    [Header("Transition Configs")]
    [SerializeField] private bool transitions = false;
    [SerializeField] private float transitionSpeed = 0.2f;

    // Cache
    private Material material = null;
    private GameStatus gameStatus = null;
    private float transitionHeight = 1.0f / 3.0f;

    private float nextTransitionHeight;

    // Start is called before the first frame update
    void Start() {
        gameStatus = FindObjectOfType<GameStatus>();

        material = GetComponent<Renderer>().material;

        nextTransitionHeight = transitionHeight;
    }

    // Update is called once per frame
    void Update() {
        if (gameStatus.shouldTransitionBackground()) {
            if (transitions) {
                scrollTopToBottom();
            }
        } else {
            scrollRightToLeft();
        }
    }

    private void scrollTopToBottom() {
        float yOffset = transitionSpeed * Time.deltaTime;

        Vector2 textureOffset = material.mainTextureOffset - new Vector2(0, yOffset);

        if (textureOffset.y >= -nextTransitionHeight) {
            material.mainTextureOffset = textureOffset;
        } else {
            gameStatus.setupForNextTransitionBackground();
            nextTransitionHeight += transitionHeight;
        }
    }

    private void scrollRightToLeft() {
        Vector2 textureOffset = new Vector2(scrollSpeed, 0f) * Time.deltaTime;

        material.mainTextureOffset += textureOffset;
    }
}
