using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutobusGameScript : GameLogicAbstract
{

    private int playerTurn = 0;
    private int turnSection = 0;

    public GameObject deck;
    public GameObject dump;
    public GameObject[] hands;
    public GameObject[] banks;
    public GameObject[] tables;    
    
    public GameObject initCard;

    private CardAreaScript deck_cas;
    private CardAreaScript dump_cas;
    private CardAreaScript[] hands_cass;
    private CardAreaScript[] banks_cass;
    private CardAreaScript[] tables_cass;   

    public TMPro.TextMeshProUGUI textMeshProUGUI;
    private List<ClassicCardScript> selectedCards = new List<ClassicCardScript>();

    private int handMax = 5;
    private int bankMax = 7;

    // Start is called before the first frame update
    void Start()
    {
        deck_cas = deck.GetComponent<CardAreaScript>();
        dump_cas = dump.GetComponent<CardAreaScript>();
        hands_cass = new CardAreaScript[hands.Length];
        banks_cass = new CardAreaScript[hands.Length];
        tables_cass = new CardAreaScript[tables.Length];
        for (int i = 0; i < hands.Length; i++) {
            hands_cass[i] = hands[i].GetComponent<CardAreaScript>();
            banks_cass[i] = banks[i].GetComponent<CardAreaScript>();
        }
        for (int i = 0; i < tables.Length; i++) {
            tables_cass[i] = tables[i].GetComponent<CardAreaScript>();
        }
        deck.GetComponent<Generatepack>().Generate();
        StartGame();
        OnTurnUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // potential bugs:
    // more than 2 card areas with a symbol

    private void StartGame() {
        playerTurn = 0;
        turnSection = 0;
        foreach(CardAreaScript cas in hands_cass) {
            for (int i = 0; i < handMax; i++)
            {
                DrawCardFromDeck(deck_cas, cas);
            }
            cas.SortCards();
            turnSection++;
        }
        foreach(CardAreaScript cas in banks_cass) {
            for (int i = 0; i < bankMax; i++)
            {
                if (i < bankMax - 1) DrawCardFromDeck(deck_cas, cas, true);
                else DrawCardFromDeck(deck_cas, cas, false);
            }
        }
        foreach(CardAreaScript cas in tables_cass) {
            AddInitCard(cas);
        }
        AddInitCard(dump_cas);
        audioSource.Stop();
    }

    private void OnTurnUpdate() {
        switch (turnSection){
            case 0: {
                textMeshProUGUI.text = "Draw cards";
                break;
            }
            case 1: {
                textMeshProUGUI.text = "Select cards";
                break;
            }
            case 2: {
                textMeshProUGUI.text = "Play cards";
                break;
            }
        }

        if (deck_cas.cardCount == 0) {
            while(dump_cas.cards.Count > 0)
            {
                ClassicCardScript ccs = dump_cas.cards[0];
                MoveCard(ccs, deck_cas);
                ccs.flipped = true;
            }
            audioSource.Stop();
            audioSource.PlayOneShot(cardSound);
            deck_cas.Shuffle();
            AddInitCard(dump_cas);
        }

        foreach(CardAreaScript cas in tables_cass) {
            if (cas.cardCount == 13) {
                bool pack = true;
                foreach (ClassicCardScript ccs in cas.cards) {
                    if(ccs.Card_value == ClassicCardObject.ccValue.JOKER) {
                        pack = false;
                    }
                }
                if (pack) {
                    List<ClassicCardScript> tempcards = new List<ClassicCardScript>(cas.cards);
                    foreach (ClassicCardScript ccs in tempcards) {
                        MoveCard(ccs, deck_cas, true);
                    }
                    deck_cas.Shuffle();
                    cas.areaSymbol = ClassicCardObject.ccSymbol.NONE;
                    AddInitCard(cas);
                }
            }
        }
    }

    public void ActionRequest(ClassicCardScript target_ccs, int playerID) {
        if (playerID != playerTurn) return;
        CardAreaScript temp_cas = target_ccs.CardArea;

        //if (hands_cass[playerTurn].cardCount == handMax && turnSection == 0) turnSection++;

        Debug.Log("card: " + target_ccs.name + " in: " + temp_cas.name + " T" + turnSection + " P" + playerID); 
        switch (turnSection) {
            case 0: { // drawing cards to handMax
                switch (temp_cas.areaType) {
                    case "deck": {
                        DrawCardFromDeck(deck_cas, hands_cass[playerTurn]);
                        if (hands_cass[playerTurn].cardCount == handMax - 1) { // don't know why it has to be -1 ¯\_(ツ)_/¯
                            turnSection++;
                        }
                        OnTurnUpdate();
                        break;
                    }
                }
                break;
            }
            case 1: { // selecting 1 card
                selectedCards.Clear();
                switch (temp_cas.areaType){
                    case "hand": {
                        target_ccs.selected = true;
                        selectedCards.Add(target_ccs);
                        turnSection++;
                        break;
                    }
                    case "dump": {
                        if (!((int)target_ccs.Card_symbol == 0 && (int)target_ccs.Card_color == 0)) {
                            dump_cas.cards[dump_cas.cards.Count - 1].selected = true;
                            selectedCards.Add(dump_cas.cards[dump_cas.cards.Count - 1]);
                            turnSection++;
                            break;
                        }
                        break;
                    }
                    case "bank": {
                        ClassicCardScript leBank = temp_cas.cards[temp_cas.cards.Count - 1];
                        leBank.selected = true;
                        selectedCards.Add(leBank);
                        turnSection++;
                        break;
                    }
                }
                break;
            }
            case 2: { // selecting more cards, unselecting cards or playing them
                switch (temp_cas.areaType){
                    case "hand": {
                        // no cross selecting
                        if (selectedCards[0].CardArea.areaType == "bank" || selectedCards[0].CardArea.areaType == "dump") break;
                       
                        // unselect hand
                        if (selectedCards.Contains(target_ccs)) {
                            target_ccs.selected = false;
                            selectedCards.Remove(target_ccs);
                            if (selectedCards.Count == 0) turnSection--;
                            break;
                        }

                        // select from hand (must be the same)
                        else if (target_ccs.Card_symbol == selectedCards[0].Card_symbol) {
                            target_ccs.selected = true;
                            selectedCards.Add(target_ccs);
                        }
                        break;
                    }
                    case "bank": {
                        if (selectedCards[0].CardArea.areaType == "bank") {
                            target_ccs.selected = false;
                            selectedCards.Remove(target_ccs);
                            if (selectedCards.Count == 0) turnSection--;
                            break;
                        }
                        break;
                    }
                    case "table": { // playing cards

                        // no joker after king fix
                        if (temp_cas.cardCount > 12 && target_ccs.Card_value != ClassicCardObject.ccValue.JOKER) {
                            break;
                        }

                        // replace joker
                        if (selectedCards.Count == 1 && (int)target_ccs.Card_value == 13 && 
                        (temp_cas.areaSymbol == selectedCards[0].Card_symbol || temp_cas.areaSymbol == ClassicCardObject.ccSymbol.NONE) &&
                        (int)selectedCards[0].Card_value == temp_cas.cards.IndexOf(target_ccs)) {
                            SwapCards(selectedCards[0], target_ccs);
                            temp_cas.areaSymbol = selectedCards[0].Card_symbol;
                            turnSection--;
                            break;
                        }

                        // multiple cards validation
                        if (selectedCards.Count > 1) {
                            selectedCards.Sort((x, y) => {return x.Card_value < y.Card_value ? -1 : x.Card_value == y.Card_value ? 0 : 1;});
                            int last = (int)selectedCards[0].Card_value;
                            for(int i = 1; i < selectedCards.Count; i++) {
                                if(last + 1 != (int)selectedCards[i].Card_value) {
                                    for(int b = i; b < selectedCards.Count; b++) {
                                        selectedCards[i].selected = false;
                                    }
                                    selectedCards.RemoveRange(i, selectedCards.Count - 1);
                                    break;
                                }
                                last = (int)selectedCards[i].Card_value;
                            }
                        }
                        
                        // replace init card
                        if (temp_cas.cards.Count == 1 && target_ccs.Card_symbol == 0 && target_ccs.Card_color == 0 &&
                        (selectedCards[0].Card_value == ClassicCardObject.ccValue.ACE || (int)selectedCards[0].Card_value == 13)) { // black hearts
                            temp_cas.areaSymbol = selectedCards[0].Card_symbol;
                            temp_cas.DeleteCard(target_ccs);
                            if (selectedCards[0].CardArea.areaType == "bank") {
                                banks_cass[playerID].cards[banks_cass[playerID].cards.Count - 2].flipped = false;
                            }
                            foreach (ClassicCardScript cardToMove in selectedCards) {
                                MoveCard(cardToMove, temp_cas);
                            }
                            selectedCards = new List<ClassicCardScript>();
                            turnSection--;
                            break;
                        }

                        // what does this do?? if has the same symbol || or empty and the same count as value wtf || is joker
                        if ((temp_cas.areaSymbol == selectedCards[0].Card_symbol || temp_cas.areaSymbol == ClassicCardObject.ccSymbol.NONE) &&
                        temp_cas.cards.Count == (int)selectedCards[0].Card_value || (int)selectedCards[0].Card_value == 13) {
                            if (temp_cas.areaSymbol == ClassicCardObject.ccSymbol.NONE) {
                                temp_cas.areaSymbol = selectedCards[0].Card_symbol;
                            }
                            if (selectedCards[0].CardArea.areaType == "bank") {
                                banks_cass[playerID].cards[banks_cass[playerID].cards.Count - 2].flipped = false;
                            }
                            foreach (ClassicCardScript cardToMove in selectedCards) {
                                MoveCard(cardToMove, temp_cas);
                            }
                            selectedCards = new List<ClassicCardScript>();
                            turnSection--;
                            break;
                        }
                        break;
                    }
                    case "dump": {
                        if (selectedCards.Contains(target_ccs)) {
                            target_ccs.selected = false;
                            selectedCards.Remove(target_ccs);
                            if (selectedCards.Count == 0) turnSection--;
                            break;
                        }
                        if(selectedCards[0].CardArea.areaType == "bank") {
                            break;
                        }
                        if (selectedCards.Count == 1 && hands_cass[playerID].cardCount >= handMax) {
                            if((int)dump_cas.cards[0].Card_symbol == 0 && (int)dump_cas.cards[0].Card_color == 0){
                                dump_cas.DeleteCard(dump_cas.cards[0]);
                            }
                            MoveCard(selectedCards[0], temp_cas);
                            selectedCards = new List<ClassicCardScript>();
                            NewPlayer();
                            break;
                        }
                        break;
                    }
                }
                break;
            }
            // case 3: {
            //     if (hands_cass[playerID].cardCount < handMax){
                    
            //     }
            //     else {
            //         turnSection = 1;
            //     }
            //     break;
            // }
        }
        OnTurnUpdate();
    }

    public void EndTurn(int playerID) {
        Debug.Log(hands_cass[playerID].cardCount.ToString() + " " + selectedCards.Count.ToString());
        if(playerID == playerTurn && hands_cass[playerID].cardCount < handMax && selectedCards.Count == 0) {
            NewPlayer();
            OnTurnUpdate();
        }
    }

    private void NewPlayer() {
        playerTurn++;
        if (playerTurn > hands.Length - 1) {
            playerTurn = 0;
        }
        turnSection = 0;
        selectedCards = new List<ClassicCardScript>();
    }

    private void AddInitCard (CardAreaScript target) {
        GameObject temp = Instantiate(initCard, deck.GetComponent<Generatepack>().hideawayTransform);
        ClassicCardScript temp_ccs = temp.GetComponent<ClassicCardScript>();
        target.cards.Add(temp_ccs);
        temp_ccs.CardArea = target;
    }
}
