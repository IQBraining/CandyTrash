using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : BaseScreen {
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _settingsButton;

    protected override void SetButtonListeners() {
        base.SetButtonListeners();

        _newGameButton.onClick.AddListener(() => {
            Hide(() => {
                GameUIController.Instance.MatrixLayoutController.Initialize();
                GameUIController.Instance.GameScreen.Initialize();
                GameUIController.Instance.GameScreen.Show();
            });
        });
        _settingsButton.onClick.AddListener(() =>
            PopupsManager.Instance.ShowSettingsPopup(PopupsManager.Instance.Hide));
    }

    protected override void RemoveButtonListeners() {
        base.RemoveButtonListeners();

        _newGameButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.RemoveAllListeners();
    }
}