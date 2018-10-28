using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasePopup : BaseView {
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private bool _requiresBackdrop;

    public bool RequiresBackdrop {
        get { return _requiresBackdrop; }
    }

    public virtual void Initialize(Action onConfirm, string message = null) {
        SetMessageText(message);
        _confirmButton.onClick.AddListener(() => { onConfirm(); });
    }

    public override void Show(Action afterShow = null) {
        gameObject.SetActive(true);
        base.Show(afterShow);
    }
    
    private void SetMessageText(string message) {
        if (!string.IsNullOrEmpty(message)) {
            _messageText.text = message;
        }
    }
}