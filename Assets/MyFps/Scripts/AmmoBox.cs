using UnityEngine;

namespace MyFps
{
    public class AmmoBox : Interactable
    {
        public override void Interact()
        {
            //총알 +7

            //오브젝트 파괴
            Destroy(gameObject);
        }
    }
}