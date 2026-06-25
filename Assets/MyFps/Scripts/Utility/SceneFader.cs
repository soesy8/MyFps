using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace MyFps
{
    /// <summary>
    /// 씬 페이더 기능 구현 클래스
    /// 씬 시작할때 페이드인 효과, 씬 종료시 페이드 아웃 효과 - 페이드 아웃하면 다음 씬으로 이동
    /// </summary>
    public class SceneFader : MonoBehaviour
    {
        #region Variables
        //페이더 이미지
        [SerializeField]
        private Image img;                  //페이드 이미지
        [SerializeField] private float fadeDuration = 1.0f;
        public AnimationCurve curve;        //페이드 효과를 위한 커브 값
        public bool isFadeIn = false;       //시작 시 페이드 효과 자동적용 여부
        public float delayTime = 0f;        //딜레이 시간
        #endregion

        #region Unity Event Method
        private void Start()
        {
            if (isFadeIn)
            {
                //시작할 떄 페이드 인 효과
                FadeStart(delayTime);
            }
        }
        #endregion

        #region Custom Method
        public void FadeStart(float delay)
        {
            StartCoroutine(FadeIn(delay));
        }
        
        //페이드 아웃 후 씬 이름으로  다음 씬으로 이동
        public void FadeTo(string sceneName)
        {
            StartCoroutine(FadeOut(sceneName));
        }

        //페이드 아우 하고 씬 빌드번호로 다음 씬 이동
        public void FadeTo(int buildIndex)
        {
            StartCoroutine(FadeOut(buildIndex));
        }
        #endregion

        #region Coroutine
        //페이드 인 효과 : 1초 동안 a: 1 -> 0
        IEnumerator FadeIn(float delay)
        {
            img.color = new Color(0f, 0f, 0f, 1f);

            if (delay > 0f)
            {
                yield return new WaitForSeconds(delay);
            }

            while (fadeDuration > 0f)
            {
                fadeDuration -= Time.deltaTime;
                float a = curve.Evaluate(fadeDuration);
                img.color = new Color(0f, 0f, 0f, a);
                yield return 0;
            }

        }

        IEnumerator FadeOut(string sceneName = "")
        {
            fadeDuration = 0f;

            while (fadeDuration < 1f)
            {
                fadeDuration += Time.deltaTime;
                float a = curve.Evaluate(fadeDuration);
                img.color = new Color(0f, 0f, 0f, a);
                yield return 0;
            }
            //페이드 아웃 완료 후 다음 씬으로 이동
            if (sceneName != null && sceneName != "")
            {
                SceneManager.LoadScene(sceneName);
            }
        }

        IEnumerator FadeOut(int buildIndex = -1)
        {
            fadeDuration = 0f;

            while (fadeDuration < 1f)
            {
                fadeDuration += Time.deltaTime;
                float a = curve.Evaluate(fadeDuration);
                img.color = new Color(0f, 0f, 0f, a);
                yield return 0;
            }
            //페이드 아웃 완료 후 다음 씬으로 이동
            if (buildIndex >= 0)
            {
                SceneManager.LoadScene(buildIndex);
            }
        }
        #endregion
    }
}