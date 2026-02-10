using UnityEngine;

public class SimpleFish : MonoBehaviour
{
    public enum MovementAxis { Horizontal, Vertical }

    [Header("Movement Settings")]
    public MovementAxis axis = MovementAxis.Horizontal; // Choose horizontal or vertical movement
    public float speed = 1f;          // Movement speed
    public float moveDistance = 3f;   // How far from startPos the fish moves
    public bool startRightOrUp = true; // true = right/up, false = left/down

    private Vector3 startPos;
    private int direction;

    void Start()
    {
        startPos = transform.position;
        direction = startRightOrUp ? 1 : -1;
    }

    void Update()
    {
        Vector3 moveDir;

        // Determine movement axis
        if (axis == MovementAxis.Horizontal)
        {
            moveDir = Vector2.right;
            // Move the fish
            transform.Translate(moveDir * direction * speed * Time.deltaTime);

            // Flip sprite for horizontal movement
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y, 1);

            // Reverse direction at bounds
            if (transform.position.x > startPos.x + moveDistance)
                direction = -1;
            else if (transform.position.x < startPos.x - moveDistance)
                direction = 1;
        }
        else // Vertical movement
        {
            moveDir = Vector2.up;
            transform.Translate(moveDir * direction * speed * Time.deltaTime);

            // Reverse direction at bounds
            if (transform.position.y > startPos.y + moveDistance)
                direction = -1;
            else if (transform.position.y < startPos.y - moveDistance)
                direction = 1;
        }
    }
}
