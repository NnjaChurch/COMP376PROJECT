using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for the main menu behaviour
//	Contributors: Rubiat
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class MainMenu : MonoBehaviour {
    // Start is called before the first frame update
    public void ButtonOptionClick(string option)
    {
        if (option == "New Game")
        {
            SceneManager.LoadScene("Test Scene");
            // TODO change this to the actual scene instead of Test Scene
        }
        else if (option == "Load Game")
        {
            // TODO have to implement this
        }
        else if (option == "Exit")
        {
            Application.Quit();
        }
    }
}
