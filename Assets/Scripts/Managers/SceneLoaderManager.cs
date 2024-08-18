using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager
{
    public float loadProgress { get; private set; }

    private SceneLoaderManagerScriptableObject sceneLoaderManagerData;
    private float timeToCutoffSeconds = 0f;
    private string nextScene;
    private bool loadSceneLoaded = true;
    private float _loadProgress = 0f;

    public SceneLoaderManager(SceneLoaderManagerScriptableObject sceneLoaderManagerData) {
        this.sceneLoaderManagerData = sceneLoaderManagerData;
    }

    public IEnumerator LoadStartSceneAsync()
    {
        return LoadSceneAsync(sceneLoaderManagerData.startGameSceneName);
    }

    public IEnumerator LoadSceneAsync(string sceneName)
    {
        nextScene = sceneName;
        timeToCutoffSeconds = 0f;
        loadProgress = 0f;
        if (!loadSceneLoaded) SceneManager.LoadScene(sceneLoaderManagerData.loadSceneName);
        loadSceneLoaded = true;

        timeToCutoffSeconds = Time.time + sceneLoaderManagerData.minLoadTimeToCutoffSeconds;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f || timeToCutoffSeconds > Time.time)
        {
            if (timeToCutoffSeconds < Time.time) {
                loadProgress = asyncLoad.progress;
            }
            else
            {
                loadProgress = Time.time / timeToCutoffSeconds;
            }
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;
        loadProgress = 1f;
    }
}
