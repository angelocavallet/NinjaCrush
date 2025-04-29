using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class StartLobbyButtton : MonoBehaviour
{
    private Button startButton;

    private void Start()
    {
        startButton = GetComponent<Button>();
        startButton.interactable = false;

    }

    private void Update()
    {
        if (NetworkManager.Singleton != null && NetworkManager.Singleton.IsServer)
        {
            startButton.interactable = true;
        }
    }

    public void StartLobby()
    {
        GameManager.instance.sceneLoaderManager.LoadNetworkScene("Level01Scene");
    }
}
