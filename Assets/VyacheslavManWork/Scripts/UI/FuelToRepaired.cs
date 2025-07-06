using System.Collections;
using UnityEngine;
using TMPro;

public class FuelToRepaired : MonoBehaviour
{
    [SerializeField] private int _needFuel;
    [SerializeField] private int _toSeconds;
    private int _timeLeft;

    [SerializeField] private TMP_Text _needFuelShow;
    [SerializeField] private TMP_Text _toSecondsShow;
    [SerializeField] private TMP_Text _timeLeftShow;

    [SerializeField] private GameObject _electrolyzePlace;

    private ObjectElectrolyzed _electrolyzedScript;
    private ListikiPodschet _listikiPodschet;

    private Coroutine _eatFuel;

    private void Start()
    {
        _listikiPodschet = FindObjectOfType<ListikiPodschet>();

        if (_electrolyzePlace != null)
        {
            _electrolyzedScript = _electrolyzePlace.GetComponent<ObjectElectrolyzed>();
        }
    }

    public void AddTime()
    {
        if (_listikiPodschet.KolichestvoListikov >= _needFuel)
        {
            _listikiPodschet.KolichestvoListikov -= _needFuel;
            _timeLeft += _toSeconds;
            _timeLeftShow.text = _timeLeft.ToString();
            if (_eatFuel == null)
            {
                _eatFuel = StartCoroutine(EatFuel());
            }
        }
    }

    private IEnumerator EatFuel()
    {
        while (_timeLeft != 0)
        {
            yield return new WaitForSeconds(1);
            _electrolyzedScript.Interact(_electrolyzePlace);
            _timeLeft--;
            _timeLeftShow.text = _timeLeft.ToString();
        }
    }

    private void OnValidate()
    {
        _needFuelShow.text = _needFuel.ToString();
        _toSecondsShow.text = _toSeconds.ToString();
    }
}