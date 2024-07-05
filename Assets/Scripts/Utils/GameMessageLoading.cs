using TMPro;
using UnityEngine;

public class GameMessageLoading : MonoBehaviour
{
    [SerializeField]
    private GameMessageLoadingScriptableObject gameMessageLoadingData;

    private TextMeshProUGUI textLoadGameMessage;
    private float lastMessageDisplayedTimeoutTime = 0f;

    private void Start()
    {
        textLoadGameMessage = GetComponentInChildren<TextMeshProUGUI>();
        WriteLoadMessage();
    }

    void Update()
    {
        if (lastMessageDisplayedTimeoutTime < Time.time)
        {
            WriteLoadMessage();
        }
    }

    private void WriteLoadMessage()
    {
        int randMsgPos = Mathf.FloorToInt(Random.Range(0, gameMessageLoadingData.messageList.Count));
        Message msg = gameMessageLoadingData.messageList[randMsgPos];
        textLoadGameMessage.text = msg.text;
        lastMessageDisplayedTimeoutTime = Time.time + (msg.timeDisplaySeconds > 0f ? msg.timeDisplaySeconds : gameMessageLoadingData.messageDisplayTimeSeconds);
    }
}
