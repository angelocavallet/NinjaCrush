using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    private bool _loaded = false;

    private void Update()
    {
        if (!_loaded && GameManager.instance)
        {
            _loaded = true;
            StartCoroutine(GameManager.instance.sceneLoaderManager.LoadNextSceneAsync());
        }
    }
}
