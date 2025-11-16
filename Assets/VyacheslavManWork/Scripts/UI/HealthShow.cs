using UnityEngine;
using UnityEngine.UI;

public class HealthShow : MonoBehaviour
{
    [SerializeField] private GameObject _shows;
    [SerializeField] private Image _hpShow;
    [SerializeField] private Image _elShow;
    [SerializeField] private Health _hpScript;
    [SerializeField] private Flower _objElectrScript;

    private void Start()
    {
        if (_objElectrScript != null)
            _objElectrScript.OnTimeElectrChange += SetElectrTimeInParts;
        if (_hpScript != null)
            _hpScript.OnHit += SetHPInParts;
    }

    private void OnMouseEnter()
    {
		_shows.SetActive(true);
    }

    private void OnMouseExit()
    {
		_shows.SetActive(false);
    }

    private void SetElectrTimeInParts()
    {
		_elShow.fillAmount = _objElectrScript.GetTimeElInParts();
	}

    private void SetHPInParts()
    {
		_hpShow.fillAmount = _hpScript.GetHealthInParts();
    }
}