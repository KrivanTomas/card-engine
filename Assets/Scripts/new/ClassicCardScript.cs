using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicCardScript : MonoBehaviour
{
    string card_value; // ACE,TWO,THREE,..,TEN,JESTER,QEEN,KING,JOKER
    string card_color; // BLACK/RED
    string card_symbol; // HEARTS/SPADES/DIAMONDS/CLUBS


    bool updatePosition = true;
    bool initialized = false;

    GameObject cardArea;
    ClassicCardObject ccObject;
    
    Vector3 CurrentAreaAssignedPosition;
    Vector3 NextAreaAssignedPosition;
    Vector3 LastPosition;

    public string Card_value { get; }
    public string Card_color { get; }
    public string Card_symbol { get; }

    // Start is called before the first frame update
    void Start()
    {
        initialized = InitProperties();
    }

    // Update is called once per frame
    void Update()
    {
        if(!initialized || !updatePosition) return;

        if(LastPosition == transform.position) return;


    }

    bool InitProperties () { // Attempts to set the properties from the scriptable object. Returns true if suceeded.
        if(ccObject != null){
            card_value = ccObject.card_value;
            card_color = ccObject.card_color;
            card_symbol = ccObject.card_symbol;
            gameObject.GetComponent<MeshRenderer>().material = ccObject.card_material;
            return true;
        }
        return false;
    }

    void InitProperties (ClassicCardObject newccObject) { // Sets the properties from the new scriptable object
        ccObject = newccObject;
        card_value = ccObject.card_value;
        card_color = ccObject.card_color;
        card_symbol = ccObject.card_symbol;
        gameObject.GetComponent<MeshRenderer>().material = ccObject.card_material;
        initialized = true;
    }
}
