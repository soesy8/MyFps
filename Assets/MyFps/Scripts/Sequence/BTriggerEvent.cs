using UnityEngine;
using System.Collections;
using TMPro;

namespace MyFps
{
    public class BTriggerEvent : MonoBehaviour
    {
        [SerializeField] private GameObject player;         //플레이어
        [SerializeField] private TextMeshProUGUI sequenceText;   //대사
        [SerializeField] private GameObject guideArrow;     //가이드 화살표

        private bool isTriggered = false;

        public bool IsTriggered
        {
            get { return isTriggered; }
            set { isTriggered = value; }
        }

        private void Start()
        {
            //sequenceText.SetActive(false);
            guideArrow.SetActive(false);
            IsTriggered = false;
        }

        private void Update()
        {
            if (guideArrow == null) return;
            guideArrow.transform.LookAt(player.transform.position);
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
            sequenceText.gameObject.SetActive(true);
            sequenceText.text = "Looks like a weapon on that table.";

            yield return new WaitForSeconds(1f);

            guideArrow.SetActive(true);

            yield return new WaitForSeconds(1f);

            player.SetActive(true);
            IsTriggered = true;

            yield return new WaitForSeconds(1f);

            sequenceText.text = "";
            sequenceText.gameObject.SetActive(false);
            transform.GetComponent<BoxCollider>().enabled = false;
            //Destroy(gameObject);
        }

    }
}