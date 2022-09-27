using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAreaScript : MonoBehaviour
{
    private ClassicCardScript ccObject;
    private Vector3 areaPosition;
    private Quaternion areaRotation;

    private List<ClassicCardScript> cardList;
    private List<Vector3> cardPositionList;
    private List<Quaternion> cardRotationList;

    public Vector3 AreaPosition { get => transform.position; set => areaPosition = value; }
    public Quaternion AreaRotation { get => transform.rotation; set => areaRotation = value; }

    //public List<ClassicCardScript> CardList { get => cardList; set => cardList = value}
    
    

    // Start is called before the first frame update
    void Start()
    {
        //if(areaPosition != null) areaPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        areaPosition = transform.position;
    }

    public void AddCard(ClassicCardScript newCard){
        //if(cardList == null) cardList[0] = newCard;
        cardList.Add(newCard);//あばない 
        cardPositionList.Add(areaPosition);             
        cardRotationList.Add(newCard.transform.rotation);
    }

    private void CardPositionInArea(){

    }
}
