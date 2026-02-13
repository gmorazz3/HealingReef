using System.Collections;
using TMPro;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [Header("Dialogue")]
    [TextArea] public string dialogueLine;

    [Header("Bubble UI")]
    public GameObject dialogueBubble;
    public TextMeshProUGUI bubbleText;
    public float typingSpeed = 0.03f;

    private Coroutine typingCoroutine;

    private void Start()
    {
        if (dialogueBubble != null)
        {
            dialogueBubble.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowBubble();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideBubble();
        }
    }

    private void ShowBubble()
    {
        if (dialogueBubble == null || bubbleText == null) return;

        dialogueBubble.SetActive(true);

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(dialogueLine));
    }

    private void HideBubble()
    {
        if (dialogueBubble == null) return;

        dialogueBubble.SetActive(false);
    }

    private IEnumerator TypeText(string text)
    {
        bubbleText.text = "";

        foreach (char c in text)
        {
            bubbleText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
