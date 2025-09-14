using UnityEngine;

public class MoveButton : MonoBehaviour, IInteractWithObj
{
    [SerializeField] private ShipMovementController _spaceShip;
    [SerializeField] private GameObject _moveTo;

    [SerializeField] private AnimationLogic _pressAnimation;

    public void Interact()
    {
        if (_spaceShip != null && _moveTo != null)
            _spaceShip.MoveToPosition(_moveTo.transform.position);
        _pressAnimation.PlayAttackAnimation();
    }
}