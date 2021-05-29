using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {
    // Config
    [SerializeField] float scrollSpeed = 0.1f;

    // Cache
    private Material material = null;

    // Start is called before the first frame update
    void Start() {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update() {
        Vector2 textureOffset = new Vector2(scrollSpeed, 0f) * Time.deltaTime;

        material.mainTextureOffset += textureOffset;
    }
}
