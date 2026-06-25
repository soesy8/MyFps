using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// Singleton<T> 상송받는 싱글톤 클래스
    /// 씬 전환 시 사라지지 않고 유지되어야 하는 싱글톤 클래스의 부모 클래스로 설계
    /// </summary>
    public class PersistentSingleton<T> : Singleton<T> where T : Singleton<T>
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this.gameObject);
        }
    }
}