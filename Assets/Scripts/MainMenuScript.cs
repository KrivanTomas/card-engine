using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    private int menu = 0;

    public Transform idk;
    public GameObject menu1;
    public GameObject menu2;
    public GameObject menu3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (menu) {
            case 0:
                idk.rotation *= Quaternion.Euler(0f,0f,70f*Time.deltaTime);
                break;
        }
    }
    
    public void QuitGame() {
        Application.Quit();
    }
}
