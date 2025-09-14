using UnityEngine;

public class TrashBag : MonoBehaviour, IInteractWithObj
{
    [Header("Добыча, получаемая при сборе:")]
    [SerializeField] private int _materialsLoot;
    [SerializeField] private int _listikiLoot;

    [SerializeField] private ListikiPodschet _listikiPodschet;

    public void Interact()
    {
        _listikiPodschet.KolichestvoMaterialov += _materialsLoot;
        _listikiPodschet.KolichestvoListikov += _listikiLoot;
        Destroy(gameObject);
    }
}