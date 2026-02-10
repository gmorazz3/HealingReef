using UnityEngine;

public class FollowPlayerUI : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(player.position);
    }
}
