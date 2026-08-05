using System;
using UnityEngine;

namespace Unity.FPS.Game
{
    //전투 참가하는 모든 유닛에 부착되는 클래스
    public class Actor : MonoBehaviour
    {
        #region Variables
        //참조
        private ActorManager actorManager;
        
        //소속
        public int affiliation;
        //조준점
        public Transform aimPoint;
        #endregion
        
        #region Unity Evnet Method

        private void Start()
        {
            //참조
            actorManager = GameObject.FindAnyObjectByType<ActorManager>();
            //actor리스트 등록
            if (actorManager && actorManager.Actors.Contains(this) == false)
            {
                actorManager.Actors.Add(this);
            }
        }

        private void OnDestroy()
        {
            //actor리스트 삭제
            if (actorManager)
            {
                actorManager.Actors.Remove(this);
            }
        }
        #endregion
    }
}