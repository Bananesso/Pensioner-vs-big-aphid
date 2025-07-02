using UnityEngine;

public class CheckSintezator : MonoBehaviour
{
    [SerializeField] private float _raycastDistance = 5;
    private Transform _playerCamera;

    private void Start()
    {
        _playerCamera = Camera.main.transform;
    }

    void Update()
    {
        if (Input.GetButton("PickupItem"))
        {
            ShootRay();
        }
    }

    void ShootRay()
    {
        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _raycastDistance))
        {
            Sintezator SintezatorScript = hit.collider.gameObject.GetComponent<Sintezator>();

            if (SintezatorScript != null)
                SintezatorScript.OpenMenu();
        }
    }
}