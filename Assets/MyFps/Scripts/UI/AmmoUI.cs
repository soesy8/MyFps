using TMPro;
using UnityEngine;

namespace MyFps
{
    public class AmmoUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI ammoUI;

        private void OnEnable()
        {
            PlayerStats.Instance.OnAmmoChanged += UpdateAmmoUI;
        }

        private void OnDisable()
        {
            PlayerStats.Instance.OnAmmoChanged -= UpdateAmmoUI;
        }

        public void UpdateAmmoUI()
        {
            ammoUI.text = $"{PlayerStats.Instance.AmmoCount} / {PlayerStats.Instance.ReserveAmmo}";
        }
    }
}