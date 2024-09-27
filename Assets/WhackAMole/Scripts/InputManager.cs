using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class InputManager : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject player;
    private float waitforEnd;
    private float end = 20;
    private bool waitingForEnd = false;
    private bool isLoading = false; // Sicherstellen, dass die Szene nur einmal geladen wird
    private ScoreScript scoreObject;
    private ScoreManager scoreManager;

    void Awake()
    {
        player.transform.position = spawnPoint.transform.position;
    }


    void Start() {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        scoreObject = GameObject.Find("ScoreObject").GetComponent<ScoreScript>();
    }

    // Update is called once per frame
    void Update() {
        if(scoreObject.MoleOver && !waitingForEnd) {
                waitingForEnd = true;
                waitforEnd = Time.time;
        }
        if (scoreObject.MoleOver && !isLoading && Time.time > waitforEnd + end)
        {
            isLoading = true; // Verhindere mehrfaches Laden der Szene
            int score = scoreManager.getScore();

            if (score >= scoreObject.MoleLevel*150)
            {
                scoreObject.MoleLevel++;
            }

            if (score > scoreObject.MoleHighscore)
            {
                scoreObject.MoleHighscore = score;
            }

            // Starte den asynchronen Szenenwechsel
            StartCoroutine(LoadHubWorldSceneAsync());
        }
    }

   // Coroutine für das asynchrone Laden der HubWorld-Szene
    IEnumerator LoadHubWorldSceneAsync()
    {
        // Lade die Szene asynchron, aber blockiere die Aktivierung, bis sie bereit ist
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("HubWorld");
        asyncLoad.allowSceneActivation = false;

        // Warte, bis die Szene zu mindestens 90 % geladen ist
        while (asyncLoad.progress < 0.9f)
        {
            yield return null; // Warte bis zur nächsten Frame
        }

        // Aktiviere die neue Szene
        asyncLoad.allowSceneActivation = true;
    }
}
