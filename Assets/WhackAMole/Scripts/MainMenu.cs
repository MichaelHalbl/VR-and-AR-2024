using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Method to start the first game
    public void StartGame1()
    {
        Debug.Log("Start Game 1 clicked");
       // SceneManager.LoadScene("GameScene1"); 
    }

    // Method to start the second game
    public void StartGame2()
    {
        Debug.Log("Start Game 2 clicked");
        //SceneManager.LoadScene("GameScene2"); 
    }
    // Method to start the third game
    public void StartGame3()
    {
        Debug.Log("Start Game 3 clicked");
       // SceneManager.LoadScene("GameScene3"); 
    }

    // Method to start the fourth game
    public void StartGame4()
    {
        Debug.Log("Start Game 4 clicked");
       // SceneManager.LoadScene("GameScene4"); 
    }

    // Method to quit the game
    public void QuitGame()
    {
        Debug.Log("Game is quitting");
        Application.Quit();
    }
}