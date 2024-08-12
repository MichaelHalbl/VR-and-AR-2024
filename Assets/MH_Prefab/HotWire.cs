using UnityEngine;

public class HotWire : MonoBehaviour
{
    private CapsuleCollider[] colliders;
    private AudioSource audioSource;
    private int score = 0;
    private float lastScoreTime = 0;
    private bool isTouchingWire = false;
    private bool hasStarted = false;
    private bool hasFinished = false;

    public Transform startPoint;  // Diese müssen im Inspector zugewiesen werden
    public Transform endPoint;    // Diese müssen im Inspector zugewiesen werden

    void Start()
    {
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
    }

    public void OnHandleCollisionEnter()
    {
        if (!hasStarted || hasFinished) return;

        PlaySound();
        UpdateScore(-10, true);
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