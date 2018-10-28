using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : BaseScreen {
    [SerializeField] private Button _backToMenuButton;

    protected override void SetButtonListeners() {
        base.SetButtonListeners();
        _backToMenuButton.onClick.AddListener(() => {
            Hide(() => {
                GameUIController.Instance.MainMenuScreen.Initialize();
                GameUIController.Instance.MainMenuScreen.Show();
            });
        });
    }

    protected override void RemoveButtonListeners() {
        base.RemoveButtonListeners();
        _backToMenuButton.onClick.RemoveAllListeners();
    }
}