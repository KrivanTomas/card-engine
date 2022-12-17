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

    public bool debugAC = false;
    // Start is called before the first frame update
    void Start()
    {
        gen = startingDeck.gameObject.GetComponent<Generatepack>();
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(handArea[0].cards.Count <= 0){
            Debug.Log("Player 1 wins.");
            StartGame();
        }
        else if(handArea[1].cards.Count <= 0){
            Debug.Log("Player 0 wins.");
            StartGame();
        }
        if(debugAC){
            DebugAutoClicker();
        }
    }

    public void DebugAutoClicker(){
        Action(handArea[0].cards[handArea[0].cards.Count-1], 0);
        Action(gameArea[0].cards[gameArea[0].cards.Count-1], 0);
    }

    public void StartGame()
    {
        foreach(ClassicCardScript card in handArea[0].cards) {
            Destroy(card.gameObject);
        }
        foreach(ClassicCardScript card in handArea[1].cards) {
            Destroy(card.gameObject);
        }
        handArea[0].cards.Clear();
        handArea[1].cards.Clear();

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
        }
        else if(value1 < value2){
            CollectCards(1);
        }
        else {
            for(int i = 0; i < 3; i++){
                Draw();
            }
        }
    }

    private void Draw() //draws a card from hand area and places it onto the game area, with the enemy doing the same
    {
        if (handArea[0].cards.Count != 0){
            MoveCard(handArea[0].cards[handArea[0].cards.Count-1], gameArea[0], false);
        }
        else{
            MoveCard(handArea[1].cards[handArea[1].cards.Count-1], gameArea[0], false);
        } 
        gameArea[0].totalStacking = gameArea[0].cards.Count * 0.0218f;

        if (handArea[1].cards.Count != 0){ // bot
            MoveCard(handArea[1].cards[handArea[1].cards.Count-1], gameArea[1], false);
        }
        else{
            MoveCard(handArea[0].cards[handArea[0].cards.Count-1], gameArea[1], false);
        }
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
