using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Generatepack : MonoBehaviour
{
    public bool generate = false;
    public GameObject card;

    private CardAreaScript cas;
    public ClassicCardObject[] ccos;
    // Start is called before the first frame update
    void Start()
    {
        cas = gameObject.GetComponent<CardAreaScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(generate){
            generate = false;
            foreach(ClassicCardScript ccsdel in cas.cards){
                Destroy(ccsdel.gameObject);
            }
            cas.cards.Clear();
            foreach(ClassicCardObject cco in ccos){
                GameObject temp_card = Instantiate(card);
                ClassicCardScript ccs = temp_card.GetComponent<ClassicCardScript>();
                ccs.InitProperties(cco);
                cas.cards.Add(ccs);
            }
        }    
    }
}
