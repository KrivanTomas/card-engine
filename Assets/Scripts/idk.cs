using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class idk : MonoBehaviour
{

    Camera cam;
    ClassicCardScript currccs;
    ClassicCardScript lastccs;

    private void Start() {
        cam = GetComponent<Camera>();
    }

    private void Update() {
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(mouseRay, out hit, 50f, 1 << LayerMask.NameToLayer("Card"))){
            currccs = hit.collider.gameObject.GetComponent<ClassicCardScript>();
            if (currccs != lastccs){
                if(currccs){
                    if (lastccs){
                        lastccs.offset = 0f;
                        lastccs.stackPush = 0f;
                    }
                    lastccs = currccs;
                    currccs.offset = 0.03f;
                    currccs.stackPush = 0.08f;  
                } 
            }
        }
        else {
            if (lastccs){
                lastccs.offset = 0f;
                lastccs.stackPush = 0f;
            }
        }
    }

//     [System.NonSerialized]
//     public CardAreaScript cAreaScript;
//     public CardAreaScript handAreaScript;

//     public GameObject tableViewObject;
//     public GameObject headViewObject;

//     private ClassicCardScript ccs;
//     private bool touchedCard = false;

//     bool mttLerp = false;
//     public float mttLerpTime = 0f;
//     private Vector3 mttLerpPosA;
//     private Quaternion mttLerpRotA;
//     private bool mttDirection;

//     Camera cam;
//     GameObject lastCard;

//     private Vector3 mousePos;
//     private bool cardFollowMouse;

//     // Start is called before the first frame update
//     void Start()
//     {
//         cam = GetComponent<Camera>();
//         mttLerpPosA = cam.transform.position;
//         mttLerpRotA = cam.transform.rotation;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
//         RaycastHit hit;
//         if(Physics.Raycast(mouseRay, out hit, 50f, 1 << LayerMask.NameToLayer("Card"))){
//             ccs = hit.collider.gameObject.GetComponent<ClassicCardScript>();

//             if (!ccs.Initialized) //initialize a card as a king of hearts //why tho
//             {
//                 ccs.CcObject = (ClassicCardObject)AssetDatabase.LoadAssetAtPath("Assets/Scripts/new/ClassicCards/KINGOFHEARTS.asset", typeof(ClassicCardObject));
//                 ccs.InitProperties();
//             }

//             if(Input.GetKey(KeyCode.Mouse0) && !touchedCard)
//             {
//                 //handAreaScript.AddCard(ccs);
//                 touchedCard = true;

//                 if(cam.transform.position == tableViewObject.transform.position){
//                 mttDirection = false; //false = to head
//                 MoveCameraToTable();
//                 return;
//                 }
//                 mttDirection = true; //true = to table
//                 MoveCameraToTable();
//             }
//         }
//         else if(lastCard != null){
//             //lastCard.GetComponent<CardBehavior>().unselect();
//             lastCard = null;
//         }

//         if(mttLerp){
//             cam.transform.position = Vector3.Slerp(mttLerpPosA, tableViewObject.transform.position, mttLerpTime);
//             cam.transform.rotation = Quaternion.Slerp(mttLerpRotA, Quaternion.Euler(81f, 0f, 0f), mttLerpTime);

            

//             mttLerpTime += 2f * Time.deltaTime * (mttDirection ? 1f : -1f);

//             if(mttDirection){
//                 if(mttLerpTime >= 1f){
//                     ccs.CurrentAreaAssignedRotation = Quaternion.Euler(-90f, 0f, 0f);
//                     cam.transform.position = tableViewObject.transform.position;
//                     mttLerp = false;
//                     touchedCard = false;
//                 }
//             }
//             else if(mttLerpTime <= 0f){
//                 ccs.CurrentAreaAssignedRotation = Quaternion.Euler(0f, 0f, 0f);
//                 cam.transform.position = headViewObject.transform.position;
//                 mttLerp = false;
//                 touchedCard = false;
//             }
//         }
//     }

//     public void MoveCameraToTable(){
//         mttLerp = true;
//     }
}
