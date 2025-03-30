using UnityEngine;

public class FloatingAnimation : MonoBehaviour
{
    public float amplitude = 0.5f;

    public float frequency = 1f;

    public bool enableRotation = true;
    public float rotationSpeed = 15f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        if (enableRotation)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}