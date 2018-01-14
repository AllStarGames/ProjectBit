using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    AsyncOperation async;
    float loadingProgress;

    public float LoadingProgress()
    {
        return loadingProgress;
    }
    public void Play()
    {
        StartCoroutine(Load(1));
    }
	public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    IEnumerator Load(int index)
    {
        async = SceneManager.LoadSceneAsync(index);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            async.allowSceneActivation = true;
            yield return null;
        }
    }
    IEnumerator Load(string sceneName)
    {
        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            async.allowSceneActivation = true;
            yield return null;
        }
    }
    void Start()
    {
        loadingProgress = 0.0f;
    }
}
