using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PopupsManager : Singleton<PopupsManager> {
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _backdrop;

    private Queue<BasePopup> _pendingPopupsQueue;
    private BasePopup _currentPopup;

    public void ShowSettingsPopup(Action onConfirm) {
        GameObject popupGameObject = Instantiate(Resources.Load("Popups/SettingsPopup", typeof(GameObject))) as GameObject;
        BasePopup popup = popupGameObject.GetComponent<BasePopup>();
        popup.Initialize(onConfirm);
        ShowPopup(popup);
    }

    public void ShowPopup(BasePopup popup, bool isPending = false) {
        if (!isPending) {
            _pendingPopupsQueue.Enqueue(popup);

            if (_pendingPopupsQueue.Count > 1 && !isPending) {
                popup.transform.SetParent(_container, false);
                popup.gameObject.SetActive(false);
                return;
            }
        }

        SetBackdropEnabled(popup.RequiresBackdrop);
        popup.transform.SetParent(_container, false);
        popup.Show();
    }

    public void Hide() {
        if (_pendingPopupsQueue.Count < 1) {
            return;
        }

        BasePopup popupToHide = _pendingPopupsQueue.Peek();
        BasePopup nextPopup = null;

        popupToHide.Hide(() => {
            _pendingPopupsQueue.Dequeue();

            if (_pendingPopupsQueue.Count > 0) {
                nextPopup = _pendingPopupsQueue.Peek();
            }
            else {
                Resources.UnloadUnusedAssets();
            }

            if (nextPopup) {
                ShowPopup(nextPopup, true);
            }
            else {
                _backdrop.SetActive(false);
            }
        });
    }

    private void SetBackdropEnabled(bool enabled) {
        _backdrop.SetActive(enabled);
    }

    private void Start() {
        _pendingPopupsQueue = new Queue<BasePopup>();
    }
}