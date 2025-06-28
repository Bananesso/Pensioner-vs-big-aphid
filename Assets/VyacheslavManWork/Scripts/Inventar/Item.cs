using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string Name;

    public Sprite ImageSprite;

    private Rigidbody _rb;

    private Collider _collider;

    [SerializeField] private bool _isPicked;

    public bool IsPicked => _isPicked;
    public abstract void Use(GameObject user, IInventory inventory);

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        if (_isPicked)
        {
            PickupItem();
        }
        else
        {
            DropItem();
        }
    }
    public void DropItem()
    {
        _isPicked = false;
        _rb.isKinematic = false;
        _collider.enabled = true;
        transform.parent = null;
    }
    public void PickupItem(Transform parent = null)
    {
        _isPicked = true;
        _rb.isKinematic = true;
        _collider.enabled = false;

        transform.parent = parent == null ? transform.parent : parent;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}