using UnityEngine;

namespace MyFps
{
    public class DExitTrigger : MonoBehaviour
    {
        [SerializeField] private SceneFader fader;
        [SerializeField] private AudioSource jumpScare;
        [SerializeField] private string loadToScene = "Playscene02";
        [SerializeField] private int loadToSceneNumber = 3;



        private void OnTriggerEnter(Collider other)
        {
            jumpScare.Stop();

            //게임 데이터 저장
            PlayerStats.Instance.SceneNumber = loadToSceneNumber;
            SaveLoad.SaveData();

            fader.FadeTo(loadToScene);
        }
    }
}