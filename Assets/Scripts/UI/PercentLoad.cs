using TMPro;
using UnityEngine;

public class PercentLoad : MonoBehaviour
{
    private TextMeshProUGUI m_TextMeshProUGUI;
    void Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
        m_TextMeshProUGUI.text = $"{GameManager.instance.sceneLoaderManager.loadProgress}%";
    }

    void Update()
    {
        m_TextMeshProUGUI.text = $"{GameManager.instance.sceneLoaderManager.loadProgress}%";
    }
}
