using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInteractScript : MonoBehaviour
{

    Camera cam;
    ClassicCardScript currccs;
    ClassicCardScript lastccs;
    
    public GameObject gameLogic;

    private AutobusGameScript Agaem;

    private void Start() {
        cam = GetComponent<Camera>();
        Agaem = gameLogic.GetComponent<AutobusGameScript>();
    }

    private void Update() {
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(mouseRay, out hit, 50f, 1 << LayerMask.NameToLayer("Card"))){
            currccs = hit.collider.gameObject.GetComponent<ClassicCardScript>();
            if(Input.GetKeyDown(KeyCode.Mouse0) && currccs) {
                Agaem.ActionRequest(currccs, 0);
            }
            if (currccs != lastccs){
                if(currccs){
                    if (lastccs){
                        lastccs.hover = false;;
                        lastccs.offset = 0f;
                        lastccs.stackPush = 0f;
                    }
                    lastccs = currccs;
                    currccs.offset = 0.03f;
                    currccs.stackPush = 0.08f;  
                    lastccs.hover = true;                   
                } 
            }
        }
        else {
            if (lastccs){
                lastccs.hover = false;
                lastccs.offset = 0f;
                lastccs.stackPush = 0f;
                lastccs = null;
            }
        }
    }
}
