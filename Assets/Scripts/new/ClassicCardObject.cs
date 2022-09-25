using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Classic Card", menuName = "ClassicCard")]
public class ClassicCardObject : ScriptableObject
{
    public ccValue card_value; // ACE,TWO,THREE,..,TEN,JACK,QUEEN,KING,JOKER
    public ccColor card_color; // BLACK/RED
    public ccSymbol card_symbol; // HEARTS/SPADES/DIAMONDS/CLUBS
    public Material card_material;

    public enum ccValue {
        ACE,
        TWO,
        THREE,
        FOUR,
        FIVE,
        SIX,
        SEVEN,
        EIGHT,
        NINE,
        TEN,
        JACK,
        QUEEN,
        KING,
        JOKER
    };

    public enum ccColor {
        BLACK,
        RED
    }

    public enum ccSymbol {
        HEARTS,
        SPADES,
        DIAMONDS,
        CLUBS,
        NONE
    }

    private void OnValidate() {
        if(card_value == ccValue.JOKER) {
            card_symbol = ccSymbol.NONE;
            return;
        }
        if(card_symbol == ccSymbol.HEARTS || card_symbol == ccSymbol.DIAMONDS) card_color = ccColor.RED;
        else card_color = ccColor.BLACK;
    }
}
