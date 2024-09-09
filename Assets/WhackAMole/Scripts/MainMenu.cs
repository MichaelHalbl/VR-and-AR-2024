using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public InputActionAsset inputActions;

    private InputAction startAction;
    private InputAction optionsAction;
    private InputAction quitAction;

    void Awake()
    {
        var menuActions = inputActions.FindActionMap("Menu");

        startAction = menuActions.FindAction("Start");
        optionsAction = menuActions.FindAction("Options");
        quitAction = menuActions.FindAction("Quit");

        startAction.performed += ctx => StartGame();
        optionsAction.performed += ctx => OpenOptions();
        quitAction.performed += ctx => QuitGame();
    }

    void OnEnable()
    {
        startAction.Enable();
        optionsAction.Enable();
        quitAction.Enable();
    }

    void OnDisable()
    {
        startAction.Disable();
        optionsAction.Disable();
        quitAction.Disable();
    }

    public void StartGame()
    {
        Debug.Log("Start clicked");
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenOptions()
    {
        Debug.Log("Options menu opened");
    }

    public void QuitGame()
    {
        Debug.Log("Game is quitting");
        // Application.Quit();
    }
}