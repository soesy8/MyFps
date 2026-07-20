using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyFps
{
    public class CreditUI : MonoBehaviour
    {
        public InputActionReference escapeAction;
        public GameObject mainMenu;

        private void Update()
        {
            if (escapeAction.action.WasPressedThisFrame())
            {
                HideCredit();
            }
        }

        private void HideCredit()
        {
            this.gameObject.SetActive(false);
            mainMenu.SetActive(true);
        }
    }
}