using UnityEngine;
using System.Collections.Generic;

namespace MySample
{
    //오브젝트 풀을 관리하는 클래스
    //풀 만들기, 풀에서 오브젝트 꺼내기, 다 쓴 오브젝트 풀에 넣기
    public class ObjectPool : MonoBehaviour
    {
        // ========== Variables ====================================
        //풀의 크기 (저장할 수 있는 오브젝트의 갯수)
        [SerializeField] private int initPoolSize;

        //저장하는 오브젝트의 프리팹
        public PooledObject objectToPool;

        //풀 (자료구조 : stack || queue)
        private Stack<PooledObject> stack;


        // ========== Properties ===================================
        public int InitPoolSize => initPoolSize;

        // ========== Unity Event Methods ==========================
        private void Start()
        {
            //풀 만들기
            SetupPool();
        }


        // ========== Custom Methods ===============================
        //풀 만들기
        private void SetupPool()
        {
            //저장할 오브젝트 체크
            if (objectToPool == null) return;

            stack = new Stack<PooledObject>();

            //풀에 저장되는 오브젝트 객체 변수
            PooledObject instance = null;

            for (int i = 0; i < initPoolSize; i++)
            {
                instance = Instantiate(objectToPool);
                instance.Pool = this;
                instance.gameObject.SetActive(false);

                stack.Push(instance);
            }
        }

        //풀에서 오브젝트 꺼내기
        public PooledObject GetPooledObject()
        {
            //오브젝트 체크
            if(objectToPool == null) return null;

            //풀 체크 - 꺼낼 오브젝트가 없을 때 새로 생성
            if (stack.Count == 0)
            {
                PooledObject newInstance = Instantiate(objectToPool);
                newInstance.Pool = this;
                return newInstance;
            }

            //풀에 오브젝트가 있으면 풀에서 꺼내기
            PooledObject nextObject = stack.Pop();
            nextObject.gameObject.SetActive(true);  //활성화

            return null;
        }


        //오브젝트 풀에 넣기
        public void ReturnToPool(PooledObject pooledObject)
        {
            stack.Push(pooledObject);
            pooledObject.gameObject.SetActive(false);
        }

    }
}