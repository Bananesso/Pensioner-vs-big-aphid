using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _interactionScript;

    public void Interact()
    {
        if (_interactionScript is IInteractWithObj interactable)
        {
            interactable.Interact();
        }
    }
}