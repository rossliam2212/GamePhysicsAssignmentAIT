using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers {
    public class MenuOptions : MonoBehaviour {
        public void StartGame() { SceneManager.LoadScene("Factory"); }
        public void Restart() { SceneManager.LoadScene("Factory"); }
        public void Quit() { Application.Quit(); }
    }
}