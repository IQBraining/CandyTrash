using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Element : MonoBehaviour, IEquatable<Element>, IEndDragHandler, IDragHandler {
    [SerializeField] private Image _elementImage;

    private RectTransform _elementRectTransform;

    private Coord2D _elementCoords;

    private string _id;

    public RectTransform ElementRectTransform {
        get { return _elementRectTransform; }
    }

    public Transform ElementImageTransform {
        get { return _elementImage.transform; }
    }

    public Image ElementImage {
        get { return _elementImage; }
        set { _elementImage = value; }
    }

    public string ID {
        get { return _id; }
        set { _id = value; }
    }

    public Coord2D ElementCoords {
        get { return _elementCoords; }
        set { _elementCoords = value; }
    }

    public bool IsHidding { get; set; }

    public void Initialize(Coord2D elementCoords, Vector2 size, Transform parent) {
        _elementCoords = new Coord2D(elementCoords.X, elementCoords.Y);
        SetSizes(size);
        SetParent(parent);
        SetRandomSprite();
    }

    public void SetRandomSprite() {
        int spriteCount = GameUIController.Instance.ElementSprites.SpriteCount;
        UniqueSprite uniqueSprite =
            GameUIController.Instance.ElementSprites.GetSpriteByIndex(Random.Range(0, spriteCount));
        _elementImage.sprite = uniqueSprite.Sprite;
        _id = uniqueSprite.ID;
    }

    public bool Equals(Element other) {
        return _id.Equals(other.ID);
    }

    public void OnEndDrag(PointerEventData eventData) {
        Vector2 pressPosition = eventData.pressPosition;
        Vector2 releasePosition = eventData.position;

        GameUIController.Instance.MatrixLayoutController.OnDragElement(this,
            GameUtils.GetDragDirection(releasePosition - pressPosition));
    }

    public void OnDrag(PointerEventData eventData) {
    }

    private void SetSizes(Vector2 size) {
        _elementRectTransform.sizeDelta = size;

        if (size.y >= size.x) {
            _elementImage.rectTransform.sizeDelta = Vector2.one * size.x;
        }
        else {
            _elementImage.rectTransform.sizeDelta = Vector2.one * size.y;
        }
    }

    private void SetParent(Transform parentToSet) {
        _elementRectTransform.SetParent(parentToSet, false);
    }

    private void Awake() {
        _elementRectTransform = GetComponent<RectTransform>();
    }
}