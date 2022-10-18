using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void Menu1() {
        menu1.SetActive(true);
        menu2.SetActive(false);
        menu3.SetActive(false);
    }

    public void Menu2() {
        menu1.SetActive(false);
        menu2.SetActive(true);
        menu3.SetActive(false);
    }

    public void Menu3() {
        menu1.SetActive(false);
        menu2.SetActive(false);
        menu3.SetActive(true);
    }

    public void VRoom() {          
        SceneManager.LoadSceneAsync("ValkaScene");
    }

    public void ARoom() {
        SceneManager.LoadSceneAsync("MainScene");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
