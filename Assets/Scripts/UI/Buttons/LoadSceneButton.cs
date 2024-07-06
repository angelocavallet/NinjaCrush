using UnityEngine;

public class LoadSceneButton : MonoBehaviour
{
    public void loadScene(string sceneName)
    {
        if (GameManager.instance) GameManager.instance.sceneLoaderManager.LoadSceneAsync(sceneName);
    }
}
