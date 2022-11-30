using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValkaGameScript : GameLogicAbstract
{
    public CardAreaScript[] handArea;
    public CardAreaScript[] gameArea;
    public CardAreaScript startingDeck;
    private CardAreaScript allGameCards;

    private Generatepack gen;
    
    //bool roundEnded = false;
    int playerCount = 2;
    //int playerTurn = 0;

    // Start is called before the first frame update
    void Start()
    {
        gen = startingDeck.gameObject.GetComponent<Generatepack>();
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(handArea[0].cards.Count == 0 || handArea[1].cards.Count == 0){
            print("The round has ended.");
            StartGame();
        }
    }

    public void StartGame()
    {
        handArea[0].cards.Clear();
        handArea[1].cards.Clear();
        for(int i = 0; i < gen.cardHideaway.GetComponent<Transform>().childCount-1; i++){
            Destroy(gen.cardHideaway.GetComponent<Transform>().GetChild(i).gameObject);
        }

        for(int i = 0; i < playerCount; i++){
            handArea[i].ownershipID = i;
        }

        gen.Generate();
        int count = startingDeck.cards.Count;
        for(int i = 0; i < count; i++){
            if(count / 2 <= i)
                DrawCardFromDeck(startingDeck, handArea[0], true);
            else 
                DrawCardFromDeck(startingDeck, handArea[1], true);
        }
        audioSource.Stop();
    }

    public void Action(ClassicCardScript ccs, int playerID)
    {
        //if(playerID != playerTurn) return;
        if(ccs.CardArea == handArea[1]) return;
        else if(ccs.CardArea != handArea[0]) {
            CompareCards();
            return;
        }
        else 
            if(gameArea[0].cards.Count > 0)
                return;
        
        ccs.hover = false;
        ccs.CardArea.cards[ccs.CardArea.cards.Count-1].hover = true;
        ccs.selected = false;
        ccs.CardArea.cards[ccs.CardArea.cards.Count-1].selected = true;
        
        Draw();
    }

    private void CompareCards()
    {
        //print(((int)gameArea[0].cards[gameArea[0].cards.Count-1].Card_value+1) + " jmeno: " + gameArea[0].cards[gameArea[0].cards.Count-1].Card_value);
        int value1 = (int)gameArea[0].cards[gameArea[0].cards.Count-1].Card_value + 1;
        int value2 = (int)gameArea[1].cards[gameArea[1].cards.Count-1].Card_value + 1;

        if(value1 > value2){
            CollectCards(0);
            print("player1 wins");
        }
        else if(value1 < value2){
            CollectCards(1);
            print("player2 wins");
        }
        else {
            for(int i = 0; i < 3; i++){
                Draw();
            }
        }
    }

    private void Draw() //draws a card from hand area and places it onto the game area, with the enemy doing the same
    {
        MoveCard(handArea[0].cards[handArea[0].cards.Count-1], gameArea[0], false);
        gameArea[0].totalStacking = gameArea[0].cards.Count * 0.0218f;

        MoveCard(handArea[1].cards[handArea[1].cards.Count-1], gameArea[1], false); //bot
        gameArea[1].totalStacking = gameArea[1].cards.Count * 0.0218f;
    }

    private void CollectCards(int playerID) //collects cards from all game areas and inserts them into playerID's hand area
    {
        for(int pc = 0; pc < playerCount; pc++)
        {
            for(int i = gameArea[pc].cards.Count-1; i >= 0; i--)
            {
                MoveCard(gameArea[pc].cards[i], handArea[playerID], true);
            }
        }
        handArea[playerID].Shuffle();
    }
}
