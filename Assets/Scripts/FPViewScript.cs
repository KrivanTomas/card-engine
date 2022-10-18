using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPViewScript : MonoBehaviour
{
    public float mouseSens;
    public Vector3 startRotation = new Vector3(30f,0f,0f);
    public Vector3 viewRotation = new Vector3(70f,0f,0f);

    public Vector3 startPosition;
    public Vector3 viewPosition;

    Vector3 currentRotation;
    Vector3 currentPosition;

    float xRot = 0;
    float yRot = 0f;
    float timeView = 0;

    public float zoomSpeed = 1;
    float zSpeed = 0;
    float timeZoom = 0;

    public float positionSpeed = 1;
    float pSpeed = 0;
    float timePosition = 0;

    public GameObject menuObject;
    bool menuOnScreen = false;

    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        currentRotation = startRotation;
        xRot = currentRotation.y;
        yRot = currentRotation.x;
    }

    // Update is called once per frame
    void Update()
    {
        View();
        Zoom();
        Position();

        if (Input.GetKeyDown(KeyCode.Escape)) {
            ShowMenu();
        }
    }

    private void ShowMenu()
    {
        if(menuOnScreen)
        {
            menuObject.SetActive(false);
            menuOnScreen = false;
            return;
        }
        menuObject.SetActive(true);
        menuOnScreen = true;
    }

    private void Zoom() {
        if(Input.GetKeyDown(KeyCode.Mouse1)) {
            zSpeed = zoomSpeed;
            timeZoom = 0f;
        }
        if(Input.GetKeyUp(KeyCode.Mouse1)) {
            zSpeed = -zoomSpeed;
            timeZoom = 1f;
        }
        timeZoom += zSpeed * Time.deltaTime;
        if(Input.GetKey(KeyCode.Mouse1)) {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 30f, timeZoom);
        }
        else {
            cam.fieldOfView = Mathf.Lerp(70f, cam.fieldOfView, timeZoom);
        }
    }


    private void View() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetKey(KeyCode.Tab))
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
    
            xRot += mouseX;
            yRot -= mouseY;
            yRot = Mathf.Clamp(yRot, -90f, 90f);
    
            transform.localRotation = Quaternion.Euler(yRot, xRot, 0f);
            return;
        }
        if (Input.GetKeyUp(KeyCode.Tab)) {
            Cursor.lockState = CursorLockMode.None;
            xRot = currentRotation.y;
            yRot = currentRotation.x;
            timeView = 0f;
        }
        timeView += 1f * Time.deltaTime;
        gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(currentRotation), timeView);
    }

    private void Position() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            currentPosition = viewPosition;
            currentRotation = viewRotation;
            pSpeed = positionSpeed;
            timePosition = 0f;
            timeView = 0f;
            if (!Input.GetKey(KeyCode.Tab)) {
            xRot = currentRotation.y;
            yRot = currentRotation.x;
            }
        }
        // if (Input.GetKey(KeyCode.Space))
        // {
        //     float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        //     float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
    
        //     xRot += mouseX;
        //     yRot -= mouseY;
        //     yRot = Mathf.Clamp(yRot, -90f, 90f);
    
        //     transform.localRotation = Quaternion.Euler(yRot, xRot, 0f);
        //     return;
        // }
        if (Input.GetKeyUp(KeyCode.Space)) {
            currentPosition = startPosition;
            currentRotation = startRotation;
            pSpeed = -positionSpeed;
            timePosition = 1f;
            timeView = 0f;
            if (!Input.GetKey(KeyCode.Tab)) {
            xRot = currentRotation.y;
            yRot = currentRotation.x;
            }
        }
        timePosition += pSpeed * Time.deltaTime;
        if(Input.GetKey(KeyCode.Space)) {
            gameObject.transform.localPosition = Vector3.Lerp(transform.localPosition, currentPosition, timePosition);
        }
        else {
            gameObject.transform.localPosition = Vector3.Lerp(currentPosition, transform.localPosition, timePosition);
        }
    }
}
