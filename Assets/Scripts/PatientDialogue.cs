using System.Collections;
using TMPro;
using UnityEngine;

public enum IllnessType
{
    BubbleHiccups,
    TangledInVines,
    FoodPoisoning,
    Sunburn
}

public class PatientDialogue : MonoBehaviour
{
    [Header("Illness Settings")]
    public IllnessType illness;

    [Header("Dialogue Text")]
    [TextArea] public string bubbleBefore;
    [TextArea] public string bubbleAfter;
    [TextArea] public string vineBefore;
    [TextArea] public string vineAfter;
    [TextArea] public string foodBefore;
    [TextArea] public string foodAfter;
    [TextArea] public string sunBefore;
    [TextArea] public string sunAfter;

    [Header("Particles")]
    public ParticleSystem bubbleParticles;
    public ParticleSystem vineParticles;
    public ParticleSystem foodParticles;
    public ParticleSystem sunParticles;
    public ParticleSystem healParticles;

    [Header("Bubble UI")]
    public GameObject dialogueBubble;
    public TextMeshProUGUI bubbleText;
    public float typingSpeed = 0.03f;

    private ParticleSystem activeIllnessParticles;
    private bool isHealed = false;
    private bool playerInRange = false;
    private Coroutine typingCoroutine;

    private void Start()
    {
        SpawnIllnessParticles();
        if (dialogueBubble != null)
            dialogueBubble.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !isHealed && Input.GetKeyDown(KeyCode.E))
        {
            HealPatient();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            ShowCorrectDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            HideBubble();
        }
    }


    private void HealPatient()
    {
        if (isHealed) return;

        isHealed = true;
        ShowCorrectDialogue();
        RemoveIllnessParticles();
        SpawnHealParticles();
    }

    private void ShowCorrectDialogue()
    {
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

    private void SpawnIllnessParticles()
    {
        switch (illness)
        {
            case IllnessType.BubbleHiccups:
                activeIllnessParticles = Instantiate(bubbleParticles, transform.position + Vector3.up, Quaternion.identity, transform);
                break;
            case IllnessType.TangledInVines:
                activeIllnessParticles = Instantiate(vineParticles, transform.position + Vector3.up, Quaternion.identity, transform);
                break;
            case IllnessType.FoodPoisoning:
                activeIllnessParticles = Instantiate(foodParticles, transform.position + Vector3.up, Quaternion.identity, transform);
                break;
            case IllnessType.Sunburn:
                activeIllnessParticles = Instantiate(sunParticles, transform.position + Vector3.up, Quaternion.identity, transform);
                break;
        }

        if (activeIllnessParticles != null)
            activeIllnessParticles.Play();
    }

    private void RemoveIllnessParticles()
    {
        if (activeIllnessParticles != null)
        {
            activeIllnessParticles.Stop();
            Destroy(activeIllnessParticles.gameObject, 0.5f);
        }
    }

    private void SpawnHealParticles()
    {
        if (healParticles != null)
        {
            ParticleSystem ps = Instantiate(healParticles, transform.position + Vector3.up, Quaternion.identity);
            ps.Play();
            Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
        }
    }

    private string GetBeforeText()
    {
        switch (illness)
        {
            case IllnessType.BubbleHiccups: return bubbleBefore;
            case IllnessType.TangledInVines: return vineBefore;
            case IllnessType.FoodPoisoning: return foodBefore;
            case IllnessType.Sunburn: return sunBefore;
            default: return "";
        }
    }

    private string GetAfterText()
    {
        switch (illness)
        {
            case IllnessType.BubbleHiccups: return bubbleAfter;
            case IllnessType.TangledInVines: return vineAfter;
            case IllnessType.FoodPoisoning: return foodAfter;
            case IllnessType.Sunburn: return sunAfter;
            default: return "";
        }
    }
}
