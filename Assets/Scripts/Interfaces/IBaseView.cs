using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseView {
    bool IsAnimating { get; set; }

    void Show(Action afterShow = null);

    void Hide(Action afterHide = null);
}