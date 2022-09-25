using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Classic Card", menuName = "ClassicCard")]
public class ClassicCardObject : ScriptableObject
{
    public string card_value; // ACE,TWO,THREE,..,TEN,JESTER,QEEN,KING,JOKER
    public string card_color; // BLACK/RED
    public string card_symbol; // HEARTS/SPADES/DIAMONDS/CLUBS
    public Material card_material;
}
