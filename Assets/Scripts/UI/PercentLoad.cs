using TMPro;
using UnityEngine;

public class PercentLoad : MonoBehaviour
{
    private TextMeshProUGUI m_TextMeshProUGUI;
    void Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
        m_TextMeshProUGUI.text = $"{Mathf.Ceil(GameManager.instance.sceneLoaderManager.loadProgress * 100)}%";
    }

    void Update()
    {
        m_TextMeshProUGUI.text = $"{Mathf.Ceil(GameManager.instance.sceneLoaderManager.loadProgress * 100)}%";
    }
}
