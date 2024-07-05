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

    public void LoadStartScene()
    {
        loadSceneLoaded = true;
        LoadScene(sceneLoaderManagerData.startGameSceneName);
    }

    public void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        timeToCutoffSeconds = 0f;
        if (!loadSceneLoaded) SceneManager.LoadScene(sceneLoaderManagerData.loadSceneName);
        loadSceneLoaded = true;

        GameManager.instance.StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        timeToCutoffSeconds = Time.time + sceneLoaderManagerData.minLoadTimeToCutoffSeconds;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f || timeToCutoffSeconds > Time.time)
        {
            loadProgress = asyncLoad.progress < 0.9f ? asyncLoad.progress * 100: 100;
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;
        loadProgress = 0f;
    }
}
