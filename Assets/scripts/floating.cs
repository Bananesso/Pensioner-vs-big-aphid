using UnityEngine;

public class SmoothWobble : MonoBehaviour
{
    [Header("Position Wobble")]
    public float verticalAmplitude = 0.2f;    // Высота движения вверх-вниз
    public float verticalFrequency = 1f;      // Скорость движения вверх-вниз

    [Header("Rotation Wobble")]
    public float tiltAngle = 15f;             // Максимальный угол наклона
    public float tiltSpeed = 2f;              // Скорость наклона
    public Vector3 tiltAxis = Vector3.forward; // Ось для наклона вперед/назад
    public Vector3 swayAxis = Vector3.right;   // Ось для качания влево/вправо

    [Header("Randomness")]
    public float randomness = 0.3f;           // Случайные вариации движения
    public float randomSeed;                  // Уникальное значение для вариаций

    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        randomSeed = Random.Range(0f, 100f); // Уникальное значение для каждого объекта
    }

    void Update()
    {
        // Добавляем случайные вариации к частотам
        float verticalOffset = Mathf.Sin(Time.time * verticalFrequency + randomSeed) * verticalAmplitude;
        float tiltOffset = Mathf.Sin(Time.time * tiltSpeed + randomSeed * 2) * tiltAngle;
        float swayOffset = Mathf.Cos(Time.time * tiltSpeed * 0.7f + randomSeed) * tiltAngle * 0.5f;

        // Применяем движение вверх-вниз
        transform.position = startPosition + Vector3.up * verticalOffset;

        // Создаем плавное вращение в нескольких осях
        Quaternion tiltRotation = Quaternion.AngleAxis(tiltOffset, tiltAxis);
        Quaternion swayRotation = Quaternion.AngleAxis(swayOffset, swayAxis);

        // Комбинируем вращения
        transform.rotation = startRotation * tiltRotation * swayRotation;
    }
