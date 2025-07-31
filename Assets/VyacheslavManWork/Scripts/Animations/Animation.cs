using UnityEngine;

public class AnimationLogic : MonoBehaviour
{
    private Animation animationComponent;

    [Header("ќб€зательно должны быть назначены обе анимации!")]
    [SerializeField] private AnimationClip _idleAnimation;
    [SerializeField] private AnimationClip _attackAnimation;

    private void Start()
    {
        animationComponent = GetComponent<Animation>();
        PlayIdleAnimation();
    }

    public void PlayIdleAnimation()
    {
            animationComponent.Play(_idleAnimation.name);
    }

    public void PlayAttackAnimation()
    {
        animationComponent.Play(_attackAnimation.name);
        Invoke(nameof(PlayIdleAnimation), _attackAnimation.length);
    }
}