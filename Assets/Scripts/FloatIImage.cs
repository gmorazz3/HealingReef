using UnityEngine;

public class UIFloatEffect : MonoBehaviour
{
    public float amplitude = 25f;   // How high it floats (UI pixels)
    public float frequency = 1f;    // How fast it floats

    private RectTransform rectTransform;
    private Vector2 startPos;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        rectTransform.anchoredPosition = startPos + new Vector2(0f, yOffset);
    }
}
