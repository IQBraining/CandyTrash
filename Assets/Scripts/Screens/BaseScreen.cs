using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class BaseScreen : MonoBehaviour, IBaseView {
    protected float _showDuration = 0.3f;
    protected float _hideDuration = 0.3f;

    protected CanvasGroup _canvasGroup;

    public bool IsAnimating { get; set; }

    public bool IsVisible {
        get { return !IsAnimating && _canvasGroup.alpha == 1; }
    }

    public virtual void Initialize() {
        SetButtonListeners();
    }

    public virtual void Show(Action afterShow = null) {
        if (_canvasGroup.alpha == 1) {
            return;
        }
        
        SetInteractable(true);

        _canvasGroup.DOFade(1, _showDuration).OnComplete(() => {
            if (afterShow != null) {
                afterShow();
            }
        });
    }

    public virtual void Hide(Action afterHide = null) {
        _canvasGroup.DOFade(0, _hideDuration).OnComplete(() => {
            SetInteractable(false);
            RemoveButtonListeners();
            if (afterHide != null) {
                afterHide();
            }
        });
    }

    protected virtual void SetButtonListeners() {
    }

    protected virtual void RemoveButtonListeners() {
    }

    private void SetInteractable(bool interactable) {
        _canvasGroup.interactable = interactable;
        _canvasGroup.blocksRaycasts = interactable;
    }

    private void Start() {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
}