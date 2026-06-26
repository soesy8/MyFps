using UnityEngine;
using System.Collections;
using TMPro;

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
        //[SerializeField] private GameObject scenarioText;
        [SerializeField] private TextMeshProUGUI scenarioText;

        private void Start()
        {
            //scenarioText.SetActive(false);
            scenarioText.gameObject.SetActive(false);

            StartCoroutine(OpeningRoutine());
        }

        IEnumerator OpeningRoutine()
        {
            // 플레이어 비활성화
            player.SetActive(false);

            // 텍스트 출력
            scenarioText.gameObject.SetActive(true);
            scenarioText.text = "I need get out of here";

            // 페이드인
            fader.FadeStart(faderDelay);

            yield return new WaitForSeconds(faderDelay + 0.2f);

            // 플레이어 활성화
            player.SetActive(true);

            // 3초 대기
            yield return new WaitForSeconds(3f);

            // 텍스트 제거
            scenarioText.text = "";
            scenarioText.gameObject.SetActive(false);
        }
    }
}