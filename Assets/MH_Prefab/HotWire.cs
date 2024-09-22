using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HotWire : MonoBehaviour
{
    private CapsuleCollider[] colliders;
    private AudioSource audioSource;
    private int score = 0;
    private float lastScoreTime = 0;
    private bool isTouchingWire = false;
    private bool hasStarted = false;
    private bool hasFinished = false;
    private ScoreScript scoreObject;
    public GameObject spawnPoint;
    public GameObject player;

    public Transform startPoint;  // Diese müssen im Inspector zugewiesen werden
    public Transform endPoint;    // Diese müssen im Inspector zugewiesen werden

    private bool isLoading = false; // Sicherstellen, dass die Szene nur einmal geladen wird

    void Awake()
    {
        player.transform.position = spawnPoint.transform.position;
    }

    void Start()
    {
        scoreObject = GameObject.Find("ScoreObject").GetComponent<ScoreScript>();
        colliders = GetComponentsInChildren<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource not found. Please add an AudioSource component.");
        }

        lastScoreTime = Time.time;

        if (startPoint == null || endPoint == null)
        {
            Debug.LogError("StartPoint or EndPoint is not assigned. Please assign these in the inspector.");
        }
    }

    void Update()
    {
        if (hasStarted && !hasFinished)
        {
            CheckForPoints();
        }

        // Wenn das Spiel beendet ist und die Szene noch nicht geladen wird, wechsle die Szene
        if (hasFinished && !isLoading)
        {
            isLoading = true; // Verhindere mehrfaches Laden der Szene

            // Aktualisiere den Highscore, falls der aktuelle Score höher ist
            if (score > scoreObject.DrahtHighscore)
            {
                scoreObject.DrahtHighscore = score;
            }

            // Starte den asynchronen Szenenwechsel
            StartCoroutine(LoadHubWorldSceneAsync());
        }
    }

    public void OnHandleCollisionEnter()
    {
        if (!hasStarted || hasFinished) return;

        PlaySound();
        UpdateScore(10, true);
        isTouchingWire = true;
        Debug.Log("Collision with WireHandle started.");
    }

    public void OnHandleCollisionExit()
    {
        if (!hasStarted || hasFinished) return;

        isTouchingWire = false;
        Debug.Log("Collision with WireHandle ended.");
    }

    private void PlaySound()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void UpdateScore(int points, bool isPenalty = false)
    {
        score += points;
        Debug.Log("Current Score: " + score);
        if (isPenalty)
        {
            Debug.Log("Penalty Points: " + points);
        }
    }

    private void CheckForPoints()
    {
        if (!isTouchingWire && Time.time - lastScoreTime >= 5f)
        {
            UpdateScore(20);
            lastScoreTime = Time.time;
        }
    }

    public void OnStartTriggerEnter()
    {
        hasStarted = true;
        Debug.Log("Game Started");
    }

    public void OnEndTriggerEnter()
    {
        hasFinished = true;
        Debug.Log("Game Finished. Final Score: " + score);
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
            yield return null;  // Warte bis zur nächsten Frame
        }

        // Aktiviere die neue Szene
        asyncLoad.allowSceneActivation = true;
    }
}
