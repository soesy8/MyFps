using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.UI
{
    //게이지바 이미지의 컬러를 관리하는 클래스
    public class FillBarColorChange : MonoBehaviour
    {
        #region Variables
        public Image foregroundImage;               //게이지 이미지
        public Color defaultForegroundColorFull;    //게이지의 originColor
        public Color flashForegroundColorFull;      //게이지가 풀로 찰 때 번쩍이는 색
        //====================================
        public Image backgroundImage;               //게이지 백그라운드 이미지
        public Color defalutBackgroundColorFull;    //백그라운드 이미지의 OriginColor
        public Color flashBackgroundColorEmpty;     //게이지가 0일 때 보이는 색
        //=====================================
        public float fullValue = 1f;        //게이지의 Full값
        public float emptyValue = 0f;       //게이지의 Empty값
        public float colorChangeSharpness = 5f;     //컬러 변경 연출 Lerp 속도 계수
        //was(last) 변수 - 리로드 시 게이지가 풀로 채워지는 순간
        private float m_PriviousValue;
        #endregion

        #region Custom Method
        //초기화
        public void Initialize(float fullValueRatio, float emptyValueRatio)
        {
            fullValue = fullValueRatio;
            emptyValue = emptyValueRatio;

            m_PriviousValue = fullValueRatio;
        }

        public void UpdateVisual(float currentRatio)
        {
            //게이지가 풀로 차는 순간
            if(currentRatio == fullValue && currentRatio != m_PriviousValue)
            {
                foregroundImage.color = flashForegroundColorFull;
            }
            //게이지가 비어 있을 때
            else if(currentRatio <= emptyValue)
            {
                backgroundImage.color = flashBackgroundColorEmpty;;
            }
            else
            {
                foregroundImage.color = Color.Lerp(foregroundImage.color, defaultForegroundColorFull,
                    colorChangeSharpness * Time.deltaTime);
                backgroundImage.color = Color.Lerp(backgroundImage.color, defalutBackgroundColorFull,
                    colorChangeSharpness * Time.deltaTime);
            }


            m_PriviousValue = currentRatio;
        }
        #endregion
    }
}