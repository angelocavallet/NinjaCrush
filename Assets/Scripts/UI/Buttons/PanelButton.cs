using UnityEngine;

public class PanelButton : MonoBehaviour
{
    [SerializeField] private GameObject panelGameObject;

    public void openPanel()
    {
        panelGameObject.SetActive(true);
    }
    public void closePanel()
    {
        panelGameObject.SetActive(true);
    }
}
