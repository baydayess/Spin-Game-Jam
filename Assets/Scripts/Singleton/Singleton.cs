using UnityEngine;

public class Singleton<T> : MonoBehaviour
{
    public static T Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            //T t = this as T;
            //Instance = t;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}