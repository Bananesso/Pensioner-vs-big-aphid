using System.Collections;
using UnityEngine;

public class RotateButton : MonoBehaviour, IInteractWithObj
{
    [Header("Поворачиваемый объект")]
    [SerializeField] private GameObject _rotateThis;

    [Header("Сила поворота")]
    [SerializeField] private float _rotateX;
    [SerializeField] private float _rotateY;
    [SerializeField] private float _rotateZ;
    [SerializeField] private int _rotateSpeed = 45;

    private Vector3 _targetEulerAngles;
    private Coroutine _activeCoroutine;

    private void Start()
    {
        _targetEulerAngles = _rotateThis.transform.eulerAngles;
    }

    public void Interact()
    {
        _targetEulerAngles += new Vector3(_rotateX, _rotateY, _rotateZ);

        _targetEulerAngles = new Vector3(
            NormalizeAngle(_targetEulerAngles.x),
            NormalizeAngle(_targetEulerAngles.y),
            NormalizeAngle(_targetEulerAngles.z)
        );

        if (_activeCoroutine != null)
        {
            StopCoroutine(_activeCoroutine);
        }
        _activeCoroutine = StartCoroutine(SmoothRotateCoroutine());
    }

    private IEnumerator SmoothRotateCoroutine()
    {
        while (true)
        {
            _rotateThis.transform.eulerAngles = new Vector3(
                Mathf.MoveTowardsAngle(_rotateThis.transform.eulerAngles.x, _targetEulerAngles.x, _rotateSpeed * Time.deltaTime),
                Mathf.MoveTowardsAngle(_rotateThis.transform.eulerAngles.y, _targetEulerAngles.y, _rotateSpeed * Time.deltaTime),
                Mathf.MoveTowardsAngle(_rotateThis.transform.eulerAngles.z, _targetEulerAngles.z, _rotateSpeed * Time.deltaTime)
            );

            if (AnglesEqual(_rotateThis.transform.eulerAngles, _targetEulerAngles))
            {
                break;
            }

            yield return null;
        }

        _rotateThis.transform.eulerAngles = _targetEulerAngles;
        _activeCoroutine = null;
    }

    private bool AnglesEqual(Vector3 currentRotation, Vector3 targetRotation)
    {
        return Mathf.Abs(Mathf.DeltaAngle(currentRotation.x, targetRotation.x)) < 0.1f &&
               Mathf.Abs(Mathf.DeltaAngle(currentRotation.y, targetRotation.y)) < 0.1f &&
               Mathf.Abs(Mathf.DeltaAngle(currentRotation.z, targetRotation.z)) < 0.1f;
    }

    private float NormalizeAngle(float angle)
    {
        angle %= 360f;

        if (angle > 180f)
            angle -= 360f;

        return angle;
    }
}