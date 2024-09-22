using UnityEngine;
using UnityEngine.SceneManagement;

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

    public Transform startPoint;  // Diese m�ssen im Inspector zugewiesen werden
    public Transform endPoint;    // Diese m�ssen im Inspector zugewiesen werden

    void Awake() {
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
        if(hasFinished) {
            if(score > scoreObject.DrahtHighscore) {
                scoreObject.DrahtHighscore = score;
            }
            var op =  SceneManager.LoadSceneAsync("HubWorld");
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
}