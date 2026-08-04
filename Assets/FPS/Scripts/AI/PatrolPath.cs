using UnityEngine;
using System.Collections.Generic;

namespace Unity.FPS.AI
{
    //패트롤 웨이포인트를 관리하는 클래스
    public class PatrolPath : MonoBehaviour
    {
        #region Variables
        public List<Transform> pathNodes = new List<Transform>();
        #endregion

        #region Unity Event Method
        //경로 그리기 기즈모
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            for(int i = 0 ; i < pathNodes.Count; i++)
            {
                int nextIndex = i + 1;
                if(nextIndex >= pathNodes.Count)
                {
                    nextIndex = 0;
                }
                Gizmos.DrawLine(pathNodes[i].position, pathNodes[nextIndex].position);
            }
        }
        #endregion

        #region Custom Method
        #endregion
    }
}