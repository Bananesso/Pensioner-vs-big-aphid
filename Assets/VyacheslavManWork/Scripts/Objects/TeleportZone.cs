using UnityEngine;

public class TeleportZone : MonoBehaviour
{
    [SerializeField] private Transform _teleportPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out var characterController) && other.TryGetComponent<PlayerController>(out var playerController))
        {
            playerController.ClearVelocity();
            characterController.Move(_teleportPoint.position - other.transform.position);
        }
    }
}