using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.03f;

    private Coroutine typingCoroutine;

    private void Awake()
    {
        Instance = this;
        dialogueUI.SetActive(false);
    }

    public void ShowDialogue(string text)
    {
        dialogueUI.SetActive(true);

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(text));
    }

    public void HideDialogue()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialogueUI.SetActive(false);
    }

    private IEnumerator TypeText(string text)
    {
        dialogueText.text = "";

        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
