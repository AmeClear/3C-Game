using UnityEngine;

/// <summary>
/// Mono版本的单例模式，创建一个物体并保证该物体不被卸载，并将脚本类挂在在物体上。
/// </summary>
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = GameObject.Find(typeof(T).Name);
                if (obj == null)
                    obj = new GameObject(typeof(T).Name);
                _instance = obj.GetComponent<T>();
                if (_instance == null)
                    _instance = obj.AddComponent<T>();
            }
            return _instance;
        }
    }
    /// <summary>
    /// 需要覆盖
    /// </summary>
    protected virtual void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (_instance == null)
        {
            _instance = this as T;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
