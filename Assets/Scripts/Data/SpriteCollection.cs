using UnityEngine;

[CreateAssetMenu]
public class SpriteCollection : ScriptableObject {
    [SerializeField] private UniqueSprite[] _uniqueSprites;

    public int SpriteCount {
        get { return _uniqueSprites.Length; }
    }

    public UniqueSprite GetSpriteByIndex(int spriteIndex) {
        return _uniqueSprites[spriteIndex];
    }
}