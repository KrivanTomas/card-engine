using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehavior : MonoBehaviour
{
    Vector3 CurrentPos;
    Vector3 NextPos;
    Vector3 StablePos;

    float time = 0;
    float lerpSpeed;
    bool lerp = false;
    bool selected = false;
    // Start is called before the first frame update
    void Start()
    {
        CurrentPos = transform.position;
        StablePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (lerp){
            time += lerpSpeed * Time.deltaTime;
            if (time >= 1f){
                time = 0;
                lerp = false;
                CurrentPos = NextPos;
            }
            transform.position = Vector3.Lerp(CurrentPos, NextPos, time);
            return;
        }

        transform.position = CurrentPos;
    }

    public void lerpTo(Vector3 nextPos, float speed){
        NextPos = nextPos;
        lerp = true;
        lerpSpeed = 1 / speed;
        CurrentPos = transform.position;
        time = 0;
    }

    public void select(){
        
        if(!lerp && !selected) StablePos = transform.position;
        if (!selected)
        {
            lerpTo(StablePos + transform.up * 0.015f, 0.2f);
            selected = true;
        }
    }
    public void unselect(){
        if (selected)
        {
            lerpTo(StablePos, 0.2f);
            selected = false;
        }
    }
}
