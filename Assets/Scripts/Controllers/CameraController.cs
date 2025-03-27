using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool ready = false;

    private void Start()
    {
        TryGetComponent();
    }

    private void Update()
    {
        if (!ready) TryGetComponent();
    }

    private void TryGetComponent()
    {
        if (GameManager.instance && !GameManager.instance.camera)
        {
            GameManager.instance.camera = GetComponent<CinemachineVirtualCamera>();
            ready = true;
        }
    }
}
