using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour {
    [SerializeField] private float _fakeLoadingDuration;

    IEnumerator Start() {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);
        asyncOperation.allowSceneActivation = false;

        float loadStartTime = Time.realtimeSinceStartup;

        while (asyncOperation.progress < 0.8f) {
            yield return null;
        }

        float loadDoneTime = Time.realtimeSinceStartup;

        float loadDuration = loadDoneTime - loadStartTime;

        if (loadDuration > _fakeLoadingDuration) {
            asyncOperation.allowSceneActivation = true;
        }
        else {
            yield return new WaitForSeconds(_fakeLoadingDuration - loadDuration);
            asyncOperation.allowSceneActivation = true;
        }
    }
}