using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SojaExiles
{
    public class Drawer_Pull_X : MonoBehaviour
    {
        public Animator pull_01;
        public bool open;
        public Transform Player;
        public float interactDistance = 10f;
        public GameObject interactPrompt;
        private bool isAnimating = false;

        void Start()
        {
            open = false;
            if (interactPrompt != null)
                interactPrompt.SetActive(false);
        }

        void Update()
        {
            if (Player)
            {
                float dist = Vector3.Distance(Player.position, transform.position);

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