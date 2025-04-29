using Unity.Netcode;
using UnityEngine;

public class NetworkManagerSingleton : MonoBehaviour
{
    private void Awake()
    {
        if (NetworkManager.Singleton != null && NetworkManager.Singleton != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }
}
