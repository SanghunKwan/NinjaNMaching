using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        ResourcePoolManager._instance.AllLoad();
    }
}
