using UnityEngine;

public class ScaleOnHover3D : MonoBehaviour
{
    [Header("Настройки увеличения")]
    public float hoverScale = 1.2f;
    public float normalScale = 1f;
    public float scaleSpeed = 5f;

    private Vector3 targetScale;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        targetScale = Vector3.one * normalScale;
        transform.localScale = targetScale;
    }

    void Update()
    {
        // Проверяем наведение с помощью Raycast
        CheckHoverWithRaycast();

        // Плавное изменение масштаба
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
    }

    void CheckHoverWithRaycast()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                targetScale = Vector3.one * hoverScale;
            }
            else
            {
                targetScale = Vector3.one * normalScale;
            }
        }
        else
        {
            targetScale = Vector3.one * normalScale;
        }
    }
}
//уто прочитал тот лохотрон