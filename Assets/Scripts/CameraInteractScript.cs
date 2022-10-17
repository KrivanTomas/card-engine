using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraInteractScript : MonoBehaviour
{

    Camera cam;
    ClassicCardScript currccs;
    ClassicCardScript lastccs;
    
    public GameObject gameLogic;
    public PostProcessVolume ppVolume;
    private DepthOfField dof;

    private AutobusGameScript Agaem;

    float dof_velocity = 0;

    private void Start() {
        ppVolume.profile.TryGetSettings(out dof);
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
                        lastccs.hover = false;
                        lastccs.stackPush = 0f;
                    }
                    lastccs = currccs;
                    currccs.stackPush = 0.05f;  
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
        if (Physics.Raycast(mouseRay, out hit, 50f)) {
            float target = Vector3.Distance(transform.position, hit.point);
            float current = dof.focusDistance.value;
            //dof.focusDistance.value = Mathf.Lerp(current, target, Mathf.PingPong(Time.time * 0.01f, 1));
            dof.focusDistance.value = Mathf.SmoothDamp(current, target, ref dof_velocity, 0.2f);
        } 
    }
}
