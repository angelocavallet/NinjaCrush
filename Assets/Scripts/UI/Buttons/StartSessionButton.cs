using Unity.Netcode;
using UnityEngine.UI;

public class StartSessionButton : NetworkBehaviour
{
    private Button startButton;

    public void OnStart()
    {
        startButton = GetComponent<Button>();
        startButton.interactable = false;
    }

    public void OnUpdate()
    {
        startButton.interactable = IsHost;
    }

    public void StartSceneListener()
    {
        NetworkManager.SceneManager.OnSceneEvent += GameManager.instance.sceneLoaderManager.OnNetworkSceneLoad;
        //GameManager.instance.sceneLoaderManager.LoadNetworkScene("Level01Scene");

    }

    public void StartSceneSession()
    {
        GameManager.instance.sceneLoaderManager.LoadNetworkScene("Level01Scene");
    }

    //@TODO Resolver essa gambiarra
}
