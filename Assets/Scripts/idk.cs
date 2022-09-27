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
    public GameObject headViewObject;

    private ClassicCardScript ccs;
    private bool touchedCard = false;

    bool mttLerp = false;
    public float mttLerpTime = 0f;
    private Vector3 mttLerpPosA;
    private Quaternion mttLerpRotA;
    private bool mttDirection;

    Camera cam;
    GameObject lastCard;

    private Vector3 mousePos;
    private bool cardFollowMouse;

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
            ccs = hit.collider.gameObject.GetComponent<ClassicCardScript>();

            if (!ccs.Initialized) //initialize a card as a king of hearts //why tho
            {
                ccs.CcObject = (ClassicCardObject)AssetDatabase.LoadAssetAtPath("Assets/Scripts/new/ClassicCards/KINGOFHEARTS.asset", typeof(ClassicCardObject));
                ccs.InitProperties();
                ccs.CurrentAreaAssignedPosition = handAreaScript.AreaPosition;
                ccs.CurrentAreaAssignedRotation = handAreaScript.AreaRotation;
            }

            if(Input.GetKey(KeyCode.Mouse0) && !touchedCard)
            {
                //handAreaScript.AddCard(ccs);
                touchedCard = true;

                if(cam.transform.position == tableViewObject.transform.position){
                mttDirection = false; //false = to head
                MoveCameraToTable();
                return;
                }
                mttDirection = true; //true = to table
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

            

            mttLerpTime += 2f * Time.deltaTime * (mttDirection ? 1f : -1f);

            if(mttDirection){
                if(mttLerpTime >= 1f){
                    ccs.CurrentAreaAssignedRotation = Quaternion.Euler(-90f, 0f, 0f);
                    cam.transform.position = tableViewObject.transform.position;
                    mttLerp = false;
                    touchedCard = false;
                }
            }
            else if(mttLerpTime <= 0f){
                ccs.CurrentAreaAssignedRotation = Quaternion.Euler(0f, 0f, 0f);
                cam.transform.position = headViewObject.transform.position;
                mttLerp = false;
                touchedCard = false;
            }
        }
    }

    public void MoveCameraToTable(){
        mttLerp = true;
    }
}
