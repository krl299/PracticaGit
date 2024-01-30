using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [Header("Rotate config")]
    public float minXRotation;
    public float maxXRotation;
    private float curXRotation;
    public float rotateSpeed;

    [Header("Zoom config")]
    [SerializeField]
    private float minZoom;
    [SerializeField]
    private float maxZoom;
    [SerializeField]
    private float zoomSpeed;

    private float curZoom;
    // reference to the camera object
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float mouseX;

    //Indicates if the camera is rotating
    [SerializeField]
    private bool rotating;
    [SerializeField]
    private Vector2 moveDirection;


    private void Start()
    {
        cam = Camera.main;
        curZoom = cam.transform.localPosition.y;

        curXRotation = -50;

    }
    //Ada was here
    public void OnZoom(InputAction.CallbackContext context)
    {
        //getting the scroll wheel value
        curZoom += context.ReadValue<Vector2>().y * zoomSpeed;
        //clamping it between min/max zoom values
        curZoom = Mathf.Clamp(curZoom, minZoom, maxZoom);
    }


    public void OnRotateToggle(InputAction.CallbackContext context)
    {
        rotating = context.ReadValueAsButton();
        if (!rotating) mouseX = 0; //To avoid infinite spin
    }
    //jose luis was here
    public void OnRotate(InputAction.CallbackContext context)
    {
        //a
        //If clicking right button mouse
        if(rotating)
        {
            mouseX = context.ReadValue<Vector2>().x;
            float mouseY = context.ReadValue<Vector2>().y;

            curXRotation += -mouseY * rotateSpeed;
            curXRotation = Mathf.Clamp(curXRotation, minXRotation, maxXRotation);                   
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //Read the input value that is being sent by the Input system
        moveDirection = context.ReadValue<Vector2>();
    }

    private void LateUpdate()
    {
        //applying ZOOM to the camera
        cam.transform.localPosition = Vector3.up * curZoom;

        //applying ROTATION to the anchor
        transform.eulerAngles = new Vector3(curXRotation, transform.eulerAngles.y + (mouseX * rotateSpeed), 0.0f);

        //applying MOVEMENT to the camera
        Vector3 forward = cam.transform.forward;
        forward.y = 0.0f;
        forward.Normalize();

        Vector3 right = cam.transform.right;

        //get a local direction
        Vector3 dir = forward * moveDirection.y + right * moveDirection.x;

        dir *= moveSpeed * Time.deltaTime;

        transform.position += dir;
    }
}
