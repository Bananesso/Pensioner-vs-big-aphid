using UnityEngine;

public class HealthShow : MonoBehaviour
{
    [SerializeField] private GameObject _hpShow;
    [SerializeField] private GameObject _electrolyzeShow;
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
        _hpShow.SetActive(true);
        _electrolyzeShow.SetActive(true);
    }

    private void OnMouseExit()
    {
        _hpShow.gameObject.SetActive(false);
        _electrolyzeShow.gameObject.SetActive(false);
    }

    private void SetElectrTimeInParts()
    {
        float elScale = _objElectrScript.GetTimeElInParts();
        _objElectrScript.transform.localScale = new Vector3(elScale, elScale, elScale);
    }

    private void SetHPInParts()
    {
        float hpScale = _hpScript.GetHealthInParts();
        _hpShow.transform.localScale = new Vector3(hpScale, hpScale, hpScale);
    }
}