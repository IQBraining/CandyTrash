using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using DragDirection = Enums.DragDirection;

public class MatrixLayoutController : MonoBehaviour {
    [SerializeField] private RectTransform _playableArea;
    [SerializeField] private GameObject _columnPrefab;
    [SerializeField] private GameObject _elementPrefab;
    [SerializeField] private ElementMatrix _elementMatrix;

    private int _existingElementsCount;

    private bool _isChecking;

    private float ColumnWidth {
        get { return _playableArea.sizeDelta.x / Config.ColumnCount; }
    }

    private float ColumnHeight {
        get { return _playableArea.sizeDelta.y * 2; }
    }

    private Vector2 Size {
        get { return new Vector2(ColumnWidth, ColumnHeight); }
    }

    public void Initialize() {
        _elementMatrix.Initialize();
        GenerateColumns();

        List<Element> allDetectedElements;

        if (_elementMatrix.TryDetectMatch(out allDetectedElements)) {
            _isChecking = true;
            StartCoroutine(HideAndMoveDetectedElements(allDetectedElements, () => { _isChecking = false; }));
        }
    }

    public void OnDragElement(Element draggedElement, DragDirection dragDirection) {
        if (_isChecking) {
            return;
        }

        _isChecking = true;

        Element nextElement = null;

        switch (dragDirection) {
            case DragDirection.Left:
                nextElement = GetVisibleElementAtPosition(draggedElement.ElementCoords.Left);
                break;
            case DragDirection.Right:
                nextElement = GetVisibleElementAtPosition(draggedElement.ElementCoords.Right);
                break;
            case DragDirection.Up:
                nextElement = GetVisibleElementAtPosition(draggedElement.ElementCoords.Up);
                break;
            case DragDirection.Down:
                nextElement = GetVisibleElementAtPosition(draggedElement.ElementCoords.Down);
                break;
        }

        if (nextElement != null) {
            StartCoroutine(DoElementsPositionSwitch(draggedElement, nextElement,
                () => {
                    List<Element> allDetectedElements;
                    if (_elementMatrix.TryDetectMatch(out allDetectedElements)) {
                        StartCoroutine(HideAndMoveDetectedElements(allDetectedElements,
                            () => { _isChecking = false; }));
                    }
                    else {
                        StartCoroutine(DoElementsPositionSwitch(draggedElement, nextElement,
                            () => { _isChecking = false; }));
                    }
                }));
        }
        else {
            _isChecking = false;
        }
    }

    private void DoElementsContentSwitch(Element from, Element to) {
        string fromTmpID = from.ID;
        Image fromTmpImage = from.ElementImage;

        from.ID = to.ID;
        from.ElementImage = to.ElementImage;

        to.ID = fromTmpID;
        to.ElementImage = fromTmpImage;
    }

    private IEnumerator HideAndMoveDetectedElements(List<Element> allDetectedElements, Action handler) {
        foreach (Element element in allDetectedElements) {
            HideAndMoveElement(element);
        }

        yield return new WaitUntil(() => allDetectedElements.All(x => !x.IsHidding));

        _elementMatrix.ReorderColumnElements();

        if (_elementMatrix.TryDetectMatch(out allDetectedElements)) {
            yield return StartCoroutine(HideAndMoveDetectedElements(allDetectedElements, handler));
        }
        else {
            if (handler != null) {
                handler();
            }
        }
    }

    private void HideAndMoveElement(Element element) {
        element.IsHidding = true;
        Vector2 startSizeDelta = element.ElementRectTransform.sizeDelta;
        element.ElementImage.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => {
            element.ElementRectTransform.DOSizeDelta(new Vector2(element.ElementRectTransform.sizeDelta.x, 0), 0.3f)
                .OnComplete(
                    () => {
                        element.ElementRectTransform.SetAsFirstSibling();
                        element.ElementRectTransform.sizeDelta = startSizeDelta;
                        element.ElementImageTransform.localScale = Vector3.one;
                        element.SetRandomSprite();
                        element.IsHidding = false;
                    });
        });
    }

    private IEnumerator DoElementsPositionSwitch(Element from, Element to, Action handler) {
        Transform fromTransform = from.ElementImageTransform;
        Transform toTransform = to.ElementImageTransform;
        Transform fromTmpParent = fromTransform.parent;
        Transform toTmpParent = toTransform.parent;

        fromTransform.DOScale(Vector3.one * 0.5f, 0.2f).OnComplete(() => {
            fromTransform.DOMove(toTransform.position, 0.3f).OnComplete(() => {
                fromTransform.DOScale(Vector3.one, 0.2f).OnComplete(() => {
                    fromTransform.SetParent(toTmpParent, true);
                });
            });
        });

        toTransform.DOScale(Vector3.one * 0.5f, 0.2f).OnComplete(() => {
            toTransform.DOMove(fromTransform.position, 0.3f).OnComplete(() => {
                toTransform.DOScale(Vector3.one, 0.2f)
                    .OnComplete(() => { toTransform.SetParent(fromTmpParent, true); });
            });
        });

        yield return new WaitForSeconds(0.8f);

        DoElementsContentSwitch(from, to);

        if (handler != null) {
            handler();
        }
    }

    private Element GetVisibleElementAtPosition(Coord2D elementCoords) {
        if (elementCoords.X < 0 || elementCoords.X >= Config.ColumnCount) {
            return null;
        }

        if (elementCoords.Y < 0 || elementCoords.Y >= Config.RawCount) {
            return null;
        }

        return _elementMatrix[elementCoords.X][elementCoords.Y];
    }

    private void GenerateColumns() {
        _existingElementsCount = _elementMatrix.ColumnsCount;

        if (_existingElementsCount <= Config.ColumnCount) {
            for (int i = 0; i < _existingElementsCount; i++) {
                _elementMatrix[i].Initialize(i, _elementPrefab, Size, transform);
            }

            for (int j = _existingElementsCount; j < Config.ColumnCount; j++) {
                GameObject columnGameObject = Instantiate(_columnPrefab);
                Column column = columnGameObject.GetComponent<Column>();
                column.Initialize(j, _elementPrefab, Size, transform);
                _elementMatrix.AddColumn(column);
            }
        }
        else {
            for (int i = 0; i < Config.ColumnCount; i++) {
                _elementMatrix[i].Initialize(i, _elementPrefab, Size, transform);
            }

            while (_elementMatrix.ColumnsCount != Config.ColumnCount) {
                _elementMatrix.RemoveLastColumn();
            }
        }
    }
}