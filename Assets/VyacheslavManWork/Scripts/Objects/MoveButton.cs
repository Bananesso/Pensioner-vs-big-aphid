using UnityEngine;

public class MoveButton : MonoBehaviour, IInteractWithObj
{
    [SerializeField] private GameObject _spaceShip;
    [SerializeField] private GameObject _moveTo;

    private ShipMovementController _shipController;

    void Start()
    {
        if (_spaceShip != null)
            _shipController = _spaceShip.GetComponent<ShipMovementController>();
    }

    public void Interact()
    {
        if (_shipController != null && _moveTo != null)
            _shipController.MoveToPosition(_moveTo.transform.position);
    }
}