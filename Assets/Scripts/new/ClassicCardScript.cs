using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicCardScript : MonoBehaviour
{
    private ClassicCardObject.ccValue card_value; // ACE,TWO,THREE,..,TEN,JACK,QUEEN,KING,JOKER
    private ClassicCardObject.ccColor card_color; // BLACK/RED
    private ClassicCardObject.ccSymbol card_symbol; // HEARTS/SPADES/DIAMONDS/CLUBS


    private bool updatePosition = true;
    private bool initialized = false;
    private bool animating = false;

    private GameObject cardArea;
    private ClassicCardObject ccObject;
    
    private Vector3 currentAreaAssignedPosition;
    private Vector3 nextAreaAssignedPosition;
    private Vector3 lastPosition;

    private Quaternion currentAreaAssignedRotation; 
    private Quaternion nextAreaAssignedRotation;
    private Quaternion lastRotation;

    public ClassicCardObject.ccValue Card_value { get => card_value; }
    public ClassicCardObject.ccColor Card_color { get => card_color; }
    public ClassicCardObject.ccSymbol Card_symbol { get => card_symbol; }

    public Vector3 CurrentAreaAssignedPosition { get => currentAreaAssignedPosition; set => currentAreaAssignedPosition = value; }
    public Vector3 NextAreaAssignedPosition { get => nextAreaAssignedPosition; set => nextAreaAssignedPosition = value; }
    public Quaternion CurrentAreaAssignedRotation { get => currentAreaAssignedRotation; set => currentAreaAssignedRotation = value; }
    public Quaternion NextAreaAssignedRotation { get => nextAreaAssignedRotation; set => nextAreaAssignedRotation = value; }

    public ClassicCardObject CcObject { get => ccObject; set => ccObject = value; } 

    public bool Initialized { get => initialized; }

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        lastRotation = transform.rotation;

        InitProperties();
    }

    // Update is called once per frame
    void Update()
    {
        if(!initialized || !updatePosition) return;

        if(animating){
            // animation code
            return;
        }

        if(lastPosition == currentAreaAssignedPosition && lastRotation == currentAreaAssignedRotation) return;

        lastPosition = currentAreaAssignedPosition;
        lastRotation = currentAreaAssignedRotation;

        transform.position = currentAreaAssignedPosition;
        transform.rotation = currentAreaAssignedRotation;
    }

    public void InitProperties () { // Attempts to set the properties from the scriptable object.
        initialized = false;
        if(ccObject == null) return;
        card_value = ccObject.card_value;
        card_color = ccObject.card_color;
        card_symbol = ccObject.card_symbol;
        gameObject.GetComponent<MeshRenderer>().material = ccObject.card_material;
        initialized = true;
    }

    public void InitProperties (ClassicCardObject newccObject) { // Sets the properties from the new scriptable object
        initialized = false;
        ccObject = newccObject;
        card_value = ccObject.card_value;
        card_color = ccObject.card_color;
        card_symbol = ccObject.card_symbol;
        gameObject.GetComponent<MeshRenderer>().material = ccObject.card_material;
        initialized = true;
    }
}
