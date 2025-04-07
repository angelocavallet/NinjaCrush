using UnityEngine.UI;
using UnityEngine;

public class StartSessionButton : MonoBehaviour
{
    private Button startButton;

    public void OnStart()
    {
        startButton = GetComponent<Button>();
        startButton.interactable = false;
    }

    public void OnUpdate()
    {
        startButton.interactable = true;
    }

    public void StartSceneListener()
    {
        GameManager.instance.sceneLoaderManager.SetUpOnNetworkSceneLoad();
    }

    //@TODO Resolver essa gambiarra
    public void StartSceneSession()
    {
        GameManager.instance.sceneLoaderManager.LoadNetworkScene("Level01Scene");
    }
}
