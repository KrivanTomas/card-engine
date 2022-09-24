using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUtility : MonoBehaviour
{
    public int cardCount;
    public float cardSpacing;
    public float cardThickness;
    public List<Vector3> CardPositions;
    public GameObject CardTest;

    float idk;
    float idk2;
    public List<GameObject> Cards;
    public bool spawn = true;
    int lastCount;
    // Start is called before the first frame update
    void Start()
    {
        lastCount = cardCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(cardCount != lastCount) {
            spawn = true;
            lastCount = cardCount;
        }
        idk = cardSpacing / (cardCount + 1f);
        idk2 = -cardSpacing / 2f;
        CardPositions.Clear();
        for(int i = 0; i < cardCount; i++){
            idk2 += idk;
            CardPositions.Add(transform.position + transform.right * idk2 + -transform.forward * cardThickness * i);
        }
        if (spawn)
        {
            foreach(GameObject card in Cards){
                Destroy(card, 0f);
            }
            Cards.Clear();
            spawn = false;
            for(int i = 0; i < CardPositions.Count; i++){
                Cards.Add(Instantiate(CardTest, CardPositions[i], transform.rotation));
            }
        }
        for(int i = 0; i < cardCount; i++){
            Cards[i].transform.position = CardPositions[i];
            Cards[i].transform.rotation = transform.rotation;
        }
    }
}
