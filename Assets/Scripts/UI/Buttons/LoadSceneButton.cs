using UnityEngine;

public class LoadSceneButton : MonoBehaviour
{
    public void loadScene(string sceneName)
    {
        if (GameManager.instance)
        {
            GameManager.instance.Continue();
            GameManager.instance.sceneLoaderManager.LoadScene(sceneName);
        }
    }

    public void loadNetworkScene(string sceneName)
    {
        if (GameManager.instance)
        {
            GameManager.instance.Continue();
            GameManager.instance.sceneLoaderManager.LoadNetworkScene(sceneName);
        }
    }
}
