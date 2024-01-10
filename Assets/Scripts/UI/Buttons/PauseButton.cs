using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private GameObject backgroundPanel;
    [SerializeField] private GameObject menuPanel;

    public void PauseGame()
    {
        if (GameManager.instance.isPaused)
        {
            menuPanel.SetActive(false);
            backgroundPanel.SetActive(false);
            GameManager.instance.Continue();
        }
        else
        {
            GameManager.instance.Pause();
            backgroundPanel.SetActive(true);
            menuPanel.SetActive(true);
        }
    }
}
