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

        //[SerializeField] private GameObject door;
        //private Animator animator;

        [SerializeField] private GameObject puzzleKey;
        //private ItemType dropKey;

        /*private void Awake()
        {
            animator = door.GetComponent<Animator>();
        }*/

        public override void Interact(PlayerInteraction player)
        {
            if (isComplete)
            {
                return;
            }

            bool hasLeft = PlayerInventory.Instance.HasItem(ItemType.EyePuzzleL);
            bool hasRight = PlayerInventory.Instance.HasItem(ItemType.EyePuzzleR);

            if (!hasLeft && !hasRight)
            {
                NeedPuzzle();
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

            if (leftEye.activeSelf && rightEye.activeSelf)
            {
                isComplete = true;
                DropItem();
            }
        }

        IEnumerator NeedPuzzleRoutine()
        {
            sequenceText.text = "Need Puzzle";

            yield return new WaitForSeconds(2f);

            sequenceText.text = "";
        }

        private void NeedPuzzle()
        {
            if (messageCoroutine != null)
            {
                StopCoroutine(messageCoroutine);
            }

            messageCoroutine = StartCoroutine(NeedPuzzleRoutine());
        }

        private void DropItem()
        {
            Debug.Log("PuzzleKey Drop");

            puzzleKey.SetActive(true);

            //animator.SetBool("IsOpen", true);
        }
    }
}