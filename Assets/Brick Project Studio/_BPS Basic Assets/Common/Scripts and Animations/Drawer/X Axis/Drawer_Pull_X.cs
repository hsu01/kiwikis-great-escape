using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace SojaExiles
{
    public class Drawer_Pull_X : MonoBehaviour
    {
        public Animator pull_01;
        public bool open;
        public Transform Player;
        public float interactDistance = 10f;
        public GameObject interactPrompt;
        [Tooltip("Fired the first time this drawer is opened (used to advance the tutorial).")]
        public UnityEvent onOpened;
        private bool hasBeenOpened = false;
        private bool isAnimating = false;
        private Collider interactCollider;

        void Start()
        {
            open = false;
            interactCollider = GetComponent<Collider>();

            // The hand-assigned Player reference is easy to get wrong, so always
            // resolve the cat by its "Player" tag (the same tag the light switch
            // uses). Fall back to the inspector field only if nothing is tagged.
            var tagged = GameObject.FindGameObjectWithTag("Player");
            if (tagged != null)
                Player = tagged.transform;

            if (interactPrompt != null)
                interactPrompt.SetActive(false);
        }

        // The object's pivot can be far from the visible drawer, so measure from
        // the collider's world center when one is present.
        private Vector3 InteractPoint =>
            interactCollider != null ? interactCollider.bounds.center : transform.position;

        void Update()
        {
            if (Player)
            {
                float dist = Vector3.Distance(Player.position, InteractPoint);

                if (dist <= interactDistance)
                {
                    if (interactPrompt != null)
                        interactPrompt.SetActive(true);

                    if (Keyboard.current != null &&
                        Keyboard.current.eKey.wasPressedThisFrame &&
                        !isAnimating)
                    {
                        if (open == false)
                            StartCoroutine(opening());
                        else
                            StartCoroutine(closing());
                    }
                }
                else
                {
                    if (interactPrompt != null)
                        interactPrompt.SetActive(false);
                }
            }
        }

        IEnumerator opening()
        {
            isAnimating = true;
            print("you are opening the drawer");
            pull_01.Play("openpull_01");
            open = true;

            if (!hasBeenOpened)
            {
                hasBeenOpened = true;
                onOpened?.Invoke();
            }

            yield return new WaitForSeconds(.5f);
            isAnimating = false;
        }

        IEnumerator closing()
        {
            isAnimating = true;
            print("you are closing the drawer");
            pull_01.Play("closepush_01");
            open = false;
            yield return new WaitForSeconds(.5f);
            isAnimating = false;
        }
    }
}