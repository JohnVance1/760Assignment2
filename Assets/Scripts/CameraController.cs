using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float movSpeed;
    private float rotSpeed;

    private Vector2 move;
    private Vector2 inputVec;
    private Vector2 rotateVec;
    private Vector3 targetDirection;

    private float rotationVelocity;

    private bool mouseClick = false;

    void Start()
    {
        movSpeed = 1;
        rotSpeed = 0.1f;
    }

    void Update()
    {
        if (inputVec != Vector2.zero)
        {
            transform.position += transform.rotation * (movSpeed * new Vector3(inputVec.x, 0, inputVec.y));

        }

        if (mouseClick)
        {
            transform.Rotate(new Vector3(-rotateVec.x * rotSpeed, rotateVec.y * rotSpeed, 0));
           
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);

        }
    }
    public void OnMove(InputValue input)
    {
        inputVec = input.Get<Vector2>();

    }

    public void OnRotateX(InputValue input)
    {
        rotateVec.y = input.Get<float>();

    }

    public void OnRotateY(InputValue input)
    {
        rotateVec.x = input.Get<float>();

    }

    public void OnMouseClick(InputValue input)
    {
        mouseClick = input.isPressed;
    }

}
