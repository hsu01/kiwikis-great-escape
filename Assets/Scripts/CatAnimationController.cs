using UnityEngine;
using StarterAssets;

public class CatAnimationController : MonoBehaviour
{
    public CharacterController controller;
    public StarterAssetsInputs input;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float speed = controller.velocity.magnitude;

        // Walk animation
        animator.SetFloat("Speed", speed);

        // Jump animation
        if (input.jump)
        {
            animator.SetTrigger("Jump");
            input.jump = false;
        }
    }
}