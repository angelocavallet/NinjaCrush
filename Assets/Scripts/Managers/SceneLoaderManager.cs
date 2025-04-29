using System.Collections;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : NetworkBehaviour
{
    public float loadProgress { get; private set; }

    private NetworkVariable<bool> _isNetworkScene = new NetworkVariable<bool>(false);

    public bool isNetworkScene
    {
        get => _isNetworkScene.Value ? _isNetworkScene.Value : false;
        private set => _isNetworkScene.Value = value;
    }

    public string nextScene
    {
        get => _nextScene.Value.ToString();
        private set => _nextScene.Value = (FixedString64Bytes)(value ?? "");
    }

    private SceneLoaderManagerScriptableObject sceneLoaderManagerData;
    private NetworkList<ulong> clientsLoaded = new NetworkList<ulong>();
    private NetworkVariable<FixedString64Bytes> _nextScene = new NetworkVariable<FixedString64Bytes>("");
    private float timeToCutoffSeconds = 0f;

    public void SetUp(SceneLoaderManagerScriptableObject sceneLoaderManagerData)
    {
        this.sceneLoaderManagerData = sceneLoaderManagerData;
        nextScene = sceneLoaderManagerData.startGameSceneName;
    }

    public void LoadScene(string sceneName)
    {
        isNetworkScene = false;
        SetSceneSettings(sceneName);
        SceneManager.LoadScene(sceneLoaderManagerData.loadSceneName);
    }

    public void LoadNetworkScene(string sceneName)
    {
        if (!NetworkManager.Singleton.IsServer) return;

        foreach (NetworkClient client in NetworkManager.Singleton.ConnectedClientsList)
        {
            client.PlayerObject?.Despawn(true);
        }

        isNetworkScene = true;
        SetSceneSettings(sceneName);
        clientsLoaded.Clear();
        NetworkManager.Singleton.SceneManager.LoadScene(sceneLoaderManagerData.loadSceneName, LoadSceneMode.Single);
    }

    public void DirectLoadNextNetworkScene()
    {
        Debug.Log($"CARREGANDO NETWORK SCENE: {nextScene} ");
        NetworkManager.Singleton.SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }

    public IEnumerator LoadNextSceneAsync()
    {
        timeToCutoffSeconds = Time.time + sceneLoaderManagerData.minLoadTimeToCutoffSeconds;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f || timeToCutoffSeconds > Time.time)
        {
            if (timeToCutoffSeconds < Time.time)
            {
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

    [ServerRpc(RequireOwnership = false)]
    public void NotifyServerSceneLoadedServerRpc(ServerRpcParams rpcParams = default)
    {
        clientsLoaded.Add(rpcParams.Receive.SenderClientId);
        loadProgress = clientsLoaded.Count / (NetworkManager.Singleton.ConnectedClientsIds.Count - 1);
        Debug.Log($"CLIENTE AVISOU QUE JA CARREGOU {loadProgress} : {clientsLoaded.Count} / {(NetworkManager.Singleton.ConnectedClientsIds.Count - 1)}");
    }

    public bool AllClientsLoaded()
    {
        return clientsLoaded.Count == (NetworkManager.Singleton.ConnectedClientsIds.Count -1);
    }

    private void SetSceneSettings(string sceneName)
    {
        nextScene = sceneName;
        timeToCutoffSeconds = 0f;
        loadProgress = 0f;
    }
}
