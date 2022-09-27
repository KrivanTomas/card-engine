using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class idk : MonoBehaviour
{
    [System.NonSerialized]
    public CardAreaScript cAreaScript;
    public CardAreaScript handAreaScript;

    public GameObject tableViewObject;

    bool mttLerp = false;
    public float mttLerpTime = 0f;
    private Vector3 mttLerpPosA;
    private Quaternion mttLerpRotA;

    Camera cam;
    GameObject lastCard;

    Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        mttLerpPosA = cam.transform.position;
        mttLerpRotA = cam.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(mouseRay, out hit, 50f, 1 << LayerMask.NameToLayer("Card"))){
            ClassicCardScript ccs = hit.collider.gameObject.GetComponent<ClassicCardScript>();

            if (!ccs.Initialized) //initialize a card as a king of hearts
            {
                ccs.CcObject = (ClassicCardObject)AssetDatabase.LoadAssetAtPath("Assets/Scripts/new/ClassicCards/KINGOFHEARTS.asset", typeof(ClassicCardObject));
                ccs.InitProperties();
                ccs.CurrentAreaAssignedPosition = handAreaScript.AreaPosition;
                ccs.CurrentAreaAssignedRotation = handAreaScript.AreaRotation;
            }

            if(Input.GetKey(KeyCode.Mouse0))
            {
                //handAreaScript.AddCard(ccs);
                
                ccs.CurrentAreaAssignedPosition = cam.transform.position + cam.transform.forward * 0.4f;
                
                MoveCameraToTable();
            }
        }
        else if(lastCard != null){
            lastCard.GetComponent<CardBehavior>().unselect();
            lastCard = null;
        }

        if(mttLerp){
            cam.transform.position = Vector3.Slerp(mttLerpPosA, tableViewObject.transform.position, mttLerpTime);
            cam.transform.rotation = Quaternion.Slerp(mttLerpRotA, Quaternion.Euler(81f, 0f, 0f), mttLerpTime);
            mttLerpTime += 2f * Time.deltaTime;
            if(mttLerpTime >= 1f){
                cam.transform.position = tableViewObject.transform.position;
                mttLerp = false;
            }
        }
    }

    public void MoveCameraToTable(){
        mttLerp = true;
    }
}
