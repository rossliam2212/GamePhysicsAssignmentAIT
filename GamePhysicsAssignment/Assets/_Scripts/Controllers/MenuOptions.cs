using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers {
    public class MenuOptions : MonoBehaviour {

        // public void Start() {
        //     SceneManager.LoadScene("Factory");
        //     print("Start Pressed");
        // }

        public void Restart() {
            SceneManager.LoadScene("Factory");
            print("Reset Pressed");
        }
        
        public void Quit() {
            Application.Quit();
            print("Quit Pressed");
        }

    }
}