using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudio : MonoBehaviour {
    // Cache
    private SceneLoader sceneLoader = null;
    private AudioSource musicPlayer = null;

    // Start is called before the first frame update
    void Start() {
        if (!this.isOnlyInstance()) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        sceneLoader = FindObjectOfType<SceneLoader>();
        musicPlayer = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (sceneLoader.isOnGameScene() && musicPlayer.isPlaying) {
            musicPlayer.Stop();
        } else if (!sceneLoader.isOnGameScene() && !musicPlayer.isPlaying) {
            musicPlayer.Play();
        }
    }

    private bool isOnlyInstance() {
        int numberOfGameStatusInstances = FindObjectsOfType<MenuAudio>().Length;

        return numberOfGameStatusInstances <= 1;
    }

}
