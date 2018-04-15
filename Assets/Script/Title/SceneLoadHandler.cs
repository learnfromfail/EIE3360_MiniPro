using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoadHandler : MonoBehaviour {

    public GameObject loadingScreen;
    public Text loadText;
    public Image loadImage;

    public void onClickLoad(GameObject loadingScreen)
    {
        loadingScreen.transform.localScale = new Vector3(1, 1, 1);
        StartCoroutine(DisplayLoadingScreen("Stage"));
    }

    IEnumerator DisplayLoadingScreen(string sceneName)
    {
        yield return new WaitForSeconds(1);
        AsyncOperation async = Application.LoadLevelAsync(sceneName);
        while (!async.isDone)
        {
            loadText.text = Mathf.Ceil((async.progress * 100)).ToString() + "%";
            loadImage.transform.localScale = new Vector2(async.progress, loadImage.transform.localScale.y);
            yield return null;
        }
    }

}
