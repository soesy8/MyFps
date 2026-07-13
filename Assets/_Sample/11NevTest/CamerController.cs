using UnityEngine;

namespace MySample
{
    /// <summary>
    /// 플레이어 쫓아가기
    /// </summary>
    public class CamerController : MonoBehaviour
    {
        public Transform player;
        public Vector3 offset;

        private void LateUpdate()
        {
            transform.position = player.position + offset;

        }
    }
}