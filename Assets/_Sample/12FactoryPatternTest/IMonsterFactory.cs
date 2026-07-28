using UnityEngine;

namespace MySample
{
    //몬스터 팩토리의 기능 정의한 인터페이스
    //필수 구현사항 : (몬스터) 생성 메서드
    public interface IMonsterFactory
    {
        public Monster CreateMonster(); //몬스터 생성 메서드
    }

    //몬스터를 생성하는 공장 만들기
    //슬라임만 생성하는 슬라임 전용 공장
    public class SlimeFactory : IMonsterFactory
    {
        //슬라임 생성 갯수
        private int count = 0;

        public Monster CreateMonster()
        {
            return new Slime();
        }

        public void SlimeCount()
        {
            count++;
        }
    }

    //좀비만 생성하는 좀비 전용 공장
    public class ZombieFactory : IMonsterFactory
    {
        public Monster CreateMonster()
        {
            return new Zombie();
        }

        public void AddSomething()
        {
            Debug.Log("Add Something");
        }
    }

    //좀비만 생성하는 좀비 전용 공장
    public class GoblinFactory : IMonsterFactory
    {
        public Monster CreateMonster()
        {
            return new Goblin();
        }

        
    }
}