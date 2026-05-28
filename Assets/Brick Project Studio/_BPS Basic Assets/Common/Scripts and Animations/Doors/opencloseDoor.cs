using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SojaExiles
{
    public class opencloseDoor : MonoBehaviour
    {
        public Animator openandclose;
        public bool open;
        public Transform Player;

        // NEW: This will appear in the Inspector so you can easily adjust it
        public float interactDistance = 15f;

        // NEW: Drag your UI object (Text, Image, Panel, etc.) here
        public GameObject interactPrompt;

        private bool isAnimating = false;

        void Start()
        {
            open = false;

            // Make sure the UI prompt is hidden when the game first starts
            if (interactPrompt != null)
            {
                interactPrompt.SetActive(false);
            }
        }

        void Update()
        {
            if (Player)
            {
                // Check the distance between the player and the door
                float dist = Vector3.Distance(Player.position, transform.position);

                // Use the custom interactDistance instead of a hardcoded number
                if (dist <= interactDistance)
                {
                    // Player is close enough: Show the UI prompt
                    if (interactPrompt != null)
                    {
                        interactPrompt.SetActive(true);
                    }

                    // Check if they press E to interact
                    if (Keyboard.current != null &&
                        Keyboard.current.eKey.wasPressedThisFrame &&
                        !isAnimating)
                    {
                        if (open == false)
                        {
                            StartCoroutine(opening());
                        }
                        else
                        {
                            StartCoroutine(closing());
                        }
                    }
                }
                else
                {
                    // Player is too far away: Hide the UI prompt
                    if (interactPrompt != null)
                    {
                        interactPrompt.SetActive(false);
                    }
                }
            }
        }

        IEnumerator opening()
        {
            isAnimating = true;
            print("you are opening the door");
            openandclose.Play("Opening");
            open = true;
            yield return new WaitForSeconds(.5f);
            isAnimating = false;
        }

        IEnumerator closing()
        {
            isAnimating = true;
            print("you are closing the door");
            openandclose.Play("Closing");
            open = false;
            yield return new WaitForSeconds(.5f);
            isAnimating = false;
        }
    }
}