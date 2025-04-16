using UnityEngine;

public class Billboard2D : MonoBehaviour
{
    [Header("Настройки")]
    [Tooltip("Если true, объект будет поворачиваться только по оси Y")]
    public bool yAxisOnly = true;

    [Tooltip("Если true, объект будет зеркально отражаться при повороте на 180 градусов")]
    public bool flipWhenBehind = true;

    [Tooltip("Смещение поворота (в градусах)")]
    public float rotationOffset = 0f;

    private Transform cameraTransform;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Находим главную камеру
        cameraTransform = Camera.main.transform;

        // Получаем SpriteRenderer для отражения спрайта
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Если SpriteRenderer не найден, выводим предупреждение
        if (spriteRenderer == null && flipWhenBehind)
        {
            Debug.LogWarning("SpriteRenderer не найден! Отражение не будет работать.");
        }
    }

    void LateUpdate()
    {
        // Получаем направление от объекта к камере
        Vector3 directionToCamera = cameraTransform.position - transform.position;

        // Если нужно только вращение по Y, обнуляем Y компонент направления
        if (yAxisOnly)
        {
            directionToCamera.y = 0;
        }

        // Если направление не нулевое, поворачиваем объект
        if (directionToCamera != Vector3.zero)
        {
            // Вычисляем поворот, который смотрит в направлении камеры
            Quaternion targetRotation = Quaternion.LookRotation(-directionToCamera);

            // Применяем смещение поворота
            targetRotation *= Quaternion.Euler(0, rotationOffset, 0);

            // Применяем поворот к объекту
            transform.rotation = targetRotation;

            // Если включено отражение при повороте на 180 градусов
            if (flipWhenBehind && spriteRenderer != null)
            {
                // Проверяем, находится ли камера перед или за объектом
                float angle = Vector3.Dot(transform.forward, directionToCamera.normalized);

                // Если камера сзади, отражаем спрайт
                spriteRenderer.flipX = angle < 0;
            }
        }
    }
}