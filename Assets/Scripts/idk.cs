using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            hit.collider.gameObject.GetComponent<CardBehavior>().select();
            if(lastCard != null && lastCard != hit.collider.gameObject){
                lastCard.GetComponent<CardBehavior>().unselect();
            }
            lastCard = hit.collider.gameObject;
        }
        else if(lastCard != null){
            lastCard.GetComponent<CardBehavior>().unselect();
            lastCard = null;
        }
    }
}
