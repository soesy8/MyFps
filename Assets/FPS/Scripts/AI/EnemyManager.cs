using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.FPS.AI
{
    //적들을 관리하는 클래스
    public class EnemyManager : MonoBehaviour
    {
        public List<EnemyController> Enemies { get; private set; }
        //생성한 적의 수
        public int NumberOfEnemiesTotal { get; private set; }
        //현재 맵에 살아있는 적의 수
        public int numberOfEnemiesRemaining => Enemies.Count;

        private void Awake()
        {
            //적 리스트 생성
            Enemies = new List<EnemyController>();
        }
        //적 리스트 등록
        public void RegisterEnemy(EnemyController enemy)
        {
            Enemies.Add(enemy);
            //생성한 모든 적의 숫자 카운트
            NumberOfEnemiesTotal++;
        }
        //적 리스트 제거
        public void RemoveEnemy(EnemyController enemy)
        {
            Enemies.Remove(enemy);
        }
    }
}