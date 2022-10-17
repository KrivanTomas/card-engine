using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicCardScript : MonoBehaviour
{
    private ClassicCardObject.ccValue card_value; // ACE,TWO,THREE,..,TEN,JACK,QUEEN,KING,JOKER
    private ClassicCardObject.ccColor card_color; // BLACK/RED
    private ClassicCardObject.ccSymbol card_symbol; // HEARTS/SPADES/DIAMONDS/CLUBS

    private bool initialized = false;
    private bool animating = false;

    private CardAreaScript cardArea;
    private ClassicCardObject ccObject;
    
    // private Vector3 currentAreaAssignedPosition;
    // private Vector3 nextAreaAssignedPosition;

    // private Quaternion currentAreaAssignedRotation; 
    // private Quaternion nextAreaAssignedRotation;

    public ClassicCardObject.ccValue Card_value { get => card_value;}
    public ClassicCardObject.ccColor Card_color { get => card_color;}
    public ClassicCardObject.ccSymbol Card_symbol { get => card_symbol;}

    // public Vector3 CurrentAreaAssignedPosition { get => currentAreaAssignedPosition; set => currentAreaAssignedPosition = value; }
    // public Vector3 NextAreaAssignedPosition { get => nextAreaAssignedPosition; set => nextAreaAssignedPosition = value; }
    // public Quaternion CurrentAreaAssignedRotation { get => currentAreaAssignedRotation; set => currentAreaAssignedRotation = value; }
    // public Quaternion NextAreaAssignedRotation { get => nextAreaAssignedRotation; set => nextAreaAssignedRotation = value; }

    public ClassicCardObject CcObject { get => ccObject; set => ccObject = value; } 
    public CardAreaScript CardArea { get => cardArea; set => cardArea = value; }

    public bool Initialized { get => initialized; }
    public bool Animating { get => animating; set => animating = value; }

    public bool selected = false;
    public bool hover = false;
    public bool flipped = false;
    public Vector3 rotation = new Vector3(0f,0f,0f);
    public float offset = 0f; //should be Vector3 but am dumb (┬┬﹏┬┬) 
    public float stackPush = 0f;
    public float stackPushFix = 0f;

    public GameObject highlight;
    private GameObject temp_highlight = null;
    private Transform cardTransform;
    private BoxCollider boxColl;
    // Start is called before the first frame update
    void Start()
    {
        //InitProperties();
        boxColl = GetComponent<BoxCollider>();
        cardTransform = GetComponent<Transform>();
    }

    // 6

    // Update is called once per frame
    void Update()
    {
        if ((hover || selected) && !temp_highlight) {
            AddHighlight();
        }
        else if (!(hover || selected) && temp_highlight) {
            RemoveHighlight();
        }
        if (selected) {
            offset = 0.02f;
        } else {
            offset = 0f;
        }



        boxColl.center = -Vector3.up * offset * (cardArea.areaType == "hand" && selected ? 1 : 0) - Vector3.right * stackPushFix * (cardArea.areaType == "hand" ? 1 : 0);
        // if(!initialized || !animating) return;

        // animation code
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

    public void AddHighlight() {
        if(!temp_highlight){
            temp_highlight = Instantiate(highlight, cardTransform);
        }
    }
    public void RemoveHighlight() {
        if (temp_highlight) {
            Destroy(temp_highlight);
            temp_highlight = null;
        }
    }
}