using UnityEngine;

public class FloatingAnimation : MonoBehaviour
{
    // Амплитуда движения (высота подъема/опускания)
    public float amplitude = 0.5f;

    // Скорость движения
    public float frequency = 1f;

    // Вращение объекта (если нужно)
    public bool enableRotation = true;
    public float rotationSpeed = 15f;

    // Начальная позиция объекта
    private Vector3 startPosition;

    void Start()
    {
        // Запоминаем начальную позицию
        startPosition = transform.position;
    }

    void Update()
    {
        // Плавное движение вверх-вниз с помощью синуса
        float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        // Плавное вращение (если включено)
        if (enableRotation)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}