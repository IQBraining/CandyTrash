using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config {
    private static int _rawCount = 5;
    private static int _columnCount = 5;

    public static int RawCount {
        get { return _rawCount; }
        set { _rawCount = value; }
    }

    public static int ColumnCount {
        get { return _columnCount; }
        set { _columnCount = value; }
    }
}