using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    //public Transform idk;
    public GameObject[] menus;
    public Slider loadingBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //idk.rotation *= Quaternion.Euler(0f,0f,70f*Time.deltaTime);
    }

    public void SwitchMenu(int index) {
        foreach (GameObject item in menus)
        {
            item.SetActive(false);
        }
        menus[index].SetActive(true);
    }

    public void LoadGame(int index) {    
              
        StartCoroutine(LoadAsync(index));
    }

    public void LoadGame(string name) {
        
        StartCoroutine(LoadAsync(name));
    }

    IEnumerator LoadAsync (int index) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        SwitchMenu(4);
        while (!operation.isDone){
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingBar.value = progress;
            yield return null;
        }
    }

    IEnumerator LoadAsync (string name) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        SwitchMenu(4);
        while (!operation.isDone){
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(progress);
            loadingBar.value = progress;
            yield return null;
        }
    }

    public void QuitGame() {
        Application.Quit();
    }
}
