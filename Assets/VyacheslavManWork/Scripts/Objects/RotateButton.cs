using UnityEngine;

public class RotateButton : MonoBehaviour, IInteractWithObj
{
    [Header("Поворачиваемый объект")]
    [SerializeField] private GameObject _rotateThis;

    [Header("Сила поворота")]
    [SerializeField] private float _rotateX;
    [SerializeField] private float _rotateY;
    [SerializeField] private float _rotateZ;

    public void Interact()
    {
        Vector3 currentRotation = _rotateThis.transform.eulerAngles;

        float newX = NormalizeAngle(currentRotation.x + _rotateX);
        float newY = NormalizeAngle(currentRotation.y + _rotateY);
        float newZ = NormalizeAngle(currentRotation.z + _rotateZ);

        _rotateThis.transform.rotation = Quaternion.Euler(newX, newY, newZ);
    }


    private float NormalizeAngle(float angle)
    {
        angle %= 360f;

        if (angle > 180f)
        {
            angle -= 360f;
        }

        return angle;
    }
}