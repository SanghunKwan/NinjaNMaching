using UnityEngine;
using UnityEngine.SceneManagement;
using DefineEnum;
using System.Collections;

public class SceneControlManager : TSingleton<SceneControlManager>
{
    SceneName _nowScene;

    GameObject _prefabWnd;
    UILoadingWnd _loadingWnd;

    public SceneName _currentScene => _nowScene;

    public void StartOnGame()
    {
        _prefabWnd = Resources.Load<GameObject>("Prefabs/UIs/LoadingWindow");

        _nowScene = SceneName.WellOfGodScene;
        //SceneManager.LoadSceneAsync(_nowScene);
        StartCoroutine(LoadingScene(_nowScene));
    }
    public void StartWellOfGodMenu()
    {
        StartCoroutine(LoadingScene(SceneName.WellOfGodScene));
    }
    public void StartGameStage()
    {
        StartCoroutine(LoadingScene(SceneName.IngameScene));
    }

    IEnumerator LoadingScene(SceneName scene)
    {
        AsyncOperation aOper;
        _nowScene = scene;
        // 로딩창 생성 및 초기화 or 활성화 및 초기화...
        if (_loadingWnd == null)
        {
            GameObject go = Instantiate(_prefabWnd, transform);
            _loadingWnd = go.GetComponent<UILoadingWnd>();
        }
        _loadingWnd.OpenWnd();
        aOper = SceneManager.LoadSceneAsync(_nowScene.ToString());

        while (!aOper.isDone)
        {
            Debug.Log(aOper.progress);
            _loadingWnd.SetLoadingRate(0.65f * aOper.progress);
            yield return null;
        }

        Debug.Log(aOper.progress);
        // 로딩 65% 지점...

        if (_nowScene == SceneName.WellOfGodScene)
        {
            WellOfGodManager._instance.InitManager(1);
        }
        if (_nowScene == SceneName.IngameScene)
        {
            IngameManager._instance.InitLoadGame(1);
        }
        //로딩 100% 지점.
        _loadingWnd.SetLoadingRate(1);
        yield return new WaitForSeconds(1.5f);
        //로딩창 닫기.
        _loadingWnd.CloseWnd();
        // Ingame Ready를 해준다.


        yield return null;
    }
}
