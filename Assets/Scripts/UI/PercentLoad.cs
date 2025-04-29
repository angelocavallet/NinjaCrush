using TMPro;
using UnityEngine;

public class PercentLoad : MonoBehaviour
{
    private TextMeshProUGUI m_TextMeshProUGUI;

    private float _loadProgress = 0f;

    void Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();

        if (GameManager.instance?.sceneLoaderManager)
        {
            _loadProgress = Mathf.Ceil(GameManager.instance.sceneLoaderManager.loadProgress * 100);
            m_TextMeshProUGUI.text = $"{_loadProgress}%";
        }
    }

    void Update()
    {
        if (!GameManager.instance?.sceneLoaderManager) return;

        _loadProgress = Mathf.Ceil(GameManager.instance.sceneLoaderManager.loadProgress * 100);
        m_TextMeshProUGUI.text = $"{_loadProgress}%";
    }
}
