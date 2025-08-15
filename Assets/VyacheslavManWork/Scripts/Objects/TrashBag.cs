using UnityEngine;

public class TrashBag : MonoBehaviour, IInteractWithObj
{
    [Header("Добыча, получаемая при сборе:")]
    [SerializeField] private int _materialsLoot;
    [SerializeField] private int _listikiLoot;

    private ListikiPodschet _listikiPodschet;

    private void Start()
    {
        _listikiPodschet = FindObjectOfType<ListikiPodschet>();
    }
    public void Interact()
    {
        _listikiPodschet.KolichestvoMaterialov += _materialsLoot;
        _listikiPodschet.KolichestvoListikov += _listikiLoot;
        Destroy(gameObject);
    }
}