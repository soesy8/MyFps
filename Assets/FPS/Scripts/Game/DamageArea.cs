using UnityEngine;
using System.Collections.Generic;

namespace Unity.FPS.Game
{
    //일정 범위 안에 있는 충돌체(Damageable) 오브젝트에게 데미지 주기
    //폭발 위치와의 거리에 반비례해서 데미지 주기
    //하나의 health에게는 한 번만 데미지 주기
    public class DamageArea : MonoBehaviour
    {
        #region Variables
        //일정 범위, 폭발 지점으로 부터 데미지를 입는 반경
        [SerializeField] private float areaOfEffectDistance = 5f;

        //거리에 따른 데미지량 계산 커브
        [SerializeField] private AnimationCurve damageRatioOverDistance;
        #endregion

        #region CustomMethod
        //폭발(범위공격) 데미지 계산
        public void InflictDamageArea(float damage, Vector3 center,
            LayerMask layer, QueryTriggerInteraction interaction, GameObject owner)
        {
            //하나의 Health에 Damageable 하나 등록
            Dictionary<Health,Damageable> uniqueDamagedHealth
                = new Dictionary<Health, Damageable>();
            
            //범위 안에 있는 충돌체(Damageable) 가져오기
            Collider[] affectedColliders = Physics.OverlapSphere
                (center, areaOfEffectDistance, layer, interaction);
            
            foreach(Collider col in affectedColliders)
            {
                Damageable damageable = col.GetComponent<Damageable>();
                if(damageable)
                {
                    Health health = damageable.GetComponentInParent<Health>();

                    //health 중복체크
                    if(health != null && uniqueDamagedHealth.ContainsKey(health) == false)
                    {
                        uniqueDamagedHealth.Add(health, damageable);
                    }
                }
            }
            //uniqueDamagedHealth에 등록되어 있는 damageable에게만 데미지 주기
            foreach(var uniqueDamageable in uniqueDamagedHealth.Values)
            {
                //거리에 따른 데미지 계산
                float distance = Vector3.Distance
                    (center, uniqueDamageable.transform.position);
                
                float curveDamage = damage *
                    damageRatioOverDistance.Evaluate(distance / areaOfEffectDistance);

                //Debug.Log($"CurveDamage : {curveDamage}");

                //damageable에 데미지 주기
                uniqueDamageable.InflictDamage(curveDamage, true, owner);
            }
        }
        #endregion
    }
}