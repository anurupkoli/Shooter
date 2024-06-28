using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void ReloadScene(){
        Debug.Log("Reloading");
        SceneManager.LoadScene(0);
    }

    public void ExitGame(){
        Application.Quit();
    }
}
