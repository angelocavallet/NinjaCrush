using Unity.Netcode;
using UnityEngine;

public class NetworkManagerButton : MonoBehaviour
{
    public void StartServer()
    {
        NetworkManager.Singleton.Shutdown();
        NetworkManager.Singleton.StartServer();
    }

    public void StartClient()
    {
        NetworkManager.Singleton.Shutdown();
        NetworkManager.Singleton.StartClient();

    }

    public void StartHost()
    {
        NetworkManager.Singleton.Shutdown();
        NetworkManager.Singleton.StartHost();
    }
}
