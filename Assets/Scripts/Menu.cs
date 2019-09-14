using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{   
    public GameObject click;
    void AudioKlick() {
		click.GetComponent<AudioSource>().Play();
	}
    public void loadGame(int a){
        AudioKlick();
        switch (a){
            case 1:
            SceneManager.LoadScene(1);
            break;
            case 2:
            SceneManager.LoadScene(2);
            break;
            case 3:
            Application.Quit();
		    Debug.Log("Exit pressed!");
            break;
            case 4:
            SceneManager.LoadScene(0);
            break;
            case 5:
            SceneManager.LoadScene(3);
            break;
        }
    }
}
