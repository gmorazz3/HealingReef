using UnityEngine;

public class Sharkyscript : MonoBehaviour
{
    Rigidbody2D shark;

    [Header("Movement")]
    public float baseSpeed = 200f;
    public float speedMultiplier = 1f;

    [Header("Visuals")]
    public float wiggleSpeed = 6f;
    public float wiggleAmount = 0.03f;

    [Header("Click Movement")]
    public float clickDeadzone = 0.2f;

    Vector3 baseScale;
    float wiggleTimer;

    Vector2 targetPosition;
    bool hasTarget = false;

    void Start()
    {
        shark = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;
    }

    void Update()
    {
        Vector2 force = Vector2.zero;

        // Keyboard movement
        bool keyboardInput = false;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            force += Vector2.left;
            Flip(false);
            keyboardInput = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            force += Vector2.right;
            Flip(true);
            keyboardInput = true;
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            force += Vector2.up;
            keyboardInput = true;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            force += Vector2.down;
            keyboardInput = true;
        }

        // If the player used keyboard, cancel click movement
        if (keyboardInput) hasTarget = false;

        // Apply keyboard force
        if (force != Vector2.zero)
        {
            force.Normalize(); // optional, keeps diagonal movement consistent
            shark.AddForce(force * baseSpeed * speedMultiplier * Time.deltaTime);
        }

        HandleClickMovement();
        TailWiggle();
    }

    void HandleClickMovement()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition = mouseWorld;
            hasTarget = true;
        }

        if (!hasTarget) return;

        Vector2 direction = targetPosition - shark.position;

        if (direction.magnitude < clickDeadzone)
        {
            hasTarget = false;
            return;
        }

        direction.Normalize();
        shark.AddForce(direction * baseSpeed * speedMultiplier * Time.deltaTime);

        if (direction.x < -0.1f) Flip(false);
        else if (direction.x > 0.1f) Flip(true);
    }

    void TailWiggle()
    {
        wiggleTimer += Time.deltaTime * wiggleSpeed;
        float wiggle = Mathf.Sin(wiggleTimer) * wiggleAmount;

        Vector3 scale = baseScale;
        scale.y += wiggle;
        transform.localScale = scale;
    }

    void Flip(bool faceRight)
    {
        Vector3 scale = baseScale;
        scale.x = Mathf.Abs(scale.x) * (faceRight ? 1 : -1);
        transform.localScale = scale;
        baseScale = scale;
    }
}
