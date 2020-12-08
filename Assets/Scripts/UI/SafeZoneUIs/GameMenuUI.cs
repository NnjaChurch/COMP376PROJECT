using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuUI : MonoBehaviour
{

    [SerializeField] UIManager manager_UI;

    public void SaveButtonClick()
    {
        // TODO check if SaveGame is correct
        manager_UI.SaveGame();
    }

    public void MainMenuButtonClick()
    {
        // TODO should this done in a method inside StageManager
        SceneManager.LoadScene("Main Menu");
    }
}
