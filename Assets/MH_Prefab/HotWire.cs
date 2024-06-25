using UnityEngine;

public class HotWire : MonoBehaviour
{
    private CapsuleCollider[] colliders;
    private AudioSource audioSource;
    private int score = 0;
    private float lastScoreTime = 0;
    private bool isTouchingWire = false;

    void Start()
    {
        colliders = GetComponentsInChildren<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource not found. Please add an AudioSource component.");
        }

        lastScoreTime = Time.time;
    }

    void Update()
    {
        CheckForPoints();
    }

    public void OnHandleCollisionEnter()
    {
        PlaySound();
        UpdateScore(-10, true);
        isTouchingWire = true;
        Debug.Log("Collision with WireHandle started.");
    }

    public void OnHandleCollisionExit()
    {
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
}