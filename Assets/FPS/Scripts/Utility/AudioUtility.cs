using UnityEngine;

namespace Unity.FPS.Utility
{
    //오디오 플레이 관련 함수 구현
    public class AudioUtility
    {
        //오디오 소스를 원하는 위치에서 플레이 시켜주는 함수
        //클립의 플레이 타임이 끝나면 자동 킬
        public static void CreateSFX(AudioClip clip, Vector3 position,
            float spartialBlend, float rolloffDistanceMin = 1f)
        {
            //빈 오브젝트 생성
            GameObject impactSfxInstance = new GameObject();
            impactSfxInstance.transform.position = position;

            //오디오 소스 추가하고 설정하고 플레이하기
            AudioSource audioSource = impactSfxInstance.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.spatialBlend = spartialBlend;
            audioSource.minDistance = rolloffDistanceMin;
            audioSource.Play();

            //자동 킬
            TimeSelfDestruct timeSelfDestruct =
                impactSfxInstance.AddComponent<TimeSelfDestruct>();
            timeSelfDestruct.lifeTime = clip.length;
        }
    }
}