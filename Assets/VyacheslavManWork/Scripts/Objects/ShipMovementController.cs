using System.Collections;
using UnityEngine;

public class ShipMovementController : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Coroutine activeCoroutine;

    public void MoveToPosition(Vector3 targetPosition)
    {
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
        }

        activeCoroutine = StartCoroutine(MoveCoroutine(targetPosition));
    }

    private IEnumerator MoveCoroutine(Vector3 targetPosition)
    {
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
                speed * Time.deltaTime
            );

            yield return null;
        }

        activeCoroutine = null;
    }
}