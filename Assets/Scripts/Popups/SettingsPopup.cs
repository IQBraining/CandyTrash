using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsPopup : BasePopup {
    [SerializeField] private TMP_InputField _rawInputField;
    [SerializeField] private TMP_InputField _columnInputField;

    private int minRawCount = 5;
    private int maxRawCount = 20;
    private int minColumnCount = 5;
    private int maxColumnCount = 20;

    public override void Initialize(Action onConfirm, string message = null) {
        _rawInputField.text = Config.RawCount.ToString();
        _columnInputField.text = Config.ColumnCount.ToString();

        SetListeners();

        onConfirm += () => {
            Config.RawCount = int.Parse(_rawInputField.text);
            Config.ColumnCount = int.Parse(_columnInputField.text);
        };

        base.Initialize(onConfirm, message);
    }

    private void SetListeners() {
        _rawInputField.onEndEdit.AddListener(ValidateRawCount);
        _columnInputField.onEndEdit.AddListener(ValidateColumnCount);
    }

    private void ValidateRawCount(string inputCount) {
        int parsedValue = string.IsNullOrEmpty(inputCount) ? 0 : int.Parse(inputCount);

        if (parsedValue >= minRawCount && parsedValue <= maxRawCount) {
            return;
        }

        parsedValue = Mathf.Clamp(parsedValue, minRawCount, maxRawCount);
        _rawInputField.text = parsedValue.ToString();
    }

    private void ValidateColumnCount(string inputCount) {
        int parsedValue = string.IsNullOrEmpty(inputCount) ? 0 : int.Parse(inputCount);

        if (parsedValue >= minColumnCount && parsedValue <= maxColumnCount) {
            return;
        }

        parsedValue = Mathf.Clamp(parsedValue, minColumnCount, maxColumnCount);
        _columnInputField.text = parsedValue.ToString();
    }
}