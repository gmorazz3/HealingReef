using UnityEngine;

public class FloatEffect : MonoBehaviour
{
    public float amplitude = 0.25f;    // How high it floats
    public float frequency = 1f;       // How fast it floats

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Simple sine wave to move up and down
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = startPos + new Vector3(0f, yOffset, 0f);
    }
}
