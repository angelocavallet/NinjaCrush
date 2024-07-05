using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new GameManagerData", menuName = "ScriptableObjects/GameManager/GameManagerData")]
public class GameManagerScriptableObject : ScriptableObject
{
    public float displayGameMessageSeconds = 2.3f;

    public float writeDialogLetterEverySeconds = 0.4f;

    public SoundManagerScriptableObject soundManagerData;

    public PlayerInputScriptableObject playerInputData;

    public SceneLoaderManagerScriptableObject sceneLoaderManagerData;

    public List<LevelManagerScriptableObject> levelManagerDataList = new List<LevelManagerScriptableObject>();
}
