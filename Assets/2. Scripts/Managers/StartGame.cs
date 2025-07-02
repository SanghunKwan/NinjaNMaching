using UnityEngine;

public class StartGame : MonoBehaviour
{
    private void Awake()
    {
        GameTableManager._instance.AllLoadTable();
        ResourcePoolManager._instance.AllLoad();
    }
    private void Start()
    {
        SceneControlManager._instance.StartOnGame();
    }
}
