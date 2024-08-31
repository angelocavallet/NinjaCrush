using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private GameObject backgroundPanel;

    public void PauseGame()
    {
        if (GameManager.instance.isPaused)
        {
            backgroundPanel.SetActive(false);
            GameManager.instance.Continue();
        }
        else
        {
            GameManager.instance.Pause();
            backgroundPanel.SetActive(true);
        }
    }
}
