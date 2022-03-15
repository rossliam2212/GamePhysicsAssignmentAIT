using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {


    private void Start() {
        
    }

    private void Update() {
        
    }

    public void Restart() {
        SceneManager.LoadScene("Factory");
    }

    public void Quit() {
        Application.Quit();
    }
}
