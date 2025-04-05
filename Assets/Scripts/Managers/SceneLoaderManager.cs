using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : NetworkBehaviour
{
    public float loadProgress { get; private set; }
    public bool isNetWorkScene { get; private set; }

    private SceneLoaderManagerScriptableObject sceneLoaderManagerData;
    private float timeToCutoffSeconds = 0f;
    private string nextScene;

    public SceneLoaderManager(SceneLoaderManagerScriptableObject sceneLoaderManagerData) {
        this.sceneLoaderManagerData = sceneLoaderManagerData;
        nextScene = sceneLoaderManagerData.startGameSceneName;
    }

    public void LoadScene(string sceneName)
    {
        isNetWorkScene = false;
        SetSceneSettings(sceneName);
        SceneManager.LoadScene(sceneLoaderManagerData.loadSceneName);
    }

    public void LoadNetworkScene(string sceneName)
    {
        isNetWorkScene = true;
        SetSceneSettings(sceneName);
        NetworkManager.Singleton.SceneManager.LoadScene(sceneLoaderManagerData.loadSceneName, LoadSceneMode.Single);
    }

    public void DirectLoadNextNetWorkScene()
    {
        NetworkManager.Singleton.SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }

    public IEnumerator LoadNextSceneAsync()
    {
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

        loadProgress = 1f;
        asyncLoad.allowSceneActivation = true;
    }

    public void OnNetworkSceneLoad(SceneEvent sceneEvent)
    {
        SetSceneSettings("Level01Scene");
    }

    private void SetSceneSettings(string sceneName)
    {
        nextScene = sceneName;
        timeToCutoffSeconds = 0f;
        loadProgress = 0f;
    }
}
