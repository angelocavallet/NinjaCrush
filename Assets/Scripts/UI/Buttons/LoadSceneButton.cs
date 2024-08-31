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
}
