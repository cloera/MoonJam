using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {
    // Config
    [SerializeField] private float scrollSpeed = 0.1f;

    [Header("Transition Configs")]
    [SerializeField] private float transitionSpeed = 0.2f;
    [SerializeField] private List<float> transitionTimesInSeconds = new List<float>();

    // Cache
    private Material material = null;
    private bool transitioning = false;
    private float transitionHeight = 1.0f / 3.0f;

    private float currentTime;
    private int currentTransitionIndex;
    private float nextTransitionTime;
    private float nextTransitionHeight;

    // Start is called before the first frame update
    void Start() {
        material = GetComponent<Renderer>().material;

        currentTime = 0;
        currentTransitionIndex = 0;
        nextTransitionTime = getNextTransitionTime(currentTransitionIndex);
        nextTransitionHeight = transitionHeight;
    }

    // Update is called once per frame
    void Update() {
        currentTime += Time.deltaTime;

        transitioning = shouldStartBackgroundTransition();

        if (transitioning) {
            scrollTopToBottom();
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
            currentTransitionIndex += 1;
            nextTransitionTime = getNextTransitionTime(currentTransitionIndex);
            nextTransitionHeight += transitionHeight;
        }
    }

    private void scrollRightToLeft() {
        Vector2 textureOffset = new Vector2(scrollSpeed, 0f) * Time.deltaTime;

        material.mainTextureOffset += textureOffset;
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
}
