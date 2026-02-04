using UnityEngine;

public class WindHallway : MonoBehaviour
{
    [Header("Wind Settings")]
    public float maxWindForce = 3000f;      // Maximum pushback force
    public float windRampTime = 2f;         // Seconds to reach max force
    public float requiredSpeed = 4f;        // Speed required to pass
    public bool useMultiplier = true;       // Slow movement overall

    private float windTimer = 0f;

    private float tempSpeed;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        SharkyController player = other.GetComponent<SharkyController>();
        tempSpeed = player.speedMultiplier;
        if (rb == null || player == null) return;

        Vector2 windDir = -transform.up; // Default down

        if (!player.isMatchaActive)
        {
            // Increase the wind force over time
            windTimer += Time.deltaTime;
            float forceMultiplier = Mathf.Clamp01(windTimer / windRampTime);
            float currentWindForce = maxWindForce * forceMultiplier;

            // Gradually reduce player's velocity along the wind direction
            Vector2 velAlongWind = Vector2.Dot(rb.linearVelocity, windDir) * windDir;
            rb.linearVelocity -= velAlongWind * forceMultiplier;

            // Apply the pushback
            rb.AddForce(windDir * currentWindForce * Time.deltaTime, ForceMode2D.Force);

            // Optional: slow overall movement
            if (useMultiplier)
                player.speedMultiplier = Mathf.Lerp(1f, 0.4f, forceMultiplier);
        }
        else
        {
            // Player boosted → normal movement
            player.speedMultiplier = tempSpeed;
            windTimer = 0f; // reset
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        SharkyController player = other.GetComponent<SharkyController>();
        if (player != null)
            player.speedMultiplier = tempSpeed;

        // Reset timer so wind ramps from zero next time
        windTimer = 0f;
    }
}
