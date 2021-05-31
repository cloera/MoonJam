using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GifAnimator : MonoBehaviour {
    [SerializeField] private float framesPerSecond = 25.0f;
    [SerializeField] private List<Sprite> spriteAnimations = new List<Sprite>();

    // Cache
    private int currentSpriteIndex = 0;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        updateSprite();
    }

    private void updateSprite() {
        int nextSpriteIndex =
            Mathf.FloorToInt(framesPerSecond * Time.time) % spriteAnimations.Count;

        Debug.Log(string.Format("Next Sprite Index: {0}", nextSpriteIndex));

        if (currentSpriteIndex != nextSpriteIndex) {
            Sprite nextSprite = spriteAnimations[nextSpriteIndex];

            spriteRenderer.sprite = nextSprite;

            currentSpriteIndex = nextSpriteIndex;
        }
    }
}
