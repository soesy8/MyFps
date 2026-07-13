using UnityEngine;

namespace MySample
{
    /// <summary>
    /// 1초 마다 좌우로 바뀌어 이동
    /// dir변수 사용 1이면 오른쪽 이동, -1이면 왼쪽 이동
    /// </summary>
    public class MoveWall : MonoBehaviour
    {
        [Tooltip("이동 속도 (유닛/초)")]
        public float speed = 1f;

        [Tooltip("1 = 오른쪽, -1 = 왼쪽")]
        public int dir = 1;

        [Tooltip("방향 전환 간격(초)")]
        public float interval = 1f;

        float timer;

        void Reset()
        {
            speed = 1f;
            dir = 1;
            interval = 1f;
            timer = 0f;
        }

        void OnValidate()
        {
            // dir는 1 또는 -1로 유지
            if (dir != 1 && dir != -1)
                dir = dir >= 0 ? 1 : -1;
            if (interval <= 0f)
                interval = 1f;
            if (speed < 0f)
                speed = 0f;
        }

        void Update()
        {
            // 이동
            transform.Translate(Vector3.right * dir * speed * Time.deltaTime);

            // 타이머 누적 후 1초(또는 interval)마다 방향 전환
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                dir = -dir;
                timer = 0f;
            }
        }
    }
}

/*
1초 마다 좌우로 바뀌어 이동
dir변수 사용 1이면 오른쪽 이동, -1이면 왼쪽 이동
스크립트 작성해줘
*/