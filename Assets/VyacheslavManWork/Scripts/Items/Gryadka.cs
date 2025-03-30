using UnityEngine;

public class Gryadka : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _flower;
    public void Interact(GameObject CurrentFlower)
    {
        if (_flower != null)
        {
            Destroy(_flower);
        }
        GameObject flower = Instantiate(CurrentFlower, transform.Find("FlowerPosition"));
        _flower = flower;
    }
}