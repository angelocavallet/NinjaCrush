using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogWriter : MonoBehaviour
{
    public bool writeFinished
    {
        get => !(charCount < fullText.Length);
    }
    private AudioSource m_AudioSource;
    private TextMeshProUGUI m_TextMeshProUGUI;
    private string fullText;
    private int charCount;
    private float timer;
    private bool writeAll = false;

    void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (!writeAll && (charCount < fullText.Length && (timer < (Time.time))))
        {
            m_TextMeshProUGUI.text += fullText[charCount++];
            timer = Time.time + GameManager.instance.writeDialogLetterEverySeconds;
        }
    }

    public void StartDialogWriter(string text)
    {
        Dictionary<string, string> replaces = new Dictionary<string, string>
        {   //Variáveis do texto devem ser escritas no Text com chaves ex {JOGADOR_NOME}
          //  {"JOGADOR_NOME", GameManager.instance.nomePersonagem},
            //{"SCORE_TOTAL", GameManager.instance.scoreTotal},
            //{"SAVE_DATE", GameManager.instance.startDate},
            //{"SCORE_FASE", GameManager.instance.faseScore.ToString()},
        };

        writeAll = false;
        fullText = StringReplacer.Replace(text, replaces);
        if (m_AudioSource) m_AudioSource.Play();
        if (m_TextMeshProUGUI) m_TextMeshProUGUI.text = "";
        timer = Time.time + GameManager.instance.writeDialogLetterEverySeconds;
        charCount = 0;
    }

    public void ResumeDialog()
    {
        writeAll = true;
        m_TextMeshProUGUI.text = fullText;
    }
}
