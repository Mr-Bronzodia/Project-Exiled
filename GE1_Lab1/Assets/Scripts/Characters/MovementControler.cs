using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControler : MonoBehaviour
{
    public CharacterController controller;

    private float speed = 6f;
    public Vector3 LookPoint { private set; get; }

    void Update()   
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Orientate player toward the mouse
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 mouseInput = new Vector3(horizontal, 0f, vertical);

        var mouseVelocity = mouseInput;

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLenght;

        if (groundPlane.Raycast(mouseRay, out rayLenght))
        {
            Vector3 pointToLook = mouseRay.GetPoint(rayLenght);
            LookPoint = new Vector3(pointToLook.x, transform.position.y, pointToLook.z);

            transform.LookAt(LookPoint);

            if (direction.magnitude >= 0.1)
            {
                controller.Move(direction * speed * Time.deltaTime);
            }
        }

    }
}
