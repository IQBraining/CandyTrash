using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BaseView : MonoBehaviour, IBaseView {
    [Header("Show/Hide Information")] [SerializeField]
    private AnimationInfo _showInfo;

    [SerializeField] private AnimationInfo _hideInfo;
    [SerializeField] private bool _destroyAfterHide = true;

    private Animator _animator;

    public bool IsAnimating { get; set; }

    public virtual void Show(Action afterShow = null) {
        SetTrigger(_showInfo, afterShow);
    }

    public virtual void Hide(Action afterHide = null) {
        afterHide += () => {
            if (_destroyAfterHide) {
                Destroy(gameObject);
            }
        };

        SetTrigger(_hideInfo, afterHide);
    }

    public void SetTrigger(AnimationInfo info, Action handler = null) {
        _animator.enabled = true;
        IsAnimating = true;
        _animator.SetTrigger(info.Trigger);
        StartCoroutine(waitToAnimationEnd(info.Duration, handler));
    }

    private IEnumerator waitToAnimationEnd(float animationDuration, Action onEnd = null) {
        yield return new WaitForSeconds(animationDuration);

        _animator.enabled = false;
        IsAnimating = false;

        if (onEnd != null) {
            onEnd();
        }
    }

    private void Awake() {
        _animator = GetComponent<Animator>();
    }
}