using System.Collections;
using TMPro;
using UnityEngine;

public enum IllnessType
{
    CrackedShell,
    Sunburn
}

public class PatientDialogue : MonoBehaviour
{
    [Header("Illness Settings")]
    public IllnessType illness;

    [Header("Dialogue Text")]
    [TextArea] public string crackedBefore;
    [TextArea] public string crackedAfter;
    [TextArea] public string sunBefore;
    [TextArea] public string sunAfter;

    [Header("Bubble UI")]
    public GameObject dialogueBubble;
    public TextMeshProUGUI bubbleText;
    public float typingSpeed = 0.03f;

    [Header("Visuals")]
    public SpriteRenderer patientSprite;

    [Header("Cracked Shell Sprites")]
    public Sprite crackedBeforeSprite;
    public Sprite crackedAfterSprite;

    [Header("Sunburn Visuals")]
    public Color sunburnColor = Color.red;
    private Color originalColor;

    [Header("Heal Effect")]
    public ParticleSystem healParticles;

    private bool isHealed = false;
    private SharkyController playerController;
    private Coroutine typingCoroutine;

    private void Start()
    {
        if (patientSprite != null)
            originalColor = patientSprite.color;

        ApplyInitialVisuals();
        HideBubble();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<SharkyController>();

            // Show BEFORE text if not healed yet
            ShowCorrectDialogue();

            // Attempt automatic heal
            TryAutoHeal();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = null;
            HideBubble();
        }
    }

    private void TryAutoHeal()
    {
        if (isHealed || playerController == null)
            return;

        bool hasCure = false;

        switch (illness)
        {
            case IllnessType.CrackedShell:
                hasCure = playerController.HasShell;
                break;
            case IllnessType.Sunburn:
                hasCure = playerController.HasAloeVera;
                break;
        }

        if (hasCure)
        {
            HealPatient();
        }
    }

    private void HealPatient()
    {
        if (isHealed) return;

        isHealed = true;
        ApplyHealedVisuals();
        ConsumeCure();
        PlayHealEffect();
        ShowCorrectDialogue(); // Show AFTER text
    }

    private void ConsumeCure()
    {
        if (playerController == null) return;

        switch (illness)
        {
            case IllnessType.CrackedShell:
                playerController.HasShell = false;
                break;
            case IllnessType.Sunburn:
                playerController.HasAloeVera = false;
                break;
        }
    }

    private void ApplyInitialVisuals()
    {
        if (illness == IllnessType.CrackedShell)
            patientSprite.sprite = crackedBeforeSprite;
        else if (illness == IllnessType.Sunburn)
            patientSprite.color = sunburnColor;
    }

    private void ApplyHealedVisuals()
    {
        if (illness == IllnessType.CrackedShell)
            patientSprite.sprite = crackedAfterSprite;
        else if (illness == IllnessType.Sunburn)
            patientSprite.color = originalColor;
    }

    private void PlayHealEffect()
    {
        if (healParticles != null)
        {
            ParticleSystem ps = Instantiate(
                healParticles,
                transform.position + Vector3.up,
                Quaternion.identity
            );
            ps.Play();
            //Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
        }
    }

    private void ShowCorrectDialogue()
    {
        if (dialogueBubble == null || bubbleText == null)
            return;

        string text = isHealed ? GetAfterText() : GetBeforeText();
        ShowBubble(text);
    }

    private void ShowBubble(string text)
    {
        if (dialogueBubble == null || bubbleText == null) return;

        dialogueBubble.SetActive(true);

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(text));
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

    private void HideBubble()
    {
        if (dialogueBubble != null)
            dialogueBubble.SetActive(false);

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
    }

    private string GetBeforeText()
    {
        switch (illness)
        {
            case IllnessType.CrackedShell: return crackedBefore;
            case IllnessType.Sunburn: return sunBefore;
            default: return "";
        }
    }

    private string GetAfterText()
    {
        switch (illness)
        {
            case IllnessType.CrackedShell: return crackedAfter;
            case IllnessType.Sunburn: return sunAfter;
            default: return "";
        }
    }
}
