using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SharkyController : MonoBehaviour
{
    private Rigidbody2D shark;           // Rigidbody2D for physics-based movement
    private Vector2 input;                // Stores current input from keyboard
    private Vector3 baseScale;            // Original shark scale for flipping and wiggle
    private float wiggleTimer;            // Timer for tail wiggle animation
    private SpriteRenderer sharkSprite;   // For visual effect during matcha boost
    private Color originalColor;          // Store original shark color

    [Header("Movement")]
    public float baseSpeed = 5f;          // Base movement speed of the shark
    public float speedMultiplier = 1f;    // Multiplies speed temporarily

    [Header("Visuals")]
    public float wiggleSpeed = 6f;        // Speed of tail wiggle
    public float wiggleAmount = 0.03f;    // Amplitude of tail wiggle

    [Header("Inventory")]
    public InventoryUI inventoryUI;       // Reference to UI controller
    public bool HasAloeVera = false;     // Track if shark has picked up Aloe Vera
    public bool HasShell = false;     // Track if shark has picked up Shell


    [Header("Powerups")]
    public float matchaBoostMultiplier = 2f;  // How much Matcha boosts speed
    public float matchaDuration = 5f;         // Duration in seconds
    public Color matchaColor = new Color(0.6f, 1f, 0.6f); // Green tint for boost

    // Track if matcha boost is active
    public bool isMatchaActive = false;

    void Awake()
    {
        shark = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;

        // Rigidbody2D settings for smooth movement
        shark.interpolation = RigidbodyInterpolation2D.Interpolate;
        shark.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        shark.gravityScale = 0f;
        shark.freezeRotation = true;

        // Get sprite for visual effects
        sharkSprite = GetComponent<SpriteRenderer>();
        originalColor = sharkSprite.color;
    }

    void Update()
    {
        // Read keyboard input
        input = Vector2.zero;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) input.x = -1;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) input.x = 1;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) input.y = 1;
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) input.y = -1;

        if (input != Vector2.zero)
            input.Normalize();

        TailWiggle();
    }

    void FixedUpdate()
    {
        // Physics-based movement
        shark.linearVelocity = input * baseSpeed * speedMultiplier;

        // Flip sprite based on horizontal input
        if (input.x < -0.1f) Flip(false);
        else if (input.x > 0.1f) Flip(true);
        if (input.x < -0.1f) Flip(false);
        else if (input.x > 0.1f) Flip(true);
    }


    private void TailWiggle()
    {
        wiggleTimer += Time.deltaTime * wiggleSpeed;
        float wiggle = Mathf.Sin(wiggleTimer) * wiggleAmount;

        Vector3 scale = baseScale;
        scale.y += wiggle;
        transform.localScale = scale;
    }

    private void Flip(bool faceRight)
    {
        Vector3 scale = baseScale;
        scale.x = Mathf.Abs(scale.x) * (faceRight ? 1 : -1);
        transform.localScale = scale;
        baseScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Pickup Aloe Vera
        if (collision.CompareTag("AloeVera") && !HasAloeVera)
        {
            collision.gameObject.SetActive(false);
            HasAloeVera = true;
            inventoryUI.SetAloe(true);
        }

        // Pickup Shell
        if (collision.CompareTag("Shell") && !HasShell)
        {
            collision.gameObject.SetActive(false);
            HasShell = true;
            inventoryUI.SetShell(true);
        }


        // Pickup Matcha for temporary speed boost
        if (collision.CompareTag("matcha") && !isMatchaActive)
        {
            collision.gameObject.SetActive(false); // Hide item immediately
            StartCoroutine(MatchaBoost(collision.gameObject));
        }
    }

    private IEnumerator MatchaBoost(GameObject matchaItem)
    {
        isMatchaActive = true;
        speedMultiplier = matchaBoostMultiplier;

        float timer = 0f;
        float pulseSpeed = 4f; // for alpha pulsing

        while (timer < matchaDuration)
        {
            // Pulse the shark color
            float alpha = Mathf.Sin(Time.time * pulseSpeed) * 0.3f + 0.7f;
            sharkSprite.color = new Color(matchaColor.r, matchaColor.g, matchaColor.b, alpha);

            timer += Time.deltaTime;
            yield return null;
        }

        // Reset after boost
        speedMultiplier = 1f;
        sharkSprite.color = originalColor;
        isMatchaActive = false;

        matchaItem.SetActive(true); // Reactivate item
    }
}
