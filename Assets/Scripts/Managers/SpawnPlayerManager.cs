using Unity.Netcode;
using UnityEngine;

public class SpawnPlayerManager : MonoBehaviour
{
    [Header("Desvio máximo do ponto de spawn")]
    public float maxOffset = 3f;

    private void Start()
    {
        if (NetworkManager.Singleton.IsClient && NetworkManager.Singleton.LocalClient?.PlayerObject != null)
        {
            NetworkObject player = NetworkManager.Singleton.LocalClient.PlayerObject;

            // Calcula uma posição aleatória ao redor do transform.position
            Vector3 randomOffset = new Vector3(
                Random.Range(-maxOffset, maxOffset),
                transform.position.y,
                transform.position.z
            );

            Vector3 spawnPosition = transform.position + randomOffset;
            player.transform.position = spawnPosition;

            Debug.Log($"[ScenePositioner] Player reposicionado para {spawnPosition}");
        }
    }
}
