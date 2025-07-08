using UnityEngine;

public class StartGame : MonoBehaviour
{
    private void Awake()
    {
        GameTableManager._instance.AllLoadTable();
        UserInfoManager._instance.InitInfoData();
        ResourcePoolManager._instance.AllLoad();
        SoundManager._instance.LoadAllSound();
    }
    private void Start()
    {
        SceneControlManager._instance.StartOnGame();
    }
}
