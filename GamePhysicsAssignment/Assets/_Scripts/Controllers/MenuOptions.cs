using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers {
    public class MenuOptions : MonoBehaviour {
        
        /// <summary>
        /// Starts the game by loading into the first level.
        /// </summary>
        public void StartGame() { SceneManager.LoadScene("Factory"); }

        /// <summary>
        /// Checks what scene is currently in player and restarts that scene.
        /// </summary>
        public void Restart() {
            if (SceneManager.GetActiveScene().buildIndex == 2)
                SceneManager.LoadScene("Robot"); 
            else
                SceneManager.LoadScene("Factory"); 
        }

        /// <summary>
        /// Loads the Main Menu Scene.
        /// </summary>
        public void MainMenu() { SceneManager.LoadScene("StartMenu"); }
        
        /// <summary>
        /// Quits the application.
        /// </summary>
        public void Quit() { Application.Quit(); }
    }
}