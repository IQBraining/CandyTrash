using System.Collections.Generic;
using UnityEngine;

public class ElementMatrix : MonoBehaviour {
    private List<Column> _columns;

    public Column this[int x] {
        get { return _columns[x]; }
    }

    public int ColumnsCount {
        get { return _columns.Count; }
    }

    public void Initialize() {
        if (_columns == null) {
            _columns = new List<Column>();
        }
    }

    public void AddColumn(Column column) {
        _columns.Add(column);
    }

    public void RemoveLastColumn() {
        Destroy(_columns[_columns.Count - 1].gameObject);
        _columns.RemoveAt(_columns.Count - 1);
    }

    public void ReorderColumnElements() {
        for (int i = 0; i < _columns.Count; i++) {
            _columns[i].OrderElements();
        }
    }

    public bool TryDetectMatch(out List<Element> matchedElements) {
        List<Element> horizontalDetectedElements = DetectHorizontalMatch();
        List<Element> verticalDetectedElements = DetectVerticalMatch();
        matchedElements = new List<Element>();

        matchedElements.AddRange(horizontalDetectedElements);
        matchedElements.AddRange(verticalDetectedElements);

        if (matchedElements.Count > 0) {
            return true;
        }

        return false;
    }

    private List<Element> DetectVerticalMatch() {
        List<Element> _matchedElements = new List<Element>();

        for (int i = 0; i < Config.ColumnCount; i++) {
            List<Element> identicElements = new List<Element>();

            for (int j = 0; j < Config.RawCount; j++) {
                if (identicElements.Count == 0) {
                    identicElements.Add(this[i][j]);
                }
                else {
                    if (identicElements.Contains(this[i][j])) {
                        identicElements.Add(this[i][j]);
                    }
                    else {
                        if (identicElements.Count > 2) {
                            _matchedElements.AddRange(identicElements);
                        }

                        identicElements.Clear();
                        identicElements.Add(this[i][j]);
                    }
                }
            }

            if (identicElements.Count > 2) {
                _matchedElements.AddRange(identicElements);
            }
        }

        return _matchedElements;
    }

    private List<Element> DetectHorizontalMatch() {
        List<Element> _matchedElements = new List<Element>();

        for (int i = 0; i < Config.RawCount; i++) {
            List<Element> identicElements = new List<Element>();

            for (int j = 0; j < Config.ColumnCount; j++) {
                if (identicElements.Count == 0) {
                    identicElements.Add(this[j][i]);
                }
                else {
                    if (identicElements.Contains(this[j][i])) {
                        identicElements.Add(this[j][i]);
                    }
                    else {
                        if (identicElements.Count > 2) {
                            _matchedElements.AddRange(identicElements);
                        }

                        identicElements.Clear();
                        identicElements.Add(this[j][i]);
                    }
                }
            }

            if (identicElements.Count > 2) {
                _matchedElements.AddRange(identicElements);
            }
        }

        return _matchedElements;
    }
}