using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Generatepack : MonoBehaviour
{
    public bool gen2sets = true;
    public bool includeJokers = true;
    public bool shuffle = true;

    public GameObject cardPrefab;
    public GameObject cardHideaway;

    public Transform hideawayTransform;
    private CardAreaScript cas;
    public ClassicCardObject[] ccos;
    // Start is called before the first frame update
    void Start()
    {
        cas = gameObject.GetComponent<CardAreaScript>();
        hideawayTransform = cardHideaway.GetComponent<Transform>();
    }

    // Update is called once per frame
    // void Update()
    // {

    // }

    public void Generate () {
        foreach (ClassicCardScript ccsdel in cas.cards) {
            Destroy(ccsdel.gameObject);
        }
        cas.cards.Clear();
        foreach (ClassicCardObject cco in ccos) {
            GameObject temp_card = Instantiate(cardPrefab, hideawayTransform);
            temp_card.name = cco.name;
            ClassicCardScript ccs = temp_card.GetComponent<ClassicCardScript>();
            ccs.InitProperties(cco);
            ccs.flipped = true;
            cas.cards.Add(ccs);
            ccs.CardArea = cas;
        }
        if (gen2sets) {
            foreach (ClassicCardObject cco in ccos) {
                GameObject temp_card = Instantiate(cardPrefab, hideawayTransform);
                temp_card.name = cco.name;
                ClassicCardScript ccs = temp_card.GetComponent<ClassicCardScript>();
                ccs.InitProperties(cco);
                ccs.flipped = true;
                cas.cards.Add(ccs);
                ccs.CardArea = cas;
            }
        }
        if (shuffle) {

            cas.Shuffle();
            //Random rand = new Random();
            // Random.InitState
            // var models = garage.OrderBy(c => rand.Next()).Select(c => c.Model).ToList();
        }
    }
}
