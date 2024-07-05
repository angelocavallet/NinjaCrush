using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class SingleActorDialogController : MonoBehaviour
{
    [SerializeField]
    private SingleActorDialogScriptableObject dialogData;

    [SerializeField]
    private ButtonClickedEvent endDialogActions = new ButtonClickedEvent();

    [SerializeField]
    private Image actorImage;

    [SerializeField]
    private GameObject dialogPanel;

    [SerializeField]
    private TextMeshProUGUI dialogText;

    [SerializeField]
    private Button continueButton;

    [SerializeField]
    private DialogWriter dialogWriter;

    private bool writeStarted = false;

    private int actualDialog = 0;

    void Start()
    {
        dialogPanel.SetActive(true);
        actorImage.gameObject.SetActive(true);
        actorImage.sprite = dialogData.actorSprite;
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(() =>
        {
            if (writeStarted && !dialogWriter.writeFinished)
            {
                dialogWriter.ResumeDialog();
                writeStarted = false;
            } else
            {
                StartNextDialog();
            }
        });

        StartNextDialog();
    }

    private void StartNextDialog()
    {
        if (actualDialog > dialogData.dialogList.Count - 1)
        {
            EndDialog();
        }
        else
        {
            Dialog dialog = dialogData.dialogList[actualDialog];
            actualDialog++;
            writeStarted = true;
            dialogWriter.StartDialogWriter(dialog.text);
        }
    }

    private void EndDialog()
    {
        endDialogActions.Invoke();
        Destroy(gameObject);
    }
}
