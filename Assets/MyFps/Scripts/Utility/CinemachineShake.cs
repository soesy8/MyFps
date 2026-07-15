using System.Collections;
using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 화면 흔들림 효과 구현 / 싱글톤 클래스
    /// 흔들림 계수 : 흔들림 속도, 크기, 시간
    /// </summary>
    public class CinemachineShake : Singleton<CinemachineShake>
    {
        #region Variables
        //참조
        private CinemachineBasicMultiChannelPerlin multiChannelPerlin;

        [SerializeField] private float amplitude = 0f;
        [SerializeField] private float frequency = 0f;
        [SerializeField] private float shakeTimer = 1f;
        #endregion

        #region Unity Event Method
        protected override void Awake()
        {
            base.Awake();

            CinemachineCamera cam =
                FindFirstObjectByType<CinemachineCamera>();

            multiChannelPerlin = cam.GetComponent<CinemachineBasicMultiChannelPerlin>();
        }
        #endregion

        #region Custom Method
        public void Shake(float amplitude, float frequency, float duration)
        {
            StopAllCoroutines();
            StartCoroutine(ShakeCoroutine(amplitude, frequency, duration));
        }

        private IEnumerator ShakeCoroutine( float amplitude, float frequency, float duration)
        {
            multiChannelPerlin.AmplitudeGain = amplitude;
            multiChannelPerlin.FrequencyGain = frequency;

            yield return new WaitForSeconds(duration);

            multiChannelPerlin.AmplitudeGain = 0f;
            multiChannelPerlin.FrequencyGain = 0f;
        }
        #endregion
    }
}