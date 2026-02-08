using UnityEngine;

public class SpotlightFollow : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        if (player == null) return;
        if (Camera.main == null) return;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(player.position);
        transform.position = screenPos;
    }
}
