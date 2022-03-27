using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MovementControler : MonoBehaviour
{
    public CharacterController controller;

    private float speed = 6f;

    public LayerMask groundLayer;

    private Character character;

    public Vector3 LookPoint { private set; get; }

    private Animator animator;

    private void Start()
    {
        character = gameObject.GetComponent<Character>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        controller.Move(Physics.gravity * Time.deltaTime);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Orientate player toward the mouse
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 mouseInput = new Vector3(horizontal, 0f, vertical);

        AimAtMouse();

        if (direction.magnitude >= 0.1)
        {
            controller.Move(direction * character.speed * Time.deltaTime);
        }

        float velocirtZ = Vector3.Dot(direction.normalized, transform.forward);
        float velocirtX = Vector3.Dot(direction.normalized, transform.right);

        animator.SetFloat("VelocityZ", velocirtZ, 0.1f, Time.deltaTime);
        animator.SetFloat("VelocityX", velocirtX, 0.1f, Time.deltaTime);


        AnimationClip currentClip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;

        if (currentClip.name == "Fast Run" | currentClip.name == "Idle" | currentClip.name == "Left Strafe" | currentClip.name == "Right Strafe" | currentClip.name == "Running Backward")
        {
            if (controller.velocity.magnitude > 0.05f)
            {
                float speedMultiplier = controller.velocity.magnitude / (currentClip.length * currentClip.frameRate);
                animator.SetFloat("Speed Multiplier", speedMultiplier >= 0.05f ? speedMultiplier : 1f, 0.1f, Time.deltaTime);
            }
            
        }

    }

    private void AimAtMouse()
    {
        Ray cameraToGroundRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cameraToGroundRay, out var hit, Mathf.Infinity, groundLayer))
        {
            Vector3 direction = hit.point - transform.position;
            direction.y = 0f;
            direction.Normalize();

            transform.forward = direction;
        }
    }

}
