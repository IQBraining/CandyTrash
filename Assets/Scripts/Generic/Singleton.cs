using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
    [SerializeField] private bool _dontDestroyOnLoad;

    private static T _instance;

    public static T Instance {
        get { return _instance; }
    }

    private void Awake() {
        if (_instance == null) {
            _instance = this as T;
        } else if (_instance != this as T) {
            Destroy(_instance.gameObject);
        }

        if (_dontDestroyOnLoad) {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy() {
        _instance = null;
    }
}