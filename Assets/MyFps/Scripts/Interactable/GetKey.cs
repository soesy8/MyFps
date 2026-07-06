using UnityEngine;

namespace MyFps
{
    public class GetKey : Interactable
    {
        //private bool isKey = false;
        public override void Interact(PlayerInteraction player)
        {
            //키 획득 시 문을 열 수 있는 권한 true
            //isKey = true;
            Debug.Log("Get Key");
            Destroy(gameObject);
        }
    }
}