using UnityEngine;

public class TeleportZone : MonoBehaviour
{
    [SerializeField] private GameObject _teleportPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = _teleportPoint.transform.position;
        }
    }
}