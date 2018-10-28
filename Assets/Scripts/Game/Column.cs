using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour {
    private List<Element> _elements;

    private RectTransform _columnTransform;

    private int _existingElementsCount;

    private int _columnIndex;

    public Element this[int index] {
        get { return _elements.Find(x => x.ElementCoords.Y == index); }
    }

    public float ElementWidth {
        get { return _columnTransform.sizeDelta.x; }
    }

    public float ElementHeight {
        get { return _columnTransform.sizeDelta.y / (2 * Config.RawCount); }
    }

    private Vector2 Size {
        get { return new Vector2(ElementWidth, ElementHeight); }
    }

    public void Initialize(int columnIndex, GameObject elementPrefab, Vector2 size, Transform parent) {
        _columnIndex = columnIndex;
        SetSizes(size);
        SetParent(parent);
        GenerateElements(elementPrefab);
    }

    public void SetSizes(Vector2 size) {
        _columnTransform.sizeDelta = size;
    }

    public void SetParent(Transform parentToSet) {
        _columnTransform.SetParent(parentToSet, false);
    }

    public void OrderElements() {
        for (int i = 0; i < _elements.Count; i++) {
            _elements[i].ElementCoords = new Coord2D(_elements[i].ElementCoords.X,
                _elements.Count - _elements[i].ElementRectTransform.GetSiblingIndex() - 1);
        }
    }

    private void GenerateElements(GameObject elementPrefab) {
        if (_elements == null) {
            _elements = new List<Element>();
        }

        _existingElementsCount = _elements.Count;

        if (_existingElementsCount <= Config.RawCount * 2) {
            for (int i = 0; i < _existingElementsCount; i++) {
                _elements[i].Initialize(new Coord2D(_columnIndex, Config.RawCount * 2 - i - 1), Size, transform);
            }

            for (int j = _existingElementsCount; j < Config.RawCount * 2; j++) {
                GameObject elementGameObject = Instantiate(elementPrefab);
                Element element = elementGameObject.GetComponent<Element>();
                element.Initialize(new Coord2D(_columnIndex, Config.RawCount * 2 - j - 1), Size, transform);
                _elements.Add(element);
            }
        }
        else {
            for (int i = 0; i < Config.RawCount * 2; i++) {
                _elements[i].Initialize(new Coord2D(_columnIndex, Config.RawCount * 2 - i - 1), Size, transform);
            }

            while (_elements.Count != Config.RawCount * 2) {
                Destroy(_elements[_elements.Count - 1].gameObject);
                _elements.RemoveAt(_elements.Count - 1);
            }
        }
    }

    private void Awake() {
        _columnTransform = GetComponent<RectTransform>();
    }
}