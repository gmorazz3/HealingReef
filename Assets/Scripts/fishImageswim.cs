using UnityEngine;

public class fishImageswim : MonoBehaviour
{
    public enum MovementAxis { Horizontal, Vertical }

    [Header("Movement Settings")]
    public MovementAxis axis = MovementAxis.Horizontal; // Horizontal or Vertical
    public float speed = 50f;          // Movement speed (UI units per second)
    public float moveDistance = 200f;  // How far from startPos the fish moves
    public bool startRightOrUp = true; // true = right/up, false = left/down

    private RectTransform rect;
    private Vector2 startPos;
    private int direction;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        startPos = rect.anchoredPosition;
        direction = startRightOrUp ? 1 : -1;
    }

    void Update()
    {
        Vector2 moveDir;

        if (axis == MovementAxis.Horizontal)
        {
            moveDir = Vector2.right;

            // Move fish
            rect.anchoredPosition += moveDir * direction * speed * Time.deltaTime;

            // Flip image for horizontal movement
            rect.localScale = new Vector3(Mathf.Abs(rect.localScale.x) * direction, rect.localScale.y, 1);

            // Reverse at bounds
            if (rect.anchoredPosition.x > startPos.x + moveDistance)
                direction = -1;
            else if (rect.anchoredPosition.x < startPos.x - moveDistance)
                direction = 1;
        }
        else // Vertical movement
        {
            moveDir = Vector2.up;
            rect.anchoredPosition += moveDir * direction * speed * Time.deltaTime;

            // Reverse at bounds
            if (rect.anchoredPosition.y > startPos.y + moveDistance)
                direction = -1;
            else if (rect.anchoredPosition.y < startPos.y - moveDistance)
                direction = 1;
        }
    }
}
