using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;

    public static bool IsInitialized => Instance != null;

    public static T Instance {
        get {
            if (_instance == null)
            {
                GameObject obj= new GameObject();
                obj.name = typeof(T).Name; 
                obj.hideFlags = HideFlags.HideAndDontSave;
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance != null)
            Debug.LogError($"[Singleton] {this.GetType()}: Trying to create a second instance");
        else
            _instance = (T) this;
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }
}

