using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private MovementController _movementController;
    [SerializeField] private JumpController _jumpController;
    [SerializeField] private CrouchController _crouchController;
    [SerializeField] private Esc _esc;
    [SerializeField] private CheckInteractable _checkInteractable;

    private CharacterController _characterController;
    private Vector3 _velocity;
    private bool _isGrounded;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _movementController.Initialize(_characterController);
        _crouchController.Initialize(_characterController);
        _checkInteractable.Initialize(Camera.main.transform);

        //_checkInteractable = new CheckInteractable(Camera.main.transform);
        //[System.Serializable] в том коде есть, поэтому new уже был вызван. В целом, можно вызвать ещё раз, но не надо (чтобы не создавать лишний объект)
        //и именно поэтому не нужно вызывать тут Esc скрипт

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        _isGrounded = _characterController.isGrounded;
        _movementController.Move(_isGrounded);
        _jumpController.HandleJump(ref _velocity, _isGrounded);
        _crouchController.HandleCrouch();
        ApplyGravity();
        _esc.EscMenu();
        _checkInteractable.Check();
    }

    private void ApplyGravity()
    {
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        _velocity.y += _jumpController.Gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }

    public void ClearVelocity()
    {
        _velocity = Vector3.zero;
    }
}