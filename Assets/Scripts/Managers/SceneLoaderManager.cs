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
        Debug.Log(sceneLoaderManagerData.minLoadTimeToCutoffSeconds);
        this.sceneLoaderManagerData = sceneLoaderManagerData;
    }

    public IEnumerator LoadStartSceneAsync()
    {
        return LoadSceneAsync(sceneLoaderManagerData.startGameSceneName);
    }

    public IEnumerator LoadSceneAsync(string sceneName)
    {
        Debug.Log($"TEMPO INICIO: {Time.time}");
        nextScene = sceneName;
        timeToCutoffSeconds = 0f;
        if (!loadSceneLoaded) SceneManager.LoadScene(sceneLoaderManagerData.loadSceneName);
        loadSceneLoaded = true;
        Debug.Log($"TEMPO DEPOIS: {Time.time} CARREGOU LOAD? {loadSceneLoaded}");

        timeToCutoffSeconds = Time.time + sceneLoaderManagerData.minLoadTimeToCutoffSeconds;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f || timeToCutoffSeconds > Time.time)
        {
            Debug.Log($"CARREGANDO: {timeToCutoffSeconds} > {Time.time} ? prog {asyncLoad.progress} CARREGOU LOAD? {loadSceneLoaded}");
            loadProgress = asyncLoad.progress < 0.9f ? asyncLoad.progress * 100 : 100;
            yield return null;
        }
        Debug.Log($"ACABOU: {timeToCutoffSeconds} > {Time.time} ? prog {asyncLoad.progress} CARREGOU LOAD? {loadSceneLoaded}");

        asyncLoad.allowSceneActivation = true;
        loadProgress = 0f;
    }
}
