using UnityEngine;

public class BuyButton : MonoBehaviour
{
    [SerializeField] private GameObject _seedPrefab;
    [SerializeField] private int _priceListiki;
    [SerializeField] private int _priceMaterials;
    [SerializeField] Transform _transform;
    private ListikiPodschet _listikiPodschet;

    private void Start()
    {
        _listikiPodschet = FindObjectOfType<ListikiPodschet>();
    }
    public void BuySeeds()
    {
        if (_listikiPodschet.KolichestvoListikov >= _priceListiki && _listikiPodschet.KolichestvoMaterialov >= _priceMaterials)
        {
            Instantiate(_seedPrefab, _transform.position, Quaternion.identity);
            _listikiPodschet.KolichestvoListikov -= _priceListiki;
            _listikiPodschet.KolichestvoMaterialov -= _priceMaterials;
        }
    }
}