using UnityEngine;

public class CatAnimationController : MonoBehaviour
{
    private Animator animator;

    public CharacterController controller;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 velocity = controller.velocity;
        velocity.y = 0f;

        float speed = velocity.magnitude;

        animator.SetBool("IsRunning", speed > 0.05f);
    }
}