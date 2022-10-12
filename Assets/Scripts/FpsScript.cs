using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI tmp;
    private float t = 0;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > 1) {
            t = 0;
            tmp.text = Mathf.Pow(Time.deltaTime, -1).ToString("f0");
        }
    }
}
