using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValkaGameScript : MonoBehaviour
{
    public CardAreaScript[] handArea;
    public CardAreaScript[] gameArea;
    public CardAreaScript startingDeck;
    
    int playerCount = 2;
    int playerTurn = 0;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < playerCount; i++){
            handArea[i].ownershipID = i;
            gameArea[i].ownershipID = i;
        }
        startingDeck.gameObject.GetComponent<Generatepack>().Generate();
        int count = startingDeck.cards.Count;
        for(int i = 0; i < count; i++){
            if(count / 2 <= i)
                DrawCardFromDeck(0, true);
            else 
                DrawCardFromDeck(1, true);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Action(ClassicCardScript ccs, int playerID)
    {
        //if(playerID != playerTurn) return;
        if(ccs.CardArea == handArea[1]) return;
        
        ccs.hover = false;
        ccs.CardArea.cards[ccs.CardArea.cards.Count-1].hover = true;

        MoveCard(handArea[playerID].cards[handArea[playerID].cards.Count-1], gameArea[playerID]);
        
        //super high level AI
        MoveCard(handArea[1].cards[handArea[1].cards.Count-1], gameArea[1]);
    }

    private void MoveCard(ClassicCardScript ccs, CardAreaScript new_cas) {
        CardAreaScript old_cas = ccs.CardArea;
        old_cas.cards.Remove(ccs);
        new_cas.cards.Add(ccs);
        ccs.selected = false;
        ccs.CardArea = new_cas;
    }
    private void MoveCard(ClassicCardScript ccs, CardAreaScript new_cas, bool flip) {
        CardAreaScript old_cas = ccs.CardArea;
        old_cas.cards.Remove(ccs);
        new_cas.cards.Add(ccs);
        ccs.selected = false;
        ccs.CardArea = new_cas;
        ccs.flipped = flip;
    }

    private void DrawCardFromDeck(int playerID, bool flip) {
        ClassicCardScript temp_ccs = startingDeck.cards[startingDeck.cards.Count - 1];
        MoveCard(temp_ccs, handArea[playerID]);
        temp_ccs.flipped = flip;
    }
    private void DrawCardFromDeck(int playerID) {
        ClassicCardScript temp_ccs = startingDeck.cards[startingDeck.cards.Count - 1];
        MoveCard(temp_ccs, handArea[playerID]);
        temp_ccs.flipped = false;
    }
}
