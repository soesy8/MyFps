using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace MySample
{
    /// <summary>
    /// Nav Agent를 관리하는 클래스 예제
    /// 마우스로 맵을 클릭하면 클릭한 지점으로 Agent가 이동한다
    /// </summary>
    public class AgentController : MonoBehaviour
    {
        #region Variables
        //참조
        private NavMeshAgent m_Agent;

        public InputActionReference clickAction;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            m_Agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            //마우스로 좌클릭하면 좌클릭한 월드 포지션 가져오기
            //가져온 월드포지션을 m_Agent의 이동 목표 지점으로 설정한다
            //ScreenToRay();
        }
        #endregion

        #region Custom Method
        private Vector3 ScreenToRay()
        {
            Vector3 worldPosition = Vector3.zero;

            Vector3 mousePos = Mouse.current.position.ReadValue();
            Vector3 mousePosition = new Vector3(mousePos.x, mousePos.y, 0f);
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                worldPosition = hit.point;
            }

            return worldPosition;
        }
        #endregion
    }
}