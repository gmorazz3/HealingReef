using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
public class PortalWhirlpool : MonoBehaviour
{
    [Header("Rotation")]
    public float rotationSpeed = 120f;

    [Header("Activation Animation")]
    public float scaleDuration = 0.5f;

    [Header("Sound")]
    public AudioClip activationSound;

    private Vector3 originalScale;
    private bool isActive = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D portalCollider;
    private AudioSource audioSource;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        portalCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();

        // Save the size you set manually in Inspector
        originalScale = transform.localScale;

        // Start hidden and non-collidable
        transform.localScale = Vector3.zero;
        spriteRenderer.enabled = false;
        portalCollider.enabled = false;
    }

    void Update()
    {
        if (!isActive) return;

        // Rotate continuously
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    // Call this function to activate the portal
    public void ActivatePortal()
    {
        isActive = true;

        // Show and enable portal
        spriteRenderer.enabled = true;
        portalCollider.enabled = true;

        // Play sound
        if (activationSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(activationSound);
        }

        // Start scale animation
        StartCoroutine(ScaleUp());
    }

    private IEnumerator ScaleUp()
    {
        float time = 0f;
        Vector3 startScale = Vector3.zero;

        while (time < scaleDuration)
        {
            // Uniform scaling to original size
            transform.localScale = Vector3.Lerp(startScale, originalScale, time / scaleDuration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive) return;

        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
