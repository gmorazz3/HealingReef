using System.Collections;
using UnityEngine;

public class DarknessZone : MonoBehaviour
{
    public CanvasGroup darknessOverlay;
    public float fadeDuration = 0.5f;

    private Coroutine fadeCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FadeTo(1f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FadeTo(0f);
        }
    }

    void FadeTo(float targetAlpha)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeRoutine(targetAlpha));
    }

    IEnumerator FadeRoutine(float targetAlpha)
    {
        float startAlpha = darknessOverlay.alpha;
        float time = 0f;

        while (time < fadeDuration)
        {
            darknessOverlay.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        darknessOverlay.alpha = targetAlpha;
        darknessOverlay.blocksRaycasts = targetAlpha > 0;
        darknessOverlay.interactable = targetAlpha > 0;
    }
}
