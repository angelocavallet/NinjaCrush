using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Message
{
    [TextArea(3,8)]
    public string text = "";

    [Tooltip("if equals zero will assume GameMessageLoadingData.messageDisplayTimeSeconds")]
    public float timeDisplaySeconds = 0f;
}

[CreateAssetMenu(fileName = "new GameMessageLoadingData", menuName = "ScriptableObjects/GameManager/Load/GameMessageLoadingData")]
public class GameMessageLoadingScriptableObject : ScriptableObject
{
    public float messageDisplayTimeSeconds;

    public List<Message> messageList = new List<Message>();
}
