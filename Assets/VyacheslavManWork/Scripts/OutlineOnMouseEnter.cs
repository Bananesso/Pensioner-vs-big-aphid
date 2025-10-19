using UnityEngine;

public class OutlineOnMouseEnter : MonoBehaviour
{
    [SerializeField] private OutlineMan _outline;

    private void OnMouseEnter()
    {
        _outline.EnableOutline();
    }

    private void OnMouseExit()
    {
        _outline.DisableOutline();
    }
}