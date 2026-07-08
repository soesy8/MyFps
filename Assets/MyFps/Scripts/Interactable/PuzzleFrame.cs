using TMPro;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

namespace MyFps
{
    public class PuzzleFrame : Interactable
    {
        [SerializeField] private GameObject leftEye;
        [SerializeField] private GameObject rightEye;

        private bool isComplete = false;

        [SerializeField] private TextMeshProUGUI sequenceText;
        private Coroutine messageCoroutine;

        [SerializeField] private GameObject door;

        public override void Interact(PlayerInteraction player)
        {
            //if (leftEye.activeSelf && rightEye.activeSelf) return;

            bool hasLeft = PlayerInventory.Instance.HasItem(ItemType.EyePuzzleL);
            bool hasRight = PlayerInventory.Instance.HasItem(ItemType.EyePuzzleR);

            if (!hasLeft && !hasRight)
            {
                if (messageCoroutine != null)
                {
                    StopCoroutine(messageCoroutine);
                }

                messageCoroutine = StartCoroutine(NeedPuzzleRoutine());

                return;
            }

            if (hasLeft)
            {
                PlayerInventory.Instance.RemoveItem(ItemType.EyePuzzleL);
                leftEye.SetActive(true);
            }

            if (hasRight)
            {
                PlayerInventory.Instance.RemoveItem(ItemType.EyePuzzleR);
                rightEye.SetActive(true);
            }
            /*if (!PlayerInventory.Instance.HasItem(ItemType.EyePuzzleL) && !PlayerInventory.Instance.HasItem(ItemType.EyePuzzleR))
            {
                Debug.Log("Have no Puzzle");

                if (messageCoroutine != null)
                {
                    StopCoroutine(messageCoroutine);
                }

                messageCoroutine = StartCoroutine(NeedPuzzleRoutine());

                return;
            }*/
        }

        IEnumerator NeedPuzzleRoutine()
        {
            sequenceText.text = "Need Puzzle";

            yield return new WaitForSeconds(2f);

            sequenceText.text = "";
        }
    }
}