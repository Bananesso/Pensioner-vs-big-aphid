using System.Collections;
using UnityEngine;

public class GrowLeaves : MonoBehaviour, IInteractWithObj
{
    [Header("Настройки")]
    [SerializeField] private float _growTime;
    [SerializeField] private int _leavesLoot;
    [SerializeField] private GameObject _leavesVisual;

    private ListikiPodschet _listikiPodschet;

    void Start()
    {
        _listikiPodschet = FindObjectOfType<ListikiPodschet>();
        StartCoroutine(Grow());
    }

    public void Interact()
    {
        if (_leavesVisual.activeSelf)
        {
            _leavesVisual.SetActive(false);
            _listikiPodschet.KolichestvoListikov += _leavesLoot;
            StartCoroutine(Grow());
        }
        else return;
    }

    private IEnumerator Grow()
    {
        yield return new WaitForSeconds(_growTime);
        _leavesVisual.SetActive(true);
    }
}