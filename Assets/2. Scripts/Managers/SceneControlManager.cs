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
        // �ε�â ���� �� �ʱ�ȭ or Ȱ��ȭ �� �ʱ�ȭ...
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
        // �ε� 65% ����...

        if (_nowScene == SceneName.WellOfGodScene)
        {
            WellOfGodManager._instance.InitManager(1);
        }
        if (_nowScene == SceneName.IngameScene)
        {
            IngameManager._instance.InitLoadGame(1);
        }
        //�ε� 100% ����.
        _loadingWnd.SetLoadingRate(1);
        yield return new WaitForSeconds(1.5f);
        //�ε�â �ݱ�.
        _loadingWnd.CloseWnd();
        // Ingame Ready�� ���ش�.


        yield return null;
    }
}
