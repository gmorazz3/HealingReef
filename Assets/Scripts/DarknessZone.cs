using UnityEngine;

public class DarknessZone : MonoBehaviour
{
    public GameObject darknessGroup;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            darknessGroup.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            darknessGroup.SetActive(false);
    }
}
