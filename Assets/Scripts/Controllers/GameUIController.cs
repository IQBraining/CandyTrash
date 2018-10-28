using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : Singleton<GameUIController> {
    [SerializeField] private SpriteCollection _elementSprites;
    [SerializeField] private BaseScreen _mainMenuScreen;
    [SerializeField] private BaseScreen _gameScreen;
    [SerializeField] private MatrixLayoutController _matrixLayoutController;

    private List<string> _symbols;

    public SpriteCollection ElementSprites {
        get { return _elementSprites; }
    }

    public BaseScreen MainMenuScreen {
        get { return _mainMenuScreen; }
    }

    public BaseScreen GameScreen {
        get { return _gameScreen; }
    }

    public MatrixLayoutController MatrixLayoutController {
        get { return _matrixLayoutController; }
    }

    public List<string> Symbols {
        get { return _symbols; }
    }
    
    private void PrepareSymbolList() {
        if (_symbols == null) {
            _symbols = new List<string>();
        }

        for (int i = 0; i < _elementSprites.SpriteCount; i++) {
            _symbols.Add(_elementSprites.GetSpriteByIndex(i).Id);
        }
    }

    private IEnumerator Start() {
        yield return new WaitForSeconds(1.0f);
        PrepareSymbolList();

        _mainMenuScreen.Initialize();
        _mainMenuScreen.Show();
    }
}