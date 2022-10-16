using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPViewScript : MonoBehaviour
{
    public float mouseSens;
    public Vector3 startPosition = new Vector3(15f,0f,0f);
    float xRot = 0;
    float yRot = 0f;
    float timeView = 0;

    public float zoomSpeed = 1;
    float zSpeed = 0;
    float timeZoom = 0;

    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        View();
        Zoom();
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
            Cursor.lockState = CursorLockMode.Confined;
            xRot = startPosition.y;
            yRot = startPosition.x;
            timeView = 0f;
        }
        timeView += 1f * Time.deltaTime;
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(startPosition), timeView);
    }
}
