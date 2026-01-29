using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SharkyController : MonoBehaviour
{
    Rigidbody2D shark;

    [Header("Movement")]
    public float baseSpeed = 200f;
    public float speedMultiplier = 1f;

    [Header("Visuals")]
    public float wiggleSpeed = 6f;
    public float wiggleAmount = 0.03f;

    Vector3 baseScale;
    float wiggleTimer;

    void Start()
    {
        shark = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;
    }

    void Update()
    {
        HandleKeyboardMovement();
        TailWiggle();
    }

    private void HandleKeyboardMovement()
    {
        Vector2 input = Vector2.zero;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) input.x = -1;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) input.x = 1;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) input.y = 1;
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) input.y = -1;

        if (input != Vector2.zero)
        {
            input.Normalize();
            shark.linearVelocity = input * baseSpeed * speedMultiplier * Time.deltaTime;

            // Flip sprite
            if (input.x < -0.1f) Flip(false);
            else if (input.x > 0.1f) Flip(true);
        }
        else
        {
            shark.linearVelocity = Vector2.zero;
        }
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
}
