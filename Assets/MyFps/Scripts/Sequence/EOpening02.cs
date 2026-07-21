using UnityEngine;

namespace MyFps
{
    public class EOpening02 : MonoBehaviour
    {
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private SceneFader fader;
        [SerializeField] private GameObject pistol;
        [SerializeField] private GameObject ammoUI;

        private void Awake()
        {
            audioManager = AudioManager.Instance;
            //GameObject player = GetComponent<GameObject>();
            //Debug.Log(audioManager);
        }

        private void Start()
        {
            fader.FadeStart(0.5f);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            //배경음 플레이
            audioManager.PlayBgm("Bgm01");

            pistol.SetActive(true);
            ammoUI.SetActive(true);
            /*PlayerShoot shoot = pistol.GetComponent<PlayerShoot>();
            player.SetPlayerShoot(shoot)*/;

            AmmoUI _ammoUI = ammoUI.GetComponent<AmmoUI>();
            _ammoUI.UpdateAmmoUI();
        }


    }
}
