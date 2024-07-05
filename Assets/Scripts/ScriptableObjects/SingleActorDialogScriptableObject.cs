using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

[System.Serializable]
public class Dialog
{
    [TextArea(3, 8)]
    public string text;
}

[CreateAssetMenu(fileName = "new SingleActorDialogData", menuName = "ScriptableObjects/Dialog/SingleActorDialogData")]
public class SingleActorDialogScriptableObject : ScriptableObject
{
    public string actorName;

    public Sprite actorSprite;

    public List<Dialog> dialogList = new List<Dialog>();
}
