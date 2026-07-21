using UnityEngine;

namespace MyFps
{
    public class FEscapeTrigger : MonoBehaviour
    {
        [SerializeField] private SceneFader fader;
        [SerializeField] private string loadToScene = "MainMenu";
        //[SerializeField] private int loadToSceneNumber = 4;

        private void OnTriggerEnter(Collider other)
        {
            AudioManager.Instance.Stop("Bgm01");

            //클리어 처리
            //게임 데이터 저장
            //PlayerStats.Instance.SceneNumber = loadToSceneNumber;
            SaveLoad.SaveData();

            fader.FadeTo(loadToScene);
        }
    }
}