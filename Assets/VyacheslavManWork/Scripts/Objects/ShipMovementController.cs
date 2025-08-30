using System.Collections;
using UnityEngine;

public class ShipMovementController : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private CharacterController characterController;
    private Coroutine _activeCoroutine;

    public void MoveToPosition(Vector3 targetPosition)
    {
        if (_activeCoroutine != null)
        {
            StopCoroutine(_activeCoroutine);
        }

        _activeCoroutine = StartCoroutine(MoveCoroutine(targetPosition));
    }

    private IEnumerator MoveCoroutine(Vector3 targetPosition)
    {
        characterController.enabled = false;

        Vector3 target = new Vector3(
            targetPosition.x,
            transform.position.y,
            transform.position.z
        );

        while (!Mathf.Approximately(transform.position.x, target.x))
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                _speed * Time.deltaTime
            );

            yield return null;
        }

        characterController.enabled = true;
        _activeCoroutine = null;
    }
}