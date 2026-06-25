using UnityEngine;
using System.Collections;

namespace MyFps
{
    /// <summary>
    /// 첫 번째 플레이씬 오프닝 연출
    /// </summary>
    public class AOpening : MonoBehaviour
    {
        [SerializeField] private SceneFader fader;
        [SerializeField] private GameObject player;
        [SerializeField] private float faderDelay = 1f;
        [SerializeField] private GameObject scenarioText;

        private void Start()
        {
            scenarioText.SetActive(false);

            StartCoroutine(OpeningRoutine());
        }

        IEnumerator OpeningRoutine()
        {
            // 플레이어 비활성화
            player.SetActive(false);

            // 텍스트 출력
            scenarioText.SetActive(true);

            // 페이드인
            fader.FadeStart(faderDelay);

            yield return new WaitForSeconds(faderDelay + 0.2f);

            // 플레이어 활성화
            player.SetActive(true);

            // 3초 대기
            yield return new WaitForSeconds(3f);

            // 텍스트 제거
            scenarioText.SetActive(false);
        }
    }
}