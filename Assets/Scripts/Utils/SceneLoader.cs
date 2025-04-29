using Unity.Netcode;
using UnityEngine;

public class SceneLoader : NetworkBehaviour
{
    private bool _loaded = false;


    public void Update()
    {
        if (_loaded) return;
        if (!GameManager.instance) return;
        if (!GameManager.instance.sceneLoaderManager) return;

        if (GameManager.instance.sceneLoaderManager.isNetworkScene)
        {
            if (!GameManager.instance.sceneLoaderManager.AllClientsLoaded()) return;
            _loaded = true;
            Debug.Log("TODOS CARREGADOS");

            if (!NetworkManager.Singleton.IsServer) return;

            Debug.Log("CARREGANDO A PROXIMA SCENE");
            GameManager.instance.sceneLoaderManager.DirectLoadNextNetworkScene();
            return;
        }

        HandleSceneLogic();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        HandleSceneLogic();
    }

    private void HandleSceneLogic()
    {
        if (!GameManager.instance.sceneLoaderManager.isNetworkScene)
        {
            _loaded = true;
            StartCoroutine(GameManager.instance.sceneLoaderManager.LoadNextSceneAsync());
            return;
        }


        if (!NetworkManager.Singleton.IsServer)
        {
            Debug.Log("AVISANDO QUE JA CARREGUEI AQUI A PROXIMA SCENE");
            _loaded = true;
            GameManager.instance.sceneLoaderManager.NotifyServerSceneLoadedServerRpc();
            return;
        }
    }
}
