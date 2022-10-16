using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAreaScript : MonoBehaviour
{
    public StackingDirection stackingDirection = StackingDirection.RIGHT;
    public StackOrigin stackOrigin = StackOrigin.CENTER;
    public CardSpacing cardSpacing = CardSpacing.FROMAREA;
    public bool stableOrigin = false;
    public string areaType;
    public ClassicCardObject.ccSymbol areaSymbol = ClassicCardObject.ccSymbol.NONE;
    public int ownershipID = -1;

    public int cardCount;
    public float cardThicc;
    public float cardWidth;
    public float cardHeight;
    public float cardAreaLength;
    [Range(0f,1f)]
    public float totalStacking;

    public bool lineDebug;
    public Material mat;
    public GameObject cardPrefab;

    public List<ClassicCardScript> cards;

    private Vector3[] cardPositions;
    private LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        cardWidth = cardPrefab.transform.localScale.x * 0.064f;
        cardHeight = cardPrefab.transform.localScale.y * 0.089f;
        cardThicc = cardPrefab.transform.localScale.z * 0.0003f;
        lr = gameObject.AddComponent<LineRenderer>();
        lr.material = mat;
        lr.startWidth = 0.01f; lr.endWidth = 0.01f;
        lr.startColor = Color.cyan; lr.endColor = Color.magenta;
        lr.enabled = lineDebug;
    }

    // Update is called once per frame
    void Update()
    {
        cardCount = cards.Count;
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
        float wtf = 0;
        for(int icard = 0; icard < cardCount; icard++){
            localTransform[icard] = (tempCardSpacing * (icard + tempStableOrigin)+wtf) * totalStacking + tempStackOrigin * totalStacking;
            wtf += cards[icard].stackPush;
        }   

        // smth from local space to world space
        cardPositions = new Vector3[cardCount];
        for(int icard = 0; icard < cardCount; icard++){

            cardPositions[icard] = tempStackDir * localTransform[icard] + transform.position + (-transform.forward * cardThicc * icard ); 
            cards[icard].gameObject.transform.position = cardPositions[icard];// + transform.up * cards[icard].offset; 
            cards[icard].rotation = cards[icard].flipped ? new Vector3(0f,180f,0f) : new Vector3(0f,0f,0f);
            cards[icard].gameObject.transform.rotation = transform.rotation * Quaternion.Euler(cards[icard].rotation);  
        }



        //Set line (debug)
        lr.enabled = lineDebug;
        if(lineDebug){
            lr.positionCount = cardCount;
            lr.SetPositions(cardPositions);
        }
    }

    public void Shuffle () {
        for (int i = 0; i < cards.Count; i++) {
            ClassicCardScript temp = cards[i];
            int randomIndex = Random.Range(i, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }   

    public void DeleteCard (ClassicCardScript card) {
        cards.Remove(card);
        Destroy(card.gameObject);
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
