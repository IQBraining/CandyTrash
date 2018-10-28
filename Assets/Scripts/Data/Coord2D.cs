using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Coord2D {
    private int _x;
    private int _y;

    public int X {
        get { return _x; }
        set { _x = value; }
    }

    public int Y {
        get { return _y; }
        set { _y = value; }
    }

    public Coord2D Up {
        get { return new Coord2D(_x, _y + 1); }
    }

    public Coord2D Down {
        get { return new Coord2D(_x, _y - 1); }
    }

    public Coord2D Left {
        get { return new Coord2D(_x - 1, _y); }
    }

    public Coord2D Right {
        get { return new Coord2D(_x + 1, _y); }
    }

    public Coord2D(int x, int y) {
        _x = x;
        _y = y;
    }
}