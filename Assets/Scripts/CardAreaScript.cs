using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CardAreaScript : MonoBehaviour
{
    public StackingDirection stackingDirection = StackingDirection.RIGHT;
    public StackOrigin stackOrigin = StackOrigin.CENTER;
    public CardSpacing cardSpacing = CardSpacing.FROMAREA;
    public bool stableOrigin = false;

    public int cardCount;
    public float cardWidth;
    public float cardHeight;
    public float cardAreaLength;
    [Range(0f,1f)]
    public float totalStacking;

    public Material mat;
    public GameObject aCard;
    LineRenderer lr;

    public GameObject[] cards;
    public Vector3[] cardPositions;

    // Start is called before the first frame update
    void Start()
    {
        lr = gameObject.AddComponent<LineRenderer>();
        lr.material = mat;
        lr.startWidth = 0.01f; lr.endWidth = 0.01f;
        lr.startColor = Color.cyan; lr.endColor = Color.magenta;
    }

    // Update is called once per frame
    void Update()
    {
        float[] localTransform = new float[cardCount];
        
        Vector3 tempStackDir = new Vector3();
        switch(stackingDirection){
            case (StackingDirection.RIGHT):
                tempStackDir = transform.right; break;
            case (StackingDirection.LEFT):
                tempStackDir = -transform.right; break;
            case (StackingDirection.UP):
                tempStackDir = transform.up; break;
            case (StackingDirection.DOWN):
                tempStackDir = -transform.up; break;
        }

        float tempCardSpacing = 0f;
        switch(cardSpacing){
            case (CardSpacing.FROMAREA):
                tempCardSpacing = cardAreaLength / (cardCount + 1); break;
            case (CardSpacing.FROMCARDS):
                tempCardSpacing = (stackingDirection == StackingDirection.RIGHT || stackingDirection == StackingDirection.LEFT ? cardWidth : cardHeight); break;
        }
        
        float tempStackOrigin = 0f;
        switch(stackOrigin){
            case (StackOrigin.CENTER):
                tempStackOrigin = -tempCardSpacing * (cardCount + 1) / 2f; break;
            case (StackOrigin.HIGH):
                tempStackOrigin = -tempCardSpacing * (cardCount + 1); break;
            case (StackOrigin.LOW):
                tempStackOrigin = 0f; break;
        }
        
        int tempStableOrigin = 0;
        if(!stableOrigin || stackOrigin == StackOrigin.CENTER){
            tempStableOrigin = 1;
        }else if(stackOrigin == StackOrigin.HIGH){
            tempStableOrigin = 2;
        }else{
            tempStableOrigin = 0;
        }

        //do smth
        for(int icard = 0; icard < cardCount; icard++){
            localTransform[icard] = tempCardSpacing * (icard + tempStableOrigin) * totalStacking + tempStackOrigin * totalStacking;
        }

        // smth from local space to world space
        cardPositions = new Vector3[cardCount];
        for(int icard = 0; icard < cardCount; icard++){

            cardPositions[icard] = tempStackDir * localTransform[icard] + transform.position + (-transform.forward * 0.00030f * icard );    
        }









        // Set line (debug)
        lr.positionCount = cardCount;
        lr.SetPositions(cardPositions);

        // Set cards (debug)
        if(cardCount != cards.Length){
            foreach(GameObject card in cards){
                Destroy(card);
            }
            cards = new GameObject[cardCount];
            for (int i = 0; i < cardCount; i++){
                cards[i] = Instantiate(aCard);
                ClassicCardScript ccs = cards[i].GetComponent<ClassicCardScript>();
                switch(i){
                    case 0:
                        ccs.InitProperties((ClassicCardObject)AssetDatabase.LoadAssetAtPath("Assets/Scripts/new/ClassicCards/ACEOFHEARTS.asset", typeof(ClassicCardObject)));
                        break;
                    case 1:
                        ccs.InitProperties((ClassicCardObject)AssetDatabase.LoadAssetAtPath("Assets/Scripts/new/ClassicCards/TWOOFHEARTS.asset", typeof(ClassicCardObject)));
                        break;
                    case 2:
                        ccs.InitProperties((ClassicCardObject)AssetDatabase.LoadAssetAtPath("Assets/Scripts/new/ClassicCards/THREEOFHEARTS.asset", typeof(ClassicCardObject)));
                        break;
                    case 3:
                        ccs.InitProperties((ClassicCardObject)AssetDatabase.LoadAssetAtPath("Assets/Scripts/new/ClassicCards/FOUROFHEARTS.asset", typeof(ClassicCardObject)));
                        break;
                    default:
                        break;
                }
                
            }  
        }
        

        for (int i = 0; i < cardCount; i++){
            cards[i].transform.position = cardPositions[i];
            cards[i].transform.rotation = transform.rotation;
        }
    }

    public enum StackingDirection{
        RIGHT,
        LEFT,
        UP,
        DOWN
    }

    public enum StackOrigin {
        CENTER,
        HIGH,
        LOW
    }

    public enum CardSpacing {
        FROMAREA,
        FROMCARDS
    }

    public enum CardArching {
        NONE,
        CIRCLE,
        SPIKE,
        ARCH
    }
}
