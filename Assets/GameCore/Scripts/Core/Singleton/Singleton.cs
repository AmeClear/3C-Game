/// <summary>
/// 单例模式
/// 惰性生成方式，第一次创建生成
/// </summary>
public class Singleton<T> where T : new()
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
                _instance = new T();
            return _instance;
        }
    }

}
