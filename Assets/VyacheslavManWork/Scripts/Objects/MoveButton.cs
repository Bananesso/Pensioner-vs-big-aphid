using UnityEngine;

public class MoveButton : MonoBehaviour, IInteractWithObj
{
    [SerializeField] private GameObject _spaceShip;
    [SerializeField] private GameObject _moveTo;

    private AnimationLogic _pressAnimation;
    private ShipMovementController _shipController;

    void Start()
    {
        _pressAnimation = GetComponent<AnimationLogic>();
        if (_spaceShip != null)
            _shipController = _spaceShip.GetComponent<ShipMovementController>();
    }

    public void Interact()
    {
        if (_shipController != null && _moveTo != null)
            _shipController.MoveToPosition(_moveTo.transform.position);
        _pressAnimation.PlayAttackAnimation();
    }
}