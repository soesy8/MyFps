using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyFps
{
    public class AutoSave : MonoBehaviour
    {
        private void Start()
        {
            //씬 번호 저장
            SaveSceneNumber();
        }

        void SaveSceneNumber()
        {
            int sceneNumber = SceneManager.GetActiveScene().buildIndex;
            Debug.Log($"Save sceneNumber : {sceneNumber}");


            //PlayerPrefs
            PlayerPrefs.SetInt("SceneNumber", sceneNumber);
        }
    }
}