using UnityEngine;

public class RotateButton : MonoBehaviour, IInteractWithObj
{
    [SerializeField] private float _rotateX;
    [SerializeField] private float _rotateY;
    [SerializeField] private float _rotateZ;

    [SerializeField] private GameObject _rotateThis;

    public void Interact()
    {
        _rotateThis.transform.Rotate(_rotateX, _rotateY, _rotateZ);

        Vector3 currentRotation = _rotateThis.transform.eulerAngles;

        float normalizedX = NormalizeAngle(currentRotation.x);
        float normalizedY = NormalizeAngle(currentRotation.y);
        float normalizedZ = NormalizeAngle(currentRotation.z);

        _rotateThis.transform.eulerAngles = new Vector3(normalizedX, normalizedY, normalizedZ);
    }

    private float NormalizeAngle(float angle)
    {
        angle %= 360f;

        if (angle > 180f)
            angle -= 360f;

        return angle;
    }
}