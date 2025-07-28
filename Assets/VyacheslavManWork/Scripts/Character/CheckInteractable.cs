using UnityEngine;

[System.Serializable]
public class CheckInteractable
{
    [SerializeField] private float _raycastDistance = 5;
    private Transform _playerCamera;

    private void Start()
    {
        _playerCamera = Camera.main.transform;
    }

    public void Check()
    {
        if (Input.GetButtonDown("PickupItem"))
        {
            ShootRay();
        }
    }

    private void ShootRay()
    {
        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _raycastDistance))
        {
            Interaction interactScript = hit.collider.gameObject.GetComponent<Interaction>();

            if (interactScript != null)
                interactScript.Interact();
        }
    }
}