using UnityEngine;
using TMPro;

public class BuyButton : MonoBehaviour
{
    [SerializeField] private int _priceListiki;
    [SerializeField] private int _priceMaterials;

    [SerializeField] private GameObject _seedPrefab;
    [SerializeField] Transform _transform;

    private ListikiPodschet _listikiPodschet;

    [SerializeField] private TMP_Text _priceListikiShow;
    [SerializeField] private TMP_Text _priceMaterialsShow;

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

    private void OnValidate()
    {
        if (_priceListikiShow != null)
            _priceListikiShow.text = _priceListiki.ToString();

        if (_priceMaterialsShow != null)
            _priceMaterialsShow.text = _priceMaterials.ToString();
    }
}