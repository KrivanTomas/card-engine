using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ClassicCardScript : MonoBehaviour
{
    private ClassicCardObject.ccValue card_value; // ACE,TWO,THREE,..,TEN,JACK,QUEEN,KING,JOKER
    private ClassicCardObject.ccColor card_color; // BLACK/RED
    private ClassicCardObject.ccSymbol card_symbol; // HEARTS/SPADES/DIAMONDS/CLUBS

    private bool initialized = false;
    private bool animating = false;

    private GameObject cardArea;
    private ClassicCardObject ccObject;
    
    private Vector3 currentAreaAssignedPosition;
    private Vector3 nextAreaAssignedPosition;

    private Quaternion currentAreaAssignedRotation; 
    private Quaternion nextAreaAssignedRotation;

    private string card_skins = "Assets/Cards/ClassicCards/SPSTITA/Materials/";

    public ClassicCardObject.ccValue Card_value { get => card_value; }
    public ClassicCardObject.ccColor Card_color { get => card_color; }
    public ClassicCardObject.ccSymbol Card_symbol { get => card_symbol; }

    public Vector3 CurrentAreaAssignedPosition { get => currentAreaAssignedPosition; set => currentAreaAssignedPosition = value; }
    public Vector3 NextAreaAssignedPosition { get => nextAreaAssignedPosition; set => nextAreaAssignedPosition = value; }
    public Quaternion CurrentAreaAssignedRotation { get => currentAreaAssignedRotation; set => currentAreaAssignedRotation = value; }
    public Quaternion NextAreaAssignedRotation { get => nextAreaAssignedRotation; set => nextAreaAssignedRotation = value; }

    public ClassicCardObject CcObject { get => ccObject; set => ccObject = value; } 

    public bool Initialized { get => initialized; }
    public bool Animating { get => animating; set => animating = value; }

    public bool selected = false;
    public Vector3 rotation = new Vector3(0f,0f,0f);
    public float offset = 0f; //should be Vector3 but am dumb (┬┬﹏┬┬) 
    public float stackPush = 0f;


    private BoxCollider boxColl;
    // Start is called before the first frame update
    void Start()
    {
        InitProperties();
        boxColl = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //boxColl.center = -transform.up * offset;
        if(!initialized || !animating) return;

        // animation code
    }

    public void InitProperties () { // Attempts to set the properties from the scriptable object.
        initialized = false;
        if(ccObject == null) return;
        card_value = ccObject.card_value;
        card_color = ccObject.card_color;
        card_symbol = ccObject.card_symbol;

        gameObject.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath(card_skins + ccObject.card_material, typeof(Material));
        initialized = true;
    }

    public void InitProperties (ClassicCardObject newccObject) { // Sets the properties from the new scriptable object
        initialized = false;
        ccObject = newccObject;
        card_value = ccObject.card_value;
        card_color = ccObject.card_color;
        card_symbol = ccObject.card_symbol;
        gameObject.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath(card_skins + ccObject.card_material, typeof(Material));
        initialized = true;
    }

    public void InitProperties (string newCardSkin) { // Sets the properties from the new scriptable object
        initialized = false;
        card_skins = newCardSkin;
        gameObject.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath(card_skins + ccObject.card_material, typeof(Material));
        initialized = true;
    }

    public void InitProperties (ClassicCardObject newccObject, string newCardSkin) { // Sets the properties from the new scriptable object
        initialized = false;
        ccObject = newccObject;
        card_skins = newCardSkin;
        card_value = ccObject.card_value;
        card_color = ccObject.card_color;
        card_symbol = ccObject.card_symbol;
        gameObject.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath(card_skins + ccObject.card_material, typeof(Material));
        initialized = true;
    }
}