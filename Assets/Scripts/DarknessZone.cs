using UnityEngine;

public class DarknessZone : MonoBehaviour
{
    public GameObject darknessOverlay;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            darknessOverlay.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            darknessOverlay.SetActive(false);
        }
    }
}
