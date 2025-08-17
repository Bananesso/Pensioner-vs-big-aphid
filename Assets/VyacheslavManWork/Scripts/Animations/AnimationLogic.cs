using UnityEngine;

public class AnimationLogic : MonoBehaviour
{
    private Animation _animationComponent;

    [SerializeField] private bool _playOnAwake = true;

    [Header("ќб€зательно должны быть назначены обе анимации!")]
    [SerializeField] private AnimationClip _idleAnimation;
    [SerializeField] private AnimationClip _attackAnimation;

    private void Awake()
    {
        _animationComponent = GetComponent<Animation>();
    }

    private void Start()
    {
        if (_playOnAwake)
            PlayIdleAnimation();
    }

    public void PlayIdleAnimation()
    {
        _animationComponent.Play(_idleAnimation.name);
    }

    public void PlayAttackAnimation()
    {
        _animationComponent.Play(_attackAnimation.name);
        Invoke(nameof(PlayIdleAnimation), _attackAnimation.length);
    }
}