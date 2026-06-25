using UnityEngine;
using System.Collections;

namespace MyFps
{
    public class SceneTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject player;         //플레이어
        [SerializeField] private GameObject dialogueText;   //대사
        [SerializeField] private GameObject guideArrow;     //가이드 화살표

        private bool isTriggered = false;

        public bool IsTriggered
        {
            get { return isTriggered; }
            set { isTriggered = value; }
        }

        private void Start()
        {
            dialogueText.SetActive(false);
            guideArrow.SetActive(false);
            IsTriggered = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (IsTriggered) return;
                StartCoroutine(GuideRoutine());
            }
        }

        IEnumerator GuideRoutine()
        {
            player.SetActive(false);
            dialogueText.SetActive(true);

            yield return new WaitForSeconds(1f);

            guideArrow.SetActive(true);

            yield return new WaitForSeconds(1f);

            player.SetActive(true);
            IsTriggered = true;

            yield return new WaitForSeconds(1f);

            dialogueText.SetActive(false);
            Destroy(gameObject);
        }

    }
}