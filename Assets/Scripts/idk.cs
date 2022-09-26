using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class idk : MonoBehaviour
{
    Camera cam;
    GameObject lastCard;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(mouseRay, out hit, 50f, 1 << LayerMask.NameToLayer("Card"))){
            ClassicCardScript ccs = hit.collider.gameObject.GetComponent<ClassicCardScript>();
            if (!ccs.Initialized)
            {
                ccs.CcObject = (ClassicCardObject)AssetDatabase.LoadAssetAtPath("Assets/Scripts/new/ClassicCards/KINGOFHEARTS.asset", typeof(ClassicCardObject));
                ccs.InitProperties();
            }
        }
        else if(lastCard != null){
            lastCard.GetComponent<CardBehavior>().unselect();
            lastCard = null;
        }
    }
}
