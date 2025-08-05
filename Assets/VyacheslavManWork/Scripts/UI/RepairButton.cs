using UnityEngine;
using TMPro;

public class RepairButton : MonoBehaviour
{
    [SerializeField] private GameObject _needRepair;
    [SerializeField] private GameObject _toRepaired;

    [SerializeField] private int _priceMaterials;
    [SerializeField] private int _priceListiki;

    [SerializeField] private TMP_Text _priceListikiShow;
    [SerializeField] private TMP_Text _priceMaterialsShow;

    private ListikiPodschet _listikiPodschet;

    private void Start()
    {
        _listikiPodschet = FindObjectOfType<ListikiPodschet>();
    }

    public void Repair()
    {
        if (_listikiPodschet.KolichestvoListikov >= _priceListiki && _listikiPodschet.KolichestvoMaterialov >= _priceMaterials)
        {
            _needRepair.SetActive(false);
            _toRepaired.SetActive(true);

            _listikiPodschet.KolichestvoListikov -= _priceListiki;
            _listikiPodschet.KolichestvoMaterialov -= _priceMaterials;
        }
    }

    private void OnValidate()
    {
        _priceListikiShow.text = _priceListiki.ToString();
        _priceMaterialsShow.text = _priceMaterials.ToString();
    }
}