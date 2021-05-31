using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    // Configs
    [SerializeField] private float loadSceneDelayInSeconds = 0.5f;
    [SerializeField] private string startMenuSceneName = "Start Menu";
    [SerializeField] private string gameSceneName = "GameScene";
    [SerializeField] private string bossSceneName = "Boss Scene";

    // Cache
    private AudioSource musicPlayer;

    public void loadStartMenu() {
        GameStatus gameStatus = FindObjectOfType<GameStatus>();

        if (gameStatus != null) {
            gameStatus.resetGame();
        }

        loadScene(startMenuSceneName);
    }

    public void loadGameScene() {
        loadScene(gameSceneName);
    }

    public void loadNextScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        string nextSceneName = GetSceneNameFromBuildIndex(currentSceneIndex + 1);

        loadScene(nextSceneName);
    }

    public void quitGame() {
        Application.Quit();
    }

    public bool isOnGameScene() {
        bool isOnMainGameScene = SceneManager.GetActiveScene().name.Equals(gameSceneName);
        bool isOnBossScene = SceneManager.GetActiveScene().name.Equals(bossSceneName);

        return isOnMainGameScene || isOnBossScene;
    }

    public bool isOnBossScene() {
        return SceneManager.GetActiveScene().name.Equals(bossSceneName);
    }

    private void loadScene(string sceneName) {
        StartCoroutine(loadSceneWithDelay(sceneName));
    }

    private IEnumerator loadSceneWithDelay(string sceneName) {
        yield return new WaitForSeconds(loadSceneDelayInSeconds);

        SceneManager.LoadScene(sceneName);
    }

    private static string GetSceneNameFromBuildIndex(int buildIndex) {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(buildIndex);

        return System.IO.Path.GetFileNameWithoutExtension(scenePath);
    }

    private bool isOnlyInstance() {
        int numberOfGameStatusInstances = FindObjectsOfType<SceneLoader>().Length;

        return numberOfGameStatusInstances <= 1;
    }
}
