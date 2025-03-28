using UnityEngine;

public class Gryadka : MonoBehaviour, IInteractable
{
    private Flower _flower;
    public void Interact(GameObject CurrentFlower)
    {
        _flower = CurrentFlower.GetComponent<Flower>();
        Instantiate(_flower);
    }
}
