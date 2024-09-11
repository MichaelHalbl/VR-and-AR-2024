using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Method to start the first game
    public void StartGame()
    {
        Debug.Log("Start Game clicked");
       // SceneManager.LoadScene("GameScene1"); 
    }

    // Method to quit the game
    public void QuitGame()
    {
        Debug.Log("Game is quitting");
        Application.Quit();
    }
}