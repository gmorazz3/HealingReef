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
    public float popSpeed = 8f;

    private Coroutine typingCoroutine;
    private Coroutine scaleCoroutine;

    private void Start()
    {
        if (dialogueBubble != null)
        {
            dialogueBubble.SetActive(false);
            dialogueBubble.transform.localScale = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowBubble(dialogueLine);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideBubble();
        }
    }

    private void ShowBubble(string text)
    {
        if (dialogueBubble == null || bubbleText == null) return;

        dialogueBubble.SetActive(true);

        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);
        scaleCoroutine = StartCoroutine(ScaleBubble(Vector3.one));

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText(text));
    }

    private void HideBubble()
    {
        if (dialogueBubble == null) return;

        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);
        scaleCoroutine = StartCoroutine(ScaleBubble(Vector3.zero));
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

    private IEnumerator ScaleBubble(Vector3 targetScale)
    {
        Vector3 startScale = dialogueBubble.transform.localScale;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * popSpeed;
            dialogueBubble.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        dialogueBubble.transform.localScale = targetScale;

        if (targetScale == Vector3.zero)
            dialogueBubble.SetActive(false);
    }
}
