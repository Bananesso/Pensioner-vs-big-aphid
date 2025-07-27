using System.Collections;
using UnityEngine;

public class MoveButton : MonoBehaviour
{
    private bool isPressed = false;
    [SerializeField] private GameObject _moveTo;
    [SerializeField] float targetX = 10f;
    [SerializeField] float speed = 3f;

    public void OnPointerDown()
    {
        isPressed = true;
        StartCoroutine(CheckForLongPress());
    }

    public void OnPointerUp()
    {
        isPressed = false;
    }

    private IEnumerator CheckForLongPress()
    {
        while (isPressed)
        {
            OnLongPress();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnLongPress()
    {
        float newY = Mathf.MoveTowards(
        transform.position.x,
        targetX,
        speed * Time.deltaTime
    );
    }
}