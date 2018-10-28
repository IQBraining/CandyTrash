using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UniqueSprite {
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _id;

    public Sprite Sprite {
        get { return _sprite; }
    }

    public string ID {
        get { return _id; }
    }
}