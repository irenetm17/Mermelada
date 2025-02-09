using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private string[] arrayTextos;
    private bool didDialogueStart;
    private int lineIndex;

    public GameObject dialoguePanel;
    public float typingTime = 0.01f;
    public TMP_Text textoDialogo;

    public bool isfirst = true;
    private string[] prueba1 = { "Muy majos todos...", "Me tengo que ir paqui", "Adios paqui"};
    public void Update()
    {
        if (isfirst == true)
        {
            StartDialogue(prueba1);
            isfirst = false;
        }
    }
    public void StartDialogue(string[] array)
    {
        arrayTextos = array;
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }
    public void StopDialogue()
    {
        didDialogueStart = false;
        dialoguePanel.SetActive(false);
    }

    private IEnumerator ShowLine()
    {
        textoDialogo.text = string.Empty;
        foreach (char ch in arrayTextos[lineIndex])
        {
            textoDialogo.text += ch;
            yield return new WaitForSeconds(typingTime);
        }

        yield return new WaitForSeconds(2.5f);

        NextLine();
    }

    private void NextLine()
    {
        if (lineIndex < arrayTextos.Length - 1)
        {
            lineIndex++;
            StartCoroutine(ShowLine());
        }
        else
        {
            StopDialogue();
        }
    }
}
