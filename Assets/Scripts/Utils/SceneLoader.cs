using Unity.Netcode;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    private bool _loaded = false;

    private void Update()
    {
        if (!_loaded && GameManager.instance)
        {
            _loaded = true;

            if (GameManager.instance.sceneLoaderManager.isNetWorkScene)
            {
                if (NetworkManager.Singleton.IsHost) GameManager.instance.sceneLoaderManager.DirectLoadNextNetWorkScene();
                return;
            }

            StartCoroutine(GameManager.instance.sceneLoaderManager.LoadNextSceneAsync());
        }
    }
}
