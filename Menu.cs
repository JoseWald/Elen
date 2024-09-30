using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //public AudioSource audio;
    // Start is called before the first frame update


    public void Play(){
        SceneManager.LoadScene("SampleScene");
    }

    public void Exit(){
        Application.Quit();
        Debug.Log("Fermeture en cours");
    }

}   
