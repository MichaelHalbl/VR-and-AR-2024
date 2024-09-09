using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float amplitude = 0.5f; // Höhe des Schwebens
    public float speed = 1f; // Geschwindigkeit des Schwebens

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position = startPos + new Vector3(0.0f, Mathf.Sin(Time.time * speed) * amplitude, 0.0f);
    }
}