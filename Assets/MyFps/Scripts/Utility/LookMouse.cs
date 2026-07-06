using UnityEngine;
using UnityEngine.InputSystem;

namespace MyFps
{
    public class LookMouse : MonoBehaviour
    {


        private void Update()
        {
            //마우스 위치로 부터 월드 위치값 값져오기
            Vector3 worldPos = ScreenToWorld();
            //Vector3 worldPos = ScreenToRay();

            transform.LookAt(worldPos);
        }

        private Vector3 ScreenToWorld()
        {
            float z = 2.5f;
            Vector3 mousePos = Mouse.current.position.ReadValue();
            Vector3 mousePosition = new Vector3(mousePos.x, mousePos.y, z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            return worldPosition;

        }

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
    }
}