using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "new SceneLoaderManagerData", menuName = "ScriptableObjects/GameManager/SceneLoaderManagerData")]
public class SceneLoaderManagerScriptableObject : ScriptableObject
{
    public string loadSceneName;

    public string startGameSceneName;

    public float minLoadTimeToCutoffSeconds = 0f;

    public GameMessageLoading gameMessageLoadPrefab;

    public TextMeshProUGUI percentTextPrefab;
}
