using UnityEngine;

namespace Unity.FPS.Utility
{
    //생성 후 lifetime이 지나면 자동으로 킬
    public class TimeSelfDestruct : MonoBehaviour
    {
        public float lifeTime = 1f;
        private float spawnTime;        //생성시간

        private void Awake()
        {
            spawnTime = Time.time;
        }

        private void Update()
        {
            //
        }
    }
}