using System.Collections;
using UnityEngine;

public class GrowLeaves : MonoBehaviour, IInteractWithObj
{
    [Header("Настройки")]
    [SerializeField] private float _growTime;
    [SerializeField] private int _leavesLoot;
    [SerializeField] private int _materialsLoot;
    private bool _growed;

    private ListikiPodschet _listikiPodschet;
    private AnimationLogic _growAnimation;

    void Start()
    {
        _growAnimation = GetComponent<AnimationLogic>();
        _listikiPodschet = FindObjectOfType<ListikiPodschet>();
        StartCoroutine(Grow());
    }

    public void Interact()
    {
        if (_growed)
        {
            _listikiPodschet.KolichestvoListikov += _leavesLoot;
            _listikiPodschet.KolichestvoMaterialov += _materialsLoot;
            StartCoroutine(Grow());
        }
        else return;
    }

    private IEnumerator Grow()
    {
        _growAnimation.PlayAttackAnimation();
        yield return new WaitForSeconds(_growTime);
        _growed = true;
    }
}