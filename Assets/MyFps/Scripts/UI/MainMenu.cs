using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace MyFps
{
    public class MainMenu : MonoBehaviour
    {
        private AudioManager audioManager;

        public SceneFader fader;
        [SerializeField] private string loadScene = "PlayScene01";

        public GameObject mainMenu;
        public GameObject optionUI;
        public GameObject creditUI;

        //옵션 UI - 볼륨 조절
        public AudioMixer audioMixer;

        public Slider bgmSlider;
        public Slider sfxSlider;

        //오디오믹서 파라미터, PlayerPrefs의 키값
        private const string BgmVolume = "BgmVolume";
        private const string SfxVolume = "SfxVolume";

        private void Awake()
        {
            audioManager = AudioManager.Instance;
        }

        private void Start()
        {
            //게임 처음 실행하면 저장된 옵션 값 로드하기
            LoadOption();

            //배경음 플레이
            audioManager.PlayBgm("MenuBgm");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void StartGame()
        {
            Debug.Log("StartGame");
            audioManager.Stop("MenuBgm");
            audioManager.Play("MenuButton");
            fader.FadeTo(loadScene);
        }

        public void LoadGame()
        {
            Debug.Log("LoadGame");
            audioManager.Play("MenuButton");
        }

        public void Options()
        {
            audioManager.Play("MenuButton");
            ShowOption();
        }

        public void Credits()
        {
            ShowCredit();
            audioManager.Play("MenuButton");
        }

        public void QuitGame()
        {
            Debug.Log("QuitGame");
            audioManager.Play("MenuButton");
            fader.FadeTo("");
        }

        private void ShowOption()
        {
            mainMenu.SetActive(false);
            optionUI.SetActive(true);
        }

        public void HideOption()
        {
            optionUI.SetActive(false);
            mainMenu.SetActive(true);
        }

        //배경음 슬라이더로 볼륨 조절
        public void SetBgmVolume(float value)
        {
            //배경음 저장하기
            PlayerPrefs.SetFloat(BgmVolume, value);

            //Debug.Log($"BGM Volume : {value}");
            audioMixer.SetFloat(BgmVolume, value);
        }

        //효과음 슬라이더로 볼륨 조절
        public void SetSfxVolume(float value)
        {
            //효과음 저장하기
            PlayerPrefs.SetFloat(SfxVolume, value);

            //Debug.Log($"SFX Volume : {value}");
            audioMixer.SetFloat(SfxVolume, value);
        }

        //저장된 옵션 값 로드하기
        private void LoadOption()
        {
            //배경음, 효과음 가져오기
            float bgmVolume = PlayerPrefs.GetFloat(BgmVolume,0f);
            audioMixer.SetFloat(BgmVolume, bgmVolume);
            bgmSlider.value = bgmVolume;

            float sfxVolume = PlayerPrefs.GetFloat(SfxVolume,0f);
            audioMixer.SetFloat(SfxVolume, sfxVolume);
            sfxSlider.value = sfxVolume;


            Debug.Log($"bgmVol : {bgmVolume}");
            Debug.Log($"sfxVol : {sfxVolume}");
        }
        //크래딧 UI
        private void ShowCredit()
        {
            mainMenu.SetActive(false);
            creditUI.SetActive(true);
        }
    }
}