using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public IInteractable GetInteractable()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        IInteractable obj = null;
        if (Physics.Raycast(ray, out RaycastHit hitData, 3f))
        {
            obj = hitData.collider.gameObject.GetComponent<IInteractable>();
        }
        return obj;
    }
}