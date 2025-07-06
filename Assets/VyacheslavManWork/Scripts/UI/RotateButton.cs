using UnityEngine;

public class RotateButton : MonoBehaviour
{
    [SerializeField] private float _rotateX;
    [SerializeField] private float _rotateY;
    [SerializeField] private float _rotateZ;

    [SerializeField] private GameObject _rotateThis;

    public void RotateObject()
    {
        _rotateThis.transform.Rotate(_rotateX, _rotateY, _rotateZ);
    }
}