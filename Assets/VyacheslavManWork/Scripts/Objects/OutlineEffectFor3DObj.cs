using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(OutlineEffect))]
public class OutlineOnHover : MonoBehaviour
{
    private OutlineEffect outlineEffect;

    private void Start()
    {
        outlineEffect = GetComponent<OutlineEffect>();
        if (outlineEffect == null)
        {
            outlineEffect = gameObject.AddComponent<OutlineEffect>();
        }

        outlineEffect.OutlineColor = Color.white;
        outlineEffect.OutlineWidth = 2f;
        outlineEffect.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        outlineEffect.enabled = true;
        DOTween.To(() => outlineEffect.OutlineWidth, x => outlineEffect.OutlineWidth = x, 2f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DOTween.To(() => outlineEffect.OutlineWidth, x => outlineEffect.OutlineWidth = x, 0f, 0.2f)
            .OnComplete(() => outlineEffect.enabled = false);
    }
}