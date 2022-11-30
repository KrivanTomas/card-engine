using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicAbstract : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip cardSound;

    protected void MoveCard(ClassicCardScript ccs, CardAreaScript new_cas) {
        CardAreaScript old_cas = ccs.CardArea;
        ccs.selected = false;
        old_cas.cards.Remove(ccs);
        new_cas.cards.Add(ccs);
        ccs.CardArea = new_cas;
        audioSource.PlayOneShot(cardSound);
        ccs.timePos = 0f;
        ccs.timeRot = 0f;
    }

    protected void MoveCard(ClassicCardScript ccs, CardAreaScript new_cas, bool flip) {
        CardAreaScript old_cas = ccs.CardArea;
        old_cas.cards.Remove(ccs);
        new_cas.cards.Add(ccs);
        ccs.selected = false;
        ccs.CardArea = new_cas;
        ccs.flipped = flip;
        audioSource.Stop();
        audioSource.PlayOneShot(cardSound);
        ccs.timePos = 0f;
        ccs.timeRot = 0f;
    }

    protected void SwapCards(ClassicCardScript ccs1, ClassicCardScript ccs2) {
        ccs1.selected = false;
        ccs2.selected = false;
        CardAreaScript old_cas1 = ccs1.CardArea;
        CardAreaScript old_cas2 = ccs2.CardArea;
        int index1 = old_cas1.cards.IndexOf(ccs1);
        int index2 = old_cas2.cards.IndexOf(ccs2);
        old_cas2.cards.Insert(index2, ccs1);
        old_cas1.cards.Insert(index1, ccs2);
        old_cas1.cards.Remove(ccs1);
        old_cas2.cards.Remove(ccs2);
        ccs1.CardArea = old_cas2;
        ccs2.CardArea = old_cas1;
        audioSource.PlayOneShot(cardSound);
        ccs1.timePos = 0f;
        ccs2.timePos = 0f;
        ccs1.timeRot = 0f;
        ccs2.timeRot = 0f;
    }

    protected void DrawCardFromDeck(CardAreaScript deck, CardAreaScript target) {
        ClassicCardScript temp_ccs = deck.cards[deck.cards.Count - 1];
        MoveCard(temp_ccs, target);
        temp_ccs.flipped = false;
        audioSource.PlayOneShot(cardSound);
        temp_ccs.timePos = 0f;
        temp_ccs.timeRot = 0f;
    }

    protected void DrawCardFromDeck(CardAreaScript deck, CardAreaScript target, bool flip) {
        ClassicCardScript temp_ccs = deck.cards[deck.cards.Count - 1];
        MoveCard(temp_ccs, target);
        temp_ccs.flipped = flip;
        audioSource.PlayOneShot(cardSound);
        temp_ccs.timePos = 0f;
        temp_ccs.timeRot = 0f;
    }
}
