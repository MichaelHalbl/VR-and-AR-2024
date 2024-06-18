using UnityEngine;

public class HotWire : MonoBehaviour
{
    private CapsuleCollider[] colliders;
    private AudioSource audioSource;

    // Punktzahlverwaltung
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("WireHandle"))
        {
            PlaySound();
            UpdateScore(-10);
            isTouchingWire = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("WireHandle"))
        {
            isTouchingWire = false;
        }
    }

    void PlaySound()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void UpdateScore(int points)
    {
        score += points;
        Debug.Log("Current Score: " + score);
    }

    void CheckForPoints()
    {
        if (!isTouchingWire && Time.time - lastScoreTime >= 5f)
        {
            UpdateScore(20);
            lastScoreTime = Time.time;
        }
    }
}
